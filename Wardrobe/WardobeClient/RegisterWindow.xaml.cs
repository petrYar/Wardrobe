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
using System.Windows.Shapes;

namespace WardobeClient
{
    /// <summary>
    /// Логика взаимодействия для RegisterWindow.xaml
    /// </summary>
    public partial class RegisterWindow : Window
    {
        public RegisterWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            WardobeClient.Proxy.AccountServiceClient client = new WardobeClient.Proxy.AccountServiceClient();
            WardobeClient.Proxy.Account account = new WardobeClient.Proxy.Account()
            {
                UserName = txtUserName.Text
            };
            client.RegisterAsync(account, pbPassword.Password);

            this.Close();
        }
    }
}
