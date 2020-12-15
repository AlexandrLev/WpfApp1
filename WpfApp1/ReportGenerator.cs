using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OfficeOpenXml;
using OfficeOpenXml.Drawing.Chart;
using OfficeOpenXml.Style;

namespace WpfApp1
{
    class ReportGenerator
    {
        public static byte[] Generate(IEnumerable<ReportStoreAccounting> storeRep, string Sname)
        {
            var package = new ExcelPackage();

            var sheet = package.Workbook.Worksheets
                .Add("Store Accounting Report");

            sheet.Cells["B2"].Value = "Store name:";
            sheet.Cells["C2"].Value = Sname;

            sheet.Cells[3, 2].Value = "Name";
            sheet.Cells[3, 3].Value = "Cost";
            sheet.Cells[3, 4].Value = "Quantity";

            sheet.Column(2).Width = 20;
            sheet.Column(3).Width = 20;
            sheet.Column(4).Width = 20;

            int value=0;
            int count = 0;
            var row = 4;
            var column = 2;
            foreach (var item in storeRep)
            {
                sheet.Cells[row, column].Value = item.Name;
                sheet.Cells[row, column + 1].Value = item.Cost;
                sheet.Cells[row, column + 2].Value = item.Quantity;
                if(item.Quantity !=null)
                    value +=(int) item.Quantity;
                count++;
                row++;
            }
            sheet.Cells[row, 3].Value = "All:";
            sheet.Cells[row, 4].Value = $"{value} ";
            sheet.Cells[row, 3, row, 4].Style.Border.BorderAround(ExcelBorderStyle.Double);
            
            sheet.Cells[3, 2, 3 + count, 4].Style.Border.BorderAround(ExcelBorderStyle.Double);
            sheet.Cells[3, 2, 3, 4].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

            sheet.Protection.IsProtected = true;
            return package.GetAsByteArray();
        }

      
    }
}
