<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Transportista.aspx.cs" Inherits="feria_virtual_web.Transportista" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-T3c6CoIi6uLrA9TneNEoa7RxnatzjcDSCmG1MXxSR1GAsXEV/Dwwykc2MPK8M2HN" crossorigin="anonymous" runat="server" />
    <title></title>
</head>

<body>
<div class="container">
    <h1 class="mt-4">Envíos pendientes</h1>
    <form id="form1" runat="server">
        <div>
        </div>
        <div class="table-responsive">
 <asp:GridView ID="GridViewSubasta" runat="server" AutoGenerateColumns="False" OnRowCommand="GridView1_RowCommand" CssClass="table table-bordered table-hover">                
     <Columns>
                   <asp:BoundField DataField="ID_CABECERA_SUBASTA" HeaderText="#" ItemStyle-HorizontalAlign="Center" />
                                          <asp:BoundField DataField="FECHA_LIMITE_ENTREGA" HeaderText="Fecha límite" ItemStyle-HorizontalAlign="Center" />
                                          <asp:BoundField DataField="FECHA_DESPACHO_REALIZADO" HeaderText="Fecha entrega finalizada" ItemStyle-HorizontalAlign="Center" />
                                          <asp:BoundField DataField="NOMBRE_COMUNA" HeaderText="Comuna" ItemStyle-HorizontalAlign="Center" />
                                          <asp:BoundField DataField="ID_CABECERA_PV" HeaderText="N° de venta" ItemStyle-HorizontalAlign="Center" />
                                          <asp:BoundField DataField="RUT_CLIENTE" HeaderText="Rut de cliente" ItemStyle-HorizontalAlign="Center" />
                                          <asp:BoundField DataField="DESC_ESTADO" HeaderText="Estado" ItemStyle-HorizontalAlign="Center" />
                    <asp:ButtonField ButtonType="Button" Text="Cambiar a Entregado" CommandName="ActualizarFechaEntrega" HeaderText="Actualizar Entrega"/>
                </Columns>
     <HeaderStyle CssClass="text-center" />
         <RowStyle CssClass="text-center" />
            </asp:GridView>
        </div>
    </form>
</div>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/js/bootstrap.bundle.min.js" integrity="sha384-dZlgeVAPNl/w7th5MzUkkakH64OaaAOb4aqae8zi5A7EG5Qp4IKdB2X9ElcP9Rb" crossorigin="anonymous"></script>
</body>
</html>
