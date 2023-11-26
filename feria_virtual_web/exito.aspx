<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="exito.aspx.cs" Inherits="feria_virtual_web.exito" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Pago exitoso - Maipo Grande</title>
            <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-T3c6CoIi6uLrA9TneNEoa7RxnatzjcDSCmG1MXxSR1GAsXEV/Dwwykc2MPK8M2HN" crossorigin="anonymous">

        <script type="text/javascript">
        // Función para mostrar el mensaje y redirigir después de un tiempo
        function mostrarMensajeYRedirigir() {

            // Esperar 5 segundos (5000 milisegundos) y luego redirigir
            setTimeout(function () {
                // Cambiar 'nuevaPagina.aspx' a la URL de la página a la que deseas redirigir
                window.location.href = 'Cliente.aspx';
            }, 5000); // 5000 milisegundos = 5 segundos
        }

        // Llamar a la función cuando la página haya cargado
        window.onload = mostrarMensajeYRedirigir;
        </script>
</head>

<body class="d-flex flex-column h-100">
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
              </div>
              </nav>
    <form id="form1" runat="server" class="container-fluid">
        <main class="flex-shrink-0">
        <div class="container">
            <h1 class="mt-5">¡Felicidades, tu pago ha sido realizado con éxito!</h1>
            <p class="lead">En unos segundos serás redirigido al historial de pedidos.</p>
            <p class="lead">
                <a href="/Venta.aspx" class="btn btn-primary btn-lg">Realizar otro pedido</a>
            </div>
        </main>
    </form>
</body>
</html>
