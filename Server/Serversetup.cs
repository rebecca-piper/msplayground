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
       public static Clients client;
       
        private static Socket clientSocket;
        private static List<Socket> sockets;
        private static Socket socket;
        
        public static Clients Client { get => client; set => client = value; }
 
        public static Socket ClientSocket { get => clientSocket; set => clientSocket = value; }
        public static List<Socket> Sockets { get => sockets; set => sockets = value; }
        public static Socket Socket { get => socket; set => socket = value; }

        public void ExecuteServer()
        {
            // Establish the local endpoint 
            // for the socket. Dns.GetHostName
            // returns the name of the host 
            // running the application.
            IPHostEntry ipHost = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress ipAddr = ipHost.AddressList[0];
            IPEndPoint localEndPoint = new IPEndPoint(ipAddr, 11111);
            sockets = new List<Socket>();
            

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

                    try
                    {
                        Console.WriteLine("Waiting connection ... ");

                        // Suspend while waiting for
                        // incoming connection Using 
                        // Accept() method the server 
                        // will accept connection of client

                        socket = clientSocket.Accept();
                        sockets.Add(socket);

                    }
                    catch (SocketException e)
                    {
                        Console.WriteLine(e.ToString() + "Error in connecting client to server");
                    }
 
                    // Data buffer
                    byte[] bytes = new Byte[1024];
                    string data = null;
                   
                    while (true)
                    {
                        try
                        {
                            int numByte = socket.Receive(bytes);

                            data += Encoding.ASCII.GetString(bytes,
                                                       0, numByte);
                            client = (Clients)JsonConvert.DeserializeObject<Clients>(data);
                            //if (data.IndexOf("<EOF>") > -1)
                            break;
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.ToString() + "Error in receiving data from client");
                        }
                    }
                   
                    Program.Lottery.PlayExistingGame();
                    Console.WriteLine("Text received -> {0} ", data);
                    //Console.WriteLine(client.Playerusername);
                    //Console.WriteLine(client.Userstake);
                  
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
                Console.WriteLine(e.ToString() + "Unexcpected error occured on Server execution");
            }
        }
    }
}