using System;
using System.Collections.Generic;
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

namespace WardobeClient
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                WardobeClient.Proxy.AccountContractClient client = new WardobeClient.Proxy.AccountContractClient();
                string returnData = await client.LoginAsync(txtLogin.Text, PasswordBox.Password);
                if (!string.IsNullOrEmpty(returnData))
                {
                    // Write token in debug folder
                    using (StreamWriter sw = File.CreateText("token.txt"))
                    {
                        sw.WriteLine(returnData);
                    }

                    MainWindow mainWindow = new MainWindow();
                    this.Hide();
                    mainWindow.ShowDialog();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Login or password is incorrect!");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            RegisterWindow dlg = new RegisterWindow();
            dlg.ShowDialog();
        }
    }
}
