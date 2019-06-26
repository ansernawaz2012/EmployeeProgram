using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeProgram
{
    static class DateConvertor
    {
        public static DateTime StringToDateObject(string date)
        {
           // DateTime dateObject = DateTime.ParseExact(date, "yyyy-MM-dd", null);
            DateTime dateObject = DateTime.Parse(date);

            return dateObject.Date;
        }

        public static string DateObjectToString(DateTime DOB)
        {
            string stringStartDate = DOB.ToShortDateString();

            return stringStartDate;

        }


        /// <summary>
        /// Method to calculate age from date of birth
        /// </summary>
        /// <param name="dob"></param>
        /// <returns></returns>
        public static int GetEmployeeAge(DateTime dob)
        {
            int age;
            age = DateTime.Now.Year - dob.Year;

            if (DateTime.Now.DayOfYear < dob.DayOfYear)
            {
                age = age - 1;
            }

            return age;
        }

    }
}
