using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using MessageReceiverWebService.classes;

namespace MessageReceiverWebService
{
    /// <summary>
    /// Summary description for MessageReceiver
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class MessageReceiver : System.Web.Services.WebService
    {

        [WebMethod]
        public string GetMessage(DateTime DateTime, string Message)
        {
            try
            {
                //Web method which receives Datetime and the message from the console application, store them in database 
                //and sends them back to the console as response

                DataLayerClass DataLayer = new DataLayerClass();
                DataLayer.InsertMessage(DateTime, Message);
                return DateTime.ToString() + " : " + Message;
            }
            catch (Exception Ex)
            {
                return "Error:" + Ex.Message;
            }
        }
    }
}
