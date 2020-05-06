using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CalcTest4RVRIP
{
    public class FixerTooling
    {
        public static DateTime FixProduceTime(string fixTimeTag)
        {
            DateTime dateTime = Convert.ToDateTime(fixTimeTag);


            return dateTime;
        }
        public static DateTime FixRepairTime(string fixTimeTag)
        {
            fixTimeTag = "20" + fixTimeTag;
            DateTime dateTime = Convert.ToDateTime(fixTimeTag);
            return dateTime;
        }

        public static int GenTpJudge(string vehicleType)
        {
            List<string> all70tVehicle = new List<string> { "C70", "C70E", "C70E-A", "C70H", "C70EH", "C70EH-A", "P70", "P70H", "X70", "X4K", "X2K", "X2H", "C80B", "C80BH" };
            List<string> all60Vehicle = new List<string> { "C64AT", "C64H", "C64K", "C64T", "P64AK", "P64AT", "P64GH", "P64GK", "P64GT", "P64K", "P64T" };

            if (all70tVehicle.Contains(vehicleType))
            {
                return 0;
            }
            else if (all60Vehicle.Contains(vehicleType))
            {
                return 1;
            }
            else
            {
                //其他车型
                return 3;
            }



        }



    }
}
