using System;
using System.Linq;
using System.Windows;
using DaoProject.Dao;
using DaoProject.Model;
using Telerik.Windows.Controls;

namespace Inventario.Formularios.MobiliarioFolder
{
    /// <summary>
    /// Interaction logic for DeleteMobiliario.xaml
    /// </summary>
    public partial class DeleteMobiliario
    {
        private readonly Mobiliario mobiliario;
        private bool? observacionesResult = false;
        private String observacionesDelete = String.Empty;

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

            DialogParameters parameters = new DialogParameters();
            parameters.Content = "Observaciones de la baja:";
            parameters.Header = "Atención:";
            parameters.Closed = this.OnClosed;
            parameters.Owner = this;

           new MobiliarioModel(mobiliario).BajaMobiliario(observacionesDelete);

           this.Close();
        }

        private void OnClosed(object sender, WindowClosedEventArgs e)
        {
            RadWindow win = (RadWindow)sender;
            observacionesResult = win.DialogResult;
            observacionesDelete = win.PromptResult;
        }

        private void RbtnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
