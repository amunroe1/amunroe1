<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Registration.aspx.cs" Inherits="Group43.Registration" %>
<%@ Register TagPrefix="captcha" TagName="UserControlCaptcha" Src="~/Captcha1.ascx" %>

<!DOCTYPE html>

<!-- 
    ########################################
         CSE 598 - Assigment 6
                   Member Registration Page
          Author - Mark Adan
         version - 1.0
    last uppdate - 11/18/25
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
        width: 15%;
        padding: 2px;
        text-align: left;
        border-collapse: collapse; /* Optional: for better table appearance */
    }
</style>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Registration</title>
</head>
<body>
    <form id="form1" runat="server">
        <br />
        <div>
            <asp:Label ID="Label1" runat="server" Text="Group 43 - New Member Registration" Font-Bold="true" Font-Size="20pt" CssClass="centered-label"></asp:Label>
        </div>
        <br /><br />
        <div>
            <asp:Table ID="table1" runat="server"  CellPadding="5" CssClass="centered-table">
                <%-- ROW 1: --%>
                <asp:TableRow>
                    <asp:TableCell ID="userNameCell" Text="Email:" Width="30%" HorizontalAlign="Right"></asp:TableCell>
                    <asp:TableCell>
                        <asp:TextBox ID="userNameTxtBox" runat="server" Width="150px" TextMode="Email"></asp:TextBox>
 
                    </asp:TableCell>
                </asp:TableRow>

                <%-- ROW 2: --%>
                <asp:TableRow>
                    <asp:TableCell ID="passwordCell" Text="Password: <br/>" Width="30%" HorizontalAlign="Right"></asp:TableCell>
                    <asp:TableCell>
                        <asp:TextBox ID="passwordTxtBox" runat="server" Width="150px"></asp:TextBox>
                    </asp:TableCell>
                </asp:TableRow>

                 <%-- ROW 3: --%>
                <asp:TableRow >
                    <asp:TableCell ID="emptyCell1"  Width="30%"></asp:TableCell>
                </asp:TableRow>

                 <%-- ROW 4: --%>
                <asp:TableRow>
                    <asp:TableCell ID="captchaCell"  Width="30%">
                           <captcha:UserControlCaptcha ID="userControlCaptcha1" Visible="false" runat="server"/>
                    </asp:TableCell>
                    <asp:TableCell>
                         <asp:Button ID="refreshBtn" runat="server" Text="Refresh Image" OnClick="refreshBtn_Click" Width="100px" />
                    </asp:TableCell>
                </asp:TableRow>

                <%-- ROW 5: --%>
                <asp:TableRow >
                    <asp:TableCell ID="verifyCell" Width="30%" HorizontalAlign="Right">Verify you're human: <br /></asp:TableCell>
                    <asp:TableCell>
                        <asp:TextBox ID="verificationTxtBox" runat="server" Width="100px"></asp:TextBox>
                    </asp:TableCell>
                </asp:TableRow>

                <%-- ROW 6: --%>
                <asp:TableRow >
                    <asp:TableCell ID="captchaErrorCell" ColumnSpan="2" HorizontalAlign="Center">
                        <asp:Label ID="captchaErrorLbl" runat="server" Text="" Font-Bold="true" Font-Size="12pt" Visible="false" BackColor="#ff0000" ></asp:Label>
                    </asp:TableCell>
                </asp:TableRow>

                <%-- ROW 7: --%>
                <asp:TableRow >
                    <asp:TableCell ID="registerCell" Width="30%" HorizontalAlign="Center">
                        <asp:Button ID="registerBtn" runat="server" Text="Register" OnClick="registerBtn_Click" Width="100px" />
                    </asp:TableCell>
                    <asp:TableCell ID="cancelCell" HorizontalAlign="Center">
                        <asp:Button ID="cancelBtn" runat="server" Text="Cancel" OnClick="cancelBtn_Click" Width="100px" />
                    </asp:TableCell>
                </asp:TableRow>

            </asp:Table>
        </div>
    </form>
</body>
</html>
