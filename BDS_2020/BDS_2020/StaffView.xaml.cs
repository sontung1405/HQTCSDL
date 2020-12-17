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
    /// Interaction logic for StaffView.xaml
    /// </summary>
    public partial class StaffView : UserControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyname = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyname));
        }

        public DataProvider DP { get; set; }

        private NhanVien user;
        public NhanVien User { get; set; }

        //House
        #region
        private ObservableCollection<BD> rentHouseList;
        public ObservableCollection<BD> RentHouseList
        {
            get
            {
                return rentHouseList;
            }
            set
            {
                rentHouseList = value;
                OnPropertyChanged("RentHouseList");
            }
        }
        private ObservableCollection<BD> sellHouseList;
        public ObservableCollection<BD> SellHouseList
        {
            get
            {
                return sellHouseList;
            }
            set
            {
                sellHouseList = value;
                OnPropertyChanged("SellHouseList");
            }
        }
        #endregion

        // Customer
        private ObservableCollection<KhachHang> customerList;
        public ObservableCollection<KhachHang> CustomerList
        {
            get
            {
                return customerList;
            }
            set
            {
                customerList = value;
                OnPropertyChanged("CustomerList");
            }
        }

        // Staff
        private ObservableCollection<NhanVien> staffList;
        public ObservableCollection<NhanVien> StaffList
        {
            get
            {
                return staffList;
            }
            set
            {
                staffList = value;
                OnPropertyChanged("StaffList");
            }
        }

        private ObservableCollection<LichSuGiaoDich> buyHistory;
        public ObservableCollection<LichSuGiaoDich> BuyHistory
        {
            get
            {
                return buyHistory;
            }
            set
            {
                buyHistory = value;
                OnPropertyChanged("BuyHistory");
            }
        }

        private ObservableCollection<LichSuGiaoDich> rentHistory;
        public ObservableCollection<LichSuGiaoDich> RentHistory
        {
            get
            {
                return rentHistory;
            }
            set
            {
                rentHistory = value;
                OnPropertyChanged("RentHistory");
            }
        }

        private ObservableCollection<LichSuXem> viewHistory;
        public ObservableCollection<LichSuXem> ViewHistory
        {
            get
            {
                return viewHistory;
            }
            set
            {
                viewHistory = value;
                OnPropertyChanged("ViewHistory");
            }
        }
        public StaffView(NhanVien staff)
        {
            InitializeComponent();
            DP = new DataProvider();
            User = staff;
            userDataGrid.Items.Add(User);
            RentHouseList = new ObservableCollection<BD>(DP.Database.BDS.Where(p => p.DeleteStatus == false && p.NhaBan_Thue == "THUE"));
            SellHouseList = new ObservableCollection<BD>(DP.Database.BDS.Where(p => p.DeleteStatus == false && p.NhaBan_Thue == "BAN"));
            CustomerList = new ObservableCollection<KhachHang>(DP.Database.KhachHangs.Where(p => p.DeleteStatus == false));
            StaffList = new ObservableCollection<NhanVien>(DP.Database.NhanViens.Where(p => p.DeleteStatus == false));
            BuyHistory = new ObservableCollection<LichSuGiaoDich>(DP.Database.LichSuGiaoDiches.Where(p => p.DeleteStatus == false && p.LoaiGD == "MUA"));
            RentHistory = new ObservableCollection<LichSuGiaoDich>(DP.Database.LichSuGiaoDiches.Where(p => p.DeleteStatus == false && p.LoaiGD == "THUE"));
            ViewHistory = new ObservableCollection<LichSuXem>(DP.Database.LichSuXems.Where(p => p.DeleteStatus == false));
            this.DataContext = this;
        }
    }
}
