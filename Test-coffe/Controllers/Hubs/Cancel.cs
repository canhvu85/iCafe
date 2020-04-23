using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Test_coffe.Controllers.Hubs
{
    public class Cancel : Hub
    {
        public async Task CancelOrder(string username, string msg)
        {
            await Clients.All.SendAsync(username,msg);

        }
    }
}
