using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OfficeOpenXml;
using OfficeOpenXml.Drawing;
using OfficeOpenXml.Drawing.Chart;
using OfficeOpenXml.Style;

namespace ExcelBatch
{
    public class Import
    {
        public void Imp()
        {
            var file = new System.IO.FileInfo(@"c:\temp\myWorkbook.xlsx");
            using (ExcelPackage package = new ExcelPackage(file))
            {
                ExcelRange cells = package.Workbook.Worksheets[0].Cells;
                for (int i = 0; i < 27; i++)
                {
                    string s = cells[3, 5].Value.ToString();
                }

               

            }
        }
    }
}
