using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
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


                for (int i = 1; i < 28; i++)
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
    }
}
