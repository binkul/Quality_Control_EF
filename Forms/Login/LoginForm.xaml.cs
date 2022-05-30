using Microsoft.EntityFrameworkCore;
using Quality_Control_EF.Forms.Register;
using Quality_Control_EF.Security;
using Quality_Control_EF.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Quality_Control_EF.Forms.Quality;

namespace Quality_Control_EF.Forms.Login
{
    /// <summary>
    /// Logika interakcji dla klasy LoginForm.xaml
    /// </summary>
    public partial class LoginForm : Window
    {
        private readonly string _loginPath = @"\Data\login.txt";
        private readonly List<string> _logins;

        public LoginForm()
        {
            InitializeComponent();

            _logins = GetLogins();
            CmbUserName.ItemsSource = _logins;
            if (_logins.Count > 0)
            {
                CmbUserName.Text = _logins[0];
            }
        }

        private void BtnSubbmit_Click(object sender, RoutedEventArgs e)
        {
            string password = Encrypt.MD5Encrypt(TxtPassword.Password);

            LabBookContext contex = new LabBookContext();
            User user = contex.Users
                .Where(x => x.Login.Equals(CmbUserName.Text))
                .Where(x => x.Password.Equals(password))
                .FirstOrDefault();

            SaveLogins();

            if (user == null)
            {
                _ = MessageBox.Show("Nieprawidłowy login lub hasło. Spróbuj ponownie",
                    "Błąd logowania", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else if ((bool)user.Active)
            {
                _ = UserSingleton.CreateInstance(user.Id, user.Name, user.Surname, user.Permission, user.Identifier, (bool)user.Active);
                QualityForm quality = new QualityForm();
                quality.Show();
                Close();
            }
            else
            {
                _ = MessageBox.Show("Użytkownik: '" + user.Login + "' jest jeszcze nieaktywny. Skontaktuj się z administratorem.",
                    "Brak uprawnień", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void BtnRegister_Click(object sender, RoutedEventArgs e)
        {
            RegisterForm registerForm = new RegisterForm();
            registerForm.Show();
            Close();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                BtnSubbmit_Click(null, null);
            }
        }

        private List<string> GetLogins()
        {
            List<string> logins = new List<string>();
            if (File.Exists(Environment.CurrentDirectory + _loginPath))
            {
                StreamReader file = new StreamReader(Environment.CurrentDirectory + _loginPath);
                string line;
                while ((line = file.ReadLine()) != null)
                {
                    logins.Add(line);
                }
                file.Close();
            }
            return logins;
        }

        private void SaveLogins()
        {
            string login = CmbUserName.Text;
            string file = Environment.CurrentDirectory + _loginPath;

            _logins.Sort();
            _logins.Remove(login);
            if (!_logins.Contains(login))
            {
                _logins.Insert(0, login);
            }

            if (!Directory.Exists(Path.GetDirectoryName(file)))
                Directory.CreateDirectory(Path.GetDirectoryName(file));

            File.WriteAllLines(Environment.CurrentDirectory + _loginPath, _logins);
        }
    }
}
