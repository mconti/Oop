/*
 * Mini serie di esempi su ereditarietà e polimorfismo.
 * maurizio.conti@ittsrimini.edu.it - Settembre 2020
 */

using System;
using System.Collections.Generic;
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

namespace Step3
{
    /// <summary>
    /// Logica di interazione per MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            dgDati.ItemsSource = new Persone("Persone.csv"); ;
        }

        private void dgDati_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            if ( e.Row.Item is IDT  )
                e.Row.Background = Brushes.LightGreen;

            else if (e.Row.Item is ITP)
                e.Row.Background = Brushes.LightYellow;
        }
    }
}
