<%@ Page Title="Contacto" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Contact.aspx.cs" Inherits="feria_virtual_web.Contact" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <head>
      	<title>Contacto</title>
        <meta charset="utf-8">
        <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">
    
    	<link href='https://fonts.googleapis.com/css?family=Roboto:400,100,300,700' rel='stylesheet' type='text/css'>
    
    	<link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css">
    	
    	<link rel="stylesheet" href="css/style.css">
    
    	</head>
    	<body>
    	<section class="ftco-section">
    		<div class="container">
    			<div class="row justify-content-center">
    				<div class="col-md-6 text-center mb-5">
    					<h2 class="heading-section">Contacto</h2>
    				</div>
    			</div>
    			<div class="row justify-content-center">
    				<div class="col-md-12">
    					<div class="wrapper">
    						<div class="row mb-5">
    							<div class="col-md-3">
    								<div class="dbox w-100 text-center">
    			        		<div class="icon d-flex align-items-center justify-content-center">
    			        			<span class="fa fa-map-marker"></span>
    			        		</div>
    			        		<div class="text">
    				            <p><span>Dirección:</span> San Pablo Antiguo, Km. 10, Ruta 68 - Rancagua, Santiago de Chile</p>
    				          </div>
    			          </div>
    							</div>
    							<div class="col-md-3">
    								<div class="dbox w-100 text-center">
    			        		<div class="icon d-flex align-items-center justify-content-center">
    			        			<span class="fa fa-phone"></span>
    			        		</div>
    			        		<div class="text">
    				            <p><span>Teléfono:</span> <a href="tel://1234567920">+56986743325</a></p>
    				          </div>
    			          </div>
    							</div>
    							<div class="col-md-3">
    								<div class="dbox w-100 text-center">
    			        		<div class="icon d-flex align-items-center justify-content-center">
    			        			<span class="fa fa-paper-plane"></span>
    			        		</div>
    			        		<div class="text">
    				            <p><span>Correo:</span> <a href="mailto:info@yoursite.com">contacto@maipogrande.cl</a></p>
    				          </div>
    			          </div>
    							</div>
    							<div class="col-md-3">
    								<div class="dbox w-100 text-center">
    			        		<div class="icon d-flex align-items-center justify-content-center">
    			        			<span class="fa fa-globe"></span>
    			        		</div>
    			        		<div class="text">
    				            <p><span>Sitio:</span> <a href="#">www.maipogrande.cl</a></p>
    				          </div>
    			          </div>
    							</div>
    						</div>
    						<div class="row no-gutters">
    							<div class="col-md-7">
    								<div class="contact-wrap w-100 p-md-5 p-4">
    									<h3 class="mb-4">Contáctese con nosotros</h3>
    									<div id="form-message-warning" class="mb-4"></div> 
    				      		<div id="form-message-success" class="mb-4">
    				            Su mensaje ha sido enviado, ¡muchas gracias!
    				      		</div>
    									<form method="POST" id="contactForm" name="contactForm" class="contactForm">
    										<div class="row">
    											<div class="col-md-6">
    												<div class="form-group">
    													<label class="label" for="name">Nombre completo</label>
    													<input type="text" class="form-control" name="name" id="name" placeholder="Nombre completo">
    												</div>
    											</div>
    											<div class="col-md-6"> 
    												<div class="form-group">
    													<label class="label" for="email">Correo electrónico</label>
    													<input type="email" class="form-control" name="email" id="email" placeholder="Correo">
    												</div>
    											</div>
    											<div class="col-md-12">
    												<div class="form-group">
    													<label class="label" for="subject">Asunto</label>
    													<input type="text" class="form-control" name="subject" id="subject" placeholder="Asunto">
    												</div>
    											</div>
    											<div class="col-md-12">
    												<div class="form-group">
    													<label class="label" for="#">Mensaje</label>
    													<textarea name="message" class="form-control" id="message" cols="30" rows="4" placeholder="Mensaje"></textarea>
    												</div>
    											</div>
    											<div class="col-md-12">
    												<div class="form-group">
    													<input type="submit" value="Enviar" class="btn btn-primary">
    													<div class="submitting"></div>
    												</div>
    											</div>
    										</div>
    									</form>
    								</div>
    							</div>
    							<div class="col-md-5 d-flex align-items-stretch">
    								<div class="info-wrap w-100 p-5 img" style="background-image: url(images/img.jpg);">
    			          </div>
    							</div>
    						</div>
    					</div>
    				</div>
    			</div>
    		</div>
    	</section>
    
    	<script src="js/jquery.min.js"></script>
      <script src="js/popper.js"></script>
      <script src="js/bootstrap.min.js"></script>
      <script src="js/jquery.validate.min.js"></script>
      <script src="js/main.js"></script>
    
    	</body>

</asp:Content>
