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
using ISL.DialogBoxes;

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
        string firstname;
        string lastname;
        string email;
        string username;
        string usertype;
        string password;
        string address;

        //public string userType = "Normal User";
        public string[] userType { get; set; }

        public Registration()
        {
            InitializeComponent();
            userType = new string[] { "Admin", "User" };
            DataContext = this;
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            MainWindow login = new MainWindow();
            login.Show();
            Close();
        }

        private void Submit_Click(object sender, RoutedEventArgs eventArg)
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
                firstname = textBoxFirstName.Text;
                lastname = textBoxLastName.Text;
                email = textBoxEmail.Text;
                username = textBoxUserName.Text;
                usertype = comboBoxUserType.Text;
                password = passwordBox1.Password;
                address = textBoxAddress.Text;

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
                        if (usertype.Equals("Admin"))
                        {
                            var dialog = new AdminPasswordDialogBox();
                            if (dialog.ShowDialog() == true)
                            {
                                string AdminPass = dialog.ResponseText;
                                if (AdminPass.Equals("admin"))
                                {
                                    InsertUser(sender, eventArg);
                                }
                                else
                                {
                                    errormessage.Text = "Registration failed, incorrect admin password.";
                                }
                            }
                        }
                        else
                        {
                            InsertUser(sender, eventArg);
                        }
                    }
                }
            }
        }

        private void InsertUser(object sender, RoutedEventArgs e)
        {
            errormessage.Text = "";
            int rows = 0;
            string insertUserSQL = "Insert into User (firstname, lastname, email, username, usertype, password, address) " +
                    "values('" + firstname + "','" + lastname + "','" + email + "','" + username + "','" + usertype + "','" + password + "','" + address + "')";

            con = new IfxConnection(cs);
            con.Open();
            try
            {
                IfxCommand cmd = new IfxCommand(insertUserSQL, con);
                cmd.CommandType = CommandType.Text;
                rows = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                string createTableSQL = "create table User (userid serial, firstname varchar(20), lastname varchar(20), " +
                    "email varchar(30), username varchar(20), usertype varchar(20), " +
                    "password varchar(30), address varchar(150))";

                IfxCommand cmd1 = new IfxCommand(createTableSQL, con);
                cmd1.CommandType = CommandType.Text;
                cmd1.ExecuteNonQuery();

                IfxCommand cmd2 = new IfxCommand(insertUserSQL, con);
                cmd2.CommandType = CommandType.Text;
                rows = cmd2.ExecuteNonQuery();
            }
            finally
            {
                con.Close();

            }
            if (rows > 0)
            {
                if (usertype.Equals("Admin"))
                    errormessage.Text = "Admin registered successfully.";
                else
                    errormessage.Text = "User registered successfully.";
            }
            else
            {
                errormessage.Text = "Registeration failed.";
            }
        }
    }
}
