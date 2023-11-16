<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="VentaInterna.aspx.cs" Inherits="feria_virtual_web.VentaInterna" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
        </div>
            <div>
            <asp:GridView ID="GridViewVentaInterna" runat="server" AutoGenerateColumns="False" OnRowCommand="GridViewVenta" OnSelectedIndexChanged="GridViewVentaInterna_SelectedIndexChanged">
            <Columns>
                    <asp:BoundField DataField="ID_PRODUCTO" HeaderText="ID_PRODUCTO" />
                    <asp:BoundField DataField="NOMBRE_PRODUCTO" HeaderText="NOMBRE_PRODUCTO" />
                    <asp:BoundField DataField="PRECIO" HeaderText="PRECIO" />
                    <asp:BoundField DataField="stock" HeaderText="stock" />
                    <asp:ButtonField ButtonType="Button" Text="seleccionar" CommandName="seleccion" HeaderText="seleccion " />
               </Columns>
               </asp:GridView>
           
        </div>
    </form>
</body>
</html>
