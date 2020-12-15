using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using OfficeOpenXml;
using OfficeOpenXml.Drawing.Chart;
using OfficeOpenXml.Style;

namespace WpfApp1
{
    class ReportGeneratorWh
    {
        public static byte[] Generate(IEnumerable<ReportWhAccounting> WhRep, int Wnumber)
        {
            var package = new ExcelPackage();

            var sheet = package.Workbook.Worksheets
                .Add("Store Accounting Report");

            sheet.Cells["B2"].Value = "Warehouse number:";
            sheet.Cells["C2"].Value = $"{Wnumber} ";

            sheet.Cells[3, 2].Value = "Name";
            sheet.Cells[3, 3].Value = "Cost";
            sheet.Cells[3, 4].Value = "Quantity";
            sheet.Cells[3, 5].Value = "Sum";

            sheet.Column(2).Width = 20;
            sheet.Column(3).Width = 20;
            sheet.Column(4).Width = 20;
            sheet.Column(5).Width = 20;

            int value=0;
            int count = 0;
            var row = 4;
            var column = 2;
            foreach (var item in WhRep)
            {
                sheet.Cells[row, column].Value = item.Name;
                sheet.Cells[row, column + 1].Value = item.Cost;
                sheet.Cells[row, column + 2].Value = item.Quantity;
                sheet.Cells[row, column + 3].Value = item.Sum;
                if(item.Quantity !=null)
                    value +=(int) item.Sum;
                count++;
                row++;
            }
            sheet.Cells[row, 4].Value = "All:";
            sheet.Cells[row, 5].Value = $"{value} ";
            sheet.Cells[row, 4, row, 5].Style.Border.BorderAround(ExcelBorderStyle.Double);
            

            sheet.Cells[3, 2, 3 + count, 5].Style.Border.BorderAround(ExcelBorderStyle.Double);
            sheet.Cells[3, 2, 3, 5].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

            sheet.Protection.IsProtected = true;
            return package.GetAsByteArray();
        }
    }
}
