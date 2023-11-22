<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ListaPedidos.aspx.cs" Inherits="feria_virtual_web.ListaPedidos" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-T3c6CoIi6uLrA9TneNEoa7RxnatzjcDSCmG1MXxSR1GAsXEV/Dwwykc2MPK8M2HN" crossorigin="anonymous" runat="server" />
    <title></title>
</head>
<body>
<nav class="navbar navbar-expand-lg navbar-light" style="background-color: #46b5d1"> <!-- Agrega la clase de fondo -->
              <div class="container-fluid">
                  <!-- Navbar Brand with Image -->
                  <a class="navbar-brand" href="/">
                      <img src="images/logo_transparent.png" alt="Logo" width="30" height="30" class="d-inline-block align-text-top">
                  </a>
    
                    <!-- Navbar Toggler Button for Responsive Design -->
                    <button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarNav" aria-controls="navbarNav" aria-expanded="false" aria-label="Toggle navigation">
                        <span class="navbar-toggler-icon"></span>
                    </button>
    
                    <!-- Navbar Links -->
                    <div class="collapse navbar-collapse" id="navbarNav">
                        <ul class="navbar-nav">
                            <!-- Opción de Productos -->
                            <li class="nav-item">
                                <a class="nav-link text-white" href="/Transportista.aspx">Envíos</a>
                            </li>
                            <li class="nav-item">
                                <a class="nav-link text-white" href="ListaPedidos.aspx">Historial</a>
                            </li>
                        </ul>
                    </div>
                   <ul class="navbar-nav justify-content-end">
                                              <li class="nav-item">
                                                  <a class="nav-link text-white" href="/">Cerrar sesión</a>
                                              </li>
                                          </ul>
                </div>
            </nav>
    <div class="container">
        <h3 class="mt-4">Historial de pedidos</h3>
        <form id="form1" runat="server">
            <div>
            </div>
            <div class="table-responsive">
                <asp:GridView ID="GridViewListaPedidos" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-hover">
                    <Columns>
                        <asp:BoundField DataField="ID_CABECERA_SUBASTA" HeaderText="#" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="FECHA_LIMITE_ENTREGA" HeaderText="Fecha límite" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="FECHA_DESPACHO_REALIZADO" HeaderText="Fecha entrega finalizada" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="NOMBRE_COMUNA" HeaderText="Comuna" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="ID_CABECERA_PV" HeaderText="N° de venta" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="RUT_CLIENTE" HeaderText="Rut de cliente" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="DESC_ESTADO" HeaderText="Estado" ItemStyle-HorizontalAlign="Center" />
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
