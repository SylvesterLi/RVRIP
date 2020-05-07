using RailwayVehicleRapairIntervalCompute;
using System;

namespace CalculateDate423
{

    public static class VehicleData
    {

        /// <summary>
        /// 修程修制改革车型计算核心
        /// </summary>
        /// <param name="vehicle"></param>
        /// <returns></returns>
        public static RailwayVehicleModel ReDateProcessKernel(RailwayVehicleModel vehicle)
        {
            //需要设置int GenTp, 
            //DateTime pre_depDate, 
            //DateTime pre_facDate, 
            //DateTime produceDate, 
            //int SealSpan

            //计算上次段修距离上次厂修的时间差
            //Gate用来储存时间差
            int Gate_year = vehicle.previousDepotDate.Year - vehicle.previousFactoryDate.Year;
            int Gate_month = vehicle.previousDepotDate.Month - vehicle.previousFactoryDate.Month;
            decimal Gate_sum = Gate_year * 12 + Gate_month;


            //70t通用
            if (vehicle.GenTpSelection == 0)
            {
                //此时开始判断是否需要加强段修。开始分类,判断特殊情况。
                int predictFacDate = vehicle.previousFactoryDate.AddMonths(vehicle.SealDuration).Year + 8;
                if ((predictFacDate == 2020 || predictFacDate == 2021) && ((vehicle.previousFactoryDate.Year - vehicle.produceDate.Year) < 2))
                {
                    //GenTp = 2;//70t的特殊情况，需要加强段修.但后面不再需要GenTp此参数，由描述车型更精确的vehicle.GenType替代
                    vehicle.GenType = RailwayVehicleModel.vehicleGenType.Spc70t;
                    vehicle.warningInfo += "\r\n70t特殊类型";
                }
                else
                {
                    vehicle.GenType = RailwayVehicleModel.vehicleGenType.Gen70t;
                    vehicle.warningInfo += "\r\n70t通用类型";
                }

                //段修修程为24个月 n为第几次段修
                int n = (int)Math.Round(Convert.ToDouble(Gate_sum / 24), MidpointRounding.AwayFromZero);//四舍五入为了减少误差


                //判断是否修正过
                if (vehicle.previousDepotDate.Date != vehicle.previousFactoryDate.AddMonths(n * 24).Date)
                {
                    vehicle.warningInfo = "\r\n前次段修数据经过修正,之前:" + vehicle.previousDepotDate + ";\r\n修正后：" + vehicle.previousFactoryDate.AddMonths(n * 24);
                }
                vehicle.previousDepotDate = vehicle.previousFactoryDate.AddMonths(n * 24);

                switch (vehicle.GenType)
                {
                    case RailwayVehicleModel.vehicleGenType.Gen70t:
                        //计算增加修程
                        //将前n次的24个月+剩下的4-n次的27个月加入到上次厂修 即可求出下次厂修时间
                        vehicle.nextFactoryDate = vehicle.previousFactoryDate.AddMonths((4 - n) * 27 + n * 24).AddMonths(vehicle.SealDuration);
                        //拿修正后的段修时间+封存期，判断第n次段修是否在2020-2021修程修制改革时间内
                        DateTime sealEndTime = vehicle.previousDepotDate.AddMonths(vehicle.SealDuration);
                        if (sealEndTime.Year == 2020 || sealEndTime.Year == 2021)
                        {
                            //在2020-2011期间，则先加两年再加3个月，得到下一次厂修日期
                            vehicle.currentDepotDate = sealEndTime.AddMonths(24).AddMonths(3);//vehicle.previousDepotDate.AddMonths(vehicle.SealDuration);
                            vehicle.nextFactoryDate = vehicle.nextFactoryDate.AddMonths(3);
                            vehicle.vNextDepotDate = vehicle.currentDepotDate.AddMonths(27);
                        }
                        else
                        {
                            //如果不在2020-2011，则按照正常算
                            //前面的FactoryDate不用算了
                            vehicle.currentDepotDate = sealEndTime.AddMonths(27);
                            vehicle.vNextDepotDate = vehicle.currentDepotDate.AddMonths(27);
                        }
                        vehicle.n = n;
                        break;
                    case RailwayVehicleModel.vehicleGenType.Spc70t:
                        //首先判断n是否为3，保险起见
                        if (n == 3)
                        {
                            //第一次做加强段修，不增加修程
                            //本次为加强段修，下次做厂修
                            vehicle.nextFactoryDate = vehicle.previousFactoryDate.AddMonths(24 * 5).AddMonths(vehicle.SealDuration);
                            vehicle.enhanceDepotDate = vehicle.previousFactoryDate.AddYears(8).AddMonths(vehicle.SealDuration);
                            vehicle.currentDepotDate = vehicle.enhanceDepotDate;
                            vehicle.vNextDepotDate = vehicle.currentDepotDate.AddMonths(24);
                            vehicle.n = n;
                        }
                        else
                        {
                            vehicle = null;
                        }
                        break;
                    default:
                        vehicle = null;
                        break;
                }
            }

            //60t通用
            else if (vehicle.GenTpSelection == 1)
            {
                vehicle.GenType = RailwayVehicleModel.vehicleGenType.Gen60t;
                //段修修程为18个月 n为第几次段修
                int n = (int)Math.Round(Convert.ToDouble(Gate_sum / 18), MidpointRounding.AwayFromZero);//四舍五入为了减少误差
                //计算增加修程
                //将前n次的18个月+剩下的6-n次的20个月加入到上次厂修 即可求出下次厂修时间
                vehicle.nextFactoryDate = vehicle.previousFactoryDate.AddMonths((6 - n) * 20 + n * 18).AddMonths(vehicle.SealDuration);
                //修正上次段修时间
                if (vehicle.previousDepotDate.Date != vehicle.previousFactoryDate.AddMonths(n * 18).Date)
                {
                    vehicle.warningInfo = "\r\n前次段修数据经过修正,之前:" + vehicle.previousDepotDate + ";\r\n修正后：" + vehicle.previousFactoryDate.AddMonths(n * 18);
                }
                vehicle.previousDepotDate = vehicle.previousFactoryDate.AddMonths(n * 18);
                //密封期
                DateTime sealEndTime = vehicle.previousDepotDate.AddMonths(vehicle.SealDuration);
                if (sealEndTime.Year == 2020 || sealEndTime.Year == 2021)
                {
                    //在2020-2011期间，则先加两年再加3个月，得到下一次厂修日期
                    vehicle.currentDepotDate = sealEndTime.AddMonths(18).AddMonths(2);
                    vehicle.nextFactoryDate = vehicle.nextFactoryDate.AddMonths(2).AddMonths(vehicle.SealDuration);
                    vehicle.vNextDepotDate = vehicle.currentDepotDate.AddMonths(20);
                }
                else
                {
                    //如果不在2020-2011，则按照正常算
                    vehicle.currentDepotDate = vehicle.previousDepotDate.AddMonths(20).AddMonths(vehicle.SealDuration);
                    vehicle.vNextDepotDate = vehicle.currentDepotDate.AddMonths(20);
                }
                vehicle.n = n;
                vehicle.warningInfo += "\r\n60t通用类型";

            }
            else//其他车就不用改修程
            {
                //其他车都是60t的车都是水泥罐之类的，1.5年修一次，5个段修，9年做一次厂修

                //其他车暂时不写
                vehicle = null;
            }
            return vehicle;
        }

        /// <summary>
        /// 处理不参加修程修制改革的车型
        /// </summary>
        /// <param name="vehicle"></param>
        /// <returns></returns>
        public static RailwayVehicleModel NonDateProcessKernel(RailwayVehicleModel vehicle)
        {

            vehicle.warningInfo = "暂不处理非修程修制改革车型";

            return vehicle;
        }



    }
}
