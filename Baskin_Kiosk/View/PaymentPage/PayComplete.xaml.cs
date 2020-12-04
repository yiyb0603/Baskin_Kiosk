﻿using Baskin_Kiosk.Common;
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
        private readonly MemberDAO memberDAO = new MemberDAO();
        private readonly CsvOrderDAO csvOrderDAO = new CsvOrderDAO();
        private readonly OrderDAO orderDAO = new OrderDAO();
        private readonly OrderViewModel orderViewModel = App.orderViewModel;
        private readonly TcpCommunication serverConnection = new TcpCommunication();
        private static bool isConnected = TcpCommunication.isConnected;

        public PayComplete(int orderType, string e)
        {
            InitializeComponent();

            MemberModel member = memberDAO.GetMember(orderType, e);

            int orderNumber = GetLastNum() + 1;
            string lastNum = orderNumber < 10 ? "00" + orderNumber : orderNumber < 100 ? "0" + orderNumber.ToString() : orderNumber.ToString();

            userName.Content = "주문자: " + member.name;
            cardNum.Content = "카드번호: " + e;
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
            csvOrderDAO.InsertOrderDB();
            orderDAO.InsertOrderDB();

            MsgPacket packet = new MsgPacket();

            foreach (Food food in orderViewModel.selectMenuList)
            {
                packet.Menus.Add(new MsgOrderInfo() { Count = food.count.ToString(), Name = food.menuName, Price = food.price.ToString() });
            }

            if (isConnected)
            {
                serverConnection.SendMessage(packet.Menus, lastNum);
            }

            if (App.selectedSeat != null)
            {
                App.lstSeat[App.selectedSeat.seatNumber - 1].time = 60;
                App.lstSeat[App.selectedSeat.seatNumber - 1].UseSeat();
            }

            orderViewModel.clearMenuList();
        }

        public int GetLastNum()
        {
            DBConnection connection = new DBConnection();
            connection.GetConnection(Constants.DB_HOST);

            int lastNum = 0;

            string sql = "SELECT order_num FROM kiosk.order ORDER BY order_num DESC LIMIT 1";

            connection.SetCommand(sql);
            connection.ExecuteNonQuery();
            MySqlDataReader reader = connection.ExecuteReader();

            while (reader.Read())
            {
                lastNum = int.Parse(reader["order_num"].ToString());
            }

            connection.CloseConnection();

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
