<%@ Page Title="Iniciar sesión" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="feria_virtual_web._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server" xmlns:asp="http://www.w3.org/1999/html">
    <head>
        <title>Iniciar sesión</title>
        <style>
        body{
        font-family: 'DM-Sans', sans-serif;
        }
        
            .login {
                min-height: 100vh;
            }

            .bg-image {
                background-image: url('images/img2.jpg');
                background-size: cover;
                background-position: center;
            }

            .login-heading {
                font-weight: 300;
            }

            .btn-login {
                font-size: 0.9rem;
                letter-spacing: 0.05rem;
                padding: 0.75rem 1rem;
            }
        </style>
    </head>
    <div class="container-fluid ps-md-0">
        <div class="row g-0">
            <div class="d-none d-md-flex col-md-4 col-lg-6 bg-image"></div>
            <div class="col-md-8 col-lg-6">
                <div class="login d-flex align-items-center py-5">
                    <div class="container">
                        <div class="row">
                            <div class="col-md-9 col-lg-8 mx-auto">
                                <h3 class="login-heading mb-4">Bienvenido</h3>
                                <asp:TextBox
                                    CssClass="form-control mb-3"
                                    TextMode="SingleLine"
                                    PlaceHolder="Usuario"
                                    ID="TextUsuario"
                                    runat="server">
                                </asp:TextBox>
                                <asp:TextBox
                                    CssClass="form-control mb-3"
                                    TextMode="Password"
                                    PlaceHolder="Contraseña"
                                    ID="TextPassword"
                                    runat="server">
                                </asp:TextBox>
                                <asp:CheckBox ID="chkPersistente" runat="server" Text="Recordarme" />
                                <div class="text-center">
                                    <asp:Button ID="btnlogin"
                                                runat="server"
                                                Text="Iniciar sesión" 
                                                OnClick="btnlogin_Click" 
                                                ButtonType="Submit"
                                                CssClass="btn btn-lg btn-primary btn-login text-uppercase fw-bold mb-2" />
                                </div>
                                <asp:Label ID="lblMensaje" runat="server" Text=""></asp:Label>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
