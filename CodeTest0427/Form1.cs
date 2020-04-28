using CCWin;
using RailwayVehicleRapairIntervalCompute;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CodeTest0427
{
    public partial class Form1 : Skin_VS
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void skinButton1_Click(object sender, EventArgs e)
        {
            RailwayVehicleModel railwayVehicleModel = new RailwayVehicleModel();


            skinButton1.Text = "";
            railwayVehicleModel.warningInfo = "";
        }
    }
}
