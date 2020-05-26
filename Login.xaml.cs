using Informix.Net.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace ISL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window  
    {
        IfxConnection con;
        //Connection String
        string cs = "DataBase=logindb;Server=ol_informix1410_9;User ID = informix; Password=Rinvoke1;";

        public MainWindow()  
        {  
            InitializeComponent();
        }  
        
        Registration registration = new Registration();

        private void button1_Click(object sender, RoutedEventArgs e)  
        {  
            if (textBoxUserName.Text.Length == 0)  
            {  
                errormessage.Text = "Enter a user name.";
                textBoxUserName.Focus();  
            } 
            else  
            {  
                string username = textBoxUserName.Text;  
                string password = passwordBox1.Password;
                DataSet dataSet = new DataSet();
                string selectSQL = "Select * from user where username='" + username + "'  and password='" + password + "'";
                con = new IfxConnection(cs);  
                con.Open();
                try
                {
                    IfxCommand cmd = new IfxCommand(selectSQL, con);
                    cmd.CommandType = CommandType.Text;
                    IfxDataAdapter adapter = new IfxDataAdapter();
                    adapter.SelectCommand = cmd;
                    adapter.Fill(dataSet);
                    if (dataSet.Tables[0].Rows.Count > 0)
                    {
                        string name = dataSet.Tables[0].Rows[0]["FirstName"].ToString() + " " + dataSet.Tables[0].Rows[0]["LastName"].ToString();
                        string userType = dataSet.Tables[0].Rows[0]["UserType"].ToString();

                        if (userType == "Admin")
                        {
                            AllUserDetails allUser = new AllUserDetails(name);
                            allUser.Show();
                        }

                        else
                        {
                            UserProfile userProfile = new UserProfile();
                            userProfile.TextBlockName.Text = name;
                            userProfile.Show();

                        }

                        Close();
                    }
                    else
                    {
                        errormessage.Text = "Sorry! Please enter correct username/password.";
                    }
                }
                catch (Exception ex)
                {
                    errormessage.Text = "You need to register first before login.";
                }
                finally
                {
                    con.Close();
                }
            }  
        }

        private void buttonRegister_Click(object sender, RoutedEventArgs e)
        {
            registration.Show();
            Close();
        }


    }  
}
