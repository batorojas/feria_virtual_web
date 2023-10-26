<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="feria_virtual_web._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">





           <section class="col-md-4" aria-labelledby="librariesTitle">
               <asp:TextBox ID="TextUsuario" runat="server"></asp:TextBox>
               <asp:TextBox ID="TextPassword" runat="server"></asp:TextBox>
               <asp:Button ID="btnlogin" runat="server" Text="login" OnClick="btnlogin_Click" />
            </section>



    </main>

</asp:Content>
