using System;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Raspberry.IO;
using Raspberry.IO.GeneralPurpose;
using Raspberry.Timers;
using UnitsNet;

namespace ReadSensors.Infrastructure
{
    public class Adxl345Connection : IDisposable
    {
        private static readonly TimeSpan _triggerTime = TimeSpan.FromMilliseconds(0.01);  // Waits at least 10µs = 0.01ms
        private static readonly TimeSpan _echoUpTimeout = TimeSpan.FromMilliseconds(500);

        private readonly IOutputBinaryPin _scl;
        private readonly IInputBinaryPin _sda;

        public Adxl345Connection(IOutputBinaryPin scl, IInputBinaryPin sda)
        {
            _scl = scl;
            _sda = sda;

            try
            {
                ReadAngles();
            }
            catch { }
        }

        public TimeSpan Timeout { get; set; }

        public Vector3 ReadAngles()
        {

            _scl.Write(true);
            Timer.Sleep(_triggerTime);
            _scl.Write(false);


            var test = _sda.Time(true, Timeout);
            var analogValue = _sda.Read();

          //  var value = analogValue.Value;


            //_sda.Read();
            //   return Units.Velocity.Sound.ToDistance(upTime) / 2;


            var rnd = new Random();

            return new Vector3(
                rnd.Next(0, 90),
                rnd.Next(0, 90),
                rnd.Next(0, 90));
        }

        public void Dispose()
        {
            _scl.Dispose();
            _sda.Dispose();
        }
    }
}