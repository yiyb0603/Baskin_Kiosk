using System;
using System.Windows;
using System.IO;

namespace Baskin_Kiosk.View.LoginPage
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        private const String ADMIN_ID = "admin";
        private const String ADMIN_PW = "1234";

        // 로그인 유지 유무 파일
        private const String FILE_PATH = "../../Assets/data.txt";

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
                    String response = App.connection.sendMessage();
                    this.closeLogin();
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
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

            if (File.Exists(FILE_PATH))
            {
                File.WriteAllText(FILE_PATH, this.autoCheck.IsChecked == true ? "TRUE" : "FALSE");
            }

            String response = App.connection.sendMessage();
            if (response == "200")
            {
                this.closeLogin();
            }
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
