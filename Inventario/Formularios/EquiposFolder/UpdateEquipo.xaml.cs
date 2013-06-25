using System;
using System.Linq;
using System.Windows;
using Inventario.Dao;
using Inventario.Model;
using Inventario.Singleton;

namespace Inventario.Formularios.EquiposFolder
{
    /// <summary>
    /// Interaction logic for UpdateEquipo.xaml
    /// </summary>
    public partial class UpdateEquipo
    {
        private Equipos equipo;

        public UpdateEquipo(Equipos equipo)
        {
            InitializeComponent();
            this.equipo = equipo;

        }

        private void RadWindow_Loaded_1(object sender, RoutedEventArgs e)
        {
            RcbTipoEquipo.ItemsSource = TiposEquiposSingleton.Tipos;
            RcbTipoEquipo.SelectedValue = equipo.IdTipo;
            this.DataContext = equipo;
        }

        private void RbtnCancelar_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            this.Close();
        }

        private void RbtnAceptar_Click(object sender, RoutedEventArgs e)
        {
            equipo.IdTipo = Convert.ToInt32(RcbTipoEquipo.SelectedValue);
            EquiposModel upd = new EquiposModel(equipo);
            upd.UpdateEquipo();

            DialogResult = true;

            this.Close();
        }
    }
}
