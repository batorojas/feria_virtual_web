  <%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Productor.aspx.cs" Inherits="feria_virtual_web.Productor" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
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
<form id="form1" runat="server" class="container">
    <br/>
    <h3>Registro de productos</h3>
    <br/>
        <div class="row">
            <div class="col-lg-6">
                <div class="form-group">
                    <asp:Label ID="Label1" runat="server" Text="N° de categoría" CssClass="control-label"></asp:Label>
                    <asp:DropDownList ID="ddlCategoria" runat="server" CssClass="form-control"></asp:DropDownList>
                </div>
                <div class="form-group">
                    <asp:Label ID="Label2" runat="server" Text="Nombre de producto" CssClass="control-label"></asp:Label>
                    <asp:TextBox ID="NOMBRE_PRODUCTO" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="form-group">
                    <asp:Label ID="Label3" runat="server" Text="Precio" CssClass="control-label"></asp:Label>
                    <div class="input-group mb-3">
                      <span class="input-group-text">$</span>
                    <asp:TextBox ID="PRECIO" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                </div>
                <div class="form-group">
                    <asp:Label ID="Label4" runat="server" Text="Calidad" CssClass="control-label"></asp:Label>
                    <asp:DropDownList ID="ddlCalidad" runat="server" CssClass="form-control"></asp:DropDownList>
                </div>
                <div class="form-group">
                    <asp:Label ID="Label5" runat="server" Text="Porcentaje de merma" CssClass="control-label"></asp:Label>
                    <asp:TextBox ID="PORCENTAJE_MERMA" runat="server" CssClass="form-control"></asp:TextBox>
                    <small id="porcentajeMermaHelp" class="form-text text-muted">Sólo valores decimales.</small>
                </div>
                <div class="form-group mt-3">
                    <asp:Button ID="agregar_producto" runat="server" Text="Agregar" CssClass="btn btn-primary" OnClick="agregar_producto_Click1" />
                </div>
            </div>
            <div class="col-md-6">
                <div class="gridview-container">
                   <asp:GridView ID="mostrar" runat="server" CssClass="table table-striped table-bordered table-sm table-hover table-responsive" AutoGenerateColumns="false"  OnRowCommand="mostrar_RowCommand" DataKeyNames="ID_PRODUCTO">
                                          <Columns>
                                              <asp:BoundField DataField="ID_PRODUCTO" HeaderText="#" />
                                              <asp:BoundField DataField="NOMBRE_PRODUCTO" HeaderText="Nombre" />
                                              <asp:BoundField DataField="PRECIO" HeaderText="Precio" />
                                              <asp:BoundField DataField="DESCRIPCION" HeaderText="Calidad" />
                                              <asp:BoundField DataField="PORCENTAJE_MERMA" HeaderText="Porcentaje de merma" />
                                              <asp:TemplateField HeaderText="Acción">
                                                  <ItemTemplate>
                                                      <div class="btn-group">
                                                          <asp:Button CommandName="Modificar" CommandArgument='<%# Container.DataItemIndex %>' Text="Modificar" runat="server" CssClass="btn btn-warning btn-sm" />
                                                          <asp:Button CommandName="Eliminar" CommandArgument='<%# Container.DataItemIndex %>' Text="Eliminar" runat="server" CssClass="btn btn-danger btn-sm" />
                                                      </div>
                                                  </ItemTemplate>
                                              </asp:TemplateField>
                                          </Columns>
                                      </asp:GridView>
                </div>
            </div>
        </div>
    </form>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/js/bootstrap.bundle.min.js"></script> 
<script src="https://code.jquery.com/jquery-3.7.1.min.js"></script> 
</body>
</html>