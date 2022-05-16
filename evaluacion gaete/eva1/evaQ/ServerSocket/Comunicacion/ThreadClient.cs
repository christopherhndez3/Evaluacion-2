using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UtilsMedidor;
using UtilsMedidor.DAL;
using UtilsMedidor.DTO;
using UtilsServer;

namespace Server.Comunicacion
{
    class ThreadClient
    {
        private IMedidor medidoresDAL = MedidorFiles.GetInstance();
        private ClientCom clientCom;

        public ThreadClient(ClientCom clientCom)
        {
            this.clientCom = clientCom;
        }

        public void Ejecutar()
        {
            Comunicacion(clientCom);

            clientCom.Disconnect();
        }
        private void Comunicacion(ClientCom cliente)
        {

            bool salir = false;
            while (!salir)
            {
                cliente.Write("Ingrese Y para agregar medidro, para salir N");
                string mensaje = cliente.Read();

                if (mensaje != null)
                {
                    if (mensaje.ToLower() == "n")
                    {
                        cliente.Write("Saliendo...");
                        Thread.Sleep(2000);
                        Console.WriteLine("Cliente salio");
                        salir = true;
                    }
                    else if (mensaje.ToLower() == "y")
                    {
                        Console.WriteLine("-----Mostrando Menu-----");
                        Menu(cliente);
                    }
                    else
                    {
                        cliente.Write("No se ingreso lo esperado");
                    }

                }
                else
                {
                    cliente.Write("No se ingreso nada");
                }
            }
            cliente.Disconnect();
        }
        private bool Menu(ClientCom cliente)
        {
            bool continuar = true;
            string result;
            string resp = "1. Ingresar --- 2. Mostrar --- 3. Buscar --- 0. Salir";
            cliente.Write(resp);

            switch (cliente.Read().Trim())
            {
                case "1":
                    string[] array = DatosCliente(cliente);
                    lock (medidoresDAL)
                    {
                        MenuProgram.AddMedidor(Convert.ToInt32(array[0]), Convert.ToDouble(array[1]));
                    }                    
                    cliente.Write(MenuProgram.SearchMedidor(array[0]));
                    break;
                case "2":
                    string[] values = MenuProgram.ShowMedidores();
                    cliente.Write(String.Join("\n", values));
                    break;
                case "3":
                    cliente.Write("Ingrese el nombre del medidor");
                    result = MenuProgram.SearchMedidor(cliente.Read().Trim());
                    Console.WriteLine("Se ingreso el medidor: {0}", result);
                    cliente.Write(result);
                    break;
                case "0":
                    cliente.Write("Saliendo");
                    Thread.Sleep(5000);
                    return false;
                default:
                    cliente.Write("No se encontro la opcion elegida...");
                    Thread.Sleep(2000);
                    break;
            }
            return continuar;
        }
        private string[] DatosCliente(ClientCom client)
        {
            bool esValido;
            string[] array = new string[2];
            int medidorNro;
            double consumo;

            do
            {
                client.Write("Ingrese numero");
                esValido = Int32.TryParse(client.Read().Trim(), out medidorNro);
            } while (!esValido);

            do
            {
                client.Write("Ingrese consumo");
                esValido = double.TryParse(client.Read().Trim(), out consumo);
            } while (!esValido);

            array[0] = medidorNro.ToString();
            array[1] = consumo.ToString();
            return array;
        }
    }
}
