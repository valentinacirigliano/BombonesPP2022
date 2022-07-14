using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BombonesPP2022.Datos.Repositorios;
using BombonesPP2022.Entidades;

namespace BombonesPP2022.Servicios.Servicios
{
    public class TiposDeNuezServicios
    {
        private readonly TipoDeNuezRepositorio repositorio;

        public TiposDeNuezServicios()
        {
            repositorio = new TipoDeNuezRepositorio();

        }

        public List<TipoDeNuez> GetLista()
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

        public int Agregar(TipoDeNuez tipoDeNuez)
        {
            try
            {
                return repositorio.Agregar(tipoDeNuez);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public int Borrar(TipoDeNuez tipoDeNuez)
        {
            try
            {
                return repositorio.Borrar(tipoDeNuez);
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public int Editar(TipoDeNuez tipoDeNuez)
        {
            try
            {
                return repositorio.Editar(tipoDeNuez);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
