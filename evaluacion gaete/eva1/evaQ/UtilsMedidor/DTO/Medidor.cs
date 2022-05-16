using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UtilsMedidor.DTO
{
    public class Medidor
    {
        private int medidorNro;
        private DateTime fecha;
        private double consumo;
        public int MedidorNro { get => medidorNro; set => medidorNro = value; }
        public DateTime Fecha { get => fecha; set => fecha = value; }
        public double Consumo { get => consumo; set => consumo = value; }

        
    }

}
