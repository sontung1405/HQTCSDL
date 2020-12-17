using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Data;
using System.IO;
using System.Globalization;
using System.Windows.Input;
using System.Windows.Threading;
using System.Windows.Media;
using Microsoft.Win32;
using System.Windows.Media.Imaging;
using System.Drawing;
using System.ComponentModel;
using BDS_2020.Class;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
namespace BDS_2020
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
    }

    public class DateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return "Chưa có";
            }
            DateTime result;
            if (DateTime.TryParse(value.ToString(), out result) == false)
            {
                return "Chưa có";
            }
            return result.ToShortDateString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
