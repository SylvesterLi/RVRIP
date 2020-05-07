using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CalcTest4RVRIP
{
    class Program
    {
        static void Main(string[] args)
        {

            //判断车型
             

            //优化时间
            string time = "19.04";
            string[] dateForw = time.Split('.');
            time = "20" + time;
            DateTime dt = Convert.ToDateTime(time);


            //Console.WriteLine("Type:"+FixerTooling.GenTpJudge("C70E-A"));
            Console.WriteLine(System.Environment.CurrentDirectory);
            Console.ReadKey();
        }
    }
}
