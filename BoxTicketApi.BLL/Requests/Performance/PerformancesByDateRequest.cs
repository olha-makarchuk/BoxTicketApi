using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoxTicketApi.BLL.Requests.Performance
{
    public class PerformancesByDateRequest
    {
        public DateTime dateTime = DateTime.Now; // Початкове значення за замовчуванням (можна змінити)

        public string Date
        {
            get { return dateTime.ToString("yyyy-dd-MM"); } // Формат "рік-день-місяць"
            set
            {
                // Додатковий код для обробки вхідного рядка та встановлення дати
                if (DateTime.TryParseExact(value, "yyyy-dd-MM", null, System.Globalization.DateTimeStyles.None, out DateTime parsedDate))
                {
                    dateTime = parsedDate;
                }
                else
                {
                    // Обробка помилки: вхідний рядок не відповідає формату "рік-день-місяць"
                    throw new ArgumentException("Неправильний формат дати. Використовуйте формат 'рік-день-місяць'.");
                }
            }
        }
    }
}
