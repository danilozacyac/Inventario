using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Inventario.Dao;
using Inventario.Model;
using Inventario.Singleton;
using Inventario.Utils;

namespace Inventario.Formularios.ServidoresFolder
{
    /// <summary>
    /// Interaction logic for AddUpdateUsuarios.xaml
    /// </summary>
    public partial class AddUpdateUsuarios
    {
        private ServidoresPublicos servidor;
        private readonly bool isUpdating;

        public AddUpdateUsuarios()
        {
            InitializeComponent();
            servidor = new ServidoresPublicos();
            ChkDesactivar.Visibility = System.Windows.Visibility.Hidden;
        }

        public AddUpdateUsuarios(ServidoresPublicos servidor)
        {
            InitializeComponent();
            this.servidor = servidor;
            isUpdating = true;

        }

        private void RadWindow_Loaded(object sender, RoutedEventArgs e)
        {
            RbtnBuscar.Visibility = (isUpdating) ? System.Windows.Visibility.Visible : System.Windows.Visibility.Collapsed;
            RbtnAceptar.Content = (isUpdating) ? "Actualizar" : "Aceptar";

            this.DataContext = servidor;
            this.RcbTitulo.DataContext = TitulosSingleton.Titulos;
            this.RcbArea.DataContext = AreasSingleton.Areas;
            this.RcbUbicacion.DataContext = UbicacionesSingleton.Ubicaciones;
        }

        private void RbtnBuscar_Click(object sender, RoutedEventArgs e)
        {

        }

        private void RbtnAceptar_Click(object sender, RoutedEventArgs e)
        {
            servidor.IdTitulo = Convert.ToInt32(RcbTitulo.SelectedValue);
            servidor.IdArea = Convert.ToInt32(RcbArea.SelectedValue);
            servidor.IdUbicacion = Convert.ToInt32(RcbUbicacion.SelectedValue);

            if (isUpdating)
            {
                ServidoresModel model = new ServidoresModel(servidor);

                if (ChkDesactivar.IsChecked == true)
                    model.DesactivarUsuario();
                else
                    model.ActualizaInfoServidores();
            }
            else
                new ServidoresModel(servidor).SetNewUser();

            this.Close();
        }

        private void RbtnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void TxtExtension_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = MiscFunt.IsADigit(e.Text);
        }

        private void ChkDesactivar_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void ChkDesactivar_Unchecked(object sender, RoutedEventArgs e)
        {

        }


    }
}