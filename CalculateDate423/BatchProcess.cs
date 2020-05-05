using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CalculateDate423;
using OfficeOpenXml;
using OfficeOpenXml.Drawing;
using OfficeOpenXml.Drawing.Chart;
using OfficeOpenXml.Style;

namespace RailwayVehicleRapairIntervalCompute
{
    public partial class BatchProcess : Form
    {
        public BatchProcess()
        {
            InitializeComponent();
        }

        private void buttonImportExcel_Click(object sender, EventArgs e)
        {
            var file = new System.IO.FileInfo(@"c:\temp\acc.xlsx");
            using (ExcelPackage package = new ExcelPackage(file))
            {

                ExcelRange cells = package.Workbook.Worksheets[0].Cells;


                for (int i = 2; i < 28; i++)
                {
                    int dataGridIndex = dataGridView1.Rows.Add();
                    for (int j = 1; j < 4; j++)
                    {
                        //cells[x,y] x为竖着的值，y是title
                        string s = cells[i, j].Value.ToString();

                        dataGridView1.Rows[dataGridIndex].Cells[j].Value = s;
                       
                    }
                }
                //显示到DataGridView



            }

        }

        private void buttonOutput_Click(object sender, EventArgs e)
        {

        }



        private void buttonGenerateData_Click(object sender, EventArgs e)
        {

            //应该直接计算了再显示上去吧，这样就很麻烦了
            //后期附加会不会很麻烦？

            //获取到当前row。直接附加在当前row后面，应该还好
            //先计算一个吧

            //先上核心
            RailwayVehicleModel model = new RailwayVehicleModel();
            model.GenTpSelection = (int)dataGridView1.Rows[0].Cells[0].Value;
            model.produceDate = (DateTime)dataGridView1.Rows[0].Cells[1].Value;
            model.previousDepotDate = (DateTime)dataGridView1.Rows[0].Cells[1].Value;
            model.previousFactoryDate = (DateTime)dataGridView1.Rows[0].Cells[1].Value;
            model.SealDuration = (int)dataGridView1.Rows[0].Cells[1].Value;
            RailwayVehicleModel vResult=VehicleData.DateProcessKernel(model);

            //再赋值给dataGridView后面几个格子
            //规定一下格子位置，显示什么内容

            //可以直接赋值了

            //TODO：思考输入内容的类型转换问题



        }
    }
}
