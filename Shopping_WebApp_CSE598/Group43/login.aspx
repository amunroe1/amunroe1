<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="Group43.login" %>

<!DOCTYPE html>
<style>
    form{
        text-align:center
    }
    form > div{
        margin-bottom:10px;
    }
</style>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="UTF-8"/>
    <meta name="viewport" content="width=device-width, initial-scale=1.0"/>
    <title>Login Form</title>
</head>
<body>

    <form runat="server">
        <h2>Login</h2>
        <div>
            <label for="email" style="margin-right: 30px">E-mail:</label>
            <asp:textbox runat="server" id="EmailInput" name="email" required="required"/>
        </div>
        <div>
            <label for="password">Password:</label>
            <asp:textbox runat="server" id="PwdInput" name="password" required="required"/>
        </div>
        <asp:Button ID="Cancel" runat="server" Text="Cancel" style="margin-bottom:10px" OnClick="Cancel_Click" />
        <asp:button runat="server" ID="LoginButton" Text="Log In" OnClick="Login_Click" style="margin-bottom:10px"></asp:button>
        <br />
        <asp:label runat="server" id="msg" style="color:red"/>
    </form>

</body>
</html>
