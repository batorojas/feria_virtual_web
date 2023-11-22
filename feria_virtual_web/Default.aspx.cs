using System;
using System.Web;
using System.Web.Security;
using Oracle.ManagedDataAccess.Client;

namespace feria_virtual_web
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnlogin_Click(object sender, EventArgs e)
        {
            using (OracleConnection conexion = new OracleConnection("Data Source=localhost:1521/xe;User Id=maipogrande;Password=123;"))
            {
                conexion.Open();

                OracleCommand comando = new OracleCommand("SELECT * FROM USUARIO WHERE NOMBRE_USUARIO = :usuario AND PASSWORD = :password", conexion);
                comando.Parameters.Add(":usuario", TextUsuario.Text);
                comando.Parameters.Add(":password", TextPassword.Text);

                OracleDataReader lector = comando.ExecuteReader();

                if (lector.Read())
                {
                    string username = lector["NOMBRE_USUARIO"].ToString();
                    bool persistente = chkPersistente.Checked;

                    // Crear un ticket de autenticación
                    FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(
                        1,
                        username,
                        DateTime.Now,
                        DateTime.Now.AddMinutes(30), // Puedes ajustar el tiempo de expiración
                        persistente,
                        string.Empty,
                        FormsAuthentication.FormsCookiePath);

                    // Encriptar el ticket
                    string encryptedTicket = FormsAuthentication.Encrypt(ticket);

                    // Crear una cookie de autenticación
                    HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                    Response.Cookies.Add(cookie);

                    if (lector["ID_PERFIL"].ToString() == "4")
                    {
                        Response.Redirect("Productor.aspx");
                    }
                    else if (lector["ID_PERFIL"].ToString() == "3")
                    {
                        Response.Redirect("Transportista.aspx");
                    }
                    else if (lector["ID_PERFIL"].ToString() == "2")
                    {
                        Response.Redirect("Venta.aspx");
                    }
                    else
                    {
                        // Otros perfiles o manejo adicional si es necesario
                    }
                }
                else
                {
                    // Código para manejar credenciales inválidas
                    lblMensaje.Text = "Credenciales inválidas";
                }
            }
        }
    }
}
