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

namespace Baskin_Kiosk.View.LoginPage
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        private const String ADMIN_ID = "admin";
        private const String ADMIN_PW = "1234";

        public Login()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            //TODO: 로그인 처리 
            String inputID = this.inputID.Text;
            String inputPW = this.inputPW.Password;

           if (!ADMIN_ID.Equals(inputID))
            {
                MessageBox.Show("아이디가 올바르지 않습니다.");
                return;
            }

           if (!ADMIN_PW.Equals(inputPW))
            {
                MessageBox.Show("비밀번호가 올바르지 않습니다.");
                return;
            }

            MessageBox.Show("로그인에 성공하였습니다.");
            //chris - 아이디와 비밀번호가 다를 경우, 아래 코드 처리하면 안됨
            this.DialogResult = true;
            this.Close();
        }

#if false //타이틀바사용시 활용
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //TODO: 로그인 되지 않은 상태라면 아래 코드 처리
            e.Cancel = true;
        }
#endif
    }
}
