using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BombonesPP2022.Entidades;

namespace BombonesPP2022.Datos.Repositorios
{
    public class BombonesRepositorio
    {
        private ConexionBd conexionBd;

        public BombonesRepositorio()
        {
            conexionBd = new ConexionBd();
        }
        public List<Bombon> GetLista()
        {
            List<Bombon> lista = new List<Bombon>();
            try
            {
                using (var cn = conexionBd.AbrirConexion())
                {
                    var cadenaComando = "SELECT BombonId, NombreBombon, TipoChocolateId, TipoNuezId, " +
                                        "TipoRellenoId,PrecioVenta, FabricaId, RowVersion FROM Bombones";
                    var comando = new SqlCommand(cadenaComando, cn);
                    using (var reader = comando.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Bombon bombon = ConstruirBombon(reader);
                            lista.Add(bombon);
                        }

                    }

                    SetTiposChocolate(lista, cn);
                    SetTiposNuez(lista, cn);
                    SetTiposRelleno(lista, cn);
                    SetFabricas(lista,cn);
                }

                return lista;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        private void SetTiposChocolate(List<Bombon> lista, SqlConnection cn)
        {
            foreach (var bombon in lista)
            {
                bombon.TipoDeChocolate = SetTipoChocolate(bombon.TipoChocolateId, cn);
            }
        }

        private TipoDeChocolate SetTipoChocolate(int id, SqlConnection cn)
        {
            TipoDeChocolate tipoDeChocolate = null;
            var cadenaComando = "SELECT TipoChocolateId, Chocolate, RowVersion FROM TiposDeChocolate " +
                                "WHERE TipoChocolateId=@id";
            var comando = new SqlCommand(cadenaComando, cn);
            comando.Parameters.AddWithValue("@id", id);
            using (var reader = comando.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    reader.Read();
                    tipoDeChocolate = ConstruirTipoChocolate(reader);
                }
            }

            return tipoDeChocolate;

        }

        private Bombon ConstruirBombon(SqlDataReader reader)
        {
            return new Bombon()
            {
                BombonId = reader.GetInt32(0),
                NombreBombon = reader.GetString(1),
                TipoChocolateId = reader.GetInt32(2),
                TipoNuezId = reader.GetInt32(3),
                TipoRellenoId = reader.GetInt32(4),
                PrecioVenta = reader.GetDouble(5),
                Stock=reader.GetInt32(6),
                FabricaId = reader.GetInt32(7),
                RowVersion = (byte[])reader[8]
            };
        }

        private void SetTiposNuez(List<Bombon> lista, SqlConnection cn)
        {
            foreach (var bombon in lista)
            {
                bombon.TipoDeNuez = SetTipoNuez(bombon.TipoNuezId, cn);
            }
        }

        private TipoDeNuez SetTipoNuez(int id, SqlConnection cn)
        {
            TipoDeNuez tipoDeNuez = null;
            var cadenaComando = "SELECT TipoNuezId, Nuez, RowVersion FROM TipoDeNuez " +
                                "WHERE TipoNuezId=@id";
            var comando = new SqlCommand(cadenaComando, cn);
            comando.Parameters.AddWithValue("@id", id);
            using (var reader = comando.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    reader.Read();
                    tipoDeNuez = ConstruirTipoNuez(reader);
                }
            }

            return tipoDeNuez;
        }

        private TipoDeNuez ConstruirTipoNuez(SqlDataReader reader)
        {
            return new TipoDeNuez()
            {
                TipoNuezId = reader.GetInt32(0),
                Nuez = reader.GetString(1),
                RowVersion = (byte[])reader[2]
            };
        }

        private TipoDeChocolate ConstruirTipoChocolate(SqlDataReader reader)
        {
            return new TipoDeChocolate()
            {
                TipoChocolateId = reader.GetInt32(0),
                Chocolate = reader.GetString(1),
                RowVersion = (byte[])reader[2]

            };
        }

        private void SetTiposRelleno(List<Bombon> lista, SqlConnection cn)
        {
            foreach (var bombon in lista)
            {
                bombon.TipoDeRelleno = SetTipoRelleno(bombon.TipoRellenoId, cn);
            }
        }

        private TipoDeRelleno SetTipoRelleno(int id, SqlConnection cn)
        {
            TipoDeRelleno tipoDeRelleno = null;
            var cadenaComando = "SELECT TipoRellenoId, Relleno, RowVersion FROM TiposDeRelleno " +
                                "WHERE TipoRellenoId=@id";
            var comando = new SqlCommand(cadenaComando, cn);
            comando.Parameters.AddWithValue("@id", id);
            using (var reader = comando.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    reader.Read();
                    tipoDeRelleno = ConstruirTipoRelleno(reader);
                }
            }

            return tipoDeRelleno;
        }

        private TipoDeRelleno ConstruirTipoRelleno(SqlDataReader reader)
        {
            return new TipoDeRelleno()
            {
                TipoRellenoId = reader.GetInt32(0),
                Relleno = reader.GetString(1),
                RowVersion = (byte[])reader[2]
            };
        }

        private void SetFabricas(List<Bombon> lista, SqlConnection cn)
        {
            foreach (var bombon in lista)
            {
                bombon.Fabrica = SetFabrica(bombon.FabricaId, cn);
            }
        }

        private Fabrica SetFabrica(int id, SqlConnection cn)
        {
            Fabrica fabrica = null;
            var cadenaComando = "SELECT FabricaId, NombreFabrica, Direccion, GerenteDeVentas, PaisId, RowVersion FROM TipoDeNuez " +
                                "WHERE FabricaId=@id";
            var comando = new SqlCommand(cadenaComando, cn);
            comando.Parameters.AddWithValue("@id", id);
            using (var reader = comando.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    reader.Read();
                    fabrica = ConstruirFabrica(reader);
                }
            }

            return fabrica;
        }

        private Fabrica ConstruirFabrica(SqlDataReader reader)
        {
            return new Fabrica()
            {
                FabricaId = reader.GetInt32(0),
                NombreFabrica = reader.GetString(1),
                Direccion = reader.GetString(2),
                GerenteDeVentas = reader.GetString(3),
                PaisId = reader.GetInt32(4),
                RowVersion = (byte[])reader[5]
            };
        }

        public int Agregar(Bombon bombon)
        {
            int registrosAfectados = 0;
            try
            {
                using (var cn = conexionBd.AbrirConexion())
                {
                    var cadenaComando = "INSERT INTO Bombones (NombreBombon, TipoChocolateId, TipoNuezId, TipoRellenoId, PrecioVenta, Stock, FabricaId) " +
                                    "VALUES(@nom, @tipoChoc, @tipoNuez, @tipoRell, @prec, @stock, @fabId)";

                    var comando = new SqlCommand(cadenaComando, cn);
                    comando.Parameters.AddWithValue("@nom", bombon.NombreBombon);
                    comando.Parameters.AddWithValue("@tipoChoc", bombon.TipoChocolateId);
                    comando.Parameters.AddWithValue("@tipoNuez", bombon.TipoNuezId);
                    comando.Parameters.AddWithValue("@tipoRell", bombon.TipoRellenoId);
                    comando.Parameters.AddWithValue("@prec", bombon.PrecioVenta);
                    comando.Parameters.AddWithValue("@stock", bombon.Stock);
                    comando.Parameters.AddWithValue("@fabId", bombon.FabricaId);


                    registrosAfectados = comando.ExecuteNonQuery();
                    if (registrosAfectados == 0)
                    {
                        throw new Exception("No se agregaron registros");
                    }
                    else
                    {
                        cadenaComando = "SELECT @@IDENTITY";
                        comando = new SqlCommand(cadenaComando, cn);
                        bombon.BombonId = (int)(decimal)comando.ExecuteScalar();

                        cadenaComando = "SELECT RowVersion FROM Bombones WHERE BombonId=@id";
                        comando = new SqlCommand(cadenaComando, cn);
                        comando.Parameters.AddWithValue("@id", bombon.BombonId);
                        bombon.RowVersion = (byte[])comando.ExecuteScalar();
                    }


                }

                return registrosAfectados;

            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public int Borrar(Bombon bombon)
        {
            int registrosAfectados = 0;
            try
            {
                using (var cn = conexionBd.AbrirConexion())
                {
                    var cadenaComando = "DELETE FROM Bombones WHERE BombonId=@id AND RowVersion=@r";
                    var comando = new SqlCommand(cadenaComando, cn);
                    comando.Parameters.AddWithValue("@id", bombon.BombonId);
                    comando.Parameters.AddWithValue("@r", bombon.RowVersion);
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

        public int Editar(Bombon bombon)
        {
            int registrosAfectados = 0;
            try
            {
                using (var cn = conexionBd.AbrirConexion())
                {
                    var cadenaComando =
                        "UPDATE Bombones SET NombreBombon=@nom, TipoChocolateId=@tipoChoc,TipoNuezId= @tipoNuez, " +
                        "TipoRellenoId=@tipoRell, PrecioVenta=@prec, Stock=@stock, FabricaId=@fabId " +
                        " WHERE BombonId=@id AND RowVersion=@r";
                    
                    var comando = new SqlCommand(cadenaComando, cn);

                    comando.Parameters.AddWithValue("@nom", bombon.NombreBombon);
                    comando.Parameters.AddWithValue("@tipoChoc", bombon.TipoChocolateId);
                    comando.Parameters.AddWithValue("@tipoNuez", bombon.TipoNuezId);
                    comando.Parameters.AddWithValue("@tipoRell", bombon.TipoRellenoId);
                    comando.Parameters.AddWithValue("@prec", bombon.PrecioVenta);
                    comando.Parameters.AddWithValue("@stock", bombon.Stock);
                    comando.Parameters.AddWithValue("@fabId", bombon.FabricaId);
                    
                    comando.Parameters.AddWithValue("@r", bombon.RowVersion);
                    registrosAfectados = comando.ExecuteNonQuery();
                    if (registrosAfectados > 0)
                    {
                        cadenaComando = "SELECT RowVersion FROM Bombones WHERE BombonId=@id";
                        comando = new SqlCommand(cadenaComando, cn);
                        comando.Parameters.AddWithValue("@id", bombon.BombonId);
                        bombon.RowVersion = (byte[])comando.ExecuteScalar();
                    }
                }

                return registrosAfectados;
            }
            catch (Exception e)
            {
                if (e.Message.Contains("IX_"))
                {
                    throw new Exception("Bombón repetido");
                }
                throw new Exception(e.Message);
            }
        }
    }
}
