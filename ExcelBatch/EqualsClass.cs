using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExcelBatch
{
    public class EqualsClass
    {
        public int vehicleCategory(string vCateInput)
        {
            string[] vehCate70t = { "C70,C70E,C70E-A,C70H,C70EH,C70EH-A,P70,P70H,X70,X4K,X2K,X2H,C80B,C80BH" };
            string[] vehCate60t = { "C64AT,C64H,C64K,C64T,P64AK,P64AT,P64GH,P64GK,P64GT,P64K,P64T" };

            int GenType = 3;
            
            foreach (var item in vehCate70t)
            {
                if(vCateInput.Contains(item))//如果里面包含列表中选的
                {
                    GenType = 0;
                }
            }
            foreach (var item in vehCate60t)
            {
                if(vCateInput.Contains(item))
                {
                    GenType = 1;
                }
            }
            
            return GenType;
        }



    }
}
