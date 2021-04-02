using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MailSender.lib.Interfaces;

namespace MailSender.lib.Service
{
    public class StatisticService : IStatistic
    {
        private readonly Stopwatch _StopwatchTimer = Stopwatch.StartNew();
        public TimeSpan UpTime => _StopwatchTimer.Elapsed;
    }
}
