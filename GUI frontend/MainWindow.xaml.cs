using Data_tier;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
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

namespace GUI_frontend
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        IDataTier datatier;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnComments_Click(object sender, RoutedEventArgs e)
        {
            var c = new Comments("");
            c.Show();
            this.Hide();
        }

        //Connecting to the data tier when window loaded
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ChannelFactory<IDataTier> dataFactory; //opening a server connection

            NetTcpBinding tcpBinding = new NetTcpBinding();

            string sURL = "net.tcp://localhost:50001/DataTier";

            dataFactory = new ChannelFactory<IDataTier>(tcpBinding, sURL);
            datatier = dataFactory.CreateChannel();
        }

        private void btnSignup_Click(object sender, RoutedEventArgs e)
        {
            string username = txtcusername.Text;
            string password = txtcpassword.Text;

            datatier.AddUser(username, password);

            txtcusername.Text = "";
            txtcpassword.Text = "";
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            bool login = datatier.LoginUser(txtusername.Text, txtpassword.Text);

            if(login == true)
            {
                var c = new Comments(txtusername.Text);
                c.Show();
                this.Hide();

                txtusername.Text = "";
                txtpassword.Text = "";
            }
            else 
            {
                MessageBox.Show("No such user exits");
            }
        }
    }
}
