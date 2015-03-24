using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace ReadSensors.Hubs
{
    [HubName("sensors")]
    public class GpioHub : Hub
    {
        public ICollection<string> Users { get; set; }

        public GpioHub()
        {
            Users = new Collection<string>();

            ReadAcceleratorSensor();
        }

        private async void ReadAcceleratorSensor()
        {
            while (true)
            {
                await Task.Run(() => PostSensorData());

                Thread.Sleep(TimeSpan.FromSeconds(1));
            }
        }

        public void Enable(string connectionId)
        {
            Users.Add(connectionId);
            Clients.Client(connectionId).enabled();
        }

        private void PostSensorData()
        {
            if (!Users.Any()) return;
            var rnd = new Random();

            var currentSensorData = new
            {
                X = rnd.Next(0, 90),
                Y = rnd.Next(0, 90),
                Z = rnd.Next(0, 90)
            };

            Clients.Clients(Users.ToList()).showessage(currentSensorData);
        }

        public void Disable(string connectionId)
        {
            Users.Remove(connectionId);
            Clients.Client(connectionId).disabled();
        }
    }
}