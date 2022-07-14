using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BombonesPP2022.Datos.Repositorios;
using BombonesPP2022.Entidades;

namespace BombonesPP2022.Servicios.Servicios
{
    public class TiposChocolateServicios
    {
        private readonly TiposDeChocolateRepositorio repositorio;

        public TiposChocolateServicios()
        {
            repositorio = new TiposDeChocolateRepositorio();

        }

        public List<TipoDeChocolate> GetLista()
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
    }
}
