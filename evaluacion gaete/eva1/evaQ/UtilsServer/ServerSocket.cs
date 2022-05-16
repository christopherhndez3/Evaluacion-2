using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace UtilsServer

{
    public class ServerSocket
    {
        private int port;
        private Socket server;
        public ServerSocket(int puerto)
        {
            this.port = puerto;
        }
        public bool StartConnection()
        {
            try
            {
                this.server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);                
                this.server.Bind(new IPEndPoint(IPAddress.Any, this.port));                
                this.server.Listen(10);
                return true;
            }
            catch (SocketException ex)
            {
                return false;
            }
        }

        public Socket GetClient()
        {
            return this.server.Accept();
        }
        public void Exit()
        {
            try
            {
                this.server.Close();
            }
            catch (Exception ex)
            {

            }

        }
    }
}


