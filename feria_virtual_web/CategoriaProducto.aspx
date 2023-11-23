<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CategoriaProducto.aspx.cs" Inherits="feria_virtual_web.CategoriaProducto" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml" xmlns="http://www.w3.org/1999/html" xmlns="http://www.w3.org/1999/html">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
      <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Administración de productos - Maipo Grande</title>
        <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-T3c6CoIi6uLrA9TneNEoa7RxnatzjcDSCmG1MXxSR1GAsXEV/Dwwykc2MPK8M2HN" crossorigin="anonymous"> <!-- Enlace a Bootstrap CDN -->
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
                                <a class="nav-link text-white" href="/Productor.aspx">Productos</a>
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
<form id="HtmlForm" runat="server" class="container-fluid">
    <br/>
    <h3>Tipo de productos</h3>
    <div class="row">
        <div class="col-lg-6">
            <div class="form-group">
                <asp:Label ID="Label1" runat="server" Text="Categoría" CssClass="control-label"></asp:Label>
                <asp:TextBox ID="NOMBRE_CATEGORIA" runat="server" CssClass="form-control"></asp:TextBox>
                <asp:RequiredFieldValidator ID="ValidarNombreProducto" runat="server"
                                            ControlToValidate="NOMBRE_CATEGORIA"
                                            ErrorMessage="Ingrese un nombre de categoría válido."
                                            ForeColor="Red"
                                            Display="Dynamic"
                                            SetFocusOnError="True">
                </asp:RequiredFieldValidator>
                </div>
                <div class="form-group mt-3">
                                    <asp:Button ID="btnAgregarCategoria" runat="server" Text="Agregar" CssClass="btn btn-primary" OnClick="btnAgregarCategoria_OnClick" />
                                </div>
            </div>
        <div class="col-md-6">
            <asp:GridView ID="mostrar" runat="server" CssClass="table table-striped table-bordered table-sm table-hover table-responsive" AutoGenerateColumns="False" >
                <Columns>
                    <asp:BoundField DataField="ID_CATEGORIA" HeaderText="#" InsertVisible="False" ReadOnly="True"/>
                    <asp:BoundField DataField="NOMBRE_CATEGORIA" HeaderText="Categoría"/>
                </Columns>
            </asp:GridView>
        </div>
        </div>
</form>
<script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/js/bootstrap.bundle.min.js"></script> 
<script src="https://code.jquery.com/jquery-3.7.1.min.js"></script> 
</body>
</html>