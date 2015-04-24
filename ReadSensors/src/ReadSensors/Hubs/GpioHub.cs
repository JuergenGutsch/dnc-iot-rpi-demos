using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using Raspberry.IO.GeneralPurpose;
using ReadSensors.Infrastructure;
using UnitsNet;

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
                if (_users.Any())
                {
                    await Task.Run(() => PostSensorData());
                }

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
            //var angles = new Angles3D();
            //var sclPin = new GpioOutputBinaryPin(null, ConnectorPin.P1Pin15.ToProcessor());
            //var sdaPin = new GpioInputBinaryPin(null, ConnectorPin.P1Pin16.ToProcessor());
            //using (var adxl345Connection = new Adxl345Connection(sclPin, sdaPin))
            //{
            //    angles = adxl345Connection.ReadAngles();
            //}

            var distance = Length.FromMeters(0);
            var triggerPin = new GpioOutputBinaryPin(null, ConnectorPin.P1Pin12.ToProcessor());
            var echoPin = new GpioInputBinaryPin(null, ConnectorPin.P1Pin11.ToProcessor());
            using (var sdHr04Connction = new HcSr04Connection(triggerPin, echoPin))
            {
                distance = sdHr04Connction.GetDistance();
            }


            var currentSensorData = new
            {
                //X = angles.X,
                //Y = angles.Y,
                //Z = angles.Z,
                Width = distance
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