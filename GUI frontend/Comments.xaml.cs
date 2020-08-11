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
using System.Windows.Shapes;

namespace GUI_frontend
{
    /// <summary>
    /// Interaction logic for Comments.xaml
    /// </summary>
    public partial class Comments : Window
    {
        IDataTier datatier;

        public Comments(string username)
        {
            InitializeComponent();
            lusername.Content = username;
        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            var m = new MainWindow();
            m.Show();
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

            List<List<string>> comments = datatier.GetComments();
            commentListView.ItemsSource = comments;
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            string username = lusername.Content.ToString();

            if(username == "")
            {
                MessageBox.Show("Please login to submit a comment");
            }
            else if(txtComment.Text == "")
            {
                MessageBox.Show("Enter a comment to submit");
            }
            else
            {
                datatier.CreateComment(username, txtComment.Text);

                List<List<string>> comments = datatier.GetComments();
                commentListView.ItemsSource = comments;

                txtComment.Text = "";
            }
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            if(txtUsername.Text.Equals(""))
            {
                MessageBox.Show("Enter a username to search");
            }
            else
            {
                List<List<string>> comments = datatier.filterComments(txtUsername.Text);
                commentListView.ItemsSource = comments;
            }
        }
    }
}
