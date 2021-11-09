using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RPHost.SignalR
{
    public class PresenceTracker
    {
        //username -> connection_id(a list if same user connects from ore than one device or browser)
        //"gomez" => {"a348925Wr", "63br234908N"}
        private static readonly Dictionary<string, List<string>> onlineUsers = 
            new Dictionary<string, List<string>>();


        public Task UserConnected(string username, string connectionId)
        {
            lock (onlineUsers)
            {
                if (onlineUsers.ContainsKey(username))
                {
                    onlineUsers[username].Add(connectionId);
                }
                else
                {
                    onlineUsers.Add(username, new List<string>{connectionId});
                }
            }

            return Task.CompletedTask;
        }

        public Task UserDisconnected(string username, string connectionId)
        {
            lock(onlineUsers)
            {
                if(!onlineUsers.ContainsKey(username)) return Task.CompletedTask;

                onlineUsers[username].Remove(connectionId);
                if(onlineUsers[username].Count == 0)
                {
                    onlineUsers.Remove(username);
                }
            }

            return Task.CompletedTask;
        }

        public Task<string[]> GetOnlineUsers()
        {
            string[] OnlineUsers;
            lock(onlineUsers)
            {
                OnlineUsers = onlineUsers.OrderBy(k => k.Key).Select(k => k.Key).ToArray();
            }

            return Task.FromResult(OnlineUsers);
        }
    }
}