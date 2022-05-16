using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UtilsMedidor;
using UtilsMedidor.DAL;
using UtilsServer;

namespace Server.Comunicacion
{
    class ThreadServer
    {

        private IMedidor mensajesDAL = MedidorFiles.GetInstance();
        public void Ejecutar()
        {
            int port = Convert.ToInt32(ConfigurationManager.AppSettings["port"]);
            Console.WriteLine("Iniciando servidor en puerto {0}", port);

            ServerSocket serverSocket = new ServerSocket(port);
            if (serverSocket.StartConnection())
            {

                Console.WriteLine("Servidor Iniciado");
                while (true)
                {
                    Console.WriteLine("Servidor: Esperando Cliente...");
                    Socket cliente = serverSocket.GetClient();
                    Console.WriteLine("Servidor: Cliente recibido");

                    //esto estaba en generar comunicacion
                    ClientCom clienteCom = new ClientCom(cliente);
                    ThreadClient clienteThread = new ThreadClient(clienteCom);
                    Thread t = new Thread(new ThreadStart(clienteThread.Ejecutar));
                    t.IsBackground = true;
                    t.Start();
                }
            }
            else
            {
                Console.WriteLine("Puerto {0} fallo, intente mas tarde...", port);
            }
        }
        
    }
}
