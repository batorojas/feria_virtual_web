<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="compra.aspx.cs" Inherits="feria_virtual_web.compra" EnableEventValidation="false" Async="true" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml" xmlns="http://www.w3.org/1999/html">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Pagos - Maipo Grande</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-T3c6CoIi6uLrA9TneNEoa7RxnatzjcDSCmG1MXxSR1GAsXEV/Dwwykc2MPK8M2HN" crossorigin="anonymous">
</head>
<body class="bg-body-tertiary">
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
                                <a class="nav-link text-white" href="/Venta.aspx">Ingreso de pedidos</a>
                                </li>
                            <li class="nav-item">
                                <a class="nav-link text-white" href="/Compra.aspx">Pagos</a>
                                </li>
                            <li class="nav-item">
                                <a class="nav-link text-white" href="/Cliente.aspx">Historial de Pedidos</a>
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
    <form id="form1" runat="server" class="container-fluid">
        <div class="py-5 text-center">
            <h3>Paga tu pedido aquí</h3>
            <p class="lead">
                Revisa y paga tus pedidos pendientes aquí ingresando tu RUT.
                Paypal es la plataforma de pago que utilizamos para que puedas pagar de forma segura y rápida.
            </p>
            <div class="form-group">
                <label for="txtRut">RUT:</label>
                    <div class="col-auto">
                <asp:TextBox runat="server" ID="txtRut" CssClass="form-class"></asp:TextBox>
            </div>
            <asp:RegularExpressionValidator 
                                                ID="revRutCliente" 
                                                runat="server" 
                                                ControlToValidate="txtRut"
                                                ForeColor="Red"
                                                Display="Dynamic"
                                                SetFocusOnError="True"
                                                ErrorMessage="El formato ingresado es incorrecto, ingrese su RUT sin puntos, guión o dígito verificador." 
                                                ValidationExpression="^\d{1,8}$">
                            </asp:RegularExpressionValidator>
                            <asp:RequiredFieldValidator 
                                                ID="rfvRutCliente" 
                                                runat="server" 
                                                ControlToValidate="txtRut"
                                                ForeColor="Red"
                                                Display="Dynamic"
                                                SetFocusOnError="True"
                                                ErrorMessage="Ingrese el rut del cliente">
                                </asp:RequiredFieldValidator>
            <div class="form-group mt-2">
                <asp:Button runat="server" ID="btnConsultar" Text="Consultar" OnClick="btnConsultar_Click" CssClass="btn btn-primary"/>
            </div>
                <br/>
                <asp:GridView runat="server" ID="gvDetallesPV" AutoGenerateColumns="false" OnRowCommand="gvDetallesPV_RowCommand" DataKeyNames="ID_DETALLE_PV" CssClass="table table-striped table-bordered table-sm table-hover table-responsive">
                 <Columns>
                      <asp:BoundField DataField="ID_DETALLE_PV" HeaderText="N°. Pedido" Visible="False"/>
                     <asp:BoundField DataField="ID_DETALLE_PV" HeaderText="Cod. producto" />
                      <asp:BoundField DataField="NOMBRE_PRODUCTO" HeaderText="Producto" />
                      <asp:BoundField DataField="CANTIDAD" HeaderText="Cantidad" />
                      <asp:BoundField DataField="PRECIO_UNITARIO" HeaderText="Precio por unidad" />
                     <asp:BoundField DataField="TOTAL" HeaderText="Total a pagar" />
                      <asp:ButtonField ButtonType="Button" Text="Pagar" CommandName="Pagar" />
                  </Columns>
                </asp:GridView>
            
        </div>
        </div>
    </form>
<script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/js/bootstrap.bundle.min.js" integrity="sha384-dZlgeVAPNl/w7th5MzUkkakH64OaaAOb4aqae8zi5A7EG5Qp4IKdB2X9ElcP9Rb" crossorigin="anonymous"></script>
<script src="https://code.jquery.com/jquery-3.7.1.min.js"></script> 
</body>
</html>