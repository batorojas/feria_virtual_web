<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="compra.aspx.cs" Inherits="feria_virtual_web.compra" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Consulta de Detalles PV</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h1>Consulta de Detalles PV</h1>

            <div>
                <label for="txtRut">RUT:</label>
                <asp:TextBox runat="server" ID="txtRut"></asp:TextBox>
            </div>

            <div>
                <asp:Button runat="server" ID="btnConsultar" Text="Consultar Detalles PV" OnClick="btnConsultar_Click" />
            </div>

            <div>
                <asp:GridView runat="server" ID="gvDetallesPV" AutoGenerateColumns="false">
                    <Columns>
                        <asp:BoundField DataField="ID_DETALLE_PV" HeaderText="ID Detalle PV" />
                        <asp:BoundField DataField="ID_PRODUCTO" HeaderText="ID Producto" />
                        <asp:BoundField DataField="CANTIDAD" HeaderText="Cantidad" />
                        <asp:BoundField DataField="PRECIO_UNITARIO" HeaderText="Precio Unitario" />
                        <asp:ButtonField ButtonType="Button" Text="Pagar" CommandName="Pagar" />
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </form>
</body>
</html>