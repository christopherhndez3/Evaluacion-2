using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace UtilsClient
{
    public class ClientSocket
    {
        private int port;
        private string server;
        private Socket cliente;
        private StreamReader reader;
        private StreamWriter writer;
        public ClientSocket(string server, int port)
        {
            this.server = server;
            this.port = port;
        }
        public bool Conectar()
        {
            try
            {
                this.cliente = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse(this.server), this.port);
                this.cliente.Connect(endPoint);
                Stream stream = new NetworkStream(this.cliente);
                this.reader = new StreamReader(stream);
                this.writer = new StreamWriter(stream);

                return true;
            }
            catch (SocketException ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public string Leer()
        {
            try
            {
                return this.reader.ReadLine();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public void Escribir(string mensaje)
        {
            try
            {
                this.writer.WriteLine(mensaje);
                this.writer.Flush();

            }
            catch (Exception ex)
            {

            }
        }


        public void Desconectar()
        {
            try
            {
                this.cliente.Close();
            }
            catch (Exception ex)
            {

            }
        }
    }
}
