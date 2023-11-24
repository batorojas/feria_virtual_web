<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="exito.aspx.cs" Inherits="feria_virtual_web.exito" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>

        <script type="text/javascript">
        // Función para mostrar el mensaje y redirigir después de un tiempo
        function mostrarMensajeYRedirigir() {
            // Mostrar el mensaje (puedes personalizar este mensaje)
            alert("compra realizada");

            // Esperar 7 segundos (7000 milisegundos) y luego redirigir
            setTimeout(function () {
                // Cambiar 'nuevaPagina.aspx' a la URL de la página a la que deseas redirigir
                window.location.href = 'compra.aspx';
            }, 7000); // 7000 milisegundos = 7 segundos
        }

        // Llamar a la función cuando la página haya cargado
        window.onload = mostrarMensajeYRedirigir;
        </script>

</head>

<body>
    <form id="form1" runat="server">
        <div>

             
         <h1> SU COMPRA FUE GENERADA CON EXITO</h1>
        <h1> volviendo a la pagina de pagos</h1>
        </div>
    </form>
</body>
</html>
