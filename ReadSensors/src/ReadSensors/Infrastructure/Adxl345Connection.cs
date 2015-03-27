using System;
using Raspberry.IO;
using Raspberry.Timers;

namespace ReadSensors.Infrastructure
{
    public class Adxl345Connection : IDisposable
    {
        private const decimal _triggerTime = 0.01m;  // Waits at least 10µs = 0.01ms
        private static readonly TimeSpan _echoUpTimeout = TimeSpan.FromMilliseconds(500);

        private readonly IOutputBinaryPin _scl;
        private readonly IInputBinaryPin _sda;

        public Adxl345Connection(IOutputBinaryPin scl, IInputBinaryPin sda)
        {
            _scl = scl;
            _sda = sda;

            try
            {
                Get3dAngle();
            }
            catch { }
        }
        public TimeSpan Timeout { get; set; }

        private void Get3dAngle()
        {

            _scl.Write(true);
            Timer.Sleep(_triggerTime);
            _scl.Write(false);

            var upTime = _sda.Time(true, _echoUpTimeout.Seconds, Timeout.Seconds);
         //   return Units.Velocity.Sound.ToDistance(upTime) / 2;

         
        }

        public void Dispose()
        {
            _scl.Dispose();
            _sda.Dispose();
        }
    }
}