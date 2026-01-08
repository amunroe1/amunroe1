using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;
using System.Threading;
using System.Xml;
using System.Xml.Linq;
using ShippingLibrary;

namespace Group43
{
    // ====== CONTRACT ======
    [ServiceContract]
    public interface IOrderService
    {
        // Health check
        // GET /OrderService.svc/ping
        [OperationContract]
        [WebGet(UriTemplate = "ping", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        PingReply Ping();

        // Create a single order that may contain multiple items
        // POST /OrderService.svc/CreateOrder
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "CreateOrder",
                   RequestFormat = WebMessageFormat.Json,
                   ResponseFormat = WebMessageFormat.Json,
                   BodyStyle = WebMessageBodyStyle.Bare)]
        OrderConfirmation CreateOrder(OrderObj order);

        // Convenience: list & get
        // GET /OrderService.svc/orders
        [OperationContract]
        [WebGet(UriTemplate = "orders", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        OrderStatus[] ListOrders();

        // GET /OrderService.svc/orders/{id}
        [OperationContract]
        [WebGet(UriTemplate = "orders/{id}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        OrderDetail GetOrder(string id);

        // GET /OrderService.svc/ordersByEmail?email={email}
        [OperationContract]
        [WebGet(
            UriTemplate = "ordersByEmail?email={email}",
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare)]
        OrderDetail[] GetOrdersByEmail(string email);
    }

    // ====== DTOs ======
    [DataContract]
    public class PingReply
    {
        [DataMember] public string ok { get; set; }
        [DataMember] public string note { get; set; }
    }

    [DataContract]
    public class ShippingAddress
    {
        [DataMember(IsRequired = true)] public string Street { get; set; }
        [DataMember(IsRequired = true)] public string City { get; set; }
        [DataMember(IsRequired = true)] public string State { get; set; }
        [DataMember(IsRequired = true)] public string PostalCode { get; set; }
        [DataMember] public string Country { get; set; } = "US";
    }

    [DataContract]
    public class OrderItem
    {
        [DataMember] public string ItemId { get; set; }
        [DataMember] public string Title { get; set; }
        [DataMember] public decimal UnitPrice { get; set; }
        [DataMember] public int Quantity { get; set; }
        [DataMember] public decimal ShippingPrice { get; set; }  // per-order shipping; same for all items
    }

    // OrderObj sent from client when creating an order
    [DataContract]
    public class OrderObj
    {
        // array of items in the order (cart)
        [DataMember(IsRequired = true)] public List<OrderItem> Items { get; set; } = new List<OrderItem>();

        // Will be computed on server using ShippingLibrary
        [DataMember] public decimal ShippingPrice { get; set; }

        [DataMember(IsRequired = true)] public string CustomerName { get; set; }
        [DataMember(IsRequired = true)] public ShippingAddress Address { get; set; }

        [DataMember] public string Email { get; set; }
        [DataMember] public string Notes { get; set; }

        // Optional override for total (not used here, but kept for completeness)
        [DataMember] public decimal? TotalOverride { get; set; }
    }

    [DataContract]
    public class OrderConfirmation
    {
        [DataMember] public string OrderId { get; set; }
        [DataMember] public string Status { get; set; }
        [DataMember] public DateTime CreatedUtc { get; set; }
        [DataMember] public decimal Total { get; set; }
    }

    [DataContract]
    public class OrderStatus
    {
        [DataMember] public string OrderId { get; set; }
        [DataMember] public string Status { get; set; }
        [DataMember] public DateTime CreatedUtc { get; set; }
        [DataMember] public decimal Total { get; set; }
    }

    [DataContract]
    public class OrderDetail
    {
        [DataMember] public string OrderId { get; set; }
        [DataMember] public string Status { get; set; }
        [DataMember] public DateTime CreatedUtc { get; set; }
        [DataMember] public decimal Total { get; set; }

        // full list of items in this order
        [DataMember] public List<OrderItem> Items { get; set; }

        [DataMember] public CustomerInfo Customer { get; set; }
        [DataMember] public ShippingAddress Address { get; set; }

        [DataMember] public DateTime EstimatedDeliveryUtc { get; set; }
        [DataMember] public string Carrier { get; set; }
        [DataMember] public string ServiceLevel { get; set; }
    }

    [DataContract]
    public class CustomerInfo
    {
        [DataMember] public string Name { get; set; }
        [DataMember] public string Email { get; set; }
    }

    // ====== SERVICE ======
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class OrderService : IOrderService
    {
        private static readonly string OrdersPath =
            System.Web.Hosting.HostingEnvironment.MapPath("~/App_Data/orders.xml");
        private static readonly ReaderWriterLockSlim Lck = new ReaderWriterLockSlim();

        public PingReply Ping() => new PingReply { ok = "true", note = "Use POST /OrderService.svc/CreateOrder or the TryIt page." };

        public OrderConfirmation CreateOrder(OrderObj order)
        {
            if (order == null || order.Address == null || order.Items == null || order.Items.Count == 0)
                throw new WebFaultException<string>("Invalid order", System.Net.HttpStatusCode.BadRequest);

            // Normalize quantities
            foreach (var it in order.Items)
            {
                if (it.Quantity <= 0) it.Quantity = 1;
            }

            var id = "ORD-" + Guid.NewGuid().ToString("N").Substring(0, 8).ToUpperInvariant();
            var created = DateTime.UtcNow;

            // Compute subtotal from all items
            var subtotal = order.Items.Sum(it => it.UnitPrice * it.Quantity);

            // Server computes shipping using ShippingLibrary DLL
            ComputeShipping(order.Address, created,
                            out var eta, out var carrier, out var service,
                            out var shippingPrice);

            order.ShippingPrice = shippingPrice;

            // Set per-item ShippingPrice (per order) for convenience
            foreach (var it in order.Items)
            {
                it.ShippingPrice = shippingPrice;
            }

            // Server computes total 
            var total = order.TotalOverride ?? (subtotal + order.ShippingPrice);

            EnsureXmlExists();

            Lck.EnterWriteLock();
            try
            {
                var doc = XDocument.Load(OrdersPath);

                var itemsElement = new XElement("Items");
                foreach (var it in order.Items)
                {
                    itemsElement.Add(
                        new XElement("Item",
                            new XElement("ItemId", it.ItemId ?? ""),
                            new XElement("Title", it.Title ?? ""),
                            new XElement("UnitPrice", it.UnitPrice),
                            new XElement("Quantity", it.Quantity),
                            new XElement("ShippingPrice", it.ShippingPrice)
                        ));
                }

                doc.Root.Add(
                    new XElement("Order",
                        new XAttribute("id", id),
                        new XElement("CreatedUtc", created.ToString("o")),
                        new XElement("Status", "Created"),
                        itemsElement,
                        new XElement("Customer",
                            new XElement("Name", order.CustomerName ?? ""),
                            new XElement("Email", order.Email ?? "")
                        ),
                        new XElement("Address",
                            new XElement("Street", order.Address.Street ?? ""),
                            new XElement("City", order.Address.City ?? ""),
                            new XElement("State", order.Address.State ?? ""),
                            new XElement("PostalCode", order.Address.PostalCode ?? ""),
                            new XElement("Country", order.Address.Country ?? "US")
                        ),
                        new XElement("Shipping",
                            new XElement("EstimatedDeliveryUtc", eta.ToString("o")),
                            new XElement("Carrier", carrier),
                            new XElement("ServiceLevel", service)
                        ),
                        new XElement("Total", total),
                        new XElement("Notes", order.Notes ?? "")
                    )
                );
                doc.Save(OrdersPath);
            }
            finally { Lck.ExitWriteLock(); }

            return new OrderConfirmation
            {
                OrderId = id,
                Status = "Created",
                CreatedUtc = created,
                Total = total
            };
        }

        public OrderStatus[] ListOrders()
        {
            EnsureXmlExists();
            Lck.EnterReadLock();
            try
            {
                var doc = XDocument.Load(OrdersPath);
                var list = new List<OrderStatus>();
                foreach (var el in doc.Root.Elements("Order"))
                {
                    list.Add(new OrderStatus
                    {
                        OrderId = (string)el.Attribute("id"),
                        Status = (string)el.Element("Status") ?? "Unknown",
                        CreatedUtc = DateTime.Parse((string)el.Element("CreatedUtc")),
                        Total = decimal.Parse((string)el.Element("Total"))
                    });
                }
                return list.ToArray();
            }
            finally { Lck.ExitReadLock(); }
        }

        public OrderDetail GetOrder(string id)
        {
            EnsureXmlExists();

            XElement orderEl = null;
            Lck.EnterReadLock();
            try
            {
                var doc = XDocument.Load(OrdersPath);
                foreach (var el in doc.Root.Elements("Order"))
                {
                    if ((string)el.Attribute("id") == id)
                    {
                        orderEl = new XElement(el); // copy
                        break;
                    }
                }
            }
            finally { Lck.ExitReadLock(); }

            if (orderEl == null)
                throw new WebFaultException(System.Net.HttpStatusCode.NotFound);

            return BuildOrderDetailFromElement(orderEl);
        }

        public OrderDetail[] GetOrdersByEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new WebFaultException<string>("Email is required", System.Net.HttpStatusCode.BadRequest);

            EnsureXmlExists();

            var results = new List<OrderDetail>();

            Lck.EnterReadLock();
            try
            {
                var doc = XDocument.Load(OrdersPath);

                foreach (var orderEl in doc.Root.Elements("Order"))
                {
                    var cust = orderEl.Element("Customer");
                    var emailVal = (string)cust?.Element("Email") ?? string.Empty;

                    if (!email.Equals(emailVal, StringComparison.OrdinalIgnoreCase))
                        continue;

                    results.Add(BuildOrderDetailFromElement(orderEl));
                }
            }
            finally
            {
                Lck.ExitReadLock();
            }

            return results.ToArray();
        }

        // ===== Helpers =====
        private static void EnsureXmlExists()
        {
            var dir = Path.GetDirectoryName(OrdersPath);
            if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);
            if (!File.Exists(OrdersPath))
            {
                var doc = new XDocument(new XElement("Orders"));
                using (var w = XmlWriter.Create(OrdersPath, new XmlWriterSettings { Indent = true, Encoding = Encoding.UTF8 }))
                    doc.Save(w);
            }
        }

        private static OrderDetail BuildOrderDetailFromElement(XElement orderEl)
        {
            var itemsEl = orderEl.Element("Items");
            var custEl = orderEl.Element("Customer");
            var addrEl = orderEl.Element("Address");
            var shipEl = orderEl.Element("Shipping");

            var items = new List<OrderItem>();
            if (itemsEl != null)
            {
                foreach (var itEl in itemsEl.Elements("Item"))
                {
                    items.Add(new OrderItem
                    {
                        ItemId = (string)itEl.Element("ItemId"),
                        Title = (string)itEl.Element("Title"),
                        UnitPrice = decimal.Parse((string)itEl.Element("UnitPrice")),
                        Quantity = int.Parse((string)itEl.Element("Quantity")),
                        ShippingPrice = decimal.Parse((string)itEl.Element("ShippingPrice"))
                    });
                }
            }

            var detail = new OrderDetail
            {
                OrderId = (string)orderEl.Attribute("id"),
                Status = (string)orderEl.Element("Status") ?? "Unknown",
                CreatedUtc = DateTime.Parse((string)orderEl.Element("CreatedUtc")),
                Total = decimal.Parse((string)orderEl.Element("Total")),
                Items = items,
                Customer = new CustomerInfo
                {
                    Name = (string)custEl.Element("Name"),
                    Email = (string)custEl.Element("Email")
                },
                Address = new ShippingAddress
                {
                    Street = (string)addrEl.Element("Street"),
                    City = (string)addrEl.Element("City"),
                    State = (string)addrEl.Element("State"),
                    PostalCode = (string)addrEl.Element("PostalCode"),
                    Country = (string)addrEl.Element("Country")
                }
            };

            if (shipEl != null &&
                DateTime.TryParse((string)shipEl.Element("EstimatedDeliveryUtc"), out var persistedEta))
            {
                detail.EstimatedDeliveryUtc = persistedEta;
                detail.Carrier = (string)shipEl.Element("Carrier");
                detail.ServiceLevel = (string)shipEl.Element("ServiceLevel");
            }
            else
            {
                // recompute shipping if missing in XML
                ComputeShipping(detail.Address, detail.CreatedUtc,
                                out var eta, out var carrier, out var service,
                                out var shippingPrice);

                detail.EstimatedDeliveryUtc = eta;
                detail.Carrier = carrier;
                detail.ServiceLevel = service;

                // also propagate recalculated shipping
                foreach (var it in detail.Items)
                    it.ShippingPrice = shippingPrice;

                var subtotal = detail.Items.Sum(i => i.UnitPrice * i.Quantity);
                detail.Total = subtotal + shippingPrice;
            }

            return detail;
        }

        // Uses ShippingLibrary.ShippingEstimator to get a rate, then assign ETA + carrier
        private static void ComputeShipping(ShippingAddress address,
                                            DateTime createdUtc,
                                            out DateTime etaUtc,
                                            out string carrier,
                                            out string service,
                                            out decimal shippingPrice)
        {
            var state = address?.State?.Trim() ?? string.Empty;

            // call into the DLL 
            double rate = Shipping.ShippingEstimator(state);
            shippingPrice = (decimal)rate;


            if (rate <= 5.0)          // West (4.99)
            {
                carrier = "UPS";
                service = "Ground";
                etaUtc = createdUtc.AddDays(3);
            }
            else if (rate <= 7.0)     // SouthWest (6.99)
            {
                carrier = "FedEx";
                service = "2-Day";
                etaUtc = createdUtc.AddDays(2);
            }
            else if (rate <= 9.0)     // Midwest (8.99)
            {
                carrier = "USPS";
                service = "Priority";
                etaUtc = createdUtc.AddDays(4);
            }
            else if (rate <= 11.0)    // SouthEast (9.99)
            {
                carrier = "UPS";
                service = "Ground";
                etaUtc = createdUtc.AddDays(5);
            }
            else                      // NorthEast or default (10.99 / 12.99)
            {
                carrier = "USPS";
                service = "Priority";
                etaUtc = createdUtc.AddDays(2);
            }
        }
    }
}