<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AndrewTryIt.aspx.cs" Inherits="Group43.AndrewTryIt" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>TryIt</title>
    <style>
        body {
            font-family: Arial, sans-serif;
            margin: 20px;
            background-color: #f4f4f4;
            color: #333;
        }
        h1 {
            color: #0056b3;
        }
        .container {
            background-color: #fff;
            padding: 20px;
            border-radius: 8px;
            box-shadow: 0 2px 4px rgba(0,0,0,0.1);
            margin-bottom: 20px;
        }
        label {
            display: block;
            margin-bottom: 20px;
            font-weight: bold;
            margin-top: 16px;
        }
        p {
            margin-top: 5px;
            margin-bottom: 5px;
            font-size: 14px;
        }
        input[type="text"], textarea {
            padding: 10px;
            margin-bottom: 5px;
            border: 1px solid #ddd;
            border-radius: 4px;
            margin-top: 5px;
            margin-left: 16px;
        }
        .aspButton {
            border-style: none;
            border-color: inherit;
            border-width: medium;
            background-color: #007bff;
            color: white;
            padding: 10px 10px;
            border-radius: 5px;
            cursor: pointer;
            font-size: 16px;
            margin-top: 0px;
            margin-left: 12px;
            margin-bottom: 9px;
        }
        .aspButton:hover {
            background-color: #0056b3;
        }
        pre {
            background-color: #eee;
            padding: 10px;
            border-radius: 4px;
            overflow-x: auto;
            height: 67px;
        }
        .status-message {
            margin-top: 15px;
            padding: 10px;
            border-radius: 4px;
        }
        .status-success {
            background-color: #d4edda;
            color: #155724;
            border: 1px solid #c3e6cb;
        }
        .status-error {
            background-color: #f8d7da;
            color: #721c24;
            border: 1px solid #f5c6cb;
        }
    </style>
</head>
<body>
    <h1>TryIt WordCount Service</h1>

    <div class="container">
        <h2>WordCount Service Requests</h2>
        <form runat="server">
            <label for="FilterInput">Word Filtering:</label>
            <p>This method filters out stopwords and XML tags and returns a single string.</p><br />
            <asp:TextBox ID="FilterInput" runat="server"></asp:TextBox>
            <asp:Button class="aspButton" ID="FilterButton" runat="server" Text="Send Request" OnClick="FilterButton_Click" Height="39px"></asp:Button>
            <label for="responseOutput">Response Body:</label>
            <pre><asp:Label ID="responseOutput" runat="server" Text="" CssClass ="status-message"></asp:Label></pre>

          
            <br />
            <label for="CountInput">Word Count:</label>
            <p>This method takes a file input (as a byte stream) and returns a JSON string of a dictionary of the keys (unique words) and their frequency. It also calls the WordFilter method on the string obtained from the file as to not count XML tags and stop words in this process.</p><br />
         
            <asp:TextBox runat="server" ID="CountInput"></asp:TextBox>
            <asp:Button class="aspButton" ID="WordCountButton" runat="server" Text="Send Request" OnClick="WordCountButton_Click" Height="39px"></asp:Button>
            <label for="responseOutput2">Response Body:</label>
             <pre><asp:Label ID="responseOutput2" runat="server" Text="" CssClass ="status-message"></asp:Label></pre>
            <br />

            <label for="CountInput">Encryption .dll Function</label>
            <p>This encrypts/decrypts based on the Caesar cipher.</p><br />
         
            <asp:TextBox runat="server" ID="EncryptionInput"></asp:TextBox>
            <asp:Button class="aspButton" ID="EncryptButton" runat="server" Text="Encrypt" OnClick="EncryptButton_Click" Height="39px"></asp:Button>
            
            <asp:TextBox ID="DecryptionInput" runat="server" Style="margin-left: 100px"></asp:TextBox>

            <asp:Button class="aspButton" ID="Button1" runat="server" Text="Decrypt" Height="39px" OnClick="DecryptButton_Click" />

            <label for="responseOutput2">Result:</label>
            <pre><asp:Label ID="EncryptionOutput" runat="server" Text="" CssClass ="status-message"></asp:Label></pre>

            <asp:Button ID="Error" runat="server" Text="Test Error Handling" OnClick="Error_Click" />

        </form>
    </div>
</body>
</html>
