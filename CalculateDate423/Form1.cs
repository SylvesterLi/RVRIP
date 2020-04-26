using RailwayVehicleRapairIntervalCompute;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using static RailwayVehicleRapairIntervalCompute.RailwayVehicleModel;

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
            ProgressBarScaleDisplay(vehicleGenType.Gen70t, 0);
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
            if (textBox_SealSpan.Text == "")
            {
                textBox_SealSpan.Text = "0";
            }

            //RailwayVehicleModel实例化需要配置5个参数
            //70还是60吨车型、生产日期、上次厂修时间、上次段修时间、封存时间
            RailwayVehicleModel vehicleModel = new RailwayVehicleModel();

            //配置vehicleModel参数 5项
            //强制转换，别搞那么复杂
            vehicleModel.GenTpSelection = listBox1.SelectedIndex;
            vehicleModel.produceDate = dateTimePicker1_Produce.Value.Date;
            vehicleModel.previousDepotDate = dateTimePicker1_Dep.Value.Date;
            vehicleModel.previousFactoryDate = dateTimePicker2_Fac.Value.Date;
            vehicleModel.SealDuration = Convert.ToInt32(textBox_SealSpan.Text);

            //需要设置int GenTp, DateTime pre_depDate, DateTime pre_facDate, DateTime produceDate, int SealSpan
            RailwayVehicleModel vResult = VehicleData.DateProcessKernel(vehicleModel);
            
            textBox_NextDep.Text = vResult.nextDepotDate.Date.ToString();
            textBox_NextFac.Text = vResult.nextFactoryDate.Date.ToString();
            label9.Text = "修程进度：" + vResult.n.ToString();
            //进度条显示
            ProgressBarScaleDisplay(vResult.GenType, vResult.n);


            textBox1_DBG.Text = vResult.warningInfo;
            //Dbg("Log:" + DateTime.Now.ToString() + "\r\n" + vResult.nextFactoryDate.ToString());
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

        /// <summary>
        /// 防止输入非数字
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBox_SealSpan_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 8 && !Char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        public void ProgressBarScaleDisplay(RailwayVehicleModel.vehicleGenType genType, int n)
        {


            List<Label> labGroup0 = new List<Label> { label27, label18, label19, label20 };
            List<Label> labGroup1 = new List<Label> { label11, label12, label13, label15, label21, label26 };
            List<Label> labGroup2 = new List<Label> { label22, label23, label24, label25, label28 };
            List<List<Label>> labLists = new List<List<Label>> { labGroup0, labGroup1, labGroup2 };

            Action<Label> actionFalse = delegate (Label i) { i.Visible = false; };
            Action<Label> actionTrue = delegate (Label j) { j.Visible = true; };

            switch (genType)
            {

                case vehicleGenType.Gen70t://通用C70
                    progressBar1.Value = n * (120 / 4);//vResult.n * 25;
                    labGroup0.ForEach(actionTrue);
                    labGroup1.ForEach(actionFalse);
                    labGroup2.ForEach(actionFalse);

                    break;
                case vehicleGenType.Gen60t:
                    progressBar1.Value = n * (120 / 6);
                    labGroup0.ForEach(actionFalse);
                    labGroup1.ForEach(actionTrue);
                    labGroup2.ForEach(actionFalse);
                    break;
                case vehicleGenType.Spc70t://n肯定为3
                    progressBar1.Value = n * (120 / 5);
                    labGroup0.ForEach(actionFalse);
                    labGroup1.ForEach(actionFalse);
                    labGroup2.ForEach(actionTrue);
                    break;

                default:
                    progressBar1.Value = 0;
                    break;

            }


        }


    }

}
