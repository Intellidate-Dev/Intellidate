using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System.Threading.Tasks;

namespace AdminModule.SignalRHub
{
    [HubName("IntellidateHub")]
    public class IntellidateHub : Hub
    {
        public void addpost(object _object)
        {
            List<string> _ConnectionIDs = new List<string>();
            _ConnectionIDs.Add(Context.ConnectionId);

            Clients.AllExcept(_ConnectionIDs.ToArray()).addpost(_object);
        }
        public void addreply(object _object)
        {
            List<string> _ConnectionIDs = new List<string>();
            _ConnectionIDs.Add(Context.ConnectionId);

            Clients.AllExcept(_ConnectionIDs.ToArray()).addreply(_object);
        }

        public void setadminoffline(object _object)
        {
            List<string> _ConnectionIDs = new List<string>();
            _ConnectionIDs.Add(Context.ConnectionId);

            Clients.AllExcept(_ConnectionIDs.ToArray()).setadminoffline(_object);
        }

        public void setadminonline(object _object)
        {
            
            Clients.All.setadminonline(_object);
        }


        //
        public static Dictionary<string, string> OnlineClients = new Dictionary<string, string>();
        public void start()
        {
            if (OnlineClients.ContainsKey(Context.User.Identity.Name))
            {
                OnlineClients.Remove(Context.User.Identity.Name);
            }

            OnlineClients.Add(Context.User.Identity.Name, Context.ConnectionId);
            setadminonline(Context.User.Identity.Name);
            Clients.Caller.setAdminID(Context.User.Identity.Name);
        }


        public override Task OnDisconnected()
        {
            var _DictObject = OnlineClients.Where(x => x.Value == Context.ConnectionId).FirstOrDefault();
            if (_DictObject.Key != null)
            {
                if (OnlineClients.ContainsKey(_DictObject.Key))
                {
                    OnlineClients.Remove(_DictObject.Key);
                }
                setadminoffline(_DictObject.Key);
            }

            return base.OnDisconnected();
        }
    }
}