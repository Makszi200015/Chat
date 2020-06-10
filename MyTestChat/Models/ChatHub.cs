using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using MyTestChat.Models;
using MyTestChat.Services;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace MyTestChat
{
    public class ChatHub : Hub 
    {
        static List<Connections> connections = new List<Connections>();
        static List<Groups> members = new List<Groups>();
        static List<UserInGr> gr = new List<UserInGr>();

        [Authorize]
        public async Task Send(string message, string userName)
        {
            await Clients.All.SendAsync("ReceiveMessage", userName, message);
        }

        public async Task SendMessageToCalller(string message, string userName) 
        {
            await Clients.Caller.SendAsync("ReceiveMessage", userName, message);
        }

        public async Task SendMessageToUser(string who, string message) 
        {
            string usname = Context.User.Identity.Name;
            string id = Context.ConnectionId;
            await Clients.Clients(who, id).SendAsync("ReceiveMessage", message, usname);
            
        }


        //public async Task CreateGroup(string group)
        //{
        //    string usname = Context.User.Identity.Name;
        //    string id = Context.ConnectionId;

            //if (connections.Count(x => x.Conid == id) == 0)
            //{
            //    connections.Add(new Connections { Conid = id, UsName = usname });

            //    Connections connections1 = connections.FirstOrDefault(p => p.UsName == usname);

            //    if (connections1 != null)
            //    {
            //        if (gr.Count(x => x.ConidGr == id) == 0)
            //        {
            //            gr.Add(new UserInGr { ConidGr = connections1.Conid, UsNameGr = connections1.UsName });
            //            foreach (var mod in gr)
            //            {

                            //if (members.Count(x => x.Name == id) == 0)
                            //{
                            //    members.Add(new Groups { Name = "",UserIdGr = id });
                            //}

            //foreach (var mygr in members)
            //connections.Add(new Connections { Conid = mygr.Name, UsName = mygr.Name });
            //foreach (var item in members)
                //await Groups.AddToGroupAsync(id,group);
            //Clients.Groups(roomName).addNewMessage(Context.User.Identity.Name, " присоединился к чату");
        //}
                //    }
                //}
        //    }

        //}

        public async Task JoinToGroup(string group)
        {
            string user = Context.ConnectionId;

                    await Groups.AddToGroupAsync(user, group);
                
        }

        public async Task SendMessageToGroup(string group, string message)
        {
            string name = Context.User.Identity.Name;
            await Clients.Group(group).SendAsync("ReceiveMessage",message, name);
        }

        public override async Task OnConnectedAsync()
        {
            string usname = Context.User.Identity.Name;
            string id = Context.ConnectionId;

            if (connections.Count(x => x.Conid == id) == 0)
            {
                connections.Add(new Connections { Conid = id, UsName = usname });

                foreach (var item in connections)
                {

                    await Clients.AllExcept(usname).SendAsync("NewUserConnected", item.Conid, item.UsName);
                }
            }
            await base.OnConnectedAsync();

        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            string id = Context.ConnectionId;
            var item = connections.FirstOrDefault(x => x.Conid == id);
            if (item != null)
            {
              connections.Remove(item);
              await  Clients.All.SendAsync("OnDisconnected",id, item.UsName);
            }

            await base.OnDisconnectedAsync(exception);
        }


    }
}
