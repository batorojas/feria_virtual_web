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
            <h1  ></h1>  
            
                <asp:DataGrid runat="server" ID="dgDetallesPV" AutoGenerateColumns="false" OnItemCommand="dgDetallesPV_ItemCommand" OnSelectedIndexChanged="dgDetallesPV_SelectedIndexChanged">
                <Columns>
               <asp:BoundColumn DataField="ID_PRODUCTO" HeaderText="ID Producto" />
               <asp:BoundColumn DataField="CANTIDAD" HeaderText="Cantidad" />
                  <asp:BoundColumn DataField="PRECIO_UNITARIO" HeaderText="Precio Unitario" />
                 <asp:BoundColumn DataField="ID_CABECERA_PV" HeaderText="ID Cabecera PV" />
                 <asp:TemplateColumn HeaderText="Acciones">
                  <ItemTemplate>
                  <asp:Button runat="server" Text="Pagar" CommandName="PagarDetalle" />
                  </ItemTemplate>
                   </asp:TemplateColumn>


              </Columns>
                </asp:DataGrid>
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
                <label for="paypal">prueba de paipal</label>
                <asp:Button runat="server" ID="Button1" Text="prueba de paypal" OnClick="btnPaypal_Click" />
               </div>

                    </div>
            </div>
            
        </div>
    </form>
</body>
</html>

