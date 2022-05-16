
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UtilsMedidor.DAL;
using UtilsMedidor.DTO;

namespace UtilsMedidor
{

    public class MenuProgram
    {
        private static IMedidor medidorDAL = MedidorFiles.GetInstance();
        public static string[] ShowMedidores()
        {            
            List<Medidor> medidor = medidorDAL.ObtenerMedidores();
            string[] values = new string[medidor.Count()];
            for (int i=0; i < medidor.Count(); i++)
            {
                Medidor current = medidor[i]; // objeto actual
                values[i]="Medidor"+i+"|"+ current.MedidorNro + "|"+ current.Fecha + "|"+ current.Consumo;
            }
            return values;

        }
        public static string SearchMedidor(string nombre)
        {
            string values = "";
            List<Medidor> filter = medidorDAL.FiltrarMedidores(Convert.ToInt32(nombre.Trim()));
            filter.ForEach(me => values = "|"+ me.MedidorNro+" | "+ me.Fecha + " | "+me.Consumo);
            return values;
        }
        public static string AddMedidor(int medidorNro, double consumo) //hay que ver si se puede imprimir en otro contexto o por ejemplo tener una conexion para imprimir desde el serivdor al 
        {            
            string fecha;            
            Console.WriteLine("__Cliente Agregando_Medidor__");                      
            fecha = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
            Medidor me = new Medidor()
            { MedidorNro = medidorNro, Consumo = consumo, Fecha=Convert.ToDateTime(fecha)};            
            medidorDAL.AgregarMedidor(me);
            medidorDAL.FiltrarMedidores(me.MedidorNro);
            string resultado = SearchMedidor(Convert.ToString(medidorNro));
            return resultado;
        }
    }
}
