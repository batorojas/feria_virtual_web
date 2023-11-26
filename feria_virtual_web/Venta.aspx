<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Venta.aspx.cs" Inherits="feria_virtual_web.Venta"  EnableEventValidation="false"%>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml" xmlns="http://www.w3.org/1999/html" xmlns="http://www.w3.org/1999/html">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Ingreso de pedidos - Maipo Grande</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-T3c6CoIi6uLrA9TneNEoa7RxnatzjcDSCmG1MXxSR1GAsXEV/Dwwykc2MPK8M2HN" crossorigin="anonymous">
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
<form id="form1" runat="server" class="container">
    <br/>
    <div>
        <h3>Ingreso de pedidos</h3>
        <br/>
        <div class="row">
            <div class="col-lg-6">
            <div class="form-group">
                <asp:Label ID="Label1" runat="server" Text="RUT" CssClass="control-label"></asp:Label>
                <asp:TextBox runat="server" ID="txtRutCliente" CssClass="form-control"></asp:TextBox>
                <asp:RegularExpressionValidator 
                    ID="revRutCliente" 
                    runat="server" 
                    ControlToValidate="txtRutCliente"
                    ForeColor="Red"
                    Display="Dynamic"
                    SetFocusOnError="True"
                    ErrorMessage="El formato ingresado es incorrecto, ingrese su RUT sin puntos, guión o dígito verificador." 
                    ValidationExpression="^\d{1,8}$">
                </asp:RegularExpressionValidator>
                <asp:RequiredFieldValidator 
                    ID="rfvRutCliente" 
                    runat="server" 
                    ControlToValidate="txtRutCliente"
                    ForeColor="Red"
                    Display="Dynamic"
                    SetFocusOnError="True"
                    ErrorMessage="Ingrese el rut del cliente">
                </asp:RequiredFieldValidator>
            </div>
           <div class="form-floating">
                <asp:Label ID="Label2" runat="server" Text="Observaciones" CssClass="control-label"></asp:Label>
                <asp:TextBox runat="server" ID="txtObservacion" CssClass="form-control" MaxLength="300" TextMode="MultiLine" Rows="4"></asp:TextBox>
               <div id="obsHelp" class="form-text">Instrucciones y detalles para el pedido, como horario de entrega sugerido, detalles de ubicación, etc.</div>
            </div>
                 <div class="form-group">
                    <asp:Label runat="server" ID="lblIDProducto" Text="Producto" Visible="true"  ></asp:Label>
                    <asp:DropDownList ID="ddlProducto" runat="server" CssClass="form-control"></asp:DropDownList>
                </div>
                <div class="form-group">
                    <asp:Label runat="server" ID="lblPrecioUnitary" Text="Cantidad" Visible="true" ></asp:Label>
                    <asp:TextBox runat="server" ID="cantidadde" CssClass="form-control"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvCantidadDe" runat="server" 
                                                ControlToValidate="cantidadde"
                                                Display="Dynamic" 
                                                ForeColor="Red"
                                                ErrorMessage="Ingrese la cantidad.">
                    </asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="revCantidadDe" runat="server" ControlToValidate="cantidadde"
                                                    Display="Dynamic" ErrorMessage="La cantidad sólo puede contener hasta 8 caracteres numéricos."
                                                    ValidationExpression="\d{1,8}"
                                                    ForeColor="Red">
                    </asp:RegularExpressionValidator>
                </div>
               
                <div class="form-group mt-3">
                    <div id="liveAlertPlaceholder"></div>
                    <asp:Button runat="server" ID="btnAgregarPedido" Text="Pedir" OnClick="btnAgregarPedido_Click" CssClass="btn btn-primary"/>
                                    <asp:ValidationSummary ID="vsCantidadDe" runat="server" 
                                                           HeaderText="Corrija los siguientes errores:" 
                                                           DisplayMode="BulletList"
                                                           ForeColor="Red" />
                    </div>
                </div>
                <div class="col-md-6">
                    <div class="gridview-container">
                 <asp:GridView runat="server" ID="gvDetallesPV" AutoGenerateColumns="false" OnRowCommand="gvDetallesPV_RowCommand" OnSelectedIndexChanged="gvDetallesPV_SelectedIndexChanged" CssClass="table table-striped table-bordered table-sm table-hover table-responsive">
                                <Columns>
                                    <asp:BoundField DataField="ID_PRODUCTO" HeaderText="#" />
                                    <asp:BoundField DataField="NOMBRE_PRODUCTO" HeaderText="Nombre" />
                                    <asp:BoundField DataField="PRECIO" HeaderText="Precio" />
                                </Columns>
                            </asp:GridView>
                        </div>
            </div>
        </div>
    </div>
    </form>
<script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/js/bootstrap.bundle.min.js" integrity="sha384-dZlgeVAPNl/w7th5MzUkkakH64OaaAOb4aqae8zi5A7EG5Qp4IKdB2X9ElcP9Rb" crossorigin="anonymous"></script>
<script src="https://code.jquery.com/jquery-3.7.1.min.js"></script> 
</body>
</html>