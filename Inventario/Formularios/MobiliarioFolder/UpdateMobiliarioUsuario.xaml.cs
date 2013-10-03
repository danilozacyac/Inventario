using System;
using System.Linq;
using System.Windows;
using DaoProject.Dao;
using DaoProject.Model;
using DaoProject.Singleton;

namespace Inventario.Formularios.MobiliarioFolder
{
    /// <summary>
    /// Interaction logic for UpdateMobiliarioUsuario.xaml
    /// </summary>
    public partial class UpdateMobiliarioUsuario
    {
        private readonly ServidoresPublicos servidorActual;
        private ServidoresPublicos servidorNuevo;
        private readonly Mobiliario mobiliario;

        //public UpdateMobiliarioUsuario()
        //{
        //    InitializeComponent();
        //}

        public UpdateMobiliarioUsuario(ServidoresPublicos servidorActual, Mobiliario mobiliario)
        {
            InitializeComponent();
            this.servidorActual = servidorActual;
            this.mobiliario = mobiliario;
        }

        private void WinUpdateUserEquipo_Loaded(object sender, RoutedEventArgs e)
        {
            GridActual.DataContext = servidorActual;
        }

        private void RbtnBuscar_Click(object sender, RoutedEventArgs e)
        {
            servidorNuevo = new ServidoresModel().GetUsuarioPorExpediente(Convert.ToInt32(txtAExpediente.Text));

            GridNuevo.DataContext = servidorNuevo;
        }

        private void RbtnAceptar_Click(object sender, RoutedEventArgs e)
        {
            HistorialMobiliario historial = new HistorialMobiliario();
            historial.IdMobiliario = mobiliario.IdMobiliario;
            historial.ExpAnterior = servidorActual.Expediente;
            historial.ExpActual = servidorNuevo.Expediente;
            historial.Observaciones = txtAObservaciones.Text;

            MobiliarioModel model = new MobiliarioModel(mobiliario);
            model.UpdateMobiliario(historial);
            ServidoresSingleton.RemoveMobiliarioUsuario(servidorActual.Expediente, mobiliario);
            ServidoresSingleton.AddMobiliarioUsuario(servidorNuevo.Expediente, mobiliario);

            this.Close();
        }

        private void RbtnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


    }
}
