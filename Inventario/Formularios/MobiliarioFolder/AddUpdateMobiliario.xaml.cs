using System;
using System.Linq;
using System.Windows;
using DaoProject.Dao;
using DaoProject.Model;
using DaoProject.Singleton;
using ScjnUtilities;

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
            mobiliario = new Mobiliario();
            this.Header = "Agregar Mobiliario";
        }

        public AddUpdateMobiliario(Mobiliario mobiliario)
        {
            InitializeComponent();
            this.mobiliario = mobiliario;
            this.isUpdating = true;
            this.Header = "Editar Mobiliario";
        }

        private void RadWindow_Loaded(object sender, RoutedEventArgs e)
        {
            RcbTipoEquipo.DataContext = TiposEquiposSingleton.TiposMobiliario;
            RcbAreas.DataContext = AreasSingleton.Areas;
            RcbTitulos.DataContext = TitulosSingleton.Titulos;
            RcbUbicacion.DataContext = UbicacionesSingleton.Ubicaciones;
            this.DataContext = mobiliario;

            if (isUpdating)
            {
                ChkAsignar.Visibility = Visibility.Hidden;
                RcbTipoEquipo.SelectedValue = mobiliario.IdTipoMobiliario;
            }
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
            if (!isUpdating)
            {
                if (ChkAsignar.IsChecked == true && !isServidorExist)
                {
                    MessageBox.Show("No ha ingresado un número de expediente válido para asignar el equipo, si no tiene el número de expediente o no desea asignar el equipo en este momento quite la selección de la casilla \"Asignar equipo\"", "Error:", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    if (RcbTipoEquipo.SelectedValue == null || Convert.ToInt32(RcbTipoEquipo.SelectedValue) < 1)
                    {
                        MessageBox.Show("Seleccione el tipo de Mobiliario que esta por agregar", "Error:", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                    else
                    {
                        mobiliario.IdTipoMobiliario = Convert.ToInt32(RcbTipoEquipo.SelectedValue);
                        mobiliario.Inventario = Convert.ToInt32(TxtInventario.Text);
                        mobiliario.Expediente = Convert.ToInt32(TxtExpediente.Text);// (ChkAsignar.IsChecked == true) ? Convert.ToInt32(TxtExpediente.Text) : 10;
                        mobiliario.Observaciones = TxtObservaciones.Text;

                        MobiliarioModel model = new MobiliarioModel(mobiliario);
                        model.SetNewMobiliario();
                        ServidoresSingleton.AddMobiliarioUsuario(mobiliario.Expediente, mobiliario);

                        this.Close();
                    }
                }
            }
            else
            {
                mobiliario.IdTipoMobiliario = Convert.ToInt32(RcbTipoEquipo.SelectedValue);

                MobiliarioModel model = new MobiliarioModel(mobiliario);
                model.UpdateMobiliario();
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
            e.Handled = StringUtilities.IsTextAllowed(e.Text);
        }
    }
}