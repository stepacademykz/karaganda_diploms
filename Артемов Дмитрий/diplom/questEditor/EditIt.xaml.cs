using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
using System.Windows.Shapes;

namespace questEditor
{
    /// <summary>
    /// Логика взаимодействия для Edit.xaml
    /// </summary>
    public partial class EditIt : Window
    {
        static string connStr = @"Data Source=.\SQLExpress;Initial Catalog = testing; Integrated Security = True";
        DataSet ds = new DataSet();
        string sqlQ = "SELECT * FROM itQuest";

        public EditIt()
        {
            InitializeComponent();
            Fill(null, null);
            dgEdit.ItemsSource = ds.Tables["itQuest"].DefaultView;

        }

        public void Fill(object sender, RoutedEventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connStr))
            {
                ds.Clear();
                connection.Open();
                var adapQ = new SqlDataAdapter(sqlQ, connection);
                adapQ.Fill(ds, "itQuest");
            }
        }

        private void SaveEdit_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Сохранить?", "Сохранение изменений", MessageBoxButton.YesNo, MessageBoxImage.Question) ==
    MessageBoxResult.Yes)
            {
                try
                {
                    var adapterQ = new SqlDataAdapter(sqlQ, connStr);
                    var commandBuilderB = new SqlCommandBuilder(adapterQ);
                    adapterQ.Update(ds.Tables["itQuest"]);
                    MessageBox.Show("Изменения успешно сохранены", "Успех", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                    Fill(null, null);
                }
                catch
                {
                    MessageBox.Show("Введены неверные значения! Убедитесь в правильности вводимых данных");
                    Fill(null, null);
                }
            }
            else
            {
                MessageBox.Show("Изменения не были приняты", "Отмена", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        public void Exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

    }
}
