using System;
using System.Linq;
using System.Windows;
using DaoProject.Dao;
using DaoProject.Model;
using DaoProject.Singleton;
using DaoProject.Utilities;

namespace Inventario.Formularios.MobiliarioFolder
{
    /// <summary>
    /// Interaction logic for AddUpdateMobiliario.xaml
    /// </summary>
    public partial class AddUpdateMobiliario
    {
        private bool isServidorExist = false;
        private readonly bool isUpdating = false;
        private Mobiliario mobiliario;

        public AddUpdateMobiliario()
        {
            InitializeComponent();
        }

        public AddUpdateMobiliario(Mobiliario mobiliario)
        {
            InitializeComponent();
            this.mobiliario = mobiliario;
            this.isUpdating = true;
        }

        private void RadWindow_Loaded(object sender, RoutedEventArgs e)
        {
            RcbTipoEquipo.DataContext = TiposEquiposSingleton.Tipos;
            RcbAreas.DataContext = AreasSingleton.Areas;
            RcbTitulos.DataContext = TitulosSingleton.Titulos;
            RcbUbicacion.DataContext = UbicacionesSingleton.Ubicaciones;
        }

        private void RbtnBuscar_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(TxtExpediente.Text) || String.IsNullOrWhiteSpace(TxtExpediente.Text))
            {
                MessageBox.Show("Ingrese un número de registro");
                return;
            }

            ServidoresPublicos servidor = new ServidoresModel().GetUsuarioPorExpediente(Convert.ToInt32(TxtExpediente.Text));

            if (servidor != null)
            {
                Usuario.DataContext = servidor;
                RcbTitulos.SelectedValue = servidor.IdTitulo;
                RcbAreas.SelectedValue = servidor.IdArea;
                RcbUbicacion.SelectedValue = servidor.IdUbicacion;

                isServidorExist = true;
            }
        }

        private void RbtnAceptar_Click(object sender, RoutedEventArgs e)
        {
            if (ChkAsignar.IsChecked == true && !isServidorExist)
            {
                MessageBox.Show("No ha ingresado un número de expediente válido para asignar el equipo, si no tiene el número de expediente o no desea asignar el equipo en este momento quite la selección de la casilla \"Asignar equipo\"", "Error:", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                mobiliario = new Mobiliario();
                mobiliario.IdTipoMobiliario = Convert.ToInt32(RcbTipoEquipo.SelectedValue);
                mobiliario.Inventario = Convert.ToInt32(TxtInventario.Text);
                mobiliario.Expediente = Convert.ToInt32(TxtExpediente.Text);// (ChkAsignar.IsChecked == true) ? Convert.ToInt32(TxtExpediente.Text) : 10;
                mobiliario.Observaciones = TxtObservaciones.Text;

                MobiliarioModel model = new MobiliarioModel(mobiliario);
                model.SetNewMobiliario();
                ServidoresSingleton.AddMobiliarioUsuario(mobiliario.Expediente,  mobiliario );

                this.Close();
            }
        }

        private void RbtnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ChkAsignar_Checked(object sender, RoutedEventArgs e)
        {
            Usuario.Visibility = System.Windows.Visibility.Visible;
        }

        private void ChkAsignar_Unchecked(object sender, RoutedEventArgs e)
        {
            Usuario.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void TxtExpediente_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            e.Handled = MiscFunt.IsADigit(e.Text);
        }
    }
}