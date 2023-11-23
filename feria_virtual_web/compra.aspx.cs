using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using Oracle.ManagedDataAccess.Client;

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

                    string selectQuery = "SELECT DP.ID_DETALLE_PV, DP.ID_PRODUCTO, DP.CANTIDAD, DP.PRECIO_UNITARIO " +
                                         "FROM DETALLE_PV DP " +
                                         "INNER JOIN CABECERA_PV CP ON DP.ID_CABECERA_PV = CP.ID_CABECERA_PV " +
                                         "WHERE CP.RUT_CLIENTE = :Rut";

                    using (OracleCommand cmd = new OracleCommand(selectQuery, con))
                    {
                        cmd.Parameters.Add(new OracleParameter("Rut", rut));

                        OracleDataAdapter adapter = new OracleDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);

                        gvDetallesPV.DataSource = dt;
                        gvDetallesPV.DataBind();
                    }

                    con.Close();
                }
            }
            catch (Exception ex)
            {
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
                        string idDetallePV = row.Cells[0].Text;
                        string idProducto = row.Cells[1].Text;
                        string cantidad = row.Cells[2].Text;
                        string precioUnitario = row.Cells[3].Text;

                        // Realizar la multiplicación de cantidad por precio unitario
                        decimal total = Convert.ToDecimal(cantidad) * Convert.ToDecimal(precioUnitario);

                        // Formatear el total con dos decimales y punto como separador decimal
                        string montoFormateado = total.ToString("F2", CultureInfo.InvariantCulture);

                        // Obtener el token de acceso de PayPal
                        string accessToken = await ObtenerTokenAccesoPayPal().ConfigureAwait(false);

                        // Crear la orden de PayPal
                        string orderId = await CrearOrdenPayPal(accessToken, total).ConfigureAwait(false);

                        // Construir la URL de PayPal con el token de acceso y el ID de la orden
                        string urlPayPal = $"https://www.sandbox.paypal.com/checkoutnow?token={accessToken}&orderId={orderId}";

                        Response.Write($"URL de redirección: {urlPayPal}");

                        // Redirigir a la página de PayPal con el token de acceso y el ID de la orden
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

        private async Task<string> ObtenerTokenAccesoPayPal()
        {
            using (HttpClient client = new HttpClient())
            {
                string clientId = "AeSjmd1cOUZF164S1qM0n7Ul_ZAnX9K_oHyZd6fW_NLaWZvKX4vdWOIR5TVDvgEk4UJWvG62sj8JCFgt";
                string clientSecret = "EC913X7vIu-2icepQF_Lg9nn64GI9IRVchzW2s0gT8XnosB8mQOaPnukHfmmuWDNoB3VL4Mr1xDDiUTu";

                string credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{clientId}:{clientSecret}"));

                var requestData = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("grant_type", "client_credentials")
                };

                client.DefaultRequestHeaders.Add("Authorization", $"Basic {credentials}");

                HttpResponseMessage response = await client.PostAsync("https://api-m.sandbox.paypal.com/v1/oauth2/token", new FormUrlEncodedContent(requestData));

                if (response.IsSuccessStatusCode)
                {
                    var responseData = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<PayPalTokenResponse>(responseData)?.access_token;
                }
                else
                {
                    string errorMessage = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Error al obtener el token de acceso de PayPal: {errorMessage}");
                }
            }
        }

        private async Task<string> CrearOrdenPayPal(string accessToken, decimal total)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {accessToken}");

                var requestData = new
                {
                    intent = "CAPTURE",
                    purchase_units = new[]
                    {
                        new
                        {
                            amount = new
                            {
                                
                                currency_code = "USD",
                                value = total.ToString("F2", CultureInfo.InvariantCulture) // Formatear el total con dos decimales
                            }
                        }
                    }
                };

                string jsonRequest = JsonConvert.SerializeObject(requestData);

                HttpResponseMessage response = await client.PostAsync("https://api-m.sandbox.paypal.com/v2/checkout/orders", new StringContent(jsonRequest, Encoding.UTF8, "application/json"));

                if (response.IsSuccessStatusCode)
                {
                    var responseData = await response.Content.ReadAsStringAsync();
                    dynamic orderData = JsonConvert.DeserializeObject(responseData);
                    return orderData.id;
                }
                else
                {
                    string errorMessage = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Error al crear la orden de PayPal: {errorMessage}");
                }
            }
        }
    }

    public class PayPalTokenResponse
    {
        public string access_token { get; set; }
        public string token_type { get; set; }
        public int expires_in { get; set; }
    }
}