using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoxTicketApi.BLL.Requests.Performance
{
    public class PerformancesByDateRequest
    {
        public DateTime dateTime = DateTime.Now;

        public string Date
        {
            //get { return dateTime.ToString("yyyy-dd-MM"); } // Формат "рік-день-місяць"
            set
            {
                if (DateTime.TryParseExact(value, "yyyy-dd-MM", null, System.Globalization.DateTimeStyles.None, out DateTime parsedDate))
                {
                    dateTime = parsedDate;
                }
                else
                {
                    throw new Exception("Неправильний формат дати. Використовуйте формат 'рік-день-місяць'.");
                }
            }
        }
    }
}
