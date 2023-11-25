<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Cliente.aspx.cs" Inherits="feria_virtual_web.Cliente" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>


            <asp:Button ID="btnDefault" runat="server" Text="salir" OnClick="btnDefault_Click" />
            <asp:Button ID="btnCompra" runat="server" Text="seleccion de pedidos" OnClick="btnCompra_Click" />
            <asp:Button ID="btnVenta" runat="server" Text="ir a pagos" OnClick="btnVenta_Click" />


        </div>
    </form>
</body>
</html>
