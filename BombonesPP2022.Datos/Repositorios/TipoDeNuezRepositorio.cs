using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BombonesPP2022.Entidades;

namespace BombonesPP2022.Datos.Repositorios
{
    public class TipoDeNuezRepositorio
    {
        private readonly ConexionBd conexionBd;

        public TipoDeNuezRepositorio()
        {
            conexionBd = new ConexionBd();
        }

        public List<TipoDeNuez> GetLista()
        {
            List<TipoDeNuez> lista = new List<TipoDeNuez>();
            try
            {
                using (var cn = conexionBd.AbrirConexion())
                {
                    var cadenaComando = "SELECT TipoNuezId, Nuez, RowVersion FROM TipoDeNuez";
                    var comando = new SqlCommand(cadenaComando, cn);
                    using (var reader = comando.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var tipoDeNuez = ConstruirTipoDeNuez(reader);
                            lista.Add(tipoDeNuez);
                        }

                    }

                    return lista;
                }
            }
            catch (Exception)
            {
                throw new Exception("Error al leer de la tabla de TipoDeNuez");
            }
        }

        private TipoDeNuez ConstruirTipoDeNuez(SqlDataReader reader)
        {
            return new TipoDeNuez()
            {
                TipoNuezId = reader.GetInt32(0),
                Nuez = reader.GetString(1),
                RowVersion = (byte[])reader[2]
            };
        }

        public int Agregar(TipoDeNuez tipoDeNuez)
        {
            int registrosAfectados = 0;
            try
            {
                using (var cn = conexionBd.AbrirConexion())
                {
                    var cadenaComando = "INSERT INTO TipoDeNuez (Nuez) VALUES (@nom)";
                    var comando = new SqlCommand(cadenaComando, cn);
                    comando.Parameters.AddWithValue("@nom", tipoDeNuez.Nuez);
                    registrosAfectados = comando.ExecuteNonQuery();


                    if (registrosAfectados > 0)
                    {
                        cadenaComando = "SELECT @@IDENTITY";
                        comando = new SqlCommand(cadenaComando, cn);
                        tipoDeNuez.TipoNuezId = (int)(decimal)comando.ExecuteScalar();
                        cadenaComando = "SELECT RowVersion FROM TipoDeNuez WHERE TipoNuezId=@id";
                        comando = new SqlCommand(cadenaComando, cn);
                        comando.Parameters.AddWithValue("@id", tipoDeNuez.TipoNuezId);
                        tipoDeNuez.RowVersion = (byte[])comando.ExecuteScalar();
                    }
                }

                return registrosAfectados;
            }
            catch (Exception e)
            {
                if (e.Message.Contains("IX_Nuez"))
                {
                    throw new Exception("Nuez repetida");
                }

                throw new Exception(e.Message);
            }
        }

        public int Borrar(TipoDeNuez tipoDeNuez)
        {
            int registrosAfectados = 0;
            try
            {
                using (var cn = conexionBd.AbrirConexion())
                {
                    var cadenaComando = "DELETE FROM TipoDeNuez WHERE TipoNuezId=@id AND RowVersion=@r";
                    var comando = new SqlCommand(cadenaComando, cn);
                    comando.Parameters.AddWithValue("@id", tipoDeNuez.TipoNuezId);
                    comando.Parameters.AddWithValue("@r", tipoDeNuez.RowVersion);
                    registrosAfectados = comando.ExecuteNonQuery();
                }

                return registrosAfectados;
            }
            catch (Exception e)
            {
                if (e.Message.Contains("REFERENCE"))
                {
                    throw new Exception("Registro relacionado... baja denegada");
                }
                throw new Exception(e.Message);
            }
        }

        public int Editar(TipoDeNuez tipoDeNuez)
        {
            int registrosAfectados = 0;
            try
            {
                using (var cn = conexionBd.AbrirConexion())
                {
                    var cadenaComando = "UPDATE TipoDeNuez SET Nuez=@nom, WHERE TipoNuezId=@id AND RowVersion=@r";
                    var comando = new SqlCommand(cadenaComando, cn);
                    comando.Parameters.AddWithValue("@nom", tipoDeNuez.Nuez);
                    comando.Parameters.AddWithValue("@id", tipoDeNuez.TipoNuezId);
                    comando.Parameters.AddWithValue("@r", tipoDeNuez.RowVersion);
                    registrosAfectados = comando.ExecuteNonQuery();


                    if (registrosAfectados > 0)
                    {
                        cadenaComando = "SELECT RowVersion FROM TipoDeNuez WHERE TipoNuezId=@id";
                        comando = new SqlCommand(cadenaComando, cn);
                        comando.Parameters.AddWithValue("@id", tipoDeNuez.TipoNuezId);
                        tipoDeNuez.RowVersion = (byte[])comando.ExecuteScalar();
                    }
                }

                return registrosAfectados;
            }
            catch (Exception e)
            {
                if (e.Message.Contains("IX_Nuez"))
                {
                    throw new Exception("Nuez repetida");
                }
                throw new Exception(e.Message);
            }
        }
    }
}
