using System;
using Banks.Tools;

namespace Banks.Models
{
    public class DateTimeProvider
    {
        public DateTimeProvider()
        {
            CentralBankTime = DateTime.Today;
            LastUpdateTime = DateTime.Today;
        }

        public DateTime CentralBankTime { get; private set; }
        public DateTime LastUpdateTime { get; private set; }
        public bool TimeToUpdatePayments => CentralBankTime >= LastUpdateTime.AddMonths(1);
        public TimeSpan TimeSinceLastUpdate => CentralBankTime - LastUpdateTime;

        public void RewindTimeDays(int days)
        {
            if (days < 0)
                throw new BanksException("Amount of days can't be less than zero");

            CentralBankTime = CentralBankTime.AddDays(days);
        }

        public void UpdateLastUpdateTime() => LastUpdateTime = CentralBankTime;
    }
}