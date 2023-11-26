using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TLSchema;
using System.Globalization;
using System.Net.Http;
using System.Text;
using Oracle.ManagedDataAccess.Client;



namespace feria_virtual_web
{
    public partial class Cliente : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                string rut = txtRut.Text.Trim();

                // Verificar si el RUT ingresado tiene compras completadas
                bool tieneCompras = VerificarComprasCompletadas(rut);

                if (tieneCompras)
                {
                    // Mostrar las compras en el GridView
                    MostrarCompras(rut);
                    lblMensaje.Visible = false;
                }
                else
                {
                    // Mostrar un mensaje indicando que no hay compras completadas
                    gvDetallesPV.DataSource = null;
                    gvDetallesPV.DataBind();
                    lblMensaje.Text = "El usuario no posee compras completadas.";
                    lblMensaje.Visible = true;
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = $"Error: {ex.Message}";
                lblMensaje.Visible = true;
            }
        }

        private bool VerificarComprasCompletadas(string rut)
        {
            string connectionString = "Data Source=localhost:1521/xe;User Id=maipogrande;Password=123;";

            using (OracleConnection con = new OracleConnection(connectionString))
            {
                con.Open();

                string selectQuery = "SELECT COUNT(*) " +
                                     "FROM DETALLE_PV DP " +
                                     "INNER JOIN CABECERA_PV CP ON DP.ID_CABECERA_PV = CP.ID_CABECERA_PV " +
                                     "WHERE CP.ESTADO_PV = 2 " +
                                     "AND CP.RUT_CLIENTE = :Rut";

                using (OracleCommand cmd = new OracleCommand(selectQuery, con))
                {
                    cmd.Parameters.Add(new OracleParameter("Rut", rut));

                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    return count > 0;
                }
            }
        }

        private void MostrarCompras(string rut)
        {
            string connectionString = "Data Source=localhost:1521/xe;User Id=maipogrande;Password=123;";

            using (OracleConnection con = new OracleConnection(connectionString))
            {
                con.Open();

                string selectQuery = "SELECT DP.ID_DETALLE_PV, DP.ID_PRODUCTO, P.NOMBRE_PRODUCTO, DP.CANTIDAD, DP.PRECIO_UNITARIO, DP.CANTIDAD * DP.PRECIO_UNITARIO AS TOTAL \n                                     FROM DETALLE_PV DP \n                                     INNER JOIN CABECERA_PV CP ON DP.ID_CABECERA_PV = CP.ID_CABECERA_PV\n                                     JOIN PRODUCTO P ON DP.ID_PRODUCTO = P.ID_PRODUCTO\n                                     WHERE CP.ESTADO_PV = 2 " +
                                     "AND CP.RUT_CLIENTE = :Rut";

                using (OracleCommand cmd = new OracleCommand(selectQuery, con))
                {
                    cmd.Parameters.Add(new OracleParameter("Rut", rut));

                    OracleDataAdapter adapter = new OracleDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    gvDetallesPV.DataSource = dt;
                    gvDetallesPV.DataBind();
                }
            }
        }
    }
}