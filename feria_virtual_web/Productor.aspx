<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Productor.aspx.cs" Inherits="feria_virtual_web.Productor" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <h1> usuario productor</h1>
    <form id="form1" runat="server">
        <div>
        <div>
            <div> 
            <asp:Label ID="Label" runat="server" Text="RUT"></asp:Label>
            <asp:TextBox ID="ID_producto" runat="server"></asp:TextBox>
            </div>
            <div> 
            <asp:Label ID="Label2" runat="server" Text="NOMBRE"></asp:Label>
            <asp:TextBox ID="ID_CATEGORIA" runat="server"></asp:TextBox>
            </div> 
            <div> 
            <asp:Label ID="Label3" runat="server" Text="NOMBRE_PRODUCTO"></asp:Label>
            <asp:TextBox ID="NOMBRE_PRODUCTO" runat="server"></asp:TextBox>
            </div> 
            <div> 
            <asp:Label ID="Label4" runat="server" Text="PRECIO"></asp:Label>
            <asp:TextBox ID="PRECIO" runat="server"></asp:TextBox>
            </div> 
            <div> 
            <asp:Label ID="Label5" runat="server" Text="cantidad en kg"></asp:Label>
            <asp:TextBox ID="ID_CALIDAD" runat="server"></asp:TextBox>
            </div> 
            <div> 
            <asp:Button ID="agregar_produto" runat="server" Text="agregar" OnClick="agregar_produto_Click1" />
            <asp:Button ID="eliminar_producto" runat="server" Text="eliminar" OnClick="eliminar_producto_Click" />
            <asp:Button ID="modificar_producto" runat="server" Text="modificar" OnClick="modificar_producto_Click" />
            </div>
            <div>
            <asp:GridView ID="mostrar" runat="server" AutoGenerateColumns="true" OnSelectedIndexChanged="mostrar_SelectedIndexChanged"></asp:GridView>
            </div>
            
        </div>

        </div>
    </form>
</body>
</html>
