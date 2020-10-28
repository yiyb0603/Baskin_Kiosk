using Baskin_Kiosk.Common;
using Baskin_Kiosk.Model;
using Baskin_Kiosk.Model.DAO;
using Baskin_Kiosk.ViewModel;
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
            userName.Content = "주문자: "+ member.name;
            totalPrice.Content = "총 금액: " + orderViewModel.totalAmountPrice;


            foreach (Food food in orderViewModel.selectMenuList)
            {
                foreach (OrderModel order in orderViewModel.orderMenuList)
                {
                    order.orderType = food.orderType;
                    order.orderTime = DateTime.Now;
                    order.userId = member.id;
                }
            }

            orderDAO.orderMenu();
        }
    }
}
