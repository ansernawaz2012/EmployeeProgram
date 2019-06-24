using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeProgram
{
    static class DateConvertor
    {
        public static DateTime stringToDateObject(string date)
        {
            DateTime dateObject = DateTime.ParseExact(date, "yyyy-MM-dd", null);

            return dateObject.Date;
        }
    }
}
