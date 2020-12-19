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

namespace questEditor
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void EditPhys_Click(object sender, RoutedEventArgs e)
        {
            EditPhys editPhys = new EditPhys();
            editPhys.ShowDialog();

        }

        private void EditIt_Click(object sender, RoutedEventArgs e)
        {
            EditIt editIt = new EditIt();
            editIt.ShowDialog();

        }


        private void Results_Click(object sender, RoutedEventArgs e)
        {
            Results res = new Results();
            res.ShowDialog();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        
    }
}
