﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Group43.Default" %>

<!DOCTYPE html>

<!-- 
    ########################################
            CSE 598: Assignment 5 & 6
              Group: 43
        Last Update: 11/21/25
    ########################################
 -->

<style>
    .centered-label 
    {
        display:block;
        text-align:center;
        width:100%;
    }

    .button-grid
    {
        display:grid;
        grid-template-columns:auto auto auto auto;
        grid-template-rows:auto;
        gap:25px;
        justify-content:center;
        align-content:center;
    }

    .centered-table
    {
        margin-left: auto;
        margin-right: auto;
        width: 80%;
        border: 2px solid black;
        border-right: 2px solid black;
        padding: 2px;
        text-align: left;
        border-style: solid;
        vertical-align:top;
        border-collapse: collapse; /* optional*/
    }

    .centered-table th
    {
        border: 2px solid black;
        border-right: 2px solid black;
        background-color: lightblue;
        padding: 5px;
        text-align: Left;
    }
    .centered-table td
    {
        border: 1.5px solid black;
        border-right: 1.5px solid black;
        background-color: white;
        padding: 5px;
        text-align: Left;
    }

    .menu-container 
    {
        width: 80%; 
        margin: 0 auto; 
        text-align: center;
    }

</style>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Group 43 Web Application</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Label ID="Label1" runat="server" Text="Welcome to the Group 43 Shopping Application!" Font-Bold="true" Font-Size="18pt" CssClass="centered-label"></asp:Label>
            <br/><br/>
            <asp:Label ID="Label2" runat="server" Text="Browse and shop real items from a real online marketplace. </br>
                Find the best deals. Save even more by becoming a member." Font-Bold="true" Font-Size="14pt" CssClass="centered-label"></asp:Label>
        </div>
        <br /> <br />
        <div>
            <asp:Panel ID="Panel1" runat="server" CssClass="button-grid">
                <asp:Button ID="Button1" runat="server" Text="Browse" Width="75px" OnClick="Button1_Click" />
                <asp:Button ID="Button2" runat="server" Text="Register" Width="75px" OnClick="Button2_Click"/>
                <asp:Button ID="Button3" runat="server" Text="Login" Width="75px" OnClick="Button3_Click"/>
                <asp:Button ID="Button4" runat="server" Text="Admin" Width="75px" OnClick="Button4_Click" />
            </asp:Panel>
        </div>
 
        <div>
            <asp:Label ID="Label3" runat="server" Text="______________________________________________________________" 
                Font-Bold="true" Font-Size="20pt" CssClass="centered-label"></asp:Label>
            <br />
            <asp:Label ID="Label4" runat="server" Text="About this application:" 
                Font-Bold="true" Font-Size="14pt" CssClass="centered-label"></asp:Label>
        </div>
        <br />
        <div>
            <%-- COMPONENT TABLE --%>
            <asp:Table ID="ComponentTable" runat="server" BorderStyle="Solid" CssClass="centered-table">

                <%-- HEADERS --%>
                <asp:TableHeaderRow ID="header1" runat="server" CssClass="centered-table th">
                    <asp:TableHeaderCell ColumnSpan="5">Percentage of Overall Contribution: Andrew Munroe-33%  James Cajas-33%  Mark Adan-33%</asp:TableHeaderCell>
                </asp:TableHeaderRow>
                <asp:TableHeaderRow ID="header2" runat="server" >
                    <asp:TableHeaderCell>Provider Name</asp:TableHeaderCell>
                    <asp:TableHeaderCell>Page and Component Type</asp:TableHeaderCell>
                    <asp:TableHeaderCell>Component Description</asp:TableHeaderCell>
                    <asp:TableHeaderCell>Resource/Methods</asp:TableHeaderCell>
                    <asp:TableHeaderCell>TryIt Link</asp:TableHeaderCell>
                </asp:TableHeaderRow>

                <%-- ROW 1: Andrew --%>
                <asp:TableRow CssClass="centered-table">
                    <asp:TableCell ID="row1Provider" Text="Andrew Munroe" Width="10%"></asp:TableCell>
                    <asp:TableCell ID="row1Page" Text="DLL:<br/>Encyption.dll"></asp:TableCell>
                    <asp:TableCell ID="row1Desc" Text="Encryption function<br/>Intput:string<br/>Output:string" Width="30%"></asp:TableCell>
                    <asp:TableCell ID="row1Resource" Text="Caesar cipher implemented locally in C#"></asp:TableCell>
                    <asp:TableCell ID="row1TryIt">
                        <asp:HyperLink ID="HyperLink1" runat="server" Text="AndrewTryIt" NavigateUrl="~/AndrewTryIt.aspx" Width="10%"></asp:HyperLink>
                    </asp:TableCell>
                </asp:TableRow>

                <%-- ROW 2: Andrew --%>
                <asp:TableRow CssClass="centered-table">
                    <asp:TableCell ID="row2Provider" Text="Andrew Munroe" Width="10%"></asp:TableCell>
                    <asp:TableCell ID="row2Page" Text="SOAP Service:</br>WordCount.svc"></asp:TableCell>
                    <asp:TableCell ID="row2Desc" Text="Filter text<br/>Intput:string<br/>Output:string<br/><br/>Return dictionary of word frequencies<br/>Input:string<br/>Output:JSON string" Width="30%"></asp:TableCell>
                    <asp:TableCell ID="row2Resource" Text="implemented locally in C#"></asp:TableCell>
                    <asp:TableCell ID="row2TryIt">
                        <asp:HyperLink ID="HyperLink2" runat="server" Text="AndrewTryIt" NavigateUrl="~/AndrewTryIt.aspx" Width="10%"></asp:HyperLink>
                    </asp:TableCell>
                </asp:TableRow>

                <%-- ROW 3: Mark --%>
                <asp:TableRow CssClass="centered-table">
                    <asp:TableCell ID="row3Provider" Text="Mark Adan" Width="10%"></asp:TableCell>
                    <asp:TableCell ID="row3Page" Text=Default.aspx></asp:TableCell>
                    <asp:TableCell ID="row3Desc" Text="Website home page linking to all other pages" Width="30%"></asp:TableCell>
                    <asp:TableCell ID="row3Resource" Text=".aspx design C# code behind"></asp:TableCell>
                    <asp:TableCell ID="row3TryIt">
                        <asp:HyperLink ID="HyperLink3" runat="server" Text="Default.aspx" NavigateUrl="~/Default.aspx" Width="10%"></asp:HyperLink>
                    </asp:TableCell>
                </asp:TableRow>

                <%-- ROW 4: Andrew --%>
                <asp:TableRow CssClass="centered-table">
                    <asp:TableCell ID="row4Provider" Text="Andrew Munroe" Width="10%"></asp:TableCell>
                    <asp:TableCell ID="row4Page" Text="Global.asax"></asp:TableCell>
                    <asp:TableCell ID="row4Desc" Text="Session start and Application_Error event handlers" Width="30%"></asp:TableCell>
                    <asp:TableCell ID="row4Resource" Text="C# script in global.asax"></asp:TableCell>
                    <asp:TableCell ID="row4TryIt">
                        <asp:HyperLink ID="HyperLink4" runat="server" Text="AndrewTryIt" NavigateUrl="~/AndrewTryIt.aspx" Width="10%"></asp:HyperLink>
                    </asp:TableCell>
                </asp:TableRow>

                <%-- ROW 5: Mark --%>
                <asp:TableRow CssClass="centered-table">
                    <asp:TableCell ID="row5Provider" Text="Mark Adan" Width="10%"></asp:TableCell>
                    <asp:TableCell ID="row5Page" Text="User control:<br/>Captcha.ascx<br/>Captcha.ascx.cs<br/><br/>ImageHandler.ashx"></asp:TableCell>
                    <asp:TableCell ID="row5Desc" Text="User control design and code behind<br/><br/>HTTP handler to generate image output" Width="30%"></asp:TableCell>
                    <asp:TableCell ID="row5Resource" Text="Use C# Bitmap and MemoryStream library calls<br/>Use C# IHttpHandler class"></asp:TableCell>
                    <asp:TableCell ID="row5TryIt">
                        <asp:HyperLink ID="HyperLink5" runat="server" Text="CAPTCHA" NavigateUrl="~/Registration.aspx" Width="10%"></asp:HyperLink>
                    </asp:TableCell>
                </asp:TableRow>

                <%-- ROW 6: James --%>
                <asp:TableRow CssClass="centered-table">
                    <asp:TableCell ID="row6Provider" Text="James Cajas" Width="10%"></asp:TableCell>
                    <asp:TableCell ID="row6Page" Text="RESTful Service:<br/>OrderService.svc"></asp:TableCell>
                    <asp:TableCell ID="row6Desc" Text="Shopping cart and order creation" Width="30%"></asp:TableCell>
                    <asp:TableCell ID="row6Resource" Text="WCF RESTful service w/ JSON requests.<br/> Client uses JavaScript fetch() to call CreateOrder.<br/>
                                                            Uses XDocument for XML data storage."></asp:TableCell>
                    <asp:TableCell ID="row6TryIt">
                        <asp:HyperLink ID="HyperLink6" runat="server" Text="ShoppingCart" NavigateUrl="~/ShoppingCart.aspx" Width="10%"></asp:HyperLink>
                    </asp:TableCell>
                </asp:TableRow>

                <%-- ROW 7: Mark --%>
                <asp:TableRow CssClass="centered-table">
                    <asp:TableCell ID="row7Provider" Text="Mark Adan" Width="10%"></asp:TableCell>
                    <asp:TableCell ID="row7Page" Text="SOAP Service:<br/>MyEbayService1.svc<br/>Note: This was my Assignment 3 submission."></asp:TableCell>
                    <asp:TableCell ID="row7Desc" Text="Wrapper for eBay RESTful API search operations" Width="30%"></asp:TableCell>
                    <asp:TableCell ID="row7Resource" Text="Use C# Http and JSON library calls"></asp:TableCell>
                    <asp:TableCell ID="row7TryIt">
                        <asp:HyperLink ID="HyperLink7" runat="server" Text="Browse" NavigateUrl="~/Shopping.aspx" Width="10%"></asp:HyperLink>
                    </asp:TableCell>
                </asp:TableRow>

                <%-- ROW 8: Mark --%>
                <asp:TableRow CssClass="centered-table">
                    <asp:TableCell ID="row8Provider" Text="Mark Adan" Width="10%"></asp:TableCell>
                    <asp:TableCell ID="row8Page" Text="Cookies:<br/>[ShoppingCart]"></asp:TableCell>
                    <asp:TableCell ID="row8Desc" Text="Saves eBay search item in Shopping Cart" Width="30%"></asp:TableCell>
                    <asp:TableCell ID="row8Resource" Text="Use C# HttpCookie"></asp:TableCell>
                    <asp:TableCell ID="row8TryIt">
                        <asp:HyperLink ID="HyperLink8" runat="server" Text="Browse" NavigateUrl="~/Shopping.aspx" Width="10%"></asp:HyperLink>
                    </asp:TableCell>
                </asp:TableRow>

                <%-- ROW 9: James --%>
                <asp:TableRow CssClass="centered-table">
                    <asp:TableCell ID="row9Provider" Text="James Cajas" Width="10%"></asp:TableCell>
                    <asp:TableCell ID="row9Page" Text="Cookies: read in implmentation<br/>"></asp:TableCell>
                    <asp:TableCell ID="row9Desc" Text="JavaScript cookie functions: setCookie(), getCookie(), clearCookie(), used to persist cart state" Width="30%"></asp:TableCell>
                    <asp:TableCell ID="row9Resource" Text=""></asp:TableCell>
                    <asp:TableCell ID="row9TryIt">
                        <asp:HyperLink ID="HyperLink9" runat="server" Text="ShoppingCart" NavigateUrl="~/ShoppingCart.aspx" Width="10%"></asp:HyperLink>
                    </asp:TableCell>
                </asp:TableRow>

                <%-- ROW 10: Mark --%>
                <asp:TableRow CssClass="centered-table">
                    <asp:TableCell ID="row10Provider" Text="Mark Adan" Width="10%"></asp:TableCell>
                    <asp:TableCell ID="row10Page" Text="DLL:<br/>Shipping.dll"></asp:TableCell>
                    <asp:TableCell ID="row10Desc" Text="Library for shipping cost estimate" Width="30%"></asp:TableCell>
                    <asp:TableCell ID="row10Resource" Text="Use C# user class and static function"></asp:TableCell>
                    <asp:TableCell ID="row10TryIt">
                        <asp:HyperLink ID="HyperLink10" runat="server" Text="ShoppingCart" NavigateUrl="~/ShoppingCart.aspx" Width="10%"></asp:HyperLink>
                    </asp:TableCell>
                </asp:TableRow>

<%-- ROW 11: James --%>
<asp:TableRow CssClass="centered-table">
    <asp:TableCell ID="row11Provider" Text="James Cajas" Width="10%"></asp:TableCell>
    <asp:TableCell ID="row11Page" Text="DLL:<br/>group43.dll"></asp:TableCell>
    <asp:TableCell ID="row11Desc" Text="POST and GET" Width="30%"></asp:TableCell>
    <asp:TableCell ID="row11Resource" Text="GET: /ping, /orders, /orders/{id}<br/>POST: /CreateOrder(JSON)"></asp:TableCell>
    <asp:TableCell ID="row11TryIt">

        <asp:HyperLink 
            ID="HyperLink11a" 
            runat="server" 
            Text="Ping" 
            NavigateUrl="/OrderService.svc/ping" 
            Width="10%" />
        <br /><br />

        <asp:HyperLink 
            ID="HyperLink11b" 
            runat="server" 
            Text="ListOrders" 
            NavigateUrl="/OrderService.svc/orders" 
            Width="10%" />
        <br /><br />

        <asp:HyperLink 
            ID="HyperLink11c" 
            runat="server" 
            Text="Get Order ORD-A3915C9B" 
            NavigateUrl="/OrderService.svc/orders/ORD-FDC53C5C" 
            Width="10%" />

    </asp:TableCell>
</asp:TableRow>

                <%-- ROW 12: James --%>
                <asp:TableRow CssClass="centered-table">
                    <asp:TableCell ID="row12Provider" Text="James Cajas" Width="10%"></asp:TableCell>
                    <asp:TableCell ID="row12Page" Text="ShoppingCart.aspx"></asp:TableCell>
                    <asp:TableCell ID="row12Desc" Text="Order checkout page" Width="30%"></asp:TableCell>
                    <asp:TableCell ID="row12Resource" Text="aspx design and C# Code behind"></asp:TableCell>
                    <asp:TableCell ID="row12TryIt">
                        <asp:HyperLink ID="HyperLink12" runat="server" Text="ShoppingCart" NavigateUrl="~/ShoppingCart.aspx" Width="10%"></asp:HyperLink>
                    </asp:TableCell>
                </asp:TableRow>

                <%-- ROW 13: Mark --%>
                <asp:TableRow CssClass="centered-table">
                    <asp:TableCell ID="row13Provider" Text="Mark Adan" Width="10%"></asp:TableCell>
                    <asp:TableCell ID="row13Page" Text="Registration.aspx"></asp:TableCell>
                    <asp:TableCell ID="row13Desc" Text="Security Authentication Form for <br/>new (Human) members" Width="30%"></asp:TableCell>
                    <asp:TableCell ID="row13Resource" Text="CAPTCHA Verification<br/>Encryption.dll<br/>XML data storage"></asp:TableCell>
                    <asp:TableCell ID="row13TryIt">
                        <asp:HyperLink ID="HyperLink13" runat="server" Text="Register" NavigateUrl="~/Registration.aspx" Width="10%"></asp:HyperLink>
                    </asp:TableCell>
                </asp:TableRow>

                <%-- ROW 14: Andrew --%>
                <asp:TableRow CssClass="centered-table">
                    <asp:TableCell ID="row14Provider" Text="Andrew Munroe" Width="10%"></asp:TableCell>
                    <asp:TableCell ID="row14Page" Text="login.aspx"></asp:TableCell>
                    <asp:TableCell ID="row14Desc" Text="Security Authorization Form for existing <br/>members- member functionality allows members to<br/> view their past orders" Width="30%"></asp:TableCell>
                    <asp:TableCell ID="row14Resource" Text="Encryption.dll<br/>XML data storage"></asp:TableCell>
                    <asp:TableCell ID="row14TryIt">
                        <asp:HyperLink ID="HyperLink14" runat="server" Text="Login" NavigateUrl="~/login.aspx" Width="10%"></asp:HyperLink>
                    </asp:TableCell>
                </asp:TableRow>

                <%-- ROW 15: James --%>
                <asp:TableRow CssClass="centered-table">
                    <asp:TableCell ID="row15Provider" Text="James Cajas" Width="10%"></asp:TableCell>
                    <asp:TableCell ID="row15Page" Text="Admin.aspx"></asp:TableCell>
                    <asp:TableCell ID="row15Desc" Text="Security Authorization Form for system <br/>admins- admin functionality allows admins to<br/> view all orders" Width="30%"></asp:TableCell>
                    <asp:TableCell ID="row15Resource" Text="Encryption.dll<br/>XML data storage"></asp:TableCell>
                    <asp:TableCell ID="row15TryIt">
                        <asp:HyperLink ID="HyperLink15" runat="server" Text="Login" NavigateUrl="~/login.aspx" Width="10%"></asp:HyperLink>
                    </asp:TableCell>
                </asp:TableRow>

                <%-- ROW 16: James --%>
                <asp:TableRow CssClass="centered-table">
                    <asp:TableCell ID="row16Provider" Text="James Cajas" Width="10%"></asp:TableCell>
                    <asp:TableCell ID="row16Page" Text="ShoppingOrdersPage.aspx"></asp:TableCell>
                    <asp:TableCell ID="row16Desc" Text="Order Retrieval Page" Width="30%"></asp:TableCell>
                    <asp:TableCell ID="row16Resource" Text="OrderService.svc"></asp:TableCell>
                    <asp:TableCell ID="row16TryIt">
                        <asp:HyperLink ID="HyperLink16" runat="server" Text="Orders" NavigateUrl="~/ShoppingOrdersPage.aspx" Width="10%"></asp:HyperLink>
                    </asp:TableCell>
                </asp:TableRow>

            </asp:Table>
        </div>
    </form>
</body>
</html>