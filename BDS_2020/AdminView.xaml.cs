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
    /// Interaction logic for AdminView.xaml
    /// </summary>
    public partial class AdminView : UserControl, INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyname = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyname));
        }

        public DataProvider DP { get; set; }

        //House
        #region
        private ObservableCollection<BD> rentHouseList;
        public ObservableCollection<BD> RentHouseList {
            get
            {
                return rentHouseList;
            }
            set
            {
                rentHouseList = value;
                OnPropertyChanged();
            }
        }
        private ObservableCollection<BD> sellHouseList;
        public ObservableCollection<BD> SellHouseList {
            get
            {
                return sellHouseList;
            }
            set
            {
                sellHouseList = value;
                OnPropertyChanged();
            }
        }
        #endregion
        // Customer
        #region
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
                OnPropertyChanged();
            }
        }
        #endregion
        // Staff
        #region
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
                OnPropertyChanged();
            }
        }
        #endregion
        // History
        #region
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
                OnPropertyChanged();
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
                OnPropertyChanged();
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
                OnPropertyChanged();
            }
        }
        #endregion
        // Add house
        #region
        private ObservableCollection<ChiNhanh> branchList;
        public ObservableCollection<ChiNhanh> BranchList
        {
            get
            {
                return branchList;
            }
            set
            {
                branchList = value;
                OnPropertyChanged();
            }
        }
        private ObservableCollection<LoaiNha> typeList;
        public ObservableCollection<LoaiNha> TypeList
        {
            get
            {
                return typeList;
            }
            set
            {
                typeList = value;
                OnPropertyChanged();
            }
        }
        #endregion

        public AdminView()
        {
            InitializeComponent();
            DP = new DataProvider();
            this.DataContext = this;
        }

        private void deleteRentHouseButton_Click(object sender, RoutedEventArgs e)
        {
            int index = rentHouseDataGrid.SelectedIndex;

            if(index == -1)
            {
                MessageBox.Show("Chưa chọn đối tượng để xóa");
                return;
            }

            BD house = rentHouseList[index];
            RentHouseList.Remove(house);
            var dbHouse = DataProvider.Instance.Database.BDS.SingleOrDefault(p => p.MaNha == house.MaNha && p.MaCN == house.MaCN);

            if(dbHouse == null)
            {
                return;
            }

            dbHouse.DeleteStatus = true;

            DataProvider.Instance.Database.SaveChanges();

            RentHouseList = new ObservableCollection<BD>(DP.Database.BDS.Where(p => p.DeleteStatus == false && p.NhaBan_Thue == "THUE"));
            rentHouseDataGrid.SelectedIndex = -1;
        }
        private void deleteSellHouseButton_Click(object sender, RoutedEventArgs e)
        {
            int index = sellHouseDataGrid.SelectedIndex;

            if (index == -1)
            {
                MessageBox.Show("Chưa chọn đối tượng để xóa");
                return;
            }

            BD house = SellHouseList[index];

            var dbHouse = DataProvider.Instance.Database.BDS.SingleOrDefault(p => p.MaNha == house.MaNha && p.MaCN == house.MaCN);

            if (dbHouse == null)
            {
                return;
            }

            dbHouse.DeleteStatus = true;

            DataProvider.Instance.Database.SaveChanges();

            SellHouseList = new ObservableCollection<BD>(DP.Database.BDS.Where(p => p.DeleteStatus == false && p.NhaBan_Thue == "BAN"));
            sellHouseDataGrid.SelectedIndex = -1;
        }
        private void deleteCustomerButton_Click(object sender, RoutedEventArgs e)
        {
            int index = customerDataGrid.SelectedIndex;

            if (index == -1)
            {
                MessageBox.Show("Chưa chọn đối tượng để xóa");
                return;
            }

            KhachHang customer = CustomerList[index];

            var dbCustomer = DataProvider.Instance.Database.KhachHangs.SingleOrDefault(p => p.MaKH == customer.MaKH);

            if (dbCustomer == null)
            {
                return;
            }

            dbCustomer.DeleteStatus = true;

            DataProvider.Instance.Database.SaveChanges();

            CustomerList = new ObservableCollection<KhachHang>(DP.Database.KhachHangs.Where(p => p.DeleteStatus == false));
            customerDataGrid.SelectedIndex = -1;
        }

        private void deleteStaffButton_Click(object sender, RoutedEventArgs e)
        {
            int index = staffDataGrid.SelectedIndex;

            if (index == -1)
            {
                MessageBox.Show("Chưa chọn đối tượng để xóa");
                return;
            }

            NhanVien staff = StaffList[index];

            var dbStaff = DataProvider.Instance.Database.NhanViens.SingleOrDefault(p => p.MaNV == staff.MaNV);

            if (dbStaff == null)
            {
                return;
            }

            dbStaff.DeleteStatus = true;

            DataProvider.Instance.Database.SaveChanges();

            StaffList = new ObservableCollection<NhanVien>(DP.Database.NhanViens.Where(p => p.DeleteStatus == false));
            staffDataGrid.SelectedIndex = -1;
        }

        private void deleteBuyHistoryListButton_Click(object sender, RoutedEventArgs e)
        {
            int index = buyHistoryDataGrid.SelectedIndex;

            if (index == -1)
            {
                MessageBox.Show("Chưa chọn đối tượng để xóa");
                return;
            }

            LichSuGiaoDich buyHistoryList = BuyHistoryList[index];

            var dbBuyHistory = DataProvider.Instance.Database.LichSuGiaoDiches.SingleOrDefault(p => p.MaNha == buyHistoryList.MaNha &&
                                                                                                    p.MaCN == buyHistoryList.MaCN &&
                                                                                                    p.MaKH == buyHistoryList.MaKH &&
                                                                                                    p.NgayGD == buyHistoryList.NgayGD);

            if (dbBuyHistory == null)
            {
                return;
            }

            dbBuyHistory.DeleteStatus = true;

            DataProvider.Instance.Database.SaveChanges();

            BuyHistoryList = new ObservableCollection<LichSuGiaoDich>(DP.Database.LichSuGiaoDiches.Where(p => p.DeleteStatus == false && p.BD.DeleteStatus == false && p.LoaiGD == "MUA"));
            buyHistoryDataGrid.SelectedIndex = -1;
        }

        private void deleteRentHistoryListButton_Click(object sender, RoutedEventArgs e)
        {
            int index = rentHistoryDataGrid.SelectedIndex;

            if (index == -1)
            {
                MessageBox.Show("Chưa chọn đối tượng để xóa");
                return;
            }

            LichSuGiaoDich rentHistoryList = RentHistoryList[index];

            var dbRentHistory = DataProvider.Instance.Database.LichSuGiaoDiches.SingleOrDefault(p => p.MaNha == rentHistoryList.MaNha &&
                                                                                                     p.MaCN == rentHistoryList.MaCN &&
                                                                                                     p.MaKH == rentHistoryList.MaKH &&
                                                                                                     p.NgayGD == rentHistoryList.NgayGD);

            if (dbRentHistory == null)
            {
                return;
            }

            dbRentHistory.DeleteStatus = true;

            DataProvider.Instance.Database.SaveChanges();

            RentHistoryList = new ObservableCollection<LichSuGiaoDich>(DP.Database.LichSuGiaoDiches.Where(p => p.DeleteStatus == false && p.BD.DeleteStatus == false && p.LoaiGD == "THUE"));
            rentHistoryDataGrid.SelectedIndex = -1;
        }

        private void deleteViewHistoryListButton_Click(object sender, RoutedEventArgs e)
        {
            int index = viewHistoryDataGrid.SelectedIndex;

            if (index == -1)
            {
                MessageBox.Show("Chưa chọn đối tượng để xóa");
                return;
            }

            LichSuXem rentHistoryList = ViewHistoryList[index];

            var dbViewHistory = DataProvider.Instance.Database.LichSuXems.SingleOrDefault(p => p.MaNha == rentHistoryList.MaNha &&
                                                                                               p.MaCN == rentHistoryList.MaCN &&
                                                                                               p.MaKH == rentHistoryList.MaKH &&
                                                                                               p.NgayXem == rentHistoryList.NgayXem);

            if (dbViewHistory == null)
            {
                return;
            }

            dbViewHistory.DeleteStatus = true;

            DataProvider.Instance.Database.SaveChanges();

            ViewHistoryList = new ObservableCollection<LichSuXem>(DP.Database.LichSuXems.Where(p => p.DeleteStatus == false && p.BD.DeleteStatus == false && p.KhachHang.DeleteStatus == false ));
            viewHistoryDataGrid.SelectedIndex = -1;
        }

        private void mainTab_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = mainTab.SelectedIndex;
            if(index == 0)
            {

            }
            else if(index == 1)
            {
                RentHouseList = new ObservableCollection<BD>(DP.Database.BDS.Where(p => p.DeleteStatus == false && p.NhaBan_Thue == "THUE"));
            }
            else if(index == 2)
            {
                SellHouseList = new ObservableCollection<BD>(DP.Database.BDS.Where(p => p.DeleteStatus == false && p.NhaBan_Thue == "BAN"));
            }
            else if(index == 3)
            {
                CustomerList = new ObservableCollection<KhachHang>(DP.Database.KhachHangs.Where(p => p.DeleteStatus == false));
            }
            else if (index == 4)
            {
                StaffList = new ObservableCollection<NhanVien>(DP.Database.NhanViens.Where(p => p.DeleteStatus == false));
            }
            else if(index == 5)
            {
                BuyHistoryList = new ObservableCollection<LichSuGiaoDich>(DP.Database.LichSuGiaoDiches.Where(p => p.DeleteStatus == false && p.BD.DeleteStatus == false && p.LoaiGD == "MUA"));
            }
            else if(index == 6)
            {
                RentHistoryList = new ObservableCollection<LichSuGiaoDich>(DP.Database.LichSuGiaoDiches.Where(p => p.DeleteStatus == false && p.BD.DeleteStatus == false && p.LoaiGD == "THUE"));
            }
            else if(index == 7)
            {
                ViewHistoryList = new ObservableCollection<LichSuXem>(DP.Database.LichSuXems.Where(p => p.DeleteStatus == false && p.BD.DeleteStatus == false));
            }
            else if (index == 8)
            {
                StaffList = new ObservableCollection<NhanVien>(DP.Database.NhanViens.Where(p => p.DeleteStatus == false));
                BranchList = new ObservableCollection<ChiNhanh>(DP.Database.ChiNhanhs.Where(p => p.DeleteStatus == false));
                TypeList = new ObservableCollection<LoaiNha>(DP.Database.LoaiNhas.Where(p => p.DeleteStatus == false));
            }
            else if(index == 9)
            {
                StaffList = new ObservableCollection<NhanVien>(DP.Database.NhanViens.Where(p => p.DeleteStatus == false));
                BranchList = new ObservableCollection<ChiNhanh>(DP.Database.ChiNhanhs.Where(p => p.DeleteStatus == false));
            }
            else if (index == 10)
            {           
                BranchList = new ObservableCollection<ChiNhanh>(DP.Database.ChiNhanhs.Where(p => p.DeleteStatus == false));
            }
            else if(index == 11)
            {
                CustomerList = new ObservableCollection<KhachHang>(DP.Database.KhachHangs.Where(p => p.DeleteStatus == false));
                RentHouseList = new ObservableCollection<BD>(DP.Database.BDS.Where(p => p.DeleteStatus == false));
            }
        }

        private void addHouseButton_Click(object sender, RoutedEventArgs e)
        {
            App.Current.Dispatcher.BeginInvoke(DispatcherPriority.Background, new Action(() => {
                string purpose = purposeComboBox.Text;
                string staffCode = "";
                string branchCode = "";
                decimal price;
                string condition = "";
                string street = "";
                string district = "";
                string city = "";
                string region = "";
                string typeCode = "";                
                int roomCount = 0;
                string houseStatus = "TRONG";
                int viewCount = 0;
                DateTime publishDate;
                DateTime expiryDate;
                if (ownerTextBox.Text.Length == 0)
                {
                    MessageBox.Show("Chưa nhập mã chủ nhà");
                    return;
                }
                if (purpose == "Bán")
                {
                    purpose = "BAN";
                }
                else if(purpose == "Thuê")
                {
                    purpose = "THUE";
                }
                if (purpose.Length == 0)
                {
                    MessageBox.Show("Chưa chọn bán-thuê");
                    return;
                }
                if (staffListComboBox.SelectedIndex >= 0)
                {
                    staffCode  = StaffList[staffListComboBox.SelectedIndex].MaNV;
                }
                if (staffCode.Length == 0)
                {
                    MessageBox.Show("Chưa chọn mã nhân viên");
                    return;
                }
                if (branchListComboBox.SelectedIndex >= 0)
                {
                    branchCode = BranchList[branchListComboBox.SelectedIndex].MaChiNhanh;
                }
                if (branchCode.Length == 0)
                {
                    MessageBox.Show("Chưa chọn mã chi nhánh");
                    return;
                }
                if (decimal.TryParse(priceTextBox.Text, out price) == false){
                    MessageBox.Show("Giá tiền không hợp lệ");
                    return;
                }

                condition = conditionTextBox.Text;
                street = streetTextBox.Text;
                district = districtTextBox.Text;
                city = cityTextBox.Text;
                region = regionTextBox.Text;

                if(typeComboBox.SelectedIndex >= 0)
                {
                    typeCode = TypeList[typeComboBox.SelectedIndex].MaLoai;
                }
                if (typeCode.Length == 0)
                {
                    MessageBox.Show("Chưa chọn mã loại");
                    return;
                }
                if (int.TryParse(roomCountTextBox.Text, out roomCount) == false){
                    MessageBox.Show("Số phòng không hợp lệ");
                    return;
                }

                if(publishDatePicker.SelectedDate == null)
                {
                    MessageBox.Show("Chưa chọn ngày đăng");
                    return;
                }
                if(expiryDatePicker.SelectedDate == null)
                {
                    MessageBox.Show("Chưa chọn ngày hết hạn");
                    return;
                }
                publishDate = (DateTime)publishDatePicker.SelectedDate;
                expiryDate = (DateTime)expiryDatePicker.SelectedDate;

                BD newBD = new BD();
                int houseCount = DataProvider.Instance.Database.BDS.ToList().Count;
                newBD.MaNha = (houseCount + 1).ToString();
                
                newBD.MaCN = ownerTextBox.Text;

                newBD.NhaBan_Thue = purpose;
              
                newBD.MaNV = staffCode;
                
                newBD.MaChiNhanh = branchCode;
                newBD.GiaBan_GiaThue = price;
                newBD.DieuKien = condition;
                newBD.Duong = street;
                newBD.Quan = district;
                newBD.TP = city;
                newBD.KhuVuc = region;
                newBD.MaLoai = typeCode;
                newBD.SoPhong = roomCount;
                newBD.NgayDang = publishDate;
                newBD.NgayHetHang = expiryDate;
                newBD.TinhTrang = houseStatus;
                newBD.LuotXem = viewCount;
                newBD.DeleteStatus = false;

                DP.Database.BDS.Add(newBD);
                DP.Database.SaveChanges();

                MessageBox.Show("Đã thêm thành công");
            }));
        }

        private void addCustomerButton_Click(object sender, RoutedEventArgs e)
        {
            App.Current.Dispatcher.BeginInvoke(DispatcherPriority.Background, new Action(() =>
            {
                string customerCode = "";
                string customerName = "";
                string customerAddress = "";
                string customerPhoneNumber = "";
                string customerBranchCode = "";
                string customerSupportStaffCode = "";


                if (customerNameTextBox.Text.Length == 0)
                {
                    MessageBox.Show("Chưa nhập tên khách hàng");
                    return;
                }
                customerName = customerNameTextBox.Text;
                customerAddress = customerAddressTextBox.Text;
                customerPhoneNumber = customerPhoneNumberTextBox.Text;

                if(customerBranchListComboBox.SelectedIndex >= 0)
                {
                    customerBranchCode = BranchList[customerBranchListComboBox.SelectedIndex].MaChiNhanh;
                }
                if(customerStaffListComboBox.SelectedIndex >= 0)
                {
                    customerSupportStaffCode = StaffList[customerStaffListComboBox.SelectedIndex].MaNV;
                }
                if (customerBranchCode.Length == 0)
                {
                    MessageBox.Show("Chưa chọn mã chi nhánh");
                    return;
                }
                if(customerSupportStaffCode.Length == 0)
                {
                    MessageBox.Show("Chưa chọn nhân viên hỗ trợ");
                    return;
                }
                KhachHang newCustomer = new KhachHang();
                int customerCount = DataProvider.Instance.Database.KhachHangs.ToList().Count;
                newCustomer.MaKH = (customerCount + 1).ToString();
                newCustomer.TenKH = customerName;
                newCustomer.DiaChi = customerAddress;
                newCustomer.SDT = decimal.Parse(customerPhoneNumber);

                newCustomer.MaChiNhanh = customerBranchCode;
                newCustomer.MaNV = customerSupportStaffCode;
                newCustomer.DeleteStatus = false;

                DP.Database.KhachHangs.Add(newCustomer);
                DP.Database.SaveChanges();

                MessageBox.Show("Đã thêm khách hàng");
            }));
        }

        private void addStaffButton_Click(object sender, RoutedEventArgs e)
        {
            App.Current.Dispatcher.BeginInvoke(DispatcherPriority.Background, new Action(() =>
            {
                string staffCode = "";
                string staffBranchCode = "";
                string staffName = "";
                string staffAddress = "";
                string staffPhoneNumber = "";
                string staffGender = "";                
                DateTime staffBirthDate;
                int staffSalary;

                                

                if (staffBranchListComboBox.SelectedIndex >= 0)
                {
                    staffBranchCode = BranchList[staffBranchListComboBox.SelectedIndex].MaChiNhanh;
                }
                if(staffBranchCode.Length == 0)
                {
                    MessageBox.Show("Chưa chọn mã chi nhánh");
                    return;
                }
                if(staffNameTextBox.Text.Length == 0)
                {
                    MessageBox.Show("Chưa nhập tên nhân viên");
                    return;
                }
                staffName = staffNameTextBox.Text;
               
                staffAddress = staffAddressTextBox.Text;
                staffPhoneNumber = staffPhoneNumberTextBox.Text;
                if(staffGenderComboBox.Text.Length == 0)
                {
                    MessageBox.Show("Chưa chọn giới tính");
                    return;
                }
                staffGender = staffGenderComboBox.Text;
                if(staffBirthDatePicker.SelectedDate == null)
                {
                    MessageBox.Show("Chưa chọn ngày sinh");
                    return;
                }
                staffBirthDate = (DateTime)staffBirthDatePicker.SelectedDate;
                if (int.TryParse(staffSalaryTextBox.Text, out staffSalary) == false)
                {
                    MessageBox.Show("Lương không hợp lệ");
                    return;
                }
       

                NhanVien newStaff = new NhanVien();
                int staffCount = DataProvider.Instance.Database.NhanViens.ToList().Count();
                newStaff.MaNV = staffCode;
                newStaff.MaChiNhanh = staffBranchCode;
                newStaff.TenNV = staffName;
                newStaff.DiaChi = staffAddress;
                newStaff.SDT = staffPhoneNumber;
                newStaff.GioiTinh = staffGender;
                newStaff.NgaySinh = staffBirthDate;        
                newStaff.DeleteStatus = false;

                DP.Database.NhanViens.Add(newStaff);
                DP.Database.SaveChanges();

                MessageBox.Show("Đã thêm nhân viên");
            }));
        }

        private void addTransactionButton_Click(object sender, RoutedEventArgs e)
        {
            App.Current.Dispatcher.BeginInvoke(DispatcherPriority.Background, new Action(() =>
            {
                string transactionCustomerCode = "";
                string transactionHouseCode = "";
                string transactionOwnerCode = "";
                DateTime transactionDate;
                string transactionType = "";
                int rentDuration = 0;
                if(transactionCustomerListComboBox.SelectedIndex >= 0)
                {
                    transactionCustomerCode = CustomerList[transactionCustomerListComboBox.SelectedIndex].MaKH;
                }
                if(transactionCustomerCode.Length == 0)
                {
                    MessageBox.Show("Chưa chọn mã khách hàng");
                    return;
                }
                if (transactionHouseListComboBox.SelectedIndex >= 0)
                {
                    transactionHouseCode = RentHouseList[transactionHouseListComboBox.SelectedIndex].MaNha;
                    transactionOwnerCode = RentHouseList[transactionHouseListComboBox.SelectedIndex].MaCN;
                }
                if(transactionHouseCode.Length == 0)
                {
                    MessageBox.Show("Chưa chọn mã nhà - chủ nhà");
                    return;
                }

                if (transactionDatePicker.SelectedDate == null)
                {
                    MessageBox.Show("Chưa chọn ngày giao dịch");
                    return;
                }

                if (transactionTypeComboBox.Text.Length == 0)
                {
                    MessageBox.Show("Chưa chọn loại giao dịch");
                    return;
                }

               
                transactionDate = (DateTime)transactionDatePicker.SelectedDate;
                transactionType = transactionTypeComboBox.Text;

                if (transactionType == "THUE")
                {
                    if(rentDurationTextBox.Text.Length == 0)
                    {
                        MessageBox.Show("Chưa nhập số tháng thuê");
                        return;
                    }
                    if(int.TryParse(rentDurationTextBox.Text, out rentDuration) == false)
                    {
                        MessageBox.Show("Số tháng thuê không hợp lệ");
                        return;
                    }
                }
                else if(transactionType == "MUA")
                {
                    rentDuration = 0;
                    MessageBox.Show("Mua nhà thì tháng thuê là 0");
                }

                LichSuGiaoDich newTransaction = new LichSuGiaoDich();
                newTransaction.MaKH = transactionCustomerCode;
                newTransaction.MaNha = transactionHouseCode;
                newTransaction.MaCN = transactionOwnerCode;
                newTransaction.NgayGD = transactionDate;
                newTransaction.LoaiGD = transactionType;
                newTransaction.SoThangThue = rentDuration;
                newTransaction.DeleteStatus = false;

                DP.Database.LichSuGiaoDiches.Add(newTransaction);
                DP.Database.SaveChanges();
                MessageBox.Show("Đã thêm lịch sử giao dịch");

            }));           
        }

        
    }

    
}
