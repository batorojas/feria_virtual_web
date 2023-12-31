﻿using System;
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
using PayPal;
using PayPal.Api;
using Telegram.Bot;

namespace feria_virtual_web
{
    public partial class compra : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                // Configuración de PayPal para entorno de sandbox
                var apiContext = new APIContext(new OAuthTokenCredential(
                    "AeSjmd1cOUZF164S1qM0n7Ul_ZAnX9K_oHyZd6fW_NLaWZvKX4vdWOIR5TVDvgEk4UJWvG62sj8JCFgt",
                    "EC913X7vIu-2icepQF_Lg9nn64GI9IRVchzW2s0gT8XnosB8mQOaPnukHfmmuWDNoB3VL4Mr1xDDiUTu"
                ).GetAccessToken());

                Session["APIContext"] = apiContext;
            }
            catch (IdentityException ex)
            {
                // Manejar excepción específica de IdentityException
                Response.Write($"Error al obtener el token de acceso de PayPal: {ex.Message}");
                // Imprimir detalles detallados de la excepción para obtener más información
                Response.Write($"Detalles de la excepción: {ex.ToString()}");
            }
            catch (Exception ex)
            {
                // Manejar otras excepciones
                Response.Write($"Error general: {ex.Message}");
            }
        }

        protected void btnConsultar_Click(object sender, EventArgs e)
        {
            try
            {
                var apiContext = new APIContext(new OAuthTokenCredential(
                    "AeSjmd1cOUZF164S1qM0n7Ul_ZAnX9K_oHyZd6fW_NLaWZvKX4vdWOIR5TVDvgEk4UJWvG62sj8JCFgt",
                    "EC913X7vIu-2icepQF_Lg9nn64GI9IRVchzW2s0gT8XnosB8mQOaPnukHfmmuWDNoB3VL4Mr1xDDiUTu"
                ).GetAccessToken());
                string rut = txtRut.Text.Trim();

                string connectionString = "Data Source=localhost:1521/xe;User Id=maipogrande;Password=123;";

                using (OracleConnection con = new OracleConnection(connectionString))
                {
                    con.Open();

                    string selectQuery = @"SELECT DP.ID_DETALLE_PV, DP.ID_PRODUCTO, P.NOMBRE_PRODUCTO, DP.CANTIDAD, DP.PRECIO_UNITARIO, DP.CANTIDAD * DP.PRECIO_UNITARIO AS TOTAL
FROM DETALLE_PV DP
         INNER JOIN CABECERA_PV CP ON DP.ID_CABECERA_PV = CP.ID_CABECERA_PV
JOIN PRODUCTO P ON DP.ID_PRODUCTO = P.ID_PRODUCTO
WHERE CP.ESTADO_PV = 1
                                    AND CP.RUT_CLIENTE = :Rut";

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

        protected void gvDetallesPV_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            var apiContext = new APIContext(new OAuthTokenCredential(
                "AeSjmd1cOUZF164S1qM0n7Ul_ZAnX9K_oHyZd6fW_NLaWZvKX4vdWOIR5TVDvgEk4UJWvG62sj8JCFgt",
                "EC913X7vIu-2icepQF_Lg9nn64GI9IRVchzW2s0gT8XnosB8mQOaPnukHfmmuWDNoB3VL4Mr1xDDiUTu"
            ).GetAccessToken());

            try
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);

                if (rowIndex >= 0 && rowIndex < gvDetallesPV.Rows.Count)
                {
                    GridViewRow row = gvDetallesPV.Rows[rowIndex];

                    string idDetallePV = row.Cells[0].Text;
                    string idProducto = row.Cells[1].Text;
                    string nombreProducto = row.Cells[2].Text;
                    string cantidad = row.Cells[3].Text;
                    string precioUnitario = row.Cells[4].Text;
                    string totalPago = row.Cells[5].Text;

                    decimal total = Convert.ToDecimal(cantidad, CultureInfo.InvariantCulture) * Convert.ToDecimal(precioUnitario, CultureInfo.InvariantCulture);

                    string montoFormateado = total.ToString("F2", CultureInfo.InvariantCulture);

                    var payment = new Payment
                    {
                        intent = "sale",
                        payer = new Payer { payment_method = "paypal" },
                        transactions = new List<Transaction>
                {
                    new Transaction
                    {
                        amount = new Amount
                        {
                            currency = "USD",
                            total = montoFormateado
                        },
                        description = GetProductNameById(idProducto)
                    }
                },
                        redirect_urls = new RedirectUrls
                        {
                            return_url = $"{Request.Url.Scheme}://{Request.Url.Authority}/exito.aspx?success=true",
                            cancel_url = $"{Request.Url.Scheme}://{Request.Url.Authority}/fallo.aspx?success=false"
                        }
                    };

                    var createdPayment = payment.Create(apiContext);

                    var approvalUrl = createdPayment.GetApprovalUrl();

                    // Verificar telegram bot
                    if (Request.Url.ToString().StartsWith($"{Request.Url.Scheme}://{Request.Url.Authority}/exito.aspx?success=true"))
                    {
                        /*// Crear el cliente de Telegram.Bot con tu token de bot
                        var botClient = new TelegramBotClient("TU_TOKEN_DE_BOT_AQUI");

                        // Mensaje que deseas enviar
                        string mensaje = $"¡Pago exitoso! Producto: {GetProductNameById(idProducto)}, Monto: {montoFormateado}";

                        // ID de chat al que deseas enviar el mensaje (puede ser un grupo o un chat privado)
                        long chatId = TU_CHAT_ID_AQUI;

                        // Enviar el mensaje a través de la API de Telegram
                        botClient.SendTextMessageAsync(chatId, mensaje);  */
                    }

                    Response.Redirect(approvalUrl);
                }
            }
            catch (PayPal.PaymentsException ex)
            {
                Response.Write($"Error al procesar el pago: {ex.Message}");
                Response.Write($"Error details: {ex.Details}");
            }
            catch (Exception ex)
            {
                Response.Write($"Error general: {ex.Message}");
                Response.Write($"Detalles de la excepción: {ex.ToString()}");
            }
        }
        private string GetProductNameById(string idProducto)
        {
            string connectionString = "Data Source=localhost:1521/xe;User Id=maipogrande;Password=123;";
            string nombreProducto = string.Empty;

            using (OracleConnection con = new OracleConnection(connectionString))
            {
                con.Open();

                string selectProductNameQuery = "SELECT NOMBRE_PRODUCTO FROM PRODUCTO WHERE ID_PRODUCTO = :IdProducto";

                using (OracleCommand cmd = new OracleCommand(selectProductNameQuery, con))
                {
                    cmd.Parameters.Add(new OracleParameter("IdProducto", idProducto));
                    object result = cmd.ExecuteScalar();

                    if (result != null)
                    {
                        nombreProducto = result.ToString();
                    }
                }

                con.Close();
            }

            return nombreProducto;
        }
    }
}