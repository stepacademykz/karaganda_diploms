using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace diplom
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public User user = new User();
        static string connStr = @"Data Source=.\SQLExpress;Initial Catalog = testing; Integrated Security = True";
        DataSet ds = new DataSet();
        DataSet dsUser = new DataSet();
        DataSet dsRes = new DataSet();
        public int questCount = 1;
        public int halfHour = 30;
        public string trueAnswer = "";
        public int rightAnswers = 0;
        public int checkAnswer = 0;
        public int start;
        public int finish;
        public string questBase;
        public int questMax = 5;
        ImageBrush myBrush = new ImageBrush();

        public MainWindow()
        {
            InitializeComponent();
            mw.WindowState = WindowState.Normal;
            name.Focus();
        }
        private void Subject_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedItem = (TextBlock)subject.SelectedItem;
            user.Subject = selectedItem.Text.ToString();
            if (user.Subject == "Физика")
            {
                questBase = "SELECT * FROM physicsQuest WHERE id=";
                myBrush.ImageSource = new BitmapImage(new Uri(Environment.CurrentDirectory + @"/Image/1.jpeg", UriKind.Absolute));
            }
            else
            {
                myBrush.ImageSource = new BitmapImage(new Uri(Environment.CurrentDirectory + @"/Image/2.jpg", UriKind.Absolute));
                //im.Source = new BitmapImage(new Uri(@"/Image/2.jpg", UriKind.Relative));
                //ib.ImageSource = new BitmapImage(new Uri(@"Image\2.jpg", UriKind.Relative));
                questBase = "SELECT * FROM itQuest WHERE id=";

            }
        }

        private void BeginTest_Click(object sender, RoutedEventArgs e)
        {
            Background = myBrush;
            user.Name = name.Text;
            user.Surname = surname.Text;
            user.Group = group.Text;
            user.Time = DateTime.Now;
            if (user.Name == "" || user.Surname == "" || user.Group == "" || user.Subject == null)
            {
                MessageBox.Show("Для начала тестирования заполните все поля!");
            }
            else
            {
                Check_User(null, null);
                Turn_Test(null,null);
                start = DateTime.Now.Minute;
                Fill(null, null);
            }
        }

        private void Action_Click(object sender, RoutedEventArgs e)
        {
            if (rb1.IsChecked == false && rb2.IsChecked == false && rb3.IsChecked == false && rb4.IsChecked == false)
            {
                MessageBox.Show("Выберите вариант ответа!");
            }
            else
            {
                if (checkAnswer == 1)
                {
                    rightAnswers++;
                }
                questCount++;
                Fill(null, null);
            }
        }

        public void Fill(object sender, RoutedEventArgs e)
        {
            if (questCount > questMax)
            {
                user.TestTry--;
                if (MessageBox.Show("Тест окончен! Правильных ответов - " + rightAnswers * 100 / (questCount - 1) + "%. Осталось попыток - " + (user.TestTry) + " Повторить тест?", "Результат теста", MessageBoxButton.YesNo, MessageBoxImage.Information) == MessageBoxResult.No)
                {
                    Save_Result(null, null);
                }
                else
                {
                    start = DateTime.Now.Minute;
                    if (user.TestTry == 0)
                    {
                        MessageBox.Show("У вас закончились попытки!", "Тест окончен", MessageBoxButton.OK, MessageBoxImage.Error);
                        Save_Result(null, null);
                    }
                    rightAnswers = 0;
                    questCount = 1;
                    Fill(null, null);

                }
            }
            finish = DateTime.Now.Minute;
            if (start <= halfHour)
            {
                time.Content = "Осталось " + (start - finish + halfHour) + " минут";
                if (finish >= start + halfHour)
                {
                    MessageBox.Show("Время вышло. Тест окончен!");
                    user.TestTry--;
                    questCount = ++questMax;

                    Save_Result(null, null);
                }
            }
            else if (start >= halfHour)
            {
                time.Content = "Осталось " + (start - finish + halfHour) + " минут";

                if (finish <= start - halfHour)
                {
                    MessageBox.Show("Время вышло. Тест окончен!");
                    user.TestTry--;
                    questCount = ++questMax;

                    Save_Result(null, null);
                }
            }

            using (SqlConnection connection = new SqlConnection(connStr))
            {
                connection.Open();
                var adapB = new SqlDataAdapter(questBase + questCount, connection);
                adapB.Fill(ds, "Quest");
                foreach (DataTable dt in ds.Tables)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        var cells = row.ItemArray;
                        lb.Content = cells[1].ToString();
                        rb1.Content = cells[2].ToString();
                        rb2.Content = cells[3].ToString();
                        rb3.Content = cells[4].ToString();
                        rb4.Content = cells[5].ToString();
                        trueAnswer = cells[6].ToString();
                    }
                }
                rb1.IsChecked = false;
                rb2.IsChecked = false;
                rb3.IsChecked = false;
                rb4.IsChecked = false;
            }
        }

        private void Answer_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton pressed = (RadioButton)sender;
            if (pressed.Content.ToString() == trueAnswer)
            {
                checkAnswer = 1;
            }
            else
            {
                checkAnswer = 0;
            }
        }

        private void End_test(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы уверены?", "Закончить тестирование", MessageBoxButton.YesNo, MessageBoxImage.Question) ==
                MessageBoxResult.Yes)
            {
                user.TestTry--;
                questCount = ++questMax;
                Save_Result(null, null);
            }
        }

        private void Turn_Test(object sender, RoutedEventArgs e)
        {
            testSt.Visibility = Visibility.Visible;
            loginSt.Visibility = Visibility.Collapsed;
            mw.WindowState = WindowState.Maximized;
        }
        private void Turn_Login(object sender, RoutedEventArgs e)
        {
            mw.WindowState = WindowState.Normal;
            questCount = 1;
            trueAnswer = "";
            rightAnswers = 0;
            user.TestTry = 3;
            testSt.Visibility = Visibility.Collapsed;
            loginSt.Visibility = Visibility.Visible;
            Results_Click(null, null);
        }

        private void Save_Result(object sender, RoutedEventArgs e)
        {
            if (rightAnswers != 0)
            {
                user.Result = rightAnswers * 100 / (questCount - 1);
            }
            else
            {
                user.Result = 0;
            }

            string sqlCheck = "SELECT name,surname,subject FROM results WHERE name ='" + user.Name + "' AND surname='" + user.Surname + "' and subject = '" + user.Subject + "'";
            using (SqlConnection connection = new SqlConnection(connStr))
            {
                connection.Open();
                var adapB = new SqlDataAdapter(sqlCheck, connection);
                adapB.Fill(dsUser, "results");
            }
            object[] cell = new object[3];
            foreach (DataTable dt in dsUser.Tables)
            {
                foreach (DataRow row in dt.Rows)
                {
                    cell = row.ItemArray;
                }
            }

            if (cell[0] == null)
            {
                using (SqlConnection connection = new SqlConnection(connStr))
                {
                    connection.Open();
                    var adapB = new SqlDataAdapter("INSERT INTO results VALUES('" + user.Name + "','" + user.Surname + "','" + user.Group + "','" + user.Subject + "','" + user.Time + "'," + user.TestTry + "," + user.Result + ")", connection);
                    adapB.Fill(dsUser, "results");
                }
            }
            else
            {
                string sqlUpdate = "UPDATE results SET testTry = " + user.TestTry + ",result="+user.Result+", testTime ='"+user.Time+"' WHERE name = '" + user.Name + "' AND surname = '" + user.Surname + "' and subject = '" + user.Subject + "'";
                using (SqlConnection connection = new SqlConnection(connStr))
                {
                    connection.Open();
                    var adapUp = new SqlDataAdapter(sqlUpdate, connection);
                    adapUp.Fill(dsUser, "results");
                }
            }
            dsUser.Clear();
            MessageBox.Show("Результаты тестирования успешно сохранены");
            Turn_Login(null, null);
        }

        private void Check_User(object sender, RoutedEventArgs e)
        {
            string sqlCheck = "SELECT testTry FROM results WHERE name ='" + user.Name + "' AND surname='"+user.Surname+ "' and subject = '"+user.Subject+"'";
            using (SqlConnection connection = new SqlConnection(connStr))
            {
                connection.Open();
                var adapB = new SqlDataAdapter(sqlCheck, connection);
                adapB.Fill(dsUser, "results");
            }
            foreach (DataTable dt in dsUser.Tables)
            {
                foreach (DataRow row in dt.Rows)
                {
                    var cells = row.ItemArray;
                    if (cells[0].ToString() != user.TestTry.ToString())
                    {
                        user.TestTry = Convert.ToInt32(cells[0]);
                    }
                    MessageBox.Show("Количество оставшихся попыток на сдачу теста - " + user.TestTry);
                    if (user.TestTry<=0)
                    {
                        MessageBox.Show("У вас закончились попытки!", "Тест окончен", MessageBoxButton.OK, MessageBoxImage.Error);
                        Close();
                    }
                }
            }
            dsUser.Clear();
        }

        public void Results_Click(object sender, RoutedEventArgs e)
        {
            if (name.Text == "" && surname.Text == "")
            {
                MessageBox.Show("Для получения результатов заполните имя и фамилию!");
            }
            else
            {
                using (SqlConnection connection = new SqlConnection(connStr))
                {
                    dsRes.Clear();
                    connection.Open();
                    var adapU = new SqlDataAdapter("SELECT name as 'Имя', surname as 'Фамилия', groupName as 'Группа', subject as 'Предмет', testTime as'Дата и время', testTry as 'Кол-во попыток', result as 'Результат, %' FROM results WHERE name = '" + name.Text + "' AND surname = '" + surname.Text + "'", connection);
                    adapU.Fill(dsRes, "results");
                    dgResults.ItemsSource = dsRes.Tables["results"].DefaultView;
                    dgResults.Visibility = Visibility.Visible;
                }
            }
        }
    }
}
