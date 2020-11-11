using Baskin_Kiosk.Common;
using Baskin_Kiosk.Model;
using Baskin_Kiosk.Model.DAO;
using Baskin_Kiosk.Network;
using Baskin_Kiosk.Util;
using Baskin_Kiosk.View.HomePage;
using Baskin_Kiosk.ViewModel;
using MySql.Data.MySqlClient;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace Baskin_Kiosk.View.PaymentPage
{
    public partial class PayComplete : Page
    {
        private MemberDAO memberDAO = new MemberDAO();
        private OrderDAO orderDAO = new OrderDAO();
        private OrderViewModel orderViewModel = App.orderViewModel;
        private ServerConnection serverConnection = new ServerConnection();

        public PayComplete(int orderType, string e)
        {
            InitializeComponent();

            MemberModel member = memberDAO.getMember(orderType, e);

            int orderNumber = getLastNum() + 1;
            String lastNum = orderNumber < 10 ? "00" + orderNumber : orderNumber < 100 ? "00" + orderNumber.ToString() : orderNumber.ToString();

            userName.Content = "주문자: " + member.name;
            totalPrice.Content = "총 금액: " + orderViewModel.totalAmountPrice;
            orderNum.Content = "주문번호: " + lastNum;

            foreach (Food food in orderViewModel.selectMenuList)
            {
                foreach (OrderModel order in orderViewModel.orderMenuList)
                {
                    order.orderType = orderType;
                    order.orderTime = DateTime.Now;
                    order.userId = member.id;
                    order.orderNum = orderNumber;
                }
            }
            orderDAO.orderMenu();

            // 주문됐을때 서버로 메시지 보내기
            this.serverConnection.sendOrderMessage(orderViewModel.selectMenuList, orderNumber);
        }

        public int getLastNum()
        {
            DBConnection connection = new DBConnection();
            connection.getConnection();

            int lastNum = 0;

            String sql = "SELECT order_num FROM kiosk.order ORDER BY order_num DESC LIMIT 1";

            connection.setCommand(sql);
            connection.executeNonQuery();
            MySqlDataReader reader = connection.executeReader();

            while (reader.Read())
            {
                lastNum = int.Parse(reader["order_num"].ToString());
            }

            connection.closeConnection();

            if (lastNum > 100)
            {
                lastNum -= 100;
            }

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
