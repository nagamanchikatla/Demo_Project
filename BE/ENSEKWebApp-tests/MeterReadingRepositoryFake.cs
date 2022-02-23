using ENSEKWebApp.Data;
using ENSEKWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ENSEKWebApp_tests
{
    public class MeterReadingRepositoryFake : IMeterReadingRepository
    {
        private readonly List<MeterReadings> _meterReadings;
        private readonly List<UserAccount> _userAccount;
        public MeterReadingRepositoryFake()
        {
            _meterReadings = new List<MeterReadings>()
            {
                new MeterReadings
                {
                    AccountId = 1234,
                    MeterReadingDate = "22/04/2019 09:24",
                    MeterReadValue = 12345
                },
                new MeterReadings
                {
                    AccountId = 1235,
                    MeterReadingDate = "23/04/2019 09:24",
                    MeterReadValue = 23456
                },
                new MeterReadings
                {
                    AccountId = 1236,
                    MeterReadingDate = "23/04/2019 09:24",
                    MeterReadValue = 34567
                }
            };

            _userAccount = new List<UserAccount>()
            {
                new UserAccount
                {
                    AccountId = 1234,
                    FirstName = "Naga",
                    LastName = "Samala"
                },
                new UserAccount
                {
                    AccountId = 1235,
                    FirstName = "Samala",
                    LastName = "Naga"
                },
                new UserAccount
                {
                    AccountId = 1236,
                    FirstName = "Naga",
                    LastName = "Manchikatla"
                },
            };

        }
        public bool CheckAccountId(int accountId)
        {
            var accId = _userAccount.Where(a => a.AccountId == accountId).FirstOrDefault();
            if (accId == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public MeterReadings UploadMeterReading(MeterReadings readings)
        {
            Random random = new Random();
            readings.Id = random.Next();
            _meterReadings.Add(readings);
            return readings;
        }

        public UserAccount UploadUserAccount(UserAccount account)
        {
            Random random = new Random();
            account.Id = random.Next();
            _userAccount.Add(account);
            return account;
        }
    }
}
