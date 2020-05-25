using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Informix.Net.Core;

namespace ISL
{
    /// <summary>
    /// Interaction logic for Registration.xaml
    /// </summary>
    public partial class Registration : Window
    {
        IfxConnection con;
        //Connection String
        string cs = "DataBase=logindb;Server=ol_informix1410_9;User ID = informix; Password=Rinvoke1;";

        //public string userType = "Normal User";
        public string[] userType { get; set; }

        public Registration()
        {
            InitializeComponent();
            userType = new string[] { "Normal User" };
            DataContext = this;
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            MainWindow login = new MainWindow();
            login.Show();
            Close();
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            if (textBoxEmail.Text.Length == 0)
            {
                errormessage.Text = "Enter an email.";
                textBoxEmail.Focus();
            }
            else if (!Regex.IsMatch(textBoxEmail.Text, @"^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$"))
            {
                errormessage.Text = "Enter a valid email.";
                textBoxEmail.Select(0, textBoxEmail.Text.Length);
                textBoxEmail.Focus();
            }
            else
            {
                string firstname = textBoxFirstName.Text;
                string lastname = textBoxLastName.Text;
                string email = textBoxEmail.Text;
                string username = textBoxUserName.Text;
                string usertype = comboBoxUserType.Text;
                string password = passwordBox1.Password;
                if (username.Length == 0)
                {
                    errormessage.Text = "Enter username.";
                    textBoxUserName.Focus();
                }
                else
                {
                    if (passwordBox1.Password.Length == 0)
                    {
                        errormessage.Text = "Enter password.";
                        passwordBox1.Focus();
                    }
                    else if (passwordBoxConfirm.Password.Length == 0)
                    {
                        errormessage.Text = "Enter Confirm password.";
                        passwordBoxConfirm.Focus();
                    }
                    else if (passwordBox1.Password != passwordBoxConfirm.Password)
                    {
                        errormessage.Text = "Confirm password must be same as password.";
                        passwordBoxConfirm.Focus();
                    }
                    else
                    {
                        errormessage.Text = "";
                        string address = textBoxAddress.Text;
                        con = new IfxConnection(cs);
                        con.Open();
                        IfxCommand cmd = new IfxCommand("Insert into User (firstname,lastname,email,username,usertype,password,address) values('" + firstname + "','" + lastname + "','" + email + "','" + username + "','" + usertype + "','" + password + "','" + address + "')", con);
                        cmd.CommandType = CommandType.Text;
                        int rows = cmd.ExecuteNonQuery();
                        con.Close();
                        if(rows > 0)
                        {
                            errormessage.Text = "You have Registered successfully.";
                        }
                        else
                        {
                            errormessage.Text = "Registeration Failed";
                        }
                    }
                }
                
            }
        }
    }
}
