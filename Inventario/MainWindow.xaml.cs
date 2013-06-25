using System;
using System.Linq;
using System.Windows;
using Inventario.Model;

namespace Inventario
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            TxtUsuario.Focus();

            // new UsersLoading();
        }

        private void RbtnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void RbtnAceptar_Click(object sender, RoutedEventArgs e)
        {
            AccesoUsuarioModel.Usuario = TxtUsuario.Text;
            AccesoUsuarioModel.Pwd = TxtPassword.Password;

            if (AccesoModel.IsLogginCorrect())
            {
                //permite el Acceso a la aplicación
                // MessageBox.Show("Bienvenido", "Atención", MessageBoxButton.OK, MessageBoxImage.Warning);
                WMain main = new WMain();
                main.Show();
                main.BringToFront();
                this.Close();
            }
            else
            {
                MessageBox.Show("El usuario y/o contraseña que ingreso son incorrectos", "Atención", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }


    }
}