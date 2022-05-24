using Quality_Control.Forms.Login;
using Quality_Control.Security;
using Quality_Control_EF.Models;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace Quality_Control.Forms.Register
{
    /// <summary>
    /// Logika interakcji dla klasy RegisterForm.xaml
    /// </summary>
    public partial class RegisterForm : Window
    {
        public RegisterForm()
        {
            InitializeComponent();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                BtnSubbmit_Click(null, null);
            }
        }

        private void BtnSubbmit_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidateTextBox()) return;

            LabBookContext contex = new LabBookContext();
            bool status = contex.Users.Any(x => x.Login.Equals(TxtLogin.Text));

            if (!status)
            {
                User user = new User
                {
                    Name = TxtName.Text,
                    Surname = TxtSurname.Text,
                    EMail = TxtEmail.Text,
                    Login = TxtLogin.Text,
                    Identifier = TxtName.Text.ToUpper().Substring(0, 1) + TxtSurname.Text.ToUpper().Substring(0, 1),
                    Active = false,
                    Password = Encrypt.MD5Encrypt(TxtPassword.Password)
                };

                _ = contex.Users.Add(user);
                _ = contex.SaveChanges();

                LoginForm loginForm = new LoginForm();
                loginForm.Show();
                Close();
            }
            else
            {
                _ = MessageBox.Show("Użytkownik o loginie: '" + TxtLogin.Text + "' istnieje już w bazie. Użyj innego loginu do rejestracji.",
                    "Informacja", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private bool ValidateTextBox()
        {
            bool name = string.IsNullOrEmpty(TxtName.Text);
            bool surName = string.IsNullOrEmpty(TxtSurname.Text);
            bool email = string.IsNullOrEmpty(TxtEmail.Text);
            bool login = string.IsNullOrEmpty(TxtLogin.Text);
            bool password = string.IsNullOrEmpty(TxtPassword.Password);
            bool passwordRepeat = string.IsNullOrEmpty(TxtPasswordRepeat.Password);

            if (name || surName || email || email || login || password || passwordRepeat)
            {
                _ = MessageBox.Show("Należy wypełnić wszystkie pola.", "Puste pola", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (TxtPassword.Password.Length < 3)
            {
                _ = MessageBox.Show("Hasło jest za krókie. Wprowadź inne hasło.", "Błąd hasła", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (TxtPassword.Password != TxtPasswordRepeat.Password)
            {
                _ = MessageBox.Show("Powtórzone hasło się nie zgadza!", "Błąd hasła", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            return true;
        }

    }
}
