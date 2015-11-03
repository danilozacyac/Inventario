using System;
using System.Linq;
using System.Windows;
using DaoProject.Dao;
using DaoProject.Model;
using DaoProject.Singleton;

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
            RcbTipoEquipo.ItemsSource = TiposEquiposSingleton.TiposComputo;
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
            //equipo.IdTipo = Convert.ToInt32(RcbTipoEquipo.SelectedValue);
            EquiposModel upd = new EquiposModel(equipo);
            upd.UpdateEquipo(((equipo.ScEquipo.Equals(TxtScEquipo.Text)) ? String.Empty : TxtScEquipo.Text), Convert.ToInt32(RcbTipoEquipo.SelectedValue) );

            DialogResult = true;

            this.Close();
        }
    }
}
