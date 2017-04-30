using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HTMLTableGenerator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BrowseFolder_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new FolderBrowserDialog();
            dialog.ShowDialog();

            FolderPath.Text = dialog.SelectedPath;
        }

        private void CreateHTML_Click(object sender, RoutedEventArgs e)
        {
            HTMLCodeBuilder codeBuilder = new HTMLCodeBuilder();

            codeBuilder.SourceFolder = FolderPath.Text;
            codeBuilder.Template = HTMLTemplate.Text;
            codeBuilder.ImageURLRoot = ImageURLRoot.Text;
            codeBuilder.UsingTable = (bool) UsingTable.IsChecked;

            try
            {
                codeBuilder.ColumnsNumber = Int32.Parse(ColumnsNumber.Text);
                codeBuilder.ImageWidth = Int32.Parse(ImageWidth.Text);
            } catch (FormatException fe)
            {
                System.Windows.MessageBox.Show("Error: " + fe.Message);
            }            

            Result.Text = codeBuilder.Builder();
        }
    }
}
