using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            prod_up();
            store_up();
            warehouse_up();
            storeacc_up();
            whouseacc_up();

        }


        //Getting data from cell
        private string getDataFromCell(int selectedColumn, DataGrid Grid)
        {

            var selectedCell = Grid.SelectedCells[selectedColumn];
            var cellContent = selectedCell.Column.GetCellContent(selectedCell.Item);
         
                return (cellContent as TextBlock).Text;
        }
        //Getting data from DataGridComboBoxColumn cell (storeAccounting)
        private string getDataFromCellstore(int selectedColumn, DataGrid Grid)
        {
            DataGridCellInfo cells = Grid.SelectedCells[selectedColumn];
            var inf = cells.Item as StAccDop;
            if (selectedColumn == 0)
            {
                int valueID = 0;
                using (ProductDBContext db = new ProductDBContext())
                {
                    var data = db.Products.ToList();

                    foreach (var item in data)
                    {
                        if (item.Name == inf.Name) valueID = item.ProductId;
                    }

                }

                return valueID.ToString();
            }
            else
            {
                int valueID = 0;
                using (ProductDBContext db = new ProductDBContext())
                {
                    var data = db.Stores.ToList();

                    foreach (var item in data)
                    {
                        if (item.Sname == inf.Sname) valueID = item.StoreId;
                    }

                }

                return valueID.ToString();
            }
        }
        //Getting data from DataGridComboBoxColumn cell (wareHouseAccounting)
        private string getDataFromCellwh(int selectedColumn, DataGrid Grid)
        {
            DataGridCellInfo cells = Grid.SelectedCells[selectedColumn];
            var inf = cells.Item as WhAccDop;
            if (selectedColumn == 0) {
                int valueID = 0;
                using (ProductDBContext db = new ProductDBContext())
                {
                    var data = db.Products.ToList();
                    
                    foreach (var item in data)
                    {
                        if (item.Name == inf.Name) valueID = item.ProductId;
                    }

                }

            return valueID.ToString(); }
            else
            {
                int valueID = 0;
                using (ProductDBContext db = new ProductDBContext())
                {
                    var data = db.Warehouses.ToList();

                    foreach (var item in data)
                    {
                        if (item.Wnumber == inf.Wnumber) valueID = item.WhousesId;
                    }

                }

                return valueID.ToString();
            }

        }



        //PRODUCT
        private void updateButtonProduct_Click(object sender, RoutedEventArgs e)
        {
            prod_up();
        }
        private void prod_up()
        {
            using (ProductDBContext db = new ProductDBContext())
            {
                var data = db.Products.ToList();
                productGrid.ItemsSource = data;
            }
        }

        private void deleteButtonProduct_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string inf = getDataFromCell(0, productGrid);
                if (inf != null)
                {
                    using (ProductDBContext db = new ProductDBContext())
                    {
                        db.Database.ExecuteSqlRaw("EXECUTE del_Products {0};", inf);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Только что произошло обработанное исключение: " + ex.Message, "Exception Sample", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            finally
            {
                prod_up();
            }
        }

        private void upinfButtonProduct_Click(object sender, RoutedEventArgs e)
        {
            try {             
                using (ProductDBContext db = new ProductDBContext())
                {
                    db.Database.ExecuteSqlRaw("EXECUTE up_Products  {0},{1},{2},{3};", getDataFromCell(0, productGrid), getDataFromCell(1, productGrid), getDataFromCell(2, productGrid), getDataFromCell(3, productGrid));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Только что произошло обработанное исключение: " + ex.Message, "Exception Sample", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            finally
            {
                prod_up();
            }
        }

        private void insertButtonProduct_Click(object sender, RoutedEventArgs e)
        {
            try { 
                using (ProductDBContext db = new ProductDBContext())
                {
                    db.Database.ExecuteSqlRaw("EXECUTE ins_Products  {0},{1},{2};", getDataFromCell(1, productGrid), getDataFromCell(2, productGrid), getDataFromCell(3, productGrid));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Только что произошло обработанное исключение: " + ex.Message, "Exception Sample", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            finally
            {
                prod_up();
            }
        }


        //STORE
        private void updateButtonStore_Click(object sender, RoutedEventArgs e)
        {
            store_up();
        }

        private void store_up()
        {
            using (ProductDBContext db = new ProductDBContext())
            {
                var data = db.Stores.ToList();
                storeGrid.ItemsSource = data;
            }
        }

        private void insertButtonStore_Click(object sender, RoutedEventArgs e)
        {
            try { 
                using (ProductDBContext db = new ProductDBContext())
                {
                    db.Database.ExecuteSqlRaw("EXECUTE ins_Stores  {0},{1},{2},{3};", getDataFromCell(1, storeGrid), getDataFromCell(2, storeGrid), getDataFromCell(3, storeGrid), getDataFromCell(4, storeGrid));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Только что произошло обработанное исключение: " + ex.Message, "Exception Sample", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            finally
            {
                store_up();
            }
        }

        private void upinfButtonStore_Click(object sender, RoutedEventArgs e)
        {
            try { 
                using (ProductDBContext db = new ProductDBContext())
                {
                    db.Database.ExecuteSqlRaw("EXECUTE up_Stores  {0},{1},{2},{3},{4};", getDataFromCell(0, storeGrid), getDataFromCell(1, storeGrid), getDataFromCell(2, storeGrid), getDataFromCell(3, storeGrid), getDataFromCell(4, storeGrid));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Только что произошло обработанное исключение: " + ex.Message, "Exception Sample", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            finally
            {
                store_up();
            }

        }

        private void deleteButtonStore_Click(object sender, RoutedEventArgs e)
        {
            try { 
                string inf = getDataFromCell(0, storeGrid);
                if (inf != null)
                {
                    using (ProductDBContext db = new ProductDBContext())
                    {
                        db.Database.ExecuteSqlRaw("EXECUTE del_Stores {0};", inf);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Только что произошло обработанное исключение: " + ex.Message, "Exception Sample", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            finally
            {
                store_up();
            }
        }

        //Warehouse
        private void updateButtonWarehouse_Click(object sender, RoutedEventArgs e)
        {
            warehouse_up();
        }

        private void warehouse_up()
        {
            using (ProductDBContext db = new ProductDBContext())
            {
                var data = db.Warehouses.ToList();
                warehouseGrid.ItemsSource = data;
            }
        }

        private void insertButtonWarehouse_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (ProductDBContext db = new ProductDBContext())
                {
                    db.Database.ExecuteSqlRaw("EXECUTE ins_Warehouses  {0},{1},{2},{3},{4};", getDataFromCell(1, warehouseGrid), getDataFromCell(2, warehouseGrid), getDataFromCell(3, warehouseGrid), getDataFromCell(4, warehouseGrid), getDataFromCell(5, warehouseGrid));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Только что произошло обработанное исключение: " + ex.Message, "Exception Sample", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            finally
            {
                warehouse_up();
            }
        }

        private void upinfButtonWarehouse_Click(object sender, RoutedEventArgs e)
        {
            try { 
                using (ProductDBContext db = new ProductDBContext())
                {
                    db.Database.ExecuteSqlRaw("EXECUTE up_Warehouses  {0},{1},{2},{3},{4},{5};", getDataFromCell(0, warehouseGrid), getDataFromCell(1, warehouseGrid), getDataFromCell(2, warehouseGrid), getDataFromCell(3, warehouseGrid), getDataFromCell(4, warehouseGrid), getDataFromCell(5, warehouseGrid));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Только что произошло обработанное исключение: " + ex.Message, "Exception Sample", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            finally
            {
                warehouse_up();
            }
        }

        private void deleteButtonWarehouse_Click(object sender, RoutedEventArgs e)
        {
            try { 
                string inf = getDataFromCell(0, warehouseGrid);
                if (inf != null)
                {
                    using (ProductDBContext db = new ProductDBContext())
                    {
                        db.Database.ExecuteSqlRaw("EXECUTE del_Stores {0};", inf);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Только что произошло обработанное исключение: " + ex.Message, "Exception Sample", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            finally
            {
                warehouse_up();
            }
        }


        //StoreAccounting
        private void updateButtonStoreAccounting_Click(object sender, RoutedEventArgs e)
        {
            storeacc_up();
        }

        private void storeacc_up()
        {
            using (ProductDBContext db = new ProductDBContext())
            {
                var data = db.StoreAccountings.ToList();
                var data2 = db.Products.ToList();
                var data3 = db.Stores.ToList();
                var data4 = new List<StAccDop>();

                foreach (var item in data)
                {
                    var it = new StAccDop
                    {
                        Name = item.Product.Name,
                        Sname = item.Store.Sname,
                        Quantity = item.Quantity,
                        Cost = item.Cost
                    };
                    data4.Add(it);
                }
                StoreAccountingReportComboBox.DisplayMemberPath = "Sname";
                StoreAccountingReportComboBox.ItemsSource = data3;
                comboBoxStPr.ItemsSource = data2;
                comboBoxStSt.ItemsSource = data3;
                storeaccountingGrid.ItemsSource = data4;


            }
        }

        private void insertButtonStoreAccounting_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (ProductDBContext db = new ProductDBContext())
                {
                    db.Database.ExecuteSqlRaw("EXECUTE ins_StoreAccounting  {0},{1},{2},{3};", getDataFromCellstore(0, storeaccountingGrid), getDataFromCellstore(1, storeaccountingGrid), getDataFromCell(2, storeaccountingGrid), getDataFromCell(3, storeaccountingGrid));
                }
                
            }
            catch (Exception ex) {
                MessageBox.Show("A handled exception just occurred: " + ex.Message, "Exception Sample", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            finally { storeacc_up(); }
           // Microsoft.Data.SqlClient.SqlException
        }

        private void upinfButtonStoreAccounting_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (ProductDBContext db = new ProductDBContext())
                {
                    db.Database.ExecuteSqlRaw("EXECUTE up_StoreAccounting  {0},{1},{2},{3};", getDataFromCellstore(0, storeaccountingGrid), getDataFromCellstore(1, storeaccountingGrid), getDataFromCell(2, storeaccountingGrid), getDataFromCell(3, storeaccountingGrid));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("A handled exception just occurred: " + ex.Message, "Exception Sample", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            finally { storeacc_up(); }
        }

        private void deleteButtonStoreAccounting_Click(object sender, RoutedEventArgs e)
        {
            try {
                string inf = getDataFromCellstore(0, storeaccountingGrid);
                string inf2 = getDataFromCellstore(1, storeaccountingGrid);
                if (inf != null)
                {
                    using (ProductDBContext db = new ProductDBContext())
                    {
                        db.Database.ExecuteSqlRaw("EXECUTE del_StoreAccounting {0},{1};", inf,inf2);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("A handled exception just occurred: " + ex.Message, "Exception Sample", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            finally { storeacc_up(); }
        }


        //WarehouseAccounting
        private void updateButtonWhousesAccounting_Click(object sender, RoutedEventArgs e)
        {
            whouseacc_up();
        }

        private void whouseacc_up()
        {
            using (ProductDBContext db = new ProductDBContext())
            {
                var data = db.WarehouseAccountings.ToList();
                var data2 = db.Products.ToList();
                var data3 = db.Warehouses.ToList();
                var data4 = new List<WhAccDop>();    

                foreach (var item in data)
                {
                    var it = new WhAccDop
                    {
                        Name=item.Product.Name,
                        Wnumber=item.Whouses.Wnumber,
                        Quantity=item.Quantity,
                        Cost= item.Cost
                    };
                    data4.Add(it);
                }
                WhAccountingReportComboBox.DisplayMemberPath = "Wnumber";
                WhAccountingReportComboBox.ItemsSource = data3;
                comboBoxWhPr.ItemsSource = data2;
                comboBoxWhWh.ItemsSource = data3;

                warehouseaccountingGrid.ItemsSource = data4;
            }
        }

        private void insertButtonWhousesAccounting_Click(object sender, RoutedEventArgs e)
        {
            try { 
                using (ProductDBContext db = new ProductDBContext())
                {
                    db.Database.ExecuteSqlRaw("EXECUTE ins_WarehouseAccounting  {0},{1},{2},{3};", getDataFromCellwh(0, warehouseaccountingGrid), getDataFromCellwh(1, warehouseaccountingGrid), getDataFromCell(2, warehouseaccountingGrid), getDataFromCell(3, warehouseaccountingGrid));
                }
            }
            catch (Exception ex) {
                MessageBox.Show("Только что произошло обработанное исключение: " + ex.Message, "Exception Sample", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            finally {
                whouseacc_up();
            }
        }

        private void upinfButtonWhousesAccounting_Click(object sender, RoutedEventArgs e)
        {
            try { 
                using (ProductDBContext db = new ProductDBContext())
                {
                    db.Database.ExecuteSqlRaw("EXECUTE up_WarehouseAccounting  {0},{1},{2},{3};", getDataFromCellwh(0, warehouseaccountingGrid), getDataFromCellwh(1, warehouseaccountingGrid), getDataFromCell(2, warehouseaccountingGrid), getDataFromCell(3, warehouseaccountingGrid));
                }
             }
            catch (Exception ex) {
                MessageBox.Show("Только что произошло обработанное исключение: " + ex.Message, "Exception Sample", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            finally {
                whouseacc_up();
            }
        }

        private void deleteButtonWhousesAccounting_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string inf = getDataFromCellwh(0, warehouseaccountingGrid);
                string inf2 = getDataFromCellwh(1, warehouseaccountingGrid);
                if (inf != null)
                {
                    using (ProductDBContext db = new ProductDBContext())
                    {
                        db.Database.ExecuteSqlRaw("EXECUTE del_WarehouseAccounting {0},{1};", inf, inf2);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Только что произошло обработанное исключение: " + ex.Message, "Exception Sample", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            finally
            {
                whouseacc_up();
            }
        }

        private void StoreAccountingReportButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                
                using (ProductDBContext db = new ProductDBContext())
                {
                    var storev = StoreAccountingReportComboBox.SelectedItem as Store;
                    var storeRep = db.StoreAccountings.Where(p => p.StoreId == storev.StoreId).Join(db.Products, // второй набор
                    u => u.ProductId, // свойство-селектор объекта из первого набора
                    c => c.ProductId, // свойство-селектор объекта из второго набора
                    (u, c) => new  ReportStoreAccounting// результат
                    {
                        Name = c.Name,
                        Quantity = u.Quantity,
                        Cost = u.Cost
                    }).OrderBy(p => p.Name);

                    var reportExcel = ReportGenerator.Generate(storeRep, storev.Sname);
                    File.WriteAllBytes("../../../../ReportStoreAccounting.xlsx", reportExcel);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Только что произошло обработанное исключение: " + ex.Message, "Exception Sample", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void WhAccountingReportButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                using (ProductDBContext db = new ProductDBContext())
                {
                    var Whv = WhAccountingReportComboBox.SelectedItem as Warehouse;
                    var whRep = db.WarehouseAccountings.Where(p => p.WhousesId == Whv.WhousesId).Join(db.Products, // второй набор
                    u => u.ProductId, // свойство-селектор объекта из первого набора
                    c => c.ProductId, // свойство-селектор объекта из второго набора
                    (u, c) => new ReportWhAccounting// результат
                    {
                        Name = c.Name,
                        Quantity = u.Quantity,
                        Cost = u.Cost,
                        Sum = u.Quantity* u.Cost
                    }).OrderBy(p => p.Name);

                    var reportExcel = ReportGeneratorWh.Generate(whRep, Whv.Wnumber);
                    File.WriteAllBytes("../../../../ReportWhAccounting.xlsx", reportExcel);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Только что произошло обработанное исключение: " + ex.Message, "Exception Sample", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }

   
}
