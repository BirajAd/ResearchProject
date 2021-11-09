using System;
using System.Threading.Tasks;
using HotChocolate.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using RPHost.Helpers;

namespace RPHost.SignalR
{
    public class PresenceHub : Hub
    {
        private readonly PresenceTracker _tracker;
        public PresenceHub(PresenceTracker tracker)
        {
            _tracker = tracker;

        }

        [Authorize]
        public override async Task OnConnectedAsync()
        {
            var username = Context.User.GetUsername();
            await _tracker.UserConnected(username, Context.ConnectionId);
            await Clients.Others.SendAsync("UserOnline", username);

            var currentUsers = await _tracker.GetOnlineUsers();
            await Clients.All.SendAsync("GetOnlineUsers", currentUsers);
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var username = Context.User.GetUsername();
            await _tracker.UserDisconnected(username, Context.ConnectionId);
            await Clients.Others.SendAsync("UserOffline", username);
           
            var currentUsers = await _tracker.GetOnlineUsers();
            await Clients.All.SendAsync("GetOnlineUsers", currentUsers);

            await base.OnDisconnectedAsync(exception);
        }
    }
}