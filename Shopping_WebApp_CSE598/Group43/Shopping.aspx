<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Shopping.aspx.cs" Inherits="Group43.Shopping" %>

<!DOCTYPE html>

<!-- 
    ########################################
            CSE 598: Assignment 5 & 6
              Group: 43
        Last Update: 11/8/25
    ########################################
 -->

<style>
    .centered-label 
    {
        display:block;
        text-align:center;
        width:100%;
    }

    .centered-table 
    {
        margin-left: auto;
        margin-right: auto;
        width: 80%;
        border: 2px solid black;
        border-right: 2px solid black;
        padding: 8px;
        text-align: left;
        border-collapse: collapse; /* Optional: for better table appearance */
    }

    .centered-table th, .center-table td
    {
        border: 2px solid black;
        border-right: 2px solid black;
        padding: 8px;
        text-align: left;
    }

    .button-grid
    {
        display:grid;
        grid-template-columns:auto auto;
        grid-template-rows:auto auto;
        gap:25px;
        justify-content:center;
        align-content:center;
    }
        
    .centered-button 
    {
        display: block;
        margin: 0 auto;
    }

</style>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Group 43 Web Application</title>
</head>
<body>
    <form id="form1" runat="server">
        <div style="text-align:right">
             <asp:Label ID="userLabel" runat="server" Text="" Font-Size="10pt"></asp:Label>
        </div>
        <div>
            <asp:Label ID="Label1" runat="server" Text="Search the Group 43 Market Place" Font-Bold="true" Font-Size="20pt" CssClass="centered-label"></asp:Label>
        </div>
        <br/><br/>
        <div style="text-align: center;">
            <asp:TextBox ID="SearchTxtBox" runat="server" Width="400px"></asp:TextBox>
            <asp:Button ID="searchBtn" runat="server" Text="Search" OnClick="SearchBtn_Click" />
        </div>
        <br/><br/><br />
        <div>
            <asp:Table ID="table1" runat="server" BorderStyle="Solid" BorderWidth="1px" CellPadding="5" CssClass="centered-table" Visible="false">
                 <asp:TableRow>
                     <asp:TableHeaderCell Text="ID" Font-Bold="true" Width="10%"></asp:TableHeaderCell>
                    <asp:TableHeaderCell Text="Description" Font-Bold="true"></asp:TableHeaderCell>
                    <asp:TableHeaderCell Text="Price" Font-Bold="true"></asp:TableHeaderCell>
                    <asp:TableHeaderCell></asp:TableHeaderCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell ID="r1Id" Text="Row 1, Cell 0" CssClass="centered-table" Width="10%"></asp:TableCell>
                    <asp:TableCell ID="r1Desc" Text="Row 1, Cell 1" CssClass="centered-table"></asp:TableCell>
                    <asp:TableCell ID="r1Price" Text="Row 1, Cell 2" CssClass="centered-table"></asp:TableCell>
                    <asp:TableCell>
                        <asp:Button ID="addItemBtn1" runat="server" Text="add to cart" OnClick="AddItemBtn1_Click" />
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell ID="r2Id" Text="Row 2, Cell 0" CssClass="centered-table" Width="10%"></asp:TableCell>
                    <asp:TableCell ID="r2Desc" Text="Row 2, Cell 1" CssClass="centered-table"></asp:TableCell>
                    <asp:TableCell ID="r2Price" Text="Row 2, Cell 2" CssClass="centered-table"></asp:TableCell>
                    <asp:TableCell>
                        <asp:Button ID="addItemBtn2" runat="server" Text="add to cart" OnClick="AddItemBtn2_Click" />
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell ID="r3Id" Text="Row 3, Cell 0" CssClass="centered-table" Width="10%"></asp:TableCell>
                    <asp:TableCell ID="r3Desc" IText="Row 3, Cell 1" CssClass="centered-table"></asp:TableCell>
                    <asp:TableCell ID="r3Price" Text="Row 3, Cell 2" CssClass="centered-table"></asp:TableCell>
                    <asp:TableCell>
                        <asp:Button ID="addItemBtn3" runat="server" Text="add to cart" OnClick="AddItemBtn3_Click" />
                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>
        </div>
        <div>
            <asp:Label ID="noItemsLbl" runat="server" Text="No items found." Font-Bold="true" Font-Size="16pt" CssClass="centered-label" Visible="false"></asp:Label>
        </div>
        <br />
         <div>
            <asp:Label ID="cartLabel" runat="server" Text="Your cart is empty." Font-Bold="true" Font-Size="12pt" CssClass="centered-label" Visible="false"></asp:Label>
        </div>
        <br />
        <div>
            <asp:Label ID="Label3" runat="server" Text="______________________________________________________________" 
                Font-Bold="true" Font-Size="20pt" CssClass="centered-label"></asp:Label>
        </div>
        <br /><br />
        <div>
            <asp:Panel ID="Panel2" runat="server" CssClass="button-grid">
                <asp:Button ID="homeBtn" runat="server" Text="Home" Width="75px" OnClick="HomeBtn_Click" />
                <asp:Button ID="shoppingCartBtn" runat="server" Text="View Cart" Width="75px" OnClick="ShoppingCartBtn_Click"/>
            </asp:Panel>
        </div>
    </form>
</body>
</html>
