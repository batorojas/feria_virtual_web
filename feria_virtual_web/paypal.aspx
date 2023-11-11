
<%@ Page EnableEventValidation="false" Language="C#" AutoEventWireup="true" CodeBehind="paypal.aspx.cs" Inherits="feria_virtual_web.paypal" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
                <div class="row">
        <div class="col-sm-3">
            <img src="https://www.lavanguardia.com/files/content_image_desktop_filter/uploads/2018/06/08/5e997b9cbcf52.jpeg" width="200" height="250" />
        </div>
        <div class="col-sm-9">
            <h3 class="text-primary">manzanas rojas de alta calidad</h3>
            <p>
                manzanas rojas
            </p>
            <asp:Button ID="Pagar" runat="server" OnClick="Pagar_Click" Text="pagar" />


            </div>

 
    </form>
</body>
</html>
