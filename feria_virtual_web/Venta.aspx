<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Venta.aspx.cs" Inherits="feria_virtual_web.Venta"  EnableEventValidation="false"%>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Generar Solicitud</title>
</head>
<body>
    <form id="form1" runat="server">


        <div>
            <h1>Generar Solicitud</h1>

            <asp:Button ID="btnDefault" runat="server" Text="salir" OnClick="btnDefault_Click" />
            <asp:Button ID="btnCompra" runat="server" Text="seleccion de pedidos" OnClick="btnCompra_Click" />
            


            <div>
                <label for="txtObservacion">Observación PV:</label>
                <asp:TextBox runat="server" ID="txtObservacion"></asp:TextBox>
            </div>

            <div>
                <label for="txtRutCliente">RUT Cliente:</label>
                <asp:TextBox runat="server" ID="txtRutCliente"></asp:TextBox>
            </div>

            <div>
                <label for="btnGenerarSolicitud"></label>
                <asp:Button runat="server" ID="btnGenerarSolicitud" Text="Generar Solicitud" OnClick="btnGenerarSolicitud_Click" />
            </div>
            <h1>Productos</h1>

            <asp:GridView runat="server" ID="gvDetallesPV" AutoGenerateColumns="false" OnRowCommand="gvDetallesPV_RowCommand" OnSelectedIndexChanged="gvDetallesPV_SelectedIndexChanged">
                <Columns>
                    <asp:BoundField DataField="ID_PRODUCTO" HeaderText="#" />
                    <asp:BoundField DataField="NOMBRE_PRODUCTO" HeaderText="Nombre" />
                    <asp:BoundField DataField="PRECIO" HeaderText="Precio" />
                </Columns>
            </asp:GridView>

            <h1></h1>
            <div>
                <div>
                    <asp:Label runat="server" ID="lblIDProducto" Text="id_producto" Visible="true"></asp:Label>
                    <asp:TextBox runat="server" ID="id_producto"></asp:TextBox>
                </div>
                <div>
                    <asp:Label runat="server" ID="lblPrecioUnitario" Text="cantidad" Visible="true"></asp:Label>
                    <asp:TextBox runat="server" ID="cantidadde"></asp:TextBox>
                </div>

                <div>
                    <asp:Button runat="server" ID="btnAgregarPedido" Text="Agregar Pedido" OnClick="btnAgregarPedido_Click" />
                    <h1></h1>
                    <div>
                    
                    </div>

                </div>
            </div>

        </div>
    </form>
</body>
</html>