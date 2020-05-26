using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ISL.DialogBoxes
{
    /// <summary>
    /// Interaction logic for AdminPasswordDialogBox.xaml
    /// </summary>
    public partial class AdminPasswordDialogBox : Window
    {
        public AdminPasswordDialogBox()
        {
            InitializeComponent();
        }
        public string ResponseText
        {
            get { return ResponseTextBox.Password; }
            set { ResponseTextBox.Password = value; }
        }

        private void OKButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            DialogResult = true;
        }
        private void CancelButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
