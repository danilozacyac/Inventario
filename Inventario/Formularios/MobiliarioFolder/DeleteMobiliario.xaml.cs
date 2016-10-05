using System;
using System.Linq;
using System.Windows;
using DaoProject.Dao;
using DaoProject.Model;
using DaoProject.Singleton;

namespace Inventario.Formularios.MobiliarioFolder
{
    /// <summary>
    /// Interaction logic for DeleteMobiliario.xaml
    /// </summary>
    public partial class DeleteMobiliario
    {
        private readonly Mobiliario mobiliario;
        private bool? observacionesResult = false;

        public DeleteMobiliario(Mobiliario mobiliario)
        {
            InitializeComponent();
            this.mobiliario = mobiliario;
        }

        private void RadWindow_Loaded_1(object sender, RoutedEventArgs e)
        {
            this.DataContext = mobiliario;
        }

        private void RbtnEliminar_Click(object sender, RoutedEventArgs e)
        {
           new MobiliarioModel(mobiliario).BajaMobiliario(TxtObservaciones.Text);
           ServidoresSingleton.RemoveMobiliarioUsuario(mobiliario.Expediente, mobiliario);

           this.Close();
        }

        
        private void RbtnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
