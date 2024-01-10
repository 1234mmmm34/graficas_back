﻿using API_Archivo.Clases;
using CardManagement;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

namespace API_Archivo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Usuario_loteController : ControllerBase
    {

        [HttpPost]
        [Route("Agregar_inquilino")]

        public bool Agregar_inquilino([FromBody] Usuario_lote request)
        {

            bool fraccionamiento_agregado = false;
            using (MySqlConnection conexion = new MySqlConnection(Global.cadena_conexion))
            {
                int rowsaffected = 0;
                MySqlCommand comando = new MySqlCommand("insert into usuario_lote (id_usuario, id_lote, id_renta, id_fraccionamiento, codigo_acceso, intercomunicador, nombre) VALUES (@id_usuario, @id_lote, @id_renta, @id_fraccionamiento, @codigo_acceso, @intercomunicador, @nombre)", conexion);

                //Nombre_fraccionamiento=@Nombre_fraccionamiento, Direccion=@Direccion, Coordenadas=@Coordenadas, id_administrador=@id_administrador, id_tesorero=@id_tesorero)

                comando.Parameters.Add("@id_usuario", MySqlDbType.Int32).Value = request.id_usuario;
                comando.Parameters.Add("@id_lote", MySqlDbType.Int32).Value = request.id_usuario;
                comando.Parameters.Add("@id_renta", MySqlDbType.Int32).Value = request.id_renta;
                comando.Parameters.Add("@id_fraccionamiento", MySqlDbType.Int32).Value = request.id_fraccionamiento;
                comando.Parameters.Add("@codigo_acceso", MySqlDbType.VarChar).Value = request.codigo_acceso;
                comando.Parameters.Add("@intercomunicador", MySqlDbType.VarChar).Value = request.intercomunicador;
                comando.Parameters.Add("@nombre", MySqlDbType.VarChar).Value = request.nombre;
                /*
                DateTime now = DateTime.Now;
                DateTime Dateproximo_pago = DateTime.Now.AddDays(90);
                string fechaProximoPago = Dateproximo_pago.ToString("yyyy-MM-ddTHH:mm:ss");
                string fechaActual = now.ToString("yyyy-MM-ddTHH:mm:ss");
                */
                try
                {
                    conexion.Open();
                    rowsaffected = comando.ExecuteNonQuery();

                    if (rowsaffected >= 1)
                    {
                        AddDevice.Login("admin", "Repara123", "5551", "187.216.118.73");
                      //  AddDevice.InsertUser(request.id_usuario.ToString(), request.nombre, fechaActual, fechaProximoPago);
                        AddDevice.InsertCardUser(request.id_usuario.ToString());
                        fraccionamiento_agregado = true;
                    }

                }
                catch (MySqlException ex)
                {
                    //MessageBox.Show(ex.ToString());
                }
                finally
                {

                }

                return fraccionamiento_agregado;
            }
        }




        [HttpGet]
        [Route("Consultar_inquilino")]

        public List<Usuario_lote> consultar_inquilino(int id_lote)
        {

            List<Usuario_lote> Persona = new List<Usuario_lote>();

            using (MySqlConnection conexion = new MySqlConnection(Global.cadena_conexion))
            {

                MySqlCommand comando = new MySqlCommand("SELECT * FROM usuario_lote WHERE id_lote=@id_lote", conexion);

                comando.Parameters.Add("@id_lote", MySqlDbType.Int32).Value = id_lote;



                try
                {

                    conexion.Open();

                    MySqlDataReader reader = comando.ExecuteReader();

                    while (reader.Read())
                    {
                        Persona.Add(new Usuario_lote()
                        {
                            id_usuario_lote = reader.GetInt32(0),
                          //  id_lote = reader.GetInt32(1),
                         //   id_renta = reader.GetInt32(2),
                         //   id_fraccionamiento = reader.GetInt32(3),
                            codigo_acceso = reader.GetString(4),
                            intercomunicador = reader.GetString(5),
                            id_usuario = reader.GetInt32(6),
                            nombre = reader.GetString(7),
                        });
                        // MessageBox.Show();
                    }


                }
                catch (MySqlException ex)
                {

                }
                finally
                {
                    conexion.Close();
                }

                return Persona;
            }

        }



        [HttpDelete]
        [Route("Eliminar_inquilino")]

        public bool Eliminar_inquilino(int id_lote)
        {
            bool Propiedad_eliminada = false;


            using (MySqlConnection conexion = new MySqlConnection(Global.cadena_conexion))
            {
                int rowsaffected = 0;
                MySqlCommand comando = new MySqlCommand("DELETE FROM usuario_lote WHERE id_usuario_lote = @id_lote", conexion);

                //@id_usuario, @Tipo_deuda,@Nombre_deuda, @Monto, @Ruta_comprobante, @Estado

                comando.Parameters.Add("@id_lote", MySqlDbType.Int32).Value = id_lote;
                //  comando.Parameters.Add("@id_fraccionamiento", MySqlDbType.Int32).Value = id_fraccionamiento;


                try
                {
                    conexion.Open();
                    rowsaffected = comando.ExecuteNonQuery();

                    if (rowsaffected >= 1)
                    {
                        Propiedad_eliminada = true;
                    }

                }
                catch (MySqlException ex)
                {
                    //MessageBox.Show(ex.ToString());
                }
                finally
                {
                    conexion.Close();
                }
                return Propiedad_eliminada;

            }


        }

    }
}
