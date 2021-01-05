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
    /// Interaction logic for CustomerView.xaml
    /// </summary>
    public partial class CustomerView : UserControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyname = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyname));
        }

        public DataProvider DP { get; set; }

        // User
        public KhachHang User { get; set; }

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

        // History
        private ObservableCollection<LichSuGiaoDich> buyHistoryList;
        public ObservableCollection<LichSuGiaoDich> BuyHistoryList
        {
            get
            {
                return buyHistoryList;
            }
            set
            {
                buyHistoryList = value;
                OnPropertyChanged("BuyHistoryList");
            }
        }

        private ObservableCollection<LichSuGiaoDich> rentHistoryList;
        public ObservableCollection<LichSuGiaoDich> RentHistoryList
        {
            get
            {
                return rentHistoryList;
            }
            set
            {
                rentHistoryList = value;
                OnPropertyChanged("RentHistoryList");
            }
        }

        private ObservableCollection<LichSuXem> viewHistoryList;
        public ObservableCollection<LichSuXem> ViewHistoryList
        {
            get
            {
                return viewHistoryList;
            }
            set
            {
                viewHistoryList = value;
                OnPropertyChanged("ViewHistoryList");
            }
        }
        public CustomerView()
        {
            InitializeComponent();
        }

        public CustomerView(KhachHang customer)
        {
            InitializeComponent();
            DP = new DataProvider();
            User = customer;
            userDataGrid.Items.Add(User);
            string MaKH = User.MaKH;
            RentHouseList = new ObservableCollection<BD>(DP.Database.BDS.Where(p => p.DeleteStatus == false && p.NhaBan_Thue == "THUE"));
            SellHouseList = new ObservableCollection<BD>(DP.Database.BDS.Where(p => p.DeleteStatus == false && p.NhaBan_Thue == "BAN"));
            BuyHistoryList = new ObservableCollection<LichSuGiaoDich>(DP.Database.LichSuGiaoDiches.Where(p => p.DeleteStatus == false && p.MaKH == MaKH && p.LoaiGD == "MUA"));
            RentHistoryList = new ObservableCollection<LichSuGiaoDich>(DP.Database.LichSuGiaoDiches.Where(p => p.DeleteStatus == false && p.MaKH == MaKH && p.LoaiGD == "THUE"));
            ViewHistoryList = new ObservableCollection<LichSuXem>(DP.Database.LichSuXems.Where(p => p.DeleteStatus == false && p.MaKH == MaKH));
            this.DataContext = this;
        }
    }
}
