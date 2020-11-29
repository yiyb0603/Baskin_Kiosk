using System;
using System.Windows;
using System.IO;

namespace Baskin_Kiosk.View.LoginPage
{
    public partial class Login : Window
    {
        private const string ADMIN_ID = "admin";
        private const string ADMIN_PW = "1234";

        // 로그인 유지 유무 파일
        private const string FILE_PATH = "../../Assets/data.txt";

        public Login()
        {
            InitializeComponent();
            this.Loaded += Login_Loaded;
        }

        private void Login_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!File.Exists(FILE_PATH))
                {
                    File.Create(FILE_PATH);
                    return;
                }

                if (File.ReadAllText(FILE_PATH) == "TRUE")
                {   
                    string response = App.connection.SendMessage();
                    this.validateLogin(response);
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void validateLogin(string response)
        {
            switch (response)
            {
                // 서버가 꺼져있을때, 비연결 상태로 로그인 확인 눌렀을때
                case "1":
                    this.closeLogin();
                    break;

                // 서버가 꺼져있을때, 비연결 상태로 로그인 취소 눌렀을때
                case "2":
                    this.IsEnabled = true;
                    break;

                // 서버가 켜져있고, 로그인 됬을때
                case "200":
                    App.connection.ThreadStart();
                    this.closeLogin();
                    break;
            }
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            string inputID = this.inputID.Text;
            string inputPW = this.inputPW.Password;

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

            if (File.Exists(FILE_PATH))
            {
                File.WriteAllText(FILE_PATH, autoCheck.IsChecked == true ? "TRUE" : "FALSE");
            }

            string response = App.connection.SendMessage();
            this.validateLogin(response);
        }

        private void closeLogin()
        {
            this.DialogResult = true;
            this.IsEnabled = false;
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
