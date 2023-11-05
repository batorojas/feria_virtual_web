<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Transportista.aspx.cs" Inherits="feria_virtual_web.Transportista" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <h1>Envíos pendientes</h1>
    <form id="form1" runat="server">
        <div>
        </div>
        <div>
            <asp:GridView ID="GridViewSubasta" runat="server" AutoGenerateColumns="False" OnRowCommand="GridView1_RowCommand">
                <Columns>
                    <asp:BoundField DataField="ID_CABECERA_SUBASTA" HeaderText="ID" />
                    <asp:BoundField DataField="FECHA_LIMITE_ENTREGA" HeaderText="Fecha Entrega Limite" />
                    <asp:BoundField DataField="FECHA_DESPACHO_REALIZADO" HeaderText="Fecha Entrega Realizada" />
                    <asp:BoundField DataField="ID_COMUNA" HeaderText="ID_COMUNA" />
                    <asp:BoundField DataField="ID_CABECERA_PV" HeaderText="ID_CABECERA_PV" />
                    <asp:BoundField DataField="ID_ESTADO_SUBASTA" HeaderText="ID_ESTADO_SUBASTA" />
                    <asp:ButtonField ButtonType="Button" Text="Cambiar a Entregado" CommandName="ActualizarFechaEntrega" HeaderText="Actualizar Entrega" />
                </Columns>
            </asp:GridView>
        </div>
        <p>
            &nbsp;</p>
    </form>
</body>
</html>
