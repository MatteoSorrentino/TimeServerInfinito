 using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace TimeServer
{
    class Program
    {
        static void Main(string[] args)
        {
            Socket listenerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            IPAddress ipAddr = IPAddress.Any;

            IPEndPoint ipEp = new IPEndPoint(ipAddr, 23000);

            listenerSocket.Bind(ipEp);

            listenerSocket.Listen(5);
            Console.WriteLine("Server in ascolto");
            Console.WriteLine("Connessione del client");

            while (true)
            {


                try
                {
                    Socket client = listenerSocket.Accept();

                    Console.WriteLine($"Client IP: {client.RemoteEndPoint.ToString()}");

                    byte[] sendBuff = new byte[128];

                    byte[] recvBuff = new byte[128];

                    int recvBytes = 0;

                    string recvString = "";
                    string sendString = "";

                    while (true)
                    {
                        recvBytes = client.Receive(recvBuff);
                        recvString = Encoding.ASCII.GetString(recvBuff, 0, recvBytes);

                        Console.WriteLine($"Client: {recvString}");

                        if (recvString.ToUpper().Trim() == "QUIT")
                        {
                            break;
                        }
                        else if (recvString.ToUpper().Trim() == "CIAO")
                        {
                            sendString = "Ciao";
                        }
                        else if (recvString.ToUpper().Trim() == "COME VA?")
                        {
                            sendString = "Bene!";
                        }
                        else if (recvString.ToUpper().Trim() == "HELP")
                        {
                            sendString = "I comandi sono: CIAO, COME VA?, TIME, DATE, HELP, QUIT.";
                        }
                        else if (recvString.ToUpper().Trim() == "TIME")
                        {
                            sendString = DateTime.Now.ToShortTimeString();
                        }
                        else if (recvString.ToUpper().Trim() == "DATE")
                        {
                            sendString = DateTime.Now.ToShortDateString();
                        }
                        else
                        {
                            sendString = "Non ho capito";
                        }

                        sendBuff = Encoding.ASCII.GetBytes(sendString);
                        client.Send(sendBuff);

                        Array.Clear(sendBuff, 0, sendBuff.Length);
                        Array.Clear(recvBuff, 0, recvBuff.Length);
                        recvString = "";
                        sendString = "";
                        recvBytes = 0;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                Console.WriteLine("Programma terminato. Arrivederci");
                Console.WriteLine("Server in ascolto");
                Console.WriteLine("Connessione del client");

            }
            
        }
    }
}
