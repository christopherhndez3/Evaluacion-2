using System;
using System.Collections.Generic;
using System.IO;
using UtilsMedidor.DTO;

namespace UtilsMedidor.DAL
{
    //  Singleton es un patrón de diseño del tipo creacional cuyo propósito es garantizar la
    //  existencia de una sola instancia de una clase.Además el acceso a esa única instancia tiene que ser global.

    //1. El contructor tiene que ser private
    //2. Debe poseer un atributo del mismo tipo de la clase y estatico
    //3. Tener un metodo GetIntance, que devuelve una referencia al atributo
    public class MedidorFiles : IMedidor
    {
        private MedidorFiles()
        {

        }
        private static MedidorFiles instance;
        public static IMedidor GetInstance()
        {
            if (instance==null)
            {
                instance = new MedidorFiles(); //aqui si es null se le retorna el constructor vacio que actuarar como mediador para la clase a la interfaz
            }
            return instance;
        }
        private static string fileName = "medidores.txt";
        private static string ruta = Directory.GetCurrentDirectory() + "/" + fileName;
        public void AgregarMedidor(Medidor medidor)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(ruta, true))
                {
                    string text = 
                        medidor.MedidorNro + "|" 
                        + medidor.Fecha + "|"
                        + medidor.Consumo + "|";

                    writer.WriteLine(text);
                    writer.Flush();
                }
            }
            catch (Exception error)
            {
                Console.WriteLine("Hubo un error agregando el medidor" + error.Message);
                Console.WriteLine("Presione una tecla para continuar...");
                Console.ReadKey();
            }
        }

        

        public List<Medidor> FiltrarMedidores(int medidor)
        {            
            return ObtenerMedidores().FindAll(me => me.MedidorNro == medidor);            
        }

        public List<Medidor> ObtenerMedidores()
        {
            List<Medidor> medidores = new List<Medidor>();
            using (StreamReader reader = new StreamReader(ruta))
            {
                string text;
                do
                {
                    text = reader.ReadLine();
                    if (text != null)
                    {
                        string[] textArray = text.Trim().Split('|');
                        int medidorNro = Convert.ToInt32(textArray[0]);
                        DateTime fecha = Convert.ToDateTime(textArray[1]);
                        double consumo = Convert.ToDouble(textArray[2]);
                        Medidor me = new Medidor()
                        {
                            MedidorNro = medidorNro,
                            Fecha = fecha,
                            Consumo = consumo,
                        };                        
                        medidores.Add(me);
                        
                    }
                } while (text != null);
            }
            return medidores;
        }        
    }
}
