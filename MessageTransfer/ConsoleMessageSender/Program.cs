using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleMessageSender
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                try
                {
                    //Console code which waits for user input, validate them and sends the acceptable messages 
                    //to the website to be stored in database

                    Console.WriteLine("Please type your message...");
                    string Message = Console.ReadLine();
                    if (Message.Length == 0)
                    {
                        Console.WriteLine("Error: Message may not be empty");

                    }
                    else if (Message.ToLower() == "exit")
                    {
                        break;
                    }
                    else
                    {
                        MessageReceiverService.MessageReceiverSoapClient Receiver = new MessageReceiverService.MessageReceiverSoapClient();
                        DateTime DateTime = System.DateTime.Now;
                        string Response = Receiver.GetMessage(DateTime, Message);
                        Console.WriteLine("The following data was stored in the database : \n" + Response + "\n");
                    }
                }
                catch (Exception Ex)
                {
                    Console.WriteLine(Ex.Message);
                }
            }
        }
    }
}
