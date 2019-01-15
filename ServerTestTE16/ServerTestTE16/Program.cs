using System;
using System.Net;
using System.Net.Sockets;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerTestTE16
{
    class Program
    {
        static TcpListener tcpListener;

        static void Main(string[] args)
        {

            Console.CancelKeyPress += new
                ConsoleCancelEventHandler(CancelKeyPress);

            //Tcp list

            IPAddress myIp = IPAddress.Parse("127.0.0.1");
            TcpListener tcpListener = new TcpListener(myIp, 8001);
            tcpListener.Start();
            Console.WriteLine("Vätar på anslutning...");

            //Lyssna på ansluning
            while (true) { 
            Socket socket = tcpListener.AcceptSocket();
            Console.WriteLine("Anslutning accepterad från " + socket.RemoteEndPoint);

            //Ta imot meddelande

            Byte[] bMessage = new Byte[256];
            int messageSize = socket.Receive(bMessage);
            //Console.WriteLine("Medalandet mottogs");

            //Konvertera

            string message = "";
            for(int i = 0; i < messageSize; i++)
            {
                message += Convert.ToChar(bMessage[i]);           
            }
            Console.WriteLine("Medelande: " + message);
                Byte[] bSend = Encoding.UTF8.GetBytes("Tack");
                socket.Send(bSend);
                Console.WriteLine("Svar skickat");
           


            //Stäng av anslutningen

            socket.Close();
            }
            tcpListener.Stop();

            Console.ReadKey();
        }
        static void CancelKeyPress(object sender, ConsoleCancelEventArgs e)
        {
            tcpListener.Stop();
            Console.WriteLine("Servern sänngdes av");
        }
    }
}
