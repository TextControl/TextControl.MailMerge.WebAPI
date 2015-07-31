<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="WebAPI_Client.index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:DropDownList ID="DropDownList1" runat="server">
            <asp:ListItem Value="PDF">Adobe PDF</asp:ListItem>
            <asp:ListItem Value="DOCX">Office Open XML</asp:ListItem>
            <asp:ListItem Value="DOC">MS Word</asp:ListItem>
        </asp:DropDownList><br /><br />
        <asp:Button ID="Button1" runat="server" Text="Merge document" OnClick="Button1_Click" />
    </div>
    </form>
</body>
</html>
