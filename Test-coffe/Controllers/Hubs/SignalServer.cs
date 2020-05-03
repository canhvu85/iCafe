using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Test_coffe
{
    public class SignalServer : Hub
    {
        public async Task SendMessage()
        {
            await Clients.All.SendAsync("refreshBillDetails");

        }
    }
}
