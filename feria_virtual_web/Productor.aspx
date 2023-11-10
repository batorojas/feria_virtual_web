<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Productor.aspx.cs" Inherits="feria_virtual_web.Productor" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/css/bootstrap.min.css" /> <!-- Enlace a Bootstrap CDN -->
</head>
<body>
    <form id="form1" runat="server" class="container">
        <h1>Lista de productos</h1>
        <div class="row">
            <div class="col-md-6">
                <div class="form-group">
                    <asp:Label ID="Label1" runat="server" Text="N° de categoría" CssClass="control-label"></asp:Label>
                    <asp:TextBox ID="ID_CATEGORIA" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="form-group">
                    <asp:Label ID="Label2" runat="server" Text="Nombre de producto" CssClass="control-label"></asp:Label>
                    <asp:TextBox ID="NOMBRE_PRODUCTO" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="form-group">
                    <asp:Label ID="Label3" runat="server" Text="Precio" CssClass="control-label"></asp:Label>
                    <asp:TextBox ID="PRECIO" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="form-group">
                    <asp:Label ID="Label4" runat="server" Text="Calidad" CssClass="control-label"></asp:Label>
                    <asp:TextBox ID="ID_CALIDAD" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="form-group">
                    <asp:Label ID="Label5" runat="server" Text="Porcentaje de merma" CssClass="control-label"></asp:Label>
                    <asp:TextBox ID="PORCENTAJE_MERMA" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="form-group">
                    <asp:Button ID="agregar_producto" runat="server" Text="Agregar" CssClass="btn btn-primary" OnClick="agregar_producto_Click1" />
                </div>
            </div>
            <div class="col-md-6">
                <div class="gridview-container">
                   <asp:GridView ID="mostrar" runat="server" CssClass="table table-striped table-bordered table-sm table-hover table-responsive" AutoGenerateColumns="false">
                       <Columns>
                           <asp:BoundField DataField="ID_PRODUCTO" HeaderText="#" />
                           <asp:BoundField DataField="NOMBRE_PRODUCTO" HeaderText="Nombre" />
                           <asp:BoundField DataField="PRECIO" HeaderText="Precio" />
                           <asp:BoundField DataField="ID_CALIDAD" HeaderText="Calidad" />
                           <asp:BoundField DataField="PORCENTAJE_MERMA" HeaderText="Porcentaje de merma" />
                           <asp:TemplateField HeaderText="Acción">
                               <ItemTemplate>
                                   <asp:LinkButton CommandName="Modificar" CommandArgument='<%# Container.DataItemIndex %>' Text="Modificar" runat="server" CssClass="btn btn-warning btn-sm btn-block action-button" />
                                   <asp:LinkButton CommandName="Eliminar" CommandArgument='<%# Container.DataItemIndex %>' Text="Eliminar" runat="server" CssClass="btn btn-danger btn-sm btn-block action-button" />
                               </ItemTemplate>
                           </asp:TemplateField>
                       </Columns>
                   </asp:GridView>

                </div>
            </div>
        </div>
    </form>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/js/bootstrap.min.js"></script> <!-- Incluyendo el archivo JavaScript de Bootstrap -->
</body>
</html>
