using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BombonesPP2022.Entidades;

namespace BombonesPP2022.Datos.Repositorios
{
    public class TiposDeChocolateRepositorio
    {
        private readonly ConexionBd conexionBd;

        public TiposDeChocolateRepositorio()
        {
            conexionBd = new ConexionBd();
        }

        public List<TipoDeChocolate> GetLista()
        {
            List<TipoDeChocolate> lista = new List<TipoDeChocolate>();
            try
            {
                using (var cn = conexionBd.AbrirConexion())
                {
                    var cadenaComando = "SELECT TipoChocolateId, Chocolate, RowVersion FROM TiposDeChocolate";
                    var comando = new SqlCommand(cadenaComando, cn);
                    using (var reader = comando.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var tipoDeChocolate = ConstruirTipoDeChocolate(reader);
                            lista.Add(tipoDeChocolate);
                        }

                    }

                    return lista;
                }
            }
            catch (Exception)
            {
                throw new Exception("Error al leer de la tabla de TiposDeChocolate");
            }
        }

        private TipoDeChocolate ConstruirTipoDeChocolate(SqlDataReader reader)
        {
            return new TipoDeChocolate()
            {
                TipoChocolateId = reader.GetInt32(0),
                Chocolate = reader.GetString(1),
                RowVersion = (byte[])reader[2]
            };
        }
    }
}
