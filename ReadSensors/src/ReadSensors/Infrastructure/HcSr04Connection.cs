
using System;
using Raspberry.IO;
using Raspberry.Timers;
using UnitsNet;

namespace ReadSensors.Infrastructure
{
    /// <summary>
    /// Represents a connection to HC-SR04 distance sensor.
    /// </summary>
    /// <remarks>
    ///     <see cref="https://docs.google.com/document/d/1Y-yZnNhMYy7rwhAgyL_pfa39RsB-x2qR4vP8saG73rE/edit"/> for hardware specification and 
    ///     <see cref="http://www.raspberrypi-spy.co.uk/2012/12/ultrasonic-distance-measurement-using-python-part-1/"/> for implementation details.
    /// </remarks>
    public class HcSr04Connection : IDisposable
    {

        private static readonly TimeSpan _triggerTime = TimeSpan.FromMilliseconds(0.01);  // Waits at least 10µs = 0.01ms
        private static readonly TimeSpan _echoUpTimeout = TimeSpan.FromMilliseconds(500);

        private readonly IOutputBinaryPin _triggerPin;
        private readonly IInputBinaryPin _echoPin;
        

        /// <summary>
        /// Initializes a new instance of the <see cref="HcSr04Connection"/> class.
        /// </summary>
        /// <param name="triggerPin">The trigger pin.</param>
        /// <param name="echoPin">The echo pin.</param>
        public HcSr04Connection(IOutputBinaryPin triggerPin, IInputBinaryPin echoPin)
        {
            _triggerPin = triggerPin;
            _echoPin = echoPin;

            Timeout = DefaultTimeout;

            try
            {
                GetDistance();
            }
            catch { }
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        void IDisposable.Dispose()
        {
            Close();
        }
        

        /// <summary>
        /// The default timeout (50ms).
        /// </summary>
        /// <remarks>Maximum time (if no obstacle) is 38ms.</remarks>
        public static readonly TimeSpan DefaultTimeout = TimeSpan.FromMilliseconds(50);

        /// <summary>
        /// Gets or sets the timeout for distance measure.
        /// </summary>
        /// <value>
        /// The timeout.
        /// </value>
        public TimeSpan Timeout { get; set; }
        
        /// <summary>
        /// Gets the distance.
        /// </summary>
        /// <returns>The distance.</returns>
        public Length GetDistance()
        {
            _triggerPin.Write(true);
            Timer.Sleep(_triggerTime);
            _triggerPin.Write(false);

            var upTime = _echoPin.Time(true, _echoUpTimeout, Timeout);
            return Units.Velocity.Sound.ToDistance(upTime) / 2;
        }

        /// <summary>
        /// Closes the connection.
        /// </summary>
        public void Close()
        {
            _triggerPin.Dispose();
            _echoPin.Dispose();
        }
        
    }
}