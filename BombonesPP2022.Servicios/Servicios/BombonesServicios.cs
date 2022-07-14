using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BombonesPP2022.Datos.Repositorios;
using BombonesPP2022.Entidades;

namespace BombonesPP2022.Servicios.Servicios
{
    public class BombonesServicios
    {
        private readonly BombonesRepositorio repositorio;

        public BombonesServicios()
        {
            repositorio = new BombonesRepositorio();
        }

        public List<Bombon> GetLista()
        {
            try
            {
                return repositorio.GetLista();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public int Agregar(Bombon bombon)
        {
            try
            {
                return repositorio.Agregar(bombon);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public int Borrar(Bombon bombon)
        {
            try
            {
                return repositorio.Borrar(bombon);
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public int Editar(Bombon bombon)
        {
            try
            {
                return repositorio.Editar(bombon);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;

            }
        }
    }
}
