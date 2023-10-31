using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Oracle.ManagedDataAccess.Client;
namespace feria_virtual_web
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        OracleConnection conexion = new OracleConnection("Data Source=localhost:1521/xe;User Id=maipogrande;Password=123;");
        protected void btnlogin_Click(object sender, EventArgs e)
        {
            conexion.Open();
            OracleCommand comando = new OracleCommand("SELECT * FROM USUARIO WHERE NOMBRE_USUARIO = :usuario AND  PASSWORD = :password",conexion);
            comando.Parameters.Add(":usuario",TextUsuario.Text);
            comando.Parameters.Add(":password",TextPassword.Text);
            OracleDataReader lector = comando.ExecuteReader();

            if (lector.Read()) 
            {
                if (lector["ID_PERFIL"].ToString() == "4") 
                {
                    Server.Transfer("Productor.aspx");
                    conexion.Close();  
                }
                if (lector["ID_PERFIL"].ToString() == "3")
                {
                    Server.Transfer("Transportista.aspx");
                    conexion.Close();
                }
                if (lector["ID_PERFIL"].ToString() == "2")
                {
                    Server.Transfer("Cliente.aspx");
                    conexion.Close();
                }
                if (lector["ID_PERFIL"].ToString() == "1")
                {
                    throw new InvalidOperationException("error de inicio de secion " +
                          "usuario incorrecto.");
                }
            }
            else
            {
                throw new InvalidOperationException("error de inicio de secion " +
                      "usuario incorrecto.");
            }
        }
    }
}