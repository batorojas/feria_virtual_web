using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telegram.Bot;
using System.Net.Http;

namespace feria_virtual_web
{
    public partial class Transportista : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGrid();
            }
        }

        private void BindGrid()
        {
            string connectionString = "Data Source=localhost:1521/xe;User Id=maipogrande;Password=123;";

            using (OracleConnection con = new OracleConnection(connectionString))
            {
                OracleCommand cmd = new OracleCommand("SELECT CS.ID_CABECERA_SUBASTA, CS.FECHA_LIMITE_ENTREGA, CS.FECHA_DESPACHO_REALIZADO, CO.NOMBRE_COMUNA, CS.ID_CABECERA_PV, PV.RUT_CLIENTE, ES.DESC_ESTADO FROM CABECERA_SUBASTA CS JOIN COMUNA CO ON CS.ID_COMUNA = CO.ID JOIN ESTADO_SUBASTA ES ON CS.ID_ESTADO_SUBASTA = ES.ID_ESTADO_SUBASTA JOIN CABECERA_PV PV ON CS.ID_CABECERA_PV = PV.ID_CABECERA_PV WHERE CS.FECHA_DESPACHO_REALIZADO IS NULL", con);
                con.Open();
                OracleDataAdapter sda = new OracleDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                GridViewSubasta.DataSource = dt;
                GridViewSubasta.DataBind();
            }
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "ActualizarFechaEntrega")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = GridViewSubasta.Rows[index];
                string idSubasta = row.Cells[0].Text;
                ActualizarFechaDespachoRealizado(idSubasta);
                BindGrid();
                string script = "alert('El proceso de venta ha sido actualizado y se notificó al cliente de la entrega.');";
                ClientScript.RegisterStartupScript(this.GetType(), "ActualizarFecha", script, true);
            }
        }

        private async Task ActualizarFechaDespachoRealizado(string idCabeceraSubasta)
        {
            string connectionString = "Data Source=localhost:1521/xe;User Id=maipogrande;Password=123;";
            using (OracleConnection con = new OracleConnection(connectionString))
            {
                string query = "UPDATE cabecera_subasta SET FECHA_DESPACHO_REALIZADO = SYSDATE WHERE ID_CABECERA_SUBASTA = :idCabeceraSubasta";
                using (OracleCommand cmd = new OracleCommand(query, con))
                {
                    cmd.Parameters.Add(":idCabeceraSubasta", OracleDbType.Varchar2).Value = idCabeceraSubasta;
                    con.Open();
                    cmd.ExecuteNonQuery(); 
                    await EnviarMensajeTelegram();
                }
            }
        }
        private async Task EnviarMensajeTelegram()
        {
            try
            {
                string botToken = "6762818327:AAHOiQlDwmDucqKgRacNDUBY7VRlHVNkYkA"; // Reemplaza con el token de tu bot
                long chatId = 902743181; // Reemplaza con el ID del chat al que deseas enviar el mensaje
                var botClient = new TelegramBotClient(botToken);
                string mensaje = "El transportista ha llegado con su pedido, asegúrese de revisar el pedido antes de realizar el pago.";
                await botClient.SendTextMessageAsync(chatId, mensaje);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ocurrió una excepción al enviar el mensaje de Telegram: {ex.Message}");
                // Puedes manejar el error de envío de mensaje de Telegram aquí
            }
        }
    }
}