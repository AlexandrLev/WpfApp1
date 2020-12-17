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
    class ReportGeneratorWh
    {
        public static byte[] Generate(List<Warehouse> data3)
        {
            var package = new ExcelPackage();

            var sheet = package.Workbook.Worksheets
                .Add("Store Accounting Report");

            int vall = 0;
            using (ProductDBContext db = new ProductDBContext())
            {
                
                foreach (var Whv in data3)
                {
                    var whRep = db.WarehouseAccountings.Where(p => p.WhousesId == Whv.WhousesId).Join(db.Products, // второй набор
                           u => u.ProductId, // свойство-селектор объекта из первого набора
                           c => c.ProductId, // свойство-селектор объекта из второго набора
                           (u, c) => new ReportWhAccounting// результат
                           {
                               Name = c.Name,
                               Quantity = u.Quantity,
                               Cost = u.Cost,
                               Sum = u.Quantity * u.Cost
                           }).OrderBy(p => p.Name);


                    sheet.Cells[2 +(10*vall), 2].Value = "Warehouse number:";
                    sheet.Cells[2 + (10 * vall), 3].Value = $"{Whv.Wnumber} ";

                    sheet.Cells[3 + (10 * vall), 2].Value = "Name";
                    sheet.Cells[3 + (10 * vall), 3].Value = "Cost";
                    sheet.Cells[3 + (10 * vall), 4].Value = "Quantity";
                    sheet.Cells[3 + (10 * vall), 5].Value = "Sum";

                    sheet.Column(2).Width = 20;
                    sheet.Column(3).Width = 20;
                    sheet.Column(4).Width = 20;
                    sheet.Column(5).Width = 20;

                    int value = 0;
                    int count = 0;
                    var row = 4;
                    var column = 2;
                    foreach (var item in whRep)
                    {
                        sheet.Cells[row + (10 * vall), column].Value = item.Name;
                        sheet.Cells[row + (10 * vall), column + 1].Value = item.Cost;
                        sheet.Cells[row + (10 * vall), column + 2].Value = item.Quantity;
                        sheet.Cells[row + (10 * vall), column + 3].Value = item.Sum;
                        if (item.Quantity != null)
                            value += (int)item.Sum;
                        count++;
                        row++;
                    }
                    sheet.Cells[row + (10 * vall), 4].Value = "All:";
                    sheet.Cells[row + (10 * vall), 5].Value = $"{value} ";
                    sheet.Cells[row + (10 * vall), 4, row + (10 * vall), 5].Style.Border.BorderAround(ExcelBorderStyle.Double);


                    sheet.Cells[3 + (10 * vall), 2, 3 + (10 * vall) + count, 5].Style.Border.BorderAround(ExcelBorderStyle.Double);
                    sheet.Cells[3 + (10 * vall), 2, 3 + (10 * vall), 5].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    vall++;
                }
            }
            sheet.Protection.IsProtected = true;
            return package.GetAsByteArray();
        }
    }
}
