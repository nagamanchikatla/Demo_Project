using ENSEKWebApp.Models;
using System;
using System.Linq;

namespace ENSEKWebApp.Data
{
    public class MeterReadingRepository : IMeterReadingRepository
    {
        private readonly MeterReadingContext _context;

        public MeterReadingRepository(MeterReadingContext context)
        {
            _context = context;
        }
        public MeterReadings UploadMeterReading(MeterReadings readings)
        {
            _context.MeterReadings.Add(readings);
            _context.SaveChanges();

            return readings;
        }

        public UserAccount UploadUserAccount(UserAccount account)
        {
            _context.UserAccount.Add(account);
            _context.SaveChanges();

            return account;
        }

        public Boolean CheckAccountId(int accountId)
        {
            var accId = _context.UserAccount.FirstOrDefault(a => a.AccountId == accountId);

            if(accId == null)
            {
                return false;
            } else
            {
                return true;
            }
        }
    }
}
