﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using System.Text.Json;
using Newtonsoft.Json;

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
                    //List<string> messages = new List<string>();
                    //messages.Add(Program.Player.Playerusername);
                    //messages.Add(Program.Player.UserStake.ToString());
                    //messages.Add(string.Join(",", Program.Player.UserNums));
                    // Creation of message that
                    // we will send to Server
                    string usernums = string.Join(",", Program.Player.UserNums);
                    byte[] messageSent = Encoding.ASCII.GetBytes("Test Client<EOF>");

                    byte[] username = Encoding.ASCII.GetBytes(Program.Player.Playerusername);
                    byte[] userstake = Encoding.ASCII.GetBytes(Program.Player.UserStake.ToString());
                    byte[] usernumbers = Encoding.ASCII.GetBytes(usernums);
                    Player player = new Player()
                    {
                        Playerusername = Program.Player.Playerusername,
                        UserStake = Program.Player.UserStake,
                        UserNums = Program.Player.UserNums
                    };

                   string jsonstring = JsonConvert.SerializeObject(player);
                    byte[] data = Encoding.ASCII.GetBytes(jsonstring);
                    sender.Send(data);

                    //int byteSent1 = sender.Send(username);
                    //int byteSent2 = sender.Send(userstake);
                    //int byteSent3 = sender.Send(usernumbers);
                    //int byteSent = sender.Send(messageSent);
                    
                    // Data buffer
                    byte[] messageReceived = new byte[1024];
                    
                    // We receive the message using 
                    // the method Receive(). This 
                    // method returns number of bytes
                    // received, that we'll use to 
                    // convert them to string
                    int byteRecv = sender.Receive(messageReceived);
                    string serverdata = null;
                    serverdata += Encoding.ASCII.GetString(messageReceived,
                                                   0, byteRecv);
                    Console.WriteLine("Message from Server -> {0}" +
                            serverdata);
                    
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

