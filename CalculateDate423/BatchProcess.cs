using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CalcTest4RVRIP;
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


        /// <summary>
        /// 导入Excel并展示在dataGridView上
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonImportExcel_Click(object sender, EventArgs e)
        {
            var file = new System.IO.FileInfo(@"C:\Users\SANG-HP\Desktop\计划.xlsx");
            using (ExcelPackage package = new ExcelPackage(file))
            {

                ExcelRange cells = package.Workbook.Worksheets[0].Cells;

                for (int i = 2; i <= package.Workbook.Worksheets[0].Dimension.Rows; i++)
                {
                    int dataGridIndex = dataGridView1.Rows.Add();
                    for (int j = 1; j <= package.Workbook.Worksheets[0].Dimension.Columns; j++)
                    {
                        //cells[x,y] x为竖着的值，y是title

                        if (cells[i, j].Value != null)
                        {
                            dataGridView1.Rows[dataGridIndex].Cells[j - 1].Value = cells[i, j].Value.ToString();
                        }

                    }
                }
                //显示到DataGridView


            }

        }

        private void buttonOutput_Click(object sender, EventArgs e)
        {
            FileInfo file = new FileInfo(@"C:\Users\SANG-HP\Desktop\计划生成结果.xlsx");

            //通过IO创建文件myExcel
            if (file.Exists)
            {
                file.Delete();
                file = new FileInfo(@"C:\Users\SANG-HP\Desktop\计划生成结果.xlsx");
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
                for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                {
                    for (int j = 0; j < dataGridView1.Columns.Count; j++)
                    {
                        if (dataGridView1.Rows[i].Cells[j].Value != null)
                        {
                            worksheet.Cells[i + 2, j + 1].Value = dataGridView1.Rows[i].Cells[j].Value.ToString();
                        }
                        else
                        {
                            Exception ex = new Exception("数据为空！");
                        }
                    }
                }



                //设置单元格格式为文本
                worksheet.Cells["A:I"].Style.Numberformat.Format = "@";

                //save方法就保存我们这个对象，他就会去执行我们刚刚赋值的那些东西
                myExcelPackage.Save();
                MessageBox.Show("成功保存在：" + file.FullName);
            }
        }



        private void buttonGenerateData_Click(object sender, EventArgs e)
        {

            //应该直接计算了再显示上去吧，这样就很麻烦了
            //后期附加会不会很麻烦？

            //获取到当前row。直接附加在当前row后面，应该还好
            //先计算一个吧

            //先上核心
            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
            {
                RailwayVehicleModel model = new RailwayVehicleModel();
                //model.GenTpSelection = (int)dataGridView1.Rows[0].Cells[0].Value;
                //model.GenTpSelection要先做判断
                model.GenTpSelection = FixerTooling.GenTpJudge(dataGridView1.Rows[i].Cells[1].Value.ToString());
                model.produceDate = FixerTooling.FixProduceTime(dataGridView1.Rows[i].Cells[2].Value.ToString());
                model.previousFactoryDate = FixerTooling.FixRepairTime(dataGridView1.Rows[i].Cells[3].Value.ToString());
                model.previousDepotDate = FixerTooling.FixRepairTime(dataGridView1.Rows[i].Cells[4].Value.ToString());
                model.SealDuration = Convert.ToInt32(dataGridView1.Rows[i].Cells[5].Value.ToString());

                RailwayVehicleModel vResult = VehicleData.ReDateProcessKernel(model);
                if (vResult == null)
                {
                    continue;
                }
                //再赋值给dataGridView后面几个格子
                //规定一下格子位置，显示什么内容

                //可以直接赋值了

                //TODO：思考输入内容的类型转换问题
                dataGridView1.Rows[i].Cells[6].Value = vResult.currentDepotDate.ToShortDateString();
                dataGridView1.Rows[i].Cells[7].Value = vResult.vNextDepotDate.ToShortDateString();
                dataGridView1.Rows[i].Cells[8].Value = vResult.nextFactoryDate.ToShortDateString();
            }
        }


        /// <summary>
        /// 获取下载模板
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonGetTemplate_Click(object sender, EventArgs e)
        {
            FileInfo file = new FileInfo(@"C:\Users\SANG-HP\Desktop\计划填写模板.xlsx");

            //通过IO创建文件myExcel
            if (file.Exists)
            {
                file.Delete();
                file = new FileInfo(@"C:\Users\SANG-HP\Desktop\计划填写模板.xlsx");
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
                //设置单元格格式为文本
                worksheet.Cells["A:I"].Style.Numberformat.Format = "@";

                //save方法就保存我们这个对象，他就会去执行我们刚刚赋值的那些东西
                myExcelPackage.Save();
                MessageBox.Show("成功保存在：" + file.FullName);
            }

        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            Form bpForm = new Form1();
            bpForm.ShowDialog();

        }
    }
}

