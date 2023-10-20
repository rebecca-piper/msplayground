using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Threading;


using System.Net.Sockets;
namespace Server
{


    internal class ServerSetup
    {
        //public static HttpListener _httpListener = new HttpListener();
        //public void ServerDetails()
        //{
        //    Console.WriteLine("Starting server...");
        //    _httpListener.Prefixes.Add("http://localhost:5000/"); // add prefix "http://localhost:5000/"
        //    _httpListener.Start(); // start server (Run application as Administrator!)
        //    Console.WriteLine("Server started.");
        //    Thread _responseThread = new Thread(ResponseThread);
        //    _responseThread.Start(); // start the response thread
        //}

        //static void ResponseThread()
        //{
        //    while (true)
        //    {
        //        HttpListenerContext context = _httpListener.GetContext(); // get a context
        //                                                                  // Now, you'll find the request URL in context.Request.Url
        //                                                                  //byte[] _responseArray = Encoding.UTF8.GetBytes("<html><head><title>Localhost server -- port 5000</title></head>" +
        //                                                                  //"<body>Welcome to the <strong>Localhost server</strong> -- <em>port 5000!</em></body></html>"); // get the bytes to response
        //                                                                  //context.Response.OutputStream.Write(_responseArray, 0, _responseArray.Length); // write bytes to the output stream
        //                                                                  //context.Response.KeepAlive = false; // set the KeepAlive bool to false
        //                                                                  //context.Response.Close(); // close the connection
        //        Console.WriteLine("Respone given to a request.");
        //    }
        //}
        public void ExecuteServer()
        {
            // Establish the local endpoint 
            // for the socket. Dns.GetHostName
            // returns the name of the host 
            // running the application.
            IPHostEntry ipHost = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress ipAddr = ipHost.AddressList[0];
            IPEndPoint localEndPoint = new IPEndPoint(ipAddr, 11111);

            // Creation TCP/IP Socket using 
            // Socket Class Constructor
            Socket listener = new Socket(ipAddr.AddressFamily,
                         SocketType.Stream, ProtocolType.Tcp);

            try
            {

                // Using Bind() method we associate a
                // network address to the Server Socket
                // All client that will connect to this 
                // Server Socket must know this network
                // Address
                listener.Bind(localEndPoint);

                // Using Listen() method we create 
                // the Client list that will want
                // to connect to Server
                listener.Listen(10);

                while (true)
                {

                    Console.WriteLine("Waiting connection ... ");

                    // Suspend while waiting for
                    // incoming connection Using 
                    // Accept() method the server 
                    // will accept connection of client
                    Socket clientSocket = listener.Accept();

                    // Data buffer
                    byte[] bytes = new Byte[1024];
                    string data = null;

                    while (true)
                    {

                        int numByte = clientSocket.Receive(bytes);

                        data += Encoding.ASCII.GetString(bytes,
                                                   0, numByte);

                        if (data.IndexOf("<EOF>") > -1)
                            break;
                    }

                    Console.WriteLine("Text received -> {0} ", data);
                    byte[] message = Encoding.ASCII.GetBytes("Test Server");

                    // Send a message to Client 
                    // using Send() method
                    clientSocket.Send(message);

                    // Close client Socket using the
                    // Close() method. After closing,
                    // we can use the closed Socket 
                    // for a new Client Connection
                    clientSocket.Shutdown(SocketShutdown.Both);
                    clientSocket.Close();
                }
            }

            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }
}