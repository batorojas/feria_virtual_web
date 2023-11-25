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

            <div>
            <asp:Button ID="btnDefault" runat="server" Text="salir" OnClick="btnDefault_Click" />
            <asp:Button ID="btnCompra" runat="server" Text="seleccion de pedidos" OnClick="btnCompra_Click" />
            <asp:Button ID="btnVenta" runat="server" Text="ir a pedidos" OnClick="btnVenta_Click" />
            </div>
            <h1>Compras terminadas</h1>     
            <asp:TextBox ID="txtRut" runat="server" placeholder="Ingrese su RUT" />
            <asp:Button ID="btnBuscar" runat="server" Text="Buscar Compras" OnClick="btnBuscar_Click" />

            <asp:GridView ID="gvDetallesPV" runat="server" AutoGenerateColumns="False">
                <Columns>
                    <asp:BoundField DataField="ID_DETALLE_PV" HeaderText="ID Detalle PV" />
                    <asp:BoundField DataField="ID_PRODUCTO" HeaderText="ID Producto" />
                    <asp:BoundField DataField="CANTIDAD" HeaderText="Cantidad" />
                    <asp:BoundField DataField="PRECIO_UNITARIO" HeaderText="Precio Unitario" />
                </Columns>
            </asp:GridView>

            <asp:Label ID="lblMensaje" runat="server" Visible="false" />


        </div>
    </form>
</body>
</html>
