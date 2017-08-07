using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using KnockoutAndTypescript.Models;

namespace KnockoutAndTypescript.Hubs
{
    public class KidHub : Hub
    {
        public static List<string> savedMessages = new List<string>();

        public void Hello()
        {
            Clients.All.hello();
        }

        public void Send(string name, string message)
        {
            // Call the addNewMessageToPage method to update clients.
            Clients.All.addNewMessageToPage(name, message);
            savedMessages.Add(message);
        }

        public void UpdatePoints(PointSignal points)
        {
            Clients.All.addNewMessageToPage(points);
        }
    }
}