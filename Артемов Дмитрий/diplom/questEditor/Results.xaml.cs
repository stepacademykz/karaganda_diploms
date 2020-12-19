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
    /// Логика взаимодействия для Results.xaml
    /// </summary>
    public partial class Results : Window
    {
        static string connStr = @"Data Source=.\SQLExpress;Initial Catalog = testing; Integrated Security = True";
        DataSet ds = new DataSet();
        string sqlU = "SELECT name as 'Имя', surname as 'Фамилия', groupName as 'Группа', subject as 'Предмет', testTime as'Дата и время', testTry as 'Попыток', result as 'Результат, %' FROM results";

        public Results()
        {
            InitializeComponent();
            Fill(null, null);
            dgResults.ItemsSource = ds.Tables["results"].DefaultView;
        }

        private void NameSearch_Click(object sender, RoutedEventArgs e)
        {
            if (find.Text == "")
            {
                MessageBox.Show("Введите фамилию для поиска!");
            }
            else
            {
                ds.Clear();

                string sqlB = "SELECT name as 'Имя', surname as 'Фамилия', groupName as 'Группа', subject as 'Предмет', testTime as'Дата и время', testTry as 'Кол-во попыток', result as 'Результат, %' FROM results WHERE surname ='" + find.Text+"'";
                using (SqlConnection connection = new SqlConnection(connStr))
                {
                    connection.Open();
                    var adapB = new SqlDataAdapter(sqlB, connection);
                    adapB.Fill(ds, "results");
                }
                dgResults.ItemsSource = ds.Tables["results"].DefaultView;
            }
        }
        public void Fill(object sender, RoutedEventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connStr))
            {
                ds.Clear();
                connection.Open();
                var adapU = new SqlDataAdapter(sqlU, connection);
                adapU.Fill(ds, "results");
            }
        }
        private void FindAll_Click(object sender, RoutedEventArgs e)
        {
            Fill(null, null);
        }
    }
}
