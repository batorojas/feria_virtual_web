using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Oracle.ManagedDataAccess.Client;
using System.Drawing;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;



namespace feria_virtual_web
{
    public partial class compra : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {


        }

        protected void btnConsultar_Click(object sender, EventArgs e)
        {
            try
            {
                string rut = txtRut.Text.Trim();

                string connectionString = "Data Source=localhost:1521/xe;User Id=maipogrande;Password=123;";

                using (OracleConnection con = new OracleConnection(connectionString))
                {
                    con.Open();

                    // Consulta SQL modificada según tu requerimiento
                    string selectQuery = "SELECT DP.ID_DETALLE_PV, DP.ID_PRODUCTO, DP.CANTIDAD, DP.PRECIO_UNITARIO " +
                                         "FROM DETALLE_PV DP " +
                                         "INNER JOIN CABECERA_PV CP ON DP.ID_CABECERA_PV = CP.ID_CABECERA_PV " +
                                         "WHERE CP.RUT_CLIENTE = :Rut";

                    using (OracleCommand cmd = new OracleCommand(selectQuery, con))
                    {
                        cmd.Parameters.Add(new OracleParameter("Rut", rut));

                        // Crear un adaptador y un conjunto de datos para almacenar los resultados de la consulta
                        OracleDataAdapter adapter = new OracleDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);

                        // Enlazar los datos al GridView
                        gvDetallesPV.DataSource = dt;
                        gvDetallesPV.DataBind();
                    }

                    con.Close();
                }
            }
            catch (Exception ex)
            {
                // Manejar errores aquí (mostrar un mensaje de error, registrar el error, etc.)
                Response.Write("Error rut no encontrado: " + ex.Message);
            }
        }



        protected async void gvDetallesPV_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Pagar")
            {
                try
                {
                    // Obtener el índice de la fila
                    int rowIndex = Convert.ToInt32(e.CommandArgument);

                    // Verificar si rowIndex está dentro de los límites
                    if (rowIndex >= 0 && rowIndex < gvDetallesPV.Rows.Count)
                    {
                        // Obtener los datos de la fila
                        GridViewRow row = gvDetallesPV.Rows[rowIndex];

                        // Obtener los valores de las celdas directamente del GridView
                        string idDetallePV = row.Cells[0].Text; // Ajusta el índice según la posición de la columna en tu GridView
                        string idProducto = row.Cells[1].Text;  // Ajusta el índice según la posición de la columna en tu GridView
                        string cantidad = row.Cells[2].Text;     // Ajusta el índice según la posición de la columna en tu GridView
                        string precioUnitario = row.Cells[3].Text; // Ajusta el índice según la posición de la columna en tu GridView

                        // Realizar la multiplicación de cantidad por precio unitario
                        decimal total = Convert.ToDecimal(cantidad) * Convert.ToDecimal(precioUnitario);

                        // Obtener el token de acceso de PayPal
                        string accessToken = await ObtenerTokenAccesoPayPal().ConfigureAwait(false);

                        // Construir la URL de PayPal con el token de acceso y el monto total
                        string urlPayPal = $"https://www.sandbox.paypal.com/checkoutnow?token={accessToken}&amount={total}";
                        Response.Write($"URL de redirección: {urlPayPal}");
                        Response.Redirect(urlPayPal);

                        // Redirigir a la página de PayPal con el token de acceso
                        Response.Redirect(urlPayPal);
                    }
                }
                catch (Exception ex)
                {
                    // Manejar otros errores aquí (mostrar un mensaje de error, registrar el error, etc.)
                    Response.Write("Error al procesar el pago: " + ex.Message);
                }
            }
        }

        // Método para obtener el token de acceso de PayPal
        private async Task<string> ObtenerTokenAccesoPayPal()
        {
            using (HttpClient client = new HttpClient())
            {
                // Configurar las credenciales de la aplicación PayPal (reemplaza con las tuyas)
                string clientId = "AeSjmd1cOUZF164S1qM0n7Ul_ZAnX9K_oHyZd6fW_NLaWZvKX4vdWOIR5TVDvgEk4UJWvG62sj8JCFgt";
                string clientSecret = "EC913X7vIu-2icepQF_Lg9nn64GI9IRVchzW2s0gT8XnosB8mQOaPnukHfmmuWDNoB3VL4Mr1xDDiUTu";

                // Codificar las credenciales en base64
                string credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{clientId}:{clientSecret}"));

                // Configurar los datos de la solicitud
                var requestData = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("grant_type", "client_credentials")
                };

                // Configurar las cabeceras de la solicitud
                client.DefaultRequestHeaders.Add("Authorization", $"Basic {credentials}");

                // Realizar la solicitud para obtener el token de acceso
                HttpResponseMessage response = await client.PostAsync("https://api.sandbox.paypal.com/v1/oauth2/token", new FormUrlEncodedContent(requestData));

                // Leer y deserializar la respuesta
                if (response.IsSuccessStatusCode)
                {
                    var responseData = await response.Content.ReadAsStringAsync();
                    // Devolver el token de acceso
                    return responseData;
                }
                else
                {
                    // Manejar el error de la solicitud
                    string errorMessage = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Error al obtener el token de acceso de PayPal: {errorMessage}");
                }
            }
        }




    }
}