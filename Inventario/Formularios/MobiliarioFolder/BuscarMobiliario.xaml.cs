using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using Inventario.Dao;
using Inventario.Model;

namespace Inventario.Formularios.MobiliarioFolder
{
    /// <summary>
    /// Interaction logic for BuscarMobiliario.xaml
    /// </summary>
    public partial class BuscarMobiliario
    {
        private Mobiliario mobiliario;
        private ServidoresPublicos servidor;

        public BuscarMobiliario()
        {
            InitializeComponent();
        }

        private void RadWindow_Loaded_1(object sender, RoutedEventArgs e)
        {

        }

        private void RbtnBuscar_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(TxtInventario.Text) || String.IsNullOrWhiteSpace(TxtInventario.Text))
            {
                return;
            }

            ObservableCollection<Mobiliario> lista = new MobiliarioModel().GetMobiliarioPorParametro("NoInventario", TxtInventario.Text);

            mobiliario = (lista.Count > 0) ? lista[0] : null;


            if (mobiliario != null)
            {
                MobiilarioAlta.DataContext = mobiliario;
                servidor = new ServidoresModel().GetUsuarioPorExpediente(mobiliario.Expediente);
                Usuario.DataContext = servidor;
                ActionButtons.Visibility = Visibility.Visible;
                Usuario.Visibility = Visibility.Visible;
            }
            else
            {
                MobiilarioAlta.DataContext = new Mobiliario();
                Usuario.DataContext = new ServidoresPublicos();
                ActionButtons.Visibility = Visibility.Collapsed;
                Usuario.Visibility = Visibility.Collapsed;
                MessageBox.Show("El número de inventario ingresado no existe. Favor de verificar");
            }
        }

        private void RbtnReasignaEquipo_Click(object sender, RoutedEventArgs e)
        {
            UpdateMobiliarioUsuario update = new UpdateMobiliarioUsuario(servidor, mobiliario);
            update.Owner = this.Owner;
            update.Show();
            this.Close();
        }

        private void RbtnBajaEquipo_Click(object sender, RoutedEventArgs e)
        {
            DeleteMobiliario delete = new DeleteMobiliario(mobiliario);
            delete.Owner = this.Owner;
            delete.Show();
            this.Close();
        }
    }
}