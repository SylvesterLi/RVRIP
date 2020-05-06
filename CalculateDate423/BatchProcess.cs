using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CalculateDate423;
using OfficeOpenXml;


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
            var file = new System.IO.FileInfo(@"C:\Users\SANG-HP\Desktop\myExcel.xlsx");
            using (ExcelPackage package = new ExcelPackage(file))
            {

                ExcelRange cells = package.Workbook.Worksheets[0].Cells;


                for (int i = 2; i < cells.Rows; i++)
                {
                    int dataGridIndex = dataGridView1.Rows.Add();
                    for (int j = 1; j < 7; j++)
                    {
                        //cells[x,y] x为竖着的值，y是title
                        string s = cells[i, j].Value.ToString();

                        dataGridView1.Rows[dataGridIndex].Cells[j-1].Value = s;

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
            RailwayVehicleModel vResult = VehicleData.DateProcessKernel(model);

            //再赋值给dataGridView后面几个格子
            //规定一下格子位置，显示什么内容

            //可以直接赋值了

            //TODO：思考输入内容的类型转换问题



        }

        private void buttonGetTemplate_Click(object sender, EventArgs e)
        {
            FileInfo file = new FileInfo(@"C:\Users\SANG-HP\Desktop\myExcel.xlsx");

            //通过IO创建文件myExcel
            if (file.Exists)
            {
                file.Delete();
                file = new FileInfo(@"C:\Users\SANG-HP\Desktop\myExcel.xlsx");
            }
            //创建ExcelPackage对象，这个对象是面对工作簿的，就是里面的所有
            using (ExcelPackage myExcelPackage = new ExcelPackage(file))
            {
                //创建ExcelWorkSheet对象，这个对象就是面对表的，是工作簿中单个表
                ExcelWorksheet worksheet = myExcelPackage.Workbook.Worksheets.Add("Sheet1");
                //坐标1，1赋值A1就是相当于在Excel中的A1位置赋值了一个A1字符串。
                for (int i = 0; i < dataGridView1.Rows[0].Cells.Count; i++)
                {
                    worksheet.Cells[1, i + 1].Value = dataGridView1.Columns[i].HeaderCell.Value;
                }
                //save方法就保存我们这个对象，他就会去执行我们刚刚赋值的那些东西
                myExcelPackage.Save();
                MessageBox.Show("成功保存");
            }

        }


    }
}

