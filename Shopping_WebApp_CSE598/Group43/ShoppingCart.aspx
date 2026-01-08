﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ShoppingCart.aspx.cs" Inherits="Group43.ShoppingCart" %>
<!DOCTYPE html>
<html>
<head runat="server">
    <meta charset="utf-8" />
    <title>Shopping Cart</title>
    <style>
        :root { --border:#e5e5e5; --muted:#666; }
        body { font-family: Segoe UI, Arial, sans-serif; margin: 24px; }
        .grid { display: grid; grid-template-columns: 1fr 1fr; gap: 12px; max-width: 1024px; }
        label { font-weight: 600; display:block; margin-top:8px; }
        input, textarea { width: 100%; padding: 8px; box-sizing: border-box; }
        .row { display:flex; gap:12px; }
        button, #button { padding: 10px 16px; cursor:pointer; }
        .card { border:1px solid var(--border); padding:16px; border-radius:8px; margin-top:16px; }
        table { width:100%; border-collapse: collapse; }
        th, td { padding:10px; border-bottom:1px solid var(--border); text-align:left; vertical-align: top; }
        th.num, td.num { text-align:right; }
        .muted{ color:var(--muted); font-size:12px; }
        .danger { color:#b00020; }
        .right { text-align:right; }
        .icon-btn { background:none; border:none; cursor:pointer; font-size:18px; line-height:1; }
        .total-line { display:flex; justify-content:flex-end; gap:24px; margin-top:8px; }
    </style>
</head>
<body>
<form id="form1" runat="server">

    <h1>Shopping Cart
    </h1>
<p style="font-weight:600;">
    You are logged in as 
    <%= Session["userName"] != null ? Session["userName"].ToString() : "Guest" %>
</p>
    <asp:button class="button" runat="server" OnClick="Home_Click" style="width:fit-content" Text="Home"/>
    <p class="muted">
        Cart is read from cookie <code>ShoppingCart</code> with format
        <code>id,title,price|id,title,price|...</code>. Use the <b>🗑</b> button to remove items,
        adjust Qty per row, then fill shipping info and click <b>Place Orders</b>.
    </p>

    <!-- Optional helper to seed a demo cart locally -->
    <button type="button" onclick="loadDemoCart()">Load demo cart</button>
    

    <!-- CART LIST -->
    <div class="card">
        <h3>Cart</h3>
        <div id="cartList">No items found in cart.</div>
        <div id="cartTotals" class="right muted"></div>
    </div>

    <!-- SHIPPING / CONTACT ONLY -->
    <div class="card">
        <h3>Shipping &amp; Contact</h3>
        <div class="grid">
            <div>
                <label>Name</label>
                <input id="CustomerName" />
                <label>Email</label>
                <input id="Email" value='<%= Session["userName"] != null ? Session["userName"].ToString() : "" %>' />
            </div>
            <div>
                <label>Street</label>
                <input id="Street" />
                <div class="row">
                    <div style="flex:1"><label>City</label><input id="City" /></div>
                    <div style="flex:1"><label>State</label><input id="State" /></div>
                    <div style="flex:1"><label>Postal</label><input id="PostalCode" /></div>
                </div>
                <label>Country</label>
                <input id="Country" value="US" />
            </div>
        </div>
        <label>Notes</label>
        <textarea id="Notes" rows="2" placeholder="Optional notes"></textarea>
    </div>

    <div style="margin-top:16px;">
        <button type="button" onclick="placeAllOrders()">Place Orders</button>
    </div>

</form>

<script>
    // ===== Cookie utilities =====
    function getCookie(name) {
        const parts = document.cookie.split(';');
        for (let i = 0; i < parts.length; i++) {
            const p = parts[i];
            const eq = p.indexOf('=');
            if (eq < 0) continue;
            const k = p.substring(0, eq).trim();
            if (k === name) return decodeURIComponent(p.substring(eq + 1).trim());
        }
        return null;
    }
    function setCookie(name, value, maxAgeSeconds) {
        const isHttps = location.protocol === 'https:';
        let attrs = '; path=/; max-age=' + (maxAgeSeconds || 3600);
        if (isHttps) attrs += '; SameSite=None; Secure';
        else attrs += '; SameSite=Lax';
        document.cookie = name + '=' + encodeURIComponent(value) + attrs;
    }
    function clearCookie(name) {
        const isHttps = location.protocol === 'https:';
        let attrs = '; path=/; Max-Age=0';
        if (isHttps) attrs += '; SameSite=None; Secure';
        else attrs += '; SameSite=Lax';
        document.cookie = name + '=;' + attrs;
    }

    // ===== Parse / Serialize cart (id,title,price|...) =====
    function parseCart(str) {
        const items = [];
        if (!str) return items;
        const rows = str.split('|');
        for (const raw of rows) {
            const s = (raw || '').trim();
            if (!s) continue;
            const first = s.indexOf(',');
            const last = s.lastIndexOf(',');
            if (first <= 0 || last <= first) continue; // malformed
            const id = s.substring(0, first).trim();
            const title = s.substring(first + 1, last).trim();
            const price = parseFloat(s.substring(last + 1).trim());
            if (!id || isNaN(price)) continue;
            items.push({ id, title, price, qty: 1 });
        }
        return items;
    }
    function serializeCart(items) {
        return items.map(it => `${it.id},${it.title},${Number(it.price).toFixed(2)}`).join('|');
    }
    function updateCookieFromCart() {
        if (!CART.length) { clearCookie('ShoppingCart'); return; }
        setCookie('ShoppingCart', serializeCart(CART), 3600);
    }

    // ===== Client-side shipping estimator (matches ShippingLibrary) =====
    function estimateShippingFromState(state) {
        if (!state) return 0;
        state = state.trim().toUpperCase();

        const west = ["CO", "WY", "MT", "ID", "UT", "NV", "OR", "WA", "CA", "HI", "AK"];
        const southWest = ["OK", "TX", "NM", "AZ"];
        const midWest = ["ND", "SD", "NE", "KS", "MN", "IA", "MO", "WI", "IL", "IN", "OH", "MI"];
        const southEast = ["DC", "WV", "VA", "NC", "SC", "GA", "FL", "AL", "TN", "KY", "AR", "LA"];
        const northEast = ["ME", "VT", "NH", "MA", "CT", "RI", "NY", "NJ", "PA", "DE", "MD"];

        if (west.includes(state)) return 4.99;
        if (southWest.includes(state)) return 6.99;
        if (midWest.includes(state)) return 8.99;
        if (southEast.includes(state)) return 9.99;
        if (northEast.includes(state)) return 10.99;

        // unknown state -> default rate
        return 12.99;
    }

    // ===== Cart state & UI =====
    let CART = [];

    function renderCart() {
        const host = document.getElementById('cartList');
        const totals = document.getElementById('cartTotals');

        if (!CART.length) {
            host.textContent = 'No items found in cart.';
            totals.textContent = '';
            return;
        }

        let html = '<table><thead><tr>' +
            '<th style="width:40px;"></th><th>ID</th><th>Description</th>' +
            '<th class="num">Unit Price</th><th class="num">Qty</th><th class="num">Line Total</th>' +
            '</tr></thead><tbody>';

        let subtotal = 0;
        CART.forEach((it, i) => {
            const line = it.price * (it.qty || 1);
            subtotal += line;
            html += `<tr>
      <td><button type="button" class="icon-btn danger" title="Remove" onclick="removeItem(${i})">🗑</button></td>
      <td>${escapeHtml(it.id)}</td>
      <td>${escapeHtml(it.title)}</td>
      <td class="num">${it.price.toFixed(2)}</td>
      <td class="num"><input type="number" min="1" value="${it.qty}" style="width:64px" onchange="qtyChanged(${i}, this.value)"></td>
      <td class="num">${line.toFixed(2)}</td>
    </tr>`;
        });
        html += '</tbody></table>';
        host.innerHTML = html;

        // Live shipping estimate based on State field
        const stateInput = document.getElementById("State");
        const stateVal = stateInput ? stateInput.value : "";
        const shipRate = estimateShippingFromState(stateVal);

        let totalsHtml = `
            <div class="total-line">
                <div><b>Subtotal:</b></div>
                <div><b>$ ${subtotal.toFixed(2)}</b></div>
            </div>`;

        if (shipRate > 0) {
            totalsHtml += `
            <div class="total-line">
                <div>Estimated Shipping:</div>
                <div>$ ${shipRate.toFixed(2)}</div>
            </div>
            <div class="right muted">
                (Shipping is estimated here based on state)
            </div>`;
        } else {
            totalsHtml += `
            <div class="right muted">
                (Shipping is calculated per order by the service and is shown as 0.00 until a valid state is entered.)
            </div>`;
        }

        totals.innerHTML = totalsHtml;
    }

    function qtyChanged(i, v) {
        const n = Math.max(1, parseInt(v || '1', 10));
        CART[i].qty = n;
        renderCart(); // re-calc totals
    }
    function removeItem(i) {
        CART.splice(i, 1);
        updateCookieFromCart();
        renderCart();
    }

    function escapeHtml(s) { return (s || '').replace(/[&<>"']/g, c => ({ '&': '&amp;', '<': '&lt;', '>': '&gt;', '"': '&quot;', "'": '&#39;' }[c])); }

    // ===== Demo loader  =====
    function loadDemoCart() {
        const payload = 'v1|357883992583|0,Pokémon Emerald Version For GBA Tested,22.95|id3,Pokémon Ruby Version (Game Boy Advance),59.99';
        clearCookie('ShoppingCart');
        setCookie('ShoppingCart', payload, 3600);
        CART = parseCart(payload);
        renderCart();
    }

    // ===== Bootstrap =====
    (function init() {
        const raw = getCookie('ShoppingCart'); // cookie name from teammate
        CART = parseCart(raw);
        renderCart();

        // Recalculate shipping estimate whenever State changes
        const stateInput = document.getElementById("State");
        if (stateInput) {
            stateInput.addEventListener("input", function () {
                renderCart();
            });
        }
    })();

    // ===== Place a single order for the whole cart (then redirect) =====
    const base = "OrderService.svc/";
   // const base = "http://webstrar43.fulton.asu.edu/page3/OrderService.svc/";
   // const base = (
     //   location.hostname === "localhost" ||
       // location.hostname === "127.0.0.1"
    //)
       // ? (location.protocol + "//" + location.host + "/OrderService.svc/")
        //: "http://webstrar43.fulton.asu.edu/page3/OrderService.svc/";
    async function placeAllOrders() {
        if (!CART.length) {
            alert('Your cart is empty.');
            return;
        }
        // simple validation for shipping/contact
        const ship = {
            CustomerName: getVal("CustomerName"),
            Email: getVal("Email"),
            Address: {
                Street: getVal("Street"),
                City: getVal("City"),
                State: getVal("State"),
                PostalCode: getVal("PostalCode"),
                Country: getVal("Country") || "US"
            },
            Notes: getVal("Notes") || null
        };
        if (!ship.CustomerName || !ship.Address.Street || !ship.Address.City || !ship.Address.State || !ship.Address.PostalCode) {
            alert('Please fill in Name and full Shipping Address.');
            return;
        }

        // build array of items for the order
        const items = CART.map(it => ({
            ItemId: it.id,
            Title: it.title,
            UnitPrice: Number(it.price),
            Quantity: Number(it.qty || 1),
            ShippingPrice: 0.00
        }));

        const body = {
            Items: items,
            ShippingPrice: 0.00,   // server computes using ShippingLibrary
            CustomerName: ship.CustomerName,
            Email: ship.Email,
            Address: ship.Address,
            Notes: ship.Notes
        };

        try {
            await fetch(base + "CreateOrder", {
                method: "POST",
                headers: { "Content-Type": "application/json" },
                body: JSON.stringify(body)
            });
        } catch (e) {
            console.warn('Failed to place cart order', e);
            alert('Failed to place order. Please try again.');
            return;
        }

        // redirect to orders page
        clearCookie('ShoppingCart');
        CART = [];
        window.location.href = "ShoppingOrdersPage.aspx";
    }

    function getVal(id) { return document.getElementById(id).value.trim(); }
</script>
</body>
</html>