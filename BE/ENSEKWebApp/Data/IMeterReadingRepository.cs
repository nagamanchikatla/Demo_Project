using System;
using ENSEKWebApp.Models;

namespace ENSEKWebApp.Data
{
    public interface IMeterReadingRepository
    {
        MeterReadings UploadMeterReading(MeterReadings readings);
        UserAccount UploadUserAccount(UserAccount account);

        Boolean CheckAccountId(int accountId);
    }
}
