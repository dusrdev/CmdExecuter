using System;
using System.Collections.Generic;

namespace CmdExecuter.Core.Components {
    internal class Stopwatch {
        private System.Diagnostics.Stopwatch Watch { get; init; }

        public Stopwatch() {
            Watch = new System.Diagnostics.Stopwatch();
        }

        public void Start() {
            Watch.Start();
        }

        public string Stop() {
            Watch.Stop();

            var elapsed = Watch.Elapsed;

            Watch.Reset();

            List<string> times = new();

            if (elapsed.Days > 0) {
                times.Add($"{elapsed.Days} days");
                elapsed = elapsed.Subtract(TimeSpan.FromDays(elapsed.Days));
            }
            if (elapsed.Hours > 0) {
                times.Add($"{elapsed.Hours} hours");
                elapsed = elapsed.Subtract(TimeSpan.FromHours(elapsed.Hours));
            }
            if (elapsed.Minutes > 0) {
                times.Add($"{elapsed.Minutes} minutes");
                elapsed = elapsed.Subtract(TimeSpan.FromMinutes(elapsed.Minutes));
            }
            if (elapsed.Seconds > 0) {
                times.Add($"{elapsed.Seconds} seconds");
                elapsed = elapsed.Subtract(TimeSpan.FromSeconds(elapsed.Seconds));
            }
            if (elapsed.Milliseconds > 0) {
                times.Add($"{elapsed.Milliseconds} ms");
                elapsed = elapsed.Subtract(TimeSpan.FromMilliseconds(elapsed.Milliseconds));
            }

            return string.Join(", ", times);
        }
    }
}
