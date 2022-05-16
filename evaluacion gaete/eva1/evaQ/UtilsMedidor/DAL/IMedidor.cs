using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UtilsMedidor.DTO;

namespace UtilsMedidor.DAL
{
    public interface IMedidor
    {
        void AgregarMedidor(Medidor persona);
        List<Medidor> ObtenerMedidores();
        List<Medidor> FiltrarMedidores(int medidor);
    }
}
