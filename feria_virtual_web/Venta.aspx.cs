using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Oracle.ManagedDataAccess.Client;
using System.Data;
using System.Drawing;
using System.Threading.Tasks;
using Telegram.Bot;
using System.Net.Http;



namespace feria_virtual_web
{
    public partial class Venta : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            BindGrid();
            ListarProductos();
        }

    




        private bool UsuarioExiste(string rut, OracleConnection con)
        {
            // Consultar la base de datos para verificar si el usuario existe
            string selectQuery = "SELECT COUNT(*) FROM CLIENTE WHERE RUT = :Rut";

            using (OracleCommand cmd = new OracleCommand(selectQuery, con))
            {
                cmd.Parameters.Add(new OracleParameter("Rut", rut));

                int count = Convert.ToInt32(cmd.ExecuteScalar());
                return count > 0;
            }
        }


        // Obtener el IdTipoVenta desde la tabla cliente basado en el RUT
        private int ObtenerIdTipoVenta(string rutCliente, OracleConnection con)
        {
            // Crear la consulta SELECT para obtener el IdTipoVenta desde la tabla cliente
            string query = "SELECT ID_TIPO FROM CLIENTE WHERE RUT_CLIENTE = :RutCliente";

            // Crear el comando Oracle
            using (OracleCommand cmd = new OracleCommand(query, con))
            {
                // Agregar el parámetro para el RUT
                cmd.Parameters.Add(new OracleParameter("RutCliente", OracleDbType.Varchar2)).Value = rutCliente;

                // Ejecutar la consulta SELECT
                object result = cmd.ExecuteScalar();

                // Verificar si se obtuvo un resultado válido
                if (result != null && result != DBNull.Value)
                {
                    // Convertir el resultado a entero
                    return Convert.ToInt32(result);
                }

                // Si no se obtiene un resultado, puedes devolver un valor predeterminado o lanzar una excepción según tus necesidades
                return 0; // Cambia esto según tu lógica
            }
        }

        protected void gvDetallesPV_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            // Lógica del evento
        }

        private void BindGrid()
        {
            string connectionString = "Data Source=localhost:1521/xe;User Id=maipogrande;Password=123;";
            using (OracleConnection con = new OracleConnection(connectionString))
            {
                OracleCommand cmd = new OracleCommand("SELECT * FROM PRODUCTO", con);
                con.Open();
                OracleDataAdapter sda = new OracleDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                gvDetallesPV.DataSource = dt;
                gvDetallesPV.DataBind();
            }
        }

        protected void gvDetallesPV_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGrid();
        }

        protected void btnAgregarPedido_Click(object sender, EventArgs e)
        {
            try
            {
                string connectionString = "Data Source=localhost:1521/xe;User Id=maipogrande;Password=123;";

                using (OracleConnection con = new OracleConnection(connectionString))
                {
                    con.Open();

                    // Obtener el RUT del cliente ingresado en el campo txtRutCliente
                    string rutCliente = txtRutCliente.Text.Trim();

                    // Verificar si el usuario existe
                    if (UsuarioExiste(rutCliente, con))
                    {
                        // Obtener la observación del campo txtObservacion
                        string observacion = txtObservacion.Text.Trim();

                        // Consultar la base de datos
                        string insertQuery = "INSERT INTO CABECERA_PV (\"ID_CABECERA_PV\", \"FECHA_EMISION\",\"OBS_PV\",\"RUT_CLIENTE\",\"ESTADO_PV\",\"EMPRESA_TRANS\", \"ID_TIPO_VENTA\") " +
                                             "VALUES (CABECERA_PV_SEQ.NEXTVAL, SYSDATE, :Observacion, :RutCliente, 1, 1, 1)";

                        using (OracleCommand cmd = new OracleCommand(insertQuery, con))
                        {
                            cmd.Parameters.Add(new OracleParameter("Observacion", observacion));
                            cmd.Parameters.Add(new OracleParameter("RutCliente", rutCliente));

                            // Ejecutar la inserción
                            int rowsAffected = cmd.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                // Puedes mostrar un mensaje de éxito o realizar otras acciones si es necesario
                                string script = "alert('El pedido se ha ingresado de manera exitosa');";
                                ClientScript.RegisterStartupScript(this.GetType(), "PedidoIngresado", script, true);

                                // Ahora, llamar a la función para agregar el pedido
                                AgregarPedido(con);
                            }
                            else
                            {
                                string script = "alert('El pedido no se ha ingresado correctamente, inténtelo más tarde.');";
                                ClientScript.RegisterStartupScript(this.GetType(), "PedidoNoIngresado", script, true);
                            }
                        }
                    }
                    else
                    {
                        string script = "alert('El usuario no existe, revise si el RUT registrado es correcto o comuníquese con el administrador.');";
                        ClientScript.RegisterStartupScript(this.GetType(), "UsuarioNoExiste", script, true);
                    }

                    con.Close();
                }
            }
            catch (Exception ex)
            {
                // Manejar errores aquí (mostrar un mensaje de error, registrar el error, etc.)
                Response.Write("Error al generar la solicitud: " + ex.Message);
            }
        }


        private void ListarProductos()
        {
            string connectionString = "Data Source=localhost:1521/xe;User Id=maipogrande;Password=123;";
            string query = "SELECT * FROM PRODUCTO";
            using (OracleConnection connection = new OracleConnection(connectionString))
            {
                using (OracleCommand command = new OracleCommand(query, connection))
                {
                    connection.Open();

                    using (OracleDataReader reader = command.ExecuteReader())
                    {
                        // Limpiar DropDownList antes de agregar nuevas categorías
                        ddlProducto.Items.Clear();

                        // Agregar cada categoría al DropDownList
                        while (reader.Read())
                        {
                            // Obtener el valor y texto de la columna NOMBRE_PRODUCTO
                            string valorProducto = reader["NOMBRE_PRODUCTO"].ToString();
                    
                            // Agregar el ListItem al DropDownList con el valor y texto
                            ddlProducto.Items.Add(new ListItem(valorProducto, valorProducto));
                        }
                    }
                }
            }
        }



        private async Task AgregarPedido(OracleConnection con)
        {
            try
            {
                // Obtener valores de TextBox y Label fuera del DataGrid
                int indiceSeleccionado = ddlProducto.SelectedIndex + 1;
                string cantidad = cantidadde.Text;
                // Insertar en la base de datos (código de inserción depende de tu estructura de tablas)
                string insertQuery = "INSERT INTO DETALLE_PV (ID_DETALLE_PV, ID_PRODUCTO, CANTIDAD, PRECIO_UNITARIO, ID_CABECERA_PV) " +
                                     $"VALUES (DETALLE_PV_SEQ.NEXTVAL, :IdProducto, :Cantidad, " +
                                     $"(SELECT p.precio FROM producto p WHERE p.id_producto = :IdProducto), " +
                                     "(SELECT MAX(id_cabecera_pv) FROM CABECERA_PV))";

                using (OracleCommand cmd = new OracleCommand(insertQuery, con))
                {
                    cmd.Parameters.Add(new OracleParameter("IdProducto", indiceSeleccionado));
                    cmd.Parameters.Add(new OracleParameter("Cantidad", cantidad));
                    // Ejecutar la consulta INSERT
                    cmd.ExecuteNonQuery();
                    await EnviarMensajeTelegram();
                }
            }
            catch (Exception ex)
            {
                Response.Write("Error al agregar el pedido: " + ex.Message);
            }
        }
        
        private async Task EnviarMensajeTelegram()
        {
            try
            {
                string botToken = "6762818327:AAHOiQlDwmDucqKgRacNDUBY7VRlHVNkYkA"; // Reemplaza con el token de tu bot
                long chatId = 902743181; // Reemplaza con el ID del chat al que deseas enviar el mensaje
                var botClient = new TelegramBotClient(botToken);
                string mensaje = "¡Gracias por elegirnos! Su pedido ha sido ingresado y está siendo procesado, le notificaremos los detalles a la brevedad.";
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