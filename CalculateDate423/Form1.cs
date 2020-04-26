using RailwayVehicleRapairIntervalCompute;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace CalculateDate423
{
    public partial class Form1 : Form
    {
        Dictionary<string, string> dic = new Dictionary<string, string>();

        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 载入框体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {
            dic.Add("70t通用", "C70,C70E,C70E-A,C70H,C70EH,C70EH-A,P70,P70H,X70,X4K,X2K,X2H,C80B,C80BH");
            dic.Add("60t通用", "C64AT,C64H,C64K,C64T,P64AK,P64AT,P64GH,P64GK,P64GT,P64K,P64T");
            foreach (var item in dic.Keys)
            {
                listBox1.Items.Add(item.ToString());
            }
            listBox1.SelectedIndex = 0;
        }
        /// <summary>
        /// 两边联动
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string[] tValue = dic[listBox1.SelectedItem.ToString()].Split(',');
            listBox2.Items.Clear();
            listBox2.Items.AddRange(tValue);

        }



        private void button1_Generate_Click(object sender, EventArgs e)
        {
            if(textBox_SealSpan.Text=="")
            {
                textBox_SealSpan.Text = "0";
            }

            //VehicleData实例化需要4个参数，70还是60吨车型、上次厂修时间、上次段修时间、封存时间
            RailwayVehicleModel vehicleModel = new RailwayVehicleModel();

            //配置vehicleModel参数
            //强制转换，别搞那么复杂
            vehicleModel.GenTpSelection = listBox1.SelectedIndex;
            vehicleModel.produceDate = dateTimePicker1_Produce.Value.Date;
            vehicleModel.previousDepotDate = dateTimePicker1_Dep.Value.Date;
            vehicleModel.previousFactoryDate = dateTimePicker2_Fac.Value.Date;
            vehicleModel.SealDuration = Convert.ToInt32(textBox_SealSpan.Text);

            //需要设置int GenTp, DateTime pre_depDate, DateTime pre_facDate, DateTime produceDate, int SealSpan
            RailwayVehicleModel vResult = VehicleData.DateProcessKernel(vehicleModel);
            //, , , ,0





            Dbg("Log:" + DateTime.Now.ToString() + "\r\n" + vResult.previousDepotDate.ToString());
        }



        /// <summary>
        /// DebugInfo打印封装,自动换行
        /// </summary>
        /// <param name="str"></param>
        public void Dbg(string str)
        {
            string tmp = textBox1_DBG.Text;
            textBox1_DBG.Text = str + "\r\n" + tmp;
        }

        private void textBox_SealSpan_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 8 && !Char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }

}
