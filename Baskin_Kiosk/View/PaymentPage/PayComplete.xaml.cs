using Baskin_Kiosk.Common;
using Baskin_Kiosk.Model;
using Baskin_Kiosk.Model.DAO;
using Baskin_Kiosk.Util;
using Baskin_Kiosk.View.HomePage;
using Baskin_Kiosk.ViewModel;
using MySql.Data.MySqlClient;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Baskin_Kiosk.View.PaymentPage
{
    /// <summary>
    /// Interaction logic for PayComplete.xaml
    /// </summary>
    public partial class PayComplete : Page
    {
        MemberDAO memberDAO = new MemberDAO();
        OrderDAO orderDAO = new OrderDAO();
        OrderViewModel orderViewModel = App.orderViewModel;

        public PayComplete(string e)
        {
            InitializeComponent();

            MemberModel member = memberDAO.getMember(0, e);
            int lastNum = getLastNum() + 1;
            userName.Content = "주문자: " + member.name;
            totalPrice.Content = "총 금액: " + orderViewModel.totalAmountPrice;
            orderNum.Content = "주문번호: " + lastNum.ToString();

            foreach (Food food in orderViewModel.selectMenuList)
            {
                foreach (OrderModel order in orderViewModel.orderMenuList)
                {
                    order.orderType = food.orderType;
                    order.orderTime = DateTime.Now;
                    order.userId = member.id;
                    order.orderNum = lastNum;
                }
            }

            orderDAO.orderMenu();
        }

        public int getLastNum()
        {
            DBConnection connection = new DBConnection();
            connection.getConnection();

            int lastNum = 0;

            string sql = "SELECT order_num FROM kiosk.order ORDER BY order_num DESC LIMIT 1";

            connection.setCommand(sql);
            connection.executeNonQuery();
            MySqlDataReader reader = connection.executeReader();

            while (reader.Read())
            {
                lastNum = int.Parse(reader["order_num"].ToString());
            }

            connection.closeConnection();

            return lastNum;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            orderViewModel.clearMenuList();
            while (NavigationService?.CanGoBack == true) { NavigationService?.RemoveBackEntry(); }
            NavigationService.Navigate(new Home());
        }
    }
}
