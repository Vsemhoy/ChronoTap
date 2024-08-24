using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChronoTap.Storage.Localization
{
    internal class Localizer
    {
        public string monthName(int num)
        {
            string[] strings = [
                "Jan",
                "Feb",
                "Mar",
                "Apr",
                "May",
                "Jun",
                "Jul",
                "Aug",
                "Sep",
                "Oct",
                "Nov",
                "Dec"
                ];
            int index = num - 1;
            if (index > 11)
            {
                index = 11;
            }
            if (index < 0)
            {
                index = 0;
            }

            return strings[index];
        }
    }
}
