using Server.Comunicacion;
using System;
using System.Configuration;
using System.Net.Sockets;
using System.Threading;
using UtilsMedidor;
using UtilsServer;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {

            /*int port = Convert.ToInt32(ConfigurationManager.AppSettings["port"]);
            Console.WriteLine("Iniciando servidor en puerto {0}", port);

            ServerSocket server = new ServerSocket(port);
            if (server.StartConnection())
            {

                Console.WriteLine("Servidor Iniciado");
                while (true)
                {
                    Console.WriteLine("Esperando Cliente");
                    Socket socketClient = server.GetClient();
                    ClientCom client = new ClientCom(socketClient);
                    client.Write("Hola Mundo cliente, dime tu nombre???");
                    string respuesta = client.Read();
                    Console.WriteLine("Bienvenido {0}", respuesta);
                    Comunicacion(client);

                    //MenuProgram.MainMenu();

                    client.Disconnect();
                }
            }
            else
            {
                Console.WriteLine("Puerto {0} en uso, intente mas tarde...", port);
            }*/

            ThreadServer hebra = new ThreadServer();
            Thread t = new Thread(new ThreadStart(hebra.Ejecutar));
            t.Start();            
            while (Menu()) ;

        }
        
        static bool Menu()
        {
            bool continuar=true;
            string result;
            string resp = "1. Ingresar \n2. Mostrar \n3. Buscar \n0. Salir";
            Console.WriteLine(resp);
            
            switch (Console.ReadLine().Trim())
            {
                case "1":
                    string[] array = DatosCliente();
                    result = MenuProgram.AddMedidor(Convert.ToInt32(array[0]), Convert.ToDouble(array[1]));
                    Console.WriteLine(result);
                    break;
                case "2":
                    string[] values = MenuProgram.ShowMedidores();
                    Console.WriteLine(String.Join("\n", values));
                    break;
                case "3":
                    Console.WriteLine("Ingrese el nombre del medidor");
                    result= MenuProgram.SearchMedidor(Console.ReadLine().Trim());
                    Console.WriteLine("Se ingreso el medidor: {0}",result);                    
                    break;
                case "0":
                    Console.WriteLine("Saliendo");
                    Thread.Sleep(5000);
                    continuar= false;
                    break;
                default:
                    Console.WriteLine("No se encontro la opcion elegida...");
                    Thread.Sleep(2000);
                    break;
            }
            return continuar;
        }
        private static string[] DatosCliente()
        {
            bool esValido;
            string[] array = new string[2];
            int medidorNro;
            double consumo;

            do
            {
                Console.WriteLine("Ingrese numero");
                esValido = Int32.TryParse(Console.ReadLine().Trim(), out medidorNro);
            } while (!esValido);

            do
            {
                Console.WriteLine("Ingrese consumo");
                esValido = double.TryParse(Console.ReadLine().Trim(), out consumo);
            } while (!esValido);

            array[0] = medidorNro.ToString();
            array[1] = consumo.ToString();
            return array;
        }
    }
}
