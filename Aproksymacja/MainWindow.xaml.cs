using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
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

namespace Aproksymacja
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            // Klasa logiki wyliczająca dane
            Apro ap = new Apro();

            // Generowanie widoku tabeli funkcji
            ObservableCollection<FuncHelper> funcData = new ObservableCollection<FuncHelper>();
            for (int i =0; i < Apro.f.Count();i++)
            {
                funcData.Add(new FuncHelper() { X = i, Y = Apro.f[i] });
            }
            DataFunc.ItemsSource = funcData;
           
            //Generowanie widoku tabeli aproksymacji funkcji
            DataD.ItemsSource = generateDataTable(ap.runD()).DefaultView;
            DataA.ItemsSource = generateDataTable(ap.runA()).DefaultView;
        }
        
        /// <summary>
        /// Funkcja generująca tablicę danych do widoku
        /// na podstawie gotowych danych wejściowych
        /// </summary>
        public DataTable generateDataTable(int[,] input)
        {
            DataTable Data = new DataTable();

            // Tworzenie kolumn
            Data.Columns.Add("X");
            for (int i = 0; i < Apro.Ydiff; i++)
            {
                Data.Columns.Add("Y" + i);
            }

            //Tworzenie wierszy
            for (int ix = 0; ix < Apro.Xdiff; ix++)
            {
                var data = new object[Data.Columns.Count];
                data[0] = ix;
                for (int iy = 0; iy < Apro.Ydiff; iy++)
                {
                    data[iy + 1] = (input[ix, iy]);
                }
                Data.Rows.Add(data);
            }

            return Data;
        }
    }
}
