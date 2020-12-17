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
using BDS_2020.Class;
namespace BDS_2020
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public DataProvider DP { get; set; }
        public LoginWindow()
        {
            InitializeComponent();
            DP = new DataProvider();
        }

        private void loginButton_Click(object sender, RoutedEventArgs e)
        {
            string ID = IDTextBox.Text;

            if (ID.Length == 0)
            {
                MessageBox.Show("Chưa nhập ID");
                return;
            }

            MainWindow mainWindow = new MainWindow();
            
            if (ID == "ADMIN")
            {
                mainWindow.Content = new AdminView();
            }
            else
            {
                var staff = DP.Database.NhanViens.SingleOrDefault(p => p.MaNV == ID);
                var customer = DP.Database.KhachHangs.SingleOrDefault(p => p.MaKH == ID);
                if (customer != null)
                {
                   
                    mainWindow.Content = new CustomerView(customer);
                }
                else if(staff != null)
                {
                    mainWindow.Content = new StaffView(staff);
                }
                else
                {
                    MessageBox.Show("Không tồn tại ID hoặc sai mật khẩu. Vui lòng kiểm tra lại");
                    return;
                }
            }

            this.Close();
            mainWindow.ShowDialog();
            //string password = passwordTextBox.Text;
            //if (password.Length == 0)
            //{
            //    MessageBox.Show("Chưa nhập mật khẩu");
            //    return;
            //}


        }
    }
}
