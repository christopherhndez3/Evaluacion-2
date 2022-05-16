using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UtilsMedidor.DTO;

namespace UtilsMedidor.DAL
{
    public class MedidorObjects : IMedidor
    {
        private static List<Medidor> medidores = new List<Medidor>();

        public void AgregarMedidor(Medidor medidor)
        {
            medidores.Add(medidor);
        }        

        public List<Medidor> FiltrarMedidores(int medidor)
        {
            return medidores.FindAll(me=>me.MedidorNro==medidor);
        }

        public List<Medidor> ObtenerMedidores()
        {
            return medidores;
        }        
    }
}
