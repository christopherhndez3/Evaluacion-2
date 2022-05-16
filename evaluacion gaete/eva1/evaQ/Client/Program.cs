using System;
using System.Collections;
using System.Configuration;
using UtilsClient;


// Integrantes Omar Barria y Christopher Hernandez seccion 
namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            int puerto = Convert.ToInt32(ConfigurationManager.AppSettings["port"]);
            string servidor = ConfigurationManager.AppSettings["server"];
            ClientSocket cliente = new ClientSocket(servidor, puerto);
            
            try
            {
                if (cliente.Conectar())
                {
                    Comunicacion(cliente);
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Error en la conexion");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("error "+e.Message);
            }
            Console.ReadKey();
        }
        static void Comunicacion(ClientSocket cliente)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("--Conectado--");
            Console.ForegroundColor = ConsoleColor.Yellow;
            bool salir = false;
            do
            {                
                ResponseServer(cliente);
                //Console.WriteLine(cliente.Leer());
                string mensaje = Console.ReadLine().Trim();
                if (mensaje != null)
                {
                    cliente.Escribir(mensaje);
                    //Console.WriteLine(cliente.Leer());
                    ResponseServer(cliente);
                    if (mensaje.ToLower() == "n")
                    {
                        ResponseServer(cliente);
                        cliente.Desconectar();
                        salir = true;
                    }
                    else if(mensaje.ToLower()=="y")
                    {
                        ResponseServer(cliente);
                        string response = Console.ReadLine();
                        cliente.Escribir(response);
                        
                    }
                }
                else
                {
                    Console.WriteLine("No se ingreso nada");
                }
            } while (!salir);
        }
        private static void ResponseServer(ClientSocket client)
        {
            string response;
            do
            {
                response=client.Leer();
                Console.WriteLine(response);
            } while (response == null);
        }    
    }
}