﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ShoppingOrdersPage.aspx.cs" Inherits="Group43.ShoppingOrdersPage" %>
<!DOCTYPE html>
<html>
<head runat="server">
    <meta charset="utf-8" />
    <title>Your Orders</title>
    <style>
        body { font-family: Segoe UI, Arial, sans-serif; margin: 24px; }
        h1 { margin-bottom: 8px; }
        table { width:100%; border-collapse: collapse; margin-top:16px; }
        th, td { padding:10px; border-bottom:1px solid #e5e5e5; text-align:left; }
        th.num, td.num { text-align:right; }
        .muted { color:#666; font-size:12px; }
        button { padding:10px 16px; margin-bottom:16px; cursor:pointer; }
        .spinner { opacity: .7; font-style: italic; }
    </style>
</head>
<body>
<form id="form1" runat="server">
    <h1>Your Orders</h1>

    <p class="muted">
        <%
            var user = Session["userName"] as string;
            var clearance = Session["clearance"] as string;
            bool isAdminUser = (clearance == "admin");
            if (user != null)
            {
        %>
            You are logged in as <strong><%= Server.HtmlEncode(user) %></strong>
            <% if (isAdminUser) { %>
                (Admin view – showing all orders).
            <% } else { %>.
            <% } %>
        <%
            }
            else
            {
        %>
            Checking out as guest. (No user-specific orders to show.)
        <%
            }
        %>
    </p>

    <asp:Button ID="Home" runat="server" Text="Home"
                Style="margin-bottom:10px" OnClick="Home_Click" /><br />

    <button type="button" onclick="window.location.href='ShoppingCart.aspx'">← Back to Cart</button>

    <div id="ordersTable" class="spinner">Loading orders…</div>
</form>

<script>
    // Email from ASP.NET Session (set at login)
    var currentUserEmail = '<%= (Session["userName"] ?? "").ToString() %>';
    // Admin flag from Session["clearance"]
    var isAdmin = <%= (Session["clearance"] != null && Session["clearance"].ToString() == "admin") ? "true" : "false" %>;
    const base = "OrderService.svc/";
    //const base = "http://webstrar43.fulton.asu.edu/page3/OrderService.svc/";
   // const base = (
     //   location.hostname === "localhost" ||
       // location.hostname === "127.0.0.1"
    //)
      //  ? (location.protocol + "//" + location.host + "/OrderService.svc/")
        //: "http://webstrar43.fulton.asu.edu/page3/OrderService.svc/";

    // Parse WCF Date(/ticks/)
    function toIso(dotNetDate) {
        if (typeof dotNetDate === 'string') {
            const m = /\/Date\((\-?\d+)(?:[+\-]\d{4})?\)\//.exec(dotNetDate);
            if (m) {
                const ms = parseInt(m[1], 10);
                return new Date(ms).toISOString();
            }
        }
        const d = new Date(dotNetDate);
        return isNaN(d.getTime()) ? String(dotNetDate) : d.toISOString();
    }

    function escapeHtml(s) {
        return (s || '').replace(/[&<>"']/g, c => ({
            '&': '&amp;', '<': '&lt;', '>': '&gt;', '"': '&quot;', "'": '&#39;'
        }[c]));
    }

    window.onload = async function () {
        const host = document.getElementById('ordersTable');

        // ===== ADMIN: show ALL orders via /orders =====
        if (isAdmin) {
            try {
                const res = await fetch(base + "orders");
                const list = await res.json();   // OrderStatus[]

                if (!Array.isArray(list) || !list.length) {
                    host.classList.remove('spinner');
                    host.textContent = 'No orders found.';
                    return;
                }

                // Fetch full details for each order
                const detailPromises = list.map(o =>
                    fetch(base + "orders/" + encodeURIComponent(o.OrderId))
                        .then(r => r.json())
                        .catch(() => null)
                );
                const details = await Promise.all(detailPromises);

                let html = '<table><thead><tr>' +
                    '<th>Order Id</th><th>Status</th><th>Created (UTC)</th>' +
                    '<th>Items</th><th class="num">Total Qty</th><th>Customer</th><th class="num">Total</th>' +
                    '</tr></thead><tbody>';

                list.forEach((o, i) => {
                    const d = details[i] || {};
                    const created = toIso(d.CreatedUtc || o.CreatedUtc);
                    const items = Array.isArray(d.Items) ? d.Items : [];

                    let itemTitle = '(no items)';
                    if (items.length === 1) {
                        itemTitle = items[0].Title || '(no title)';
                    } else if (items.length > 1) {
                        itemTitle = (items[0].Title || '(no title)') + ` (+${items.length - 1} more)`;
                    }

                    const totalQty = items.reduce((sum, it) => sum + (it.Quantity || 0), 0);
                    const customer = d.Customer && d.Customer.Email
                        ? d.Customer.Email
                        : (d.Customer && d.Customer.Name ? d.Customer.Name : '(unknown)');
                    const total = (typeof d.Total === 'number')
                        ? d.Total
                        : (typeof o.Total === 'number' ? o.Total : 0);

                    html += `<tr>
                        <td><a href="${base}orders/${encodeURIComponent(o.OrderId)}" target="_blank">${escapeHtml(o.OrderId)}</a></td>
                        <td>${escapeHtml(d.Status || o.Status || 'Unknown')}</td>
                        <td>${escapeHtml(created)}</td>
                        <td>${escapeHtml(itemTitle)}</td>
                        <td class="num">${totalQty || ''}</td>
                        <td>${escapeHtml(customer)}</td>
                        <td class="num">${Number(total || 0).toFixed(2)}</td>
                    </tr>`;
                });

                html += '</tbody></table>';
                host.classList.remove('spinner');
                host.innerHTML = html;

            } catch (e) {
                host.classList.remove('spinner');
                host.textContent = 'Failed to load orders: ' + e;
            }

            return; // done for admin
        }

        // ===== NORMAL USER: show orders by email via /ordersByEmail =====
        const email = (currentUserEmail || '').trim();

        if (!email) {
            host.classList.remove('spinner');
            host.textContent = 'No orders to display. Please log in to view your orders.';
            return;
        }

        try {
            const res = await fetch(base + "ordersByEmail?email=" + encodeURIComponent(email));
            const list = await res.json();   // OrderDetail[]

            if (!Array.isArray(list) || !list.length) {
                host.classList.remove('spinner');
                host.textContent = 'No orders found for ' + email + '.';
                return;
            }

            let html = '<table><thead><tr>' +
                '<th>Order Id</th><th>Status</th><th>Created (UTC)</th>' +
                '<th>Items</th><th class="num">Total Qty</th><th class="num">Total</th>' +
                '</tr></thead><tbody>';

            list.forEach(o => {
                const created = toIso(o.CreatedUtc);
                const items = Array.isArray(o.Items) ? o.Items : [];

                let itemTitle = '(no items)';
                if (items.length === 1) {
                    itemTitle = items[0].Title || '(no title)';
                } else if (items.length > 1) {
                    itemTitle = (items[0].Title || '(no title)') + ` (+${items.length - 1} more)`;
                }

                const totalQty = items.reduce((sum, it) => sum + (it.Quantity || 0), 0);
                const total = (typeof o.Total === 'number') ? o.Total : 0;

                html += `<tr>
                    <td>${escapeHtml(o.OrderId)}</td>
                    <td>${escapeHtml(o.Status || 'Unknown')}</td>
                    <td>${escapeHtml(created)}</td>
                    <td>${escapeHtml(itemTitle)}</td>
                    <td class="num">${totalQty || ''}</td>
                    <td class="num">${Number(total || 0).toFixed(2)}</td>
                </tr>`;
            });

            html += '</tbody></table>';
            host.classList.remove('spinner');
            host.innerHTML = html;

        } catch (e) {
            host.classList.remove('spinner');
            host.textContent = 'Failed to load orders: ' + e;
        }
    };
</script>
</body>
</html>