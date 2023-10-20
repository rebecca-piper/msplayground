using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace LotteryGame
{
    internal class Client
    {

            // ExecuteClient() Method
           public  void ExecuteClient()
            {

                try
                {

                    // Establish the remote endpoint 
                    // for the socket. This example 
                    // uses port 11111 on the local 
                    // computer.
                    IPHostEntry ipHost = Dns.GetHostEntry(Dns.GetHostName());
                    IPAddress ipAddr = ipHost.AddressList[0];
                    IPEndPoint localEndPoint = new IPEndPoint(ipAddr, 11111);

                    // Creation TCP/IP Socket using 
                    // Socket Class Constructor
                    Socket sender = new Socket(ipAddr.AddressFamily,
                            SocketType.Stream, ProtocolType.Tcp);

                    try
                    {

                        // Connect Socket to the remote 
                        // endpoint using method Connect()
                        sender.Connect(localEndPoint);

                        // We print EndPoint information 
                        // that we are connected
                        Console.WriteLine("Socket connected to -> {0} ",
                                    sender.RemoteEndPoint.ToString());

                        // Creation of message that
                        // we will send to Server
                        byte[] messageSent = Encoding.ASCII.GetBytes("Test Client<EOF>");
                        
                    List<string> messages = new List<string>();
                    messages.Add(Program.Player.Playerusername);
                    messages.Add(Program.Player.UserStake.ToString());
                    messages.Add(string.Join(",", Program.Player.UserNums));
                    int byteSent = sender.Send(messageSent);
                    // Data buffer
                    byte[] messageReceived = new byte[1024];

                        // We receive the message using 
                        // the method Receive(). This 
                        // method returns number of bytes
                        // received, that we'll use to 
                        // convert them to string
                        int byteRecv = sender.Receive(messageReceived);
                        Console.WriteLine("Message from Server -> {0}",
                            Encoding.ASCII.GetString(messageReceived,
                                                        0, byteRecv));

                        // Close Socket using 
                        // the method Close()
                        sender.Shutdown(SocketShutdown.Both);
                        sender.Close();
                    }

                    // Manage of Socket's Exceptions
                    catch (ArgumentNullException ane)
                    {

                        Console.WriteLine("ArgumentNullException : {0}", ane.ToString());
                    }

                    catch (SocketException se)
                    {

                        Console.WriteLine("SocketException : {0}", se.ToString());
                    }

                    catch (Exception e)
                    {
                        Console.WriteLine("Unexpected exception : {0}", e.ToString());
                    }
                }

                catch (Exception e)
                {

                    Console.WriteLine(e.ToString());
                }
            }
        }
    }

