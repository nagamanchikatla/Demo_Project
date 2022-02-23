using Microsoft.AspNetCore.Mvc;
using System;
using ENSEKWebApp.DTOs;
using ENSEKWebApp.Data;
using ENSEKWebApp.Models;

namespace ENSEKWebApp.Controllers
{
    [Route(template:"")]
    [ApiController]
    public class ReadController: Controller
    {
        private readonly IMeterReadingRepository _repository;

        public ReadController(IMeterReadingRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [Route(template: "hello")]
        public IActionResult hello()
        {
            return Ok("Hello Success");
        }

        [HttpPost]
        [Route(template: "update-meter-reading")]
        public IActionResult UpdateMeterReading(MeterReadingDTO dto)
        {
            try
            {
            string[][] updatedReadings = CheckAllReading(dto.Data);

            updatedReadings = UploadReadingsToDB(updatedReadings);
            return Ok(updatedReadings);
            } catch (Exception e)
            {
                return NotFound(e);
            }
        }

        //[HttpPost]
        //[Route(template: "update-user-account")]
        public IActionResult UploadAccount(MeterReadingDTO dto)
        {
            var userAccounts = dto.Data;
            for (int i = 1; i < userAccounts.Length; i++)
            {              
                    var account = new UserAccount
                    {
                        AccountId = Convert.ToInt32(userAccounts[i][0]),
                        FirstName = userAccounts[i][1],
                        LastName = userAccounts[i][2]
                    };
                    _repository.UploadUserAccount(account);                
            }
            return Ok("Success");
        }

        public string[][] UploadReadingsToDB(string[][] readings)
        {
            for (int i = 1; i < readings.Length; i++)
            {
                if(_repository.CheckAccountId(Convert.ToInt32(readings[i][0])))
                {

                if(readings[i][3] == "isValid")
                {
                var reading = new MeterReadings
                {
                    AccountId = Convert.ToInt32(readings[i][0]),
                    MeterReadingDate = readings[i][1],
                    MeterReadValue = Convert.ToInt32(readings[i][2])
                };
                    _repository.UploadMeterReading(reading);
                }
                } else
                {
                    readings[i].SetValue("isInvalidAccountId", 3);
                }
            }
            return readings;
        }

        public String[][] CheckAllReading(string[][] allreadings)
        {
            string[][] updatedReadings = new string[allreadings.Length][];
            updatedReadings.SetValue(new string[] { allreadings[0][0], allreadings[0][1], allreadings[0][2], "Status" }, 0);
            for (int i = 1; i < allreadings.Length; i++)
            {
                updatedReadings.SetValue(new string[] { allreadings[i][0], allreadings[i][1], allreadings[i][2], ValidateReadingValue(allreadings[i])[3] },i);
            }
            updatedReadings = CheckDuplicateAccounts(updatedReadings);
            return updatedReadings;
        }

        public String[] ValidateReadingValue(String[] sArray)
        {
            string[] newStr = new string[4];
            newStr[0] = sArray[0];
            newStr[1] = sArray[1];
            newStr[2] = sArray[2];
            try
            {
                int x = Convert.ToInt32(newStr[2]);
                if (x >= 0 && x < 99999)
                {
                    newStr.SetValue("isValid", 3);
                }
                else
                {
                    newStr.SetValue("isInvalid", 3);
                }
            }
            catch
            {
                    newStr.SetValue("isInvalid", 3);
            }
            return newStr;
           
        }
        public String[][] CheckDuplicateAccounts(String[][] updatedReadings)
        {
            for (int i = 1; i < updatedReadings.Length; i++)
            {
                if(updatedReadings[i][3] == "isValid")
                {
                    for (int j = 1; j < updatedReadings.Length; j++)
                    {
                         if((i != j) && (updatedReadings[i][0] == updatedReadings[j][0]) && (updatedReadings[j][3] == "isValid"))
                        {
                            DateTime  date1 = DateTime.ParseExact(updatedReadings[i][1], "dd/MM/yyyy HH:mm", null);
                            DateTime  date2 = DateTime.ParseExact(updatedReadings[j][1], "dd/MM/yyyy HH:mm", null);

                            int comparision = DateTime.Compare(date1, date2);
                            
                            if (comparision < 0)
                            {
                                updatedReadings[i].SetValue("isOlderValue", 3);
                                updatedReadings[j].SetValue("isValid", 3);

                            } else if(comparision == 0)
                            {
                                int reading1 = Convert.ToInt32(updatedReadings[i][2]);
                                int reading2 = Convert.ToInt32(updatedReadings[j][2]);
                                if (reading1 == reading2)
                                {
                                    updatedReadings[i].SetValue("isValid", 3);
                                    updatedReadings[j].SetValue("isDuplicate", 3);
                                } else if (reading2 > reading1)
                                {
                                    updatedReadings[i].SetValue("isOlderValue", 3);
                                    updatedReadings[j].SetValue("isValid", 3);
                                } else
                                {
                                    updatedReadings[j].SetValue("isOlderValue", 3);
                                    updatedReadings[i].SetValue("isValid", 3);
                                }
                            } else
                            {
                                updatedReadings[j].SetValue("isOlderValue", 3);
                                updatedReadings[i].SetValue("isValid", 3);
                            }
                        }
                    }
                }
            }
            return updatedReadings;
        }
    }
}
