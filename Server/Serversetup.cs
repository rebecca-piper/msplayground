using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Threading;
using System.Text.Json.Serialization;
using System.Text.Json;
using Newtonsoft.Json;

using System.Net.Sockets;
using Newtonsoft.Json.Linq;

namespace Server
{


    internal class ServerSetup
    {
       public static ClientVars client;
       
        private static Socket clientSocket;
        private static List<Socket> sockets;
        private static Socket socket;
        public static ClientVars Client { get => client; set => client = value; }
        
        public static Socket ClientSocket { get => clientSocket; set => clientSocket = value; }
        public static List<Socket> Sockets { get => sockets; set => sockets = value; }
        public static Socket Socket { get => socket; set => socket = value; }

        double currentPot;
        public void ExecuteServer()
        {
            // Establish the local endpoint 
            // for the socket. Dns.GetHostName
            // returns the name of the host 
            // running the application.
            IPHostEntry ipHost = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress ipAddr = ipHost.AddressList[0];
            IPEndPoint localEndPoint = new IPEndPoint(ipAddr, 11111);

            

            try
            {
                // Creation TCP/IP Socket using 
                // Socket Class Constructor
                clientSocket = new Socket(ipAddr.AddressFamily,
                            SocketType.Stream, ProtocolType.Tcp);
                // Using Bind() method we associate a
                // network address to the Server Socket
                // All client that will connect to this 
                // Server Socket must know this network
                // Address
                clientSocket.Bind(localEndPoint);

                // Using Listen() method we create 
                // the Client list that will want
                // to connect to Server
                clientSocket.Listen(10);

                while (true)
                {

                    Console.WriteLine("Waiting connection ... ");

                    // Suspend while waiting for
                    // incoming connection Using 
                    // Accept() method the server 
                    // will accept connection of client
                    sockets = new List<Socket>();
                   socket = clientSocket.Accept();
                    sockets.Add(socket);
                    // Data buffer
                    byte[] bytes = new Byte[1024];
                    string data = null;
                   
                    while (true)
                    {

                        int numByte = socket.Receive(bytes);

                        data += Encoding.ASCII.GetString(bytes,
                                                   0, numByte);
                     client  = (ClientVars)JsonConvert.DeserializeObject<ClientVars>(data);
                        //if (data.IndexOf("<EOF>") > -1)
                            break;
                    }
                   
                    
                    Console.WriteLine("Text received -> {0} ", data);
                    //Console.WriteLine(client.Playerusername);
                    //Console.WriteLine(client.Userstake);
                    Game.Sqlclass.ExistingGame();
                    currentPot = Game.Sqlclass.StoredPot + client.Userstake;
                    Program.Lottery.Prizes(client.UserNums, Game.Sqlclass.callsArr);
                    byte[] message = Encoding.ASCII.GetBytes("Test Server");
                   
                    // Send a message to Client 
                    // using Send() method
                    //clientSocket.Send(message);

                    // Close client Socket using the
                    // Close() method. After closing,
                    // we can use the closed Socket 
                    // for a new Client Connection
                
                }
            }

            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }
}