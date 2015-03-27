using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using Raspberry.IO.GeneralPurpose;

namespace ReadSensors.Hubs
{
    [HubName("sensors")]
    public class GpioHub : Hub
    {
        private static readonly ICollection<string> _users = new Collection<string>();

        public GpioHub()
        {
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
            _users.Add(connectionId);
            Clients.Client(connectionId).enabled();
        }

        private void PostSensorData()
        {
            if (!_users.Any()) return;
            var rnd = new Random();

            var currentSensorData = new
            {
                X = rnd.Next(0, 90),
                Y = rnd.Next(0, 90),
                Z = rnd.Next(0, 90),
                Width = rnd.Next(0, 90)
            };

            Clients.Clients(_users.ToList()).showessage(currentSensorData);
        }

        public void Disable(string connectionId)
        {
            _users.Remove(connectionId);
            Clients.Client(connectionId).disabled();
        }
    }
}