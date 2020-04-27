using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RailwayVehicleRapairIntervalCompute
{
    public class RailwayVehicleModel
    {
        //ModelClass 集中管理参数
        //参数管理：
        //1.车辆类型
        //.车号
        //.制造日期
        //.前次定检段修期
        //.前次定检厂修期
        //.加强段修日期（可空）
        //.下次段修期
        //.下次厂修期
        //.封存日期
        //.下下次厂修（预留）
        //.段修进度
        //.警告信息（包含参数未填报错、前次段修修正、加强段修车提示）


        /// <summary>
        /// 车辆类型
        /// </summary>
        public enum vehicleGenType
        {
            Spc70t = 2,//特殊情况
            Gen70t = 0,//70吨通用公式
            Gen60t = 1//60吨通用公式
        }

        /// <summary>
        /// 车辆类型
        /// </summary>
        public vehicleGenType GenType;
        /// <summary>
        /// 车辆编号（暂时没用）
        /// </summary>
        public string vehicleNumber { get; set; }
        /// <summary>
        /// 制造日期
        /// </summary>
        public DateTime produceDate { get; set; }
        /// <summary>
        /// 前次定检段修期
        /// </summary>
        public DateTime previousDepotDate { get; set; }//上次段修
        /// <summary>
        /// 前次定检厂修期
        /// </summary>
        public DateTime previousFactoryDate { get; set; }//上次厂修
        /// <summary>
        /// 加强段修期
        /// </summary>
        public DateTime enhanceDepotDate { get; set; }
        /// <summary>
        /// 下次段修日期
        /// </summary>
        public DateTime nextDepotDate { get; set; }
        /// <summary>
        /// 下次厂修日期
        /// </summary>
        public DateTime nextFactoryDate { get; set; }
        /// <summary>
        /// 封存日期
        /// </summary>
        public int SealDuration { get; set; }
        /// <summary>
        /// 下次厂修期（预留）
        /// </summary>
        public DateTime vNextFacDate { get; set; }
        /// <summary>
        /// 提示信息
        /// </summary>
        public string warningInfo { get; set; }
        /// <summary>
        /// 段修进度 n表示处在（本次厂修期内的）第几个段修
        /// </summary>
        public int n { get; set; }
        /// <summary>
        /// 初次验证、分类
        /// </summary>
        public int GenTpSelection { get; set; }

    }
}
