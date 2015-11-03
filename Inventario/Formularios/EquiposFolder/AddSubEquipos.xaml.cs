using System;
using System.Linq;
using System.Windows;
using DaoProject.Dao;
using DaoProject.Singleton;
using DaoProject.Utilities;

namespace Inventario.Formularios.EquiposFolder
{
    /// <summary>
    /// Interaction logic for AddSubEquipos.xaml
    /// </summary>
    public partial class AddSubEquipos
    {
        private Equipos equipo = null;
        private readonly int expediente;

        public AddSubEquipos(int expediente)
        {
            InitializeComponent();
            equipo = new Equipos();
            this.expediente = expediente;
        }

        private void RadWindow_Loaded(object sender, RoutedEventArgs e)
        {
            RcbTipoEquipo.DataContext = TiposEquiposSingleton.TiposComputo;
            this.DataContext = equipo;
        }

        private void RbtnAgregar_Click(object sender, RoutedEventArgs e)
        {

            equipo.Expediente = expediente;
            equipo.IdEquipo = Convert.ToInt32(RcbTipoEquipo.SelectedValue);

            RgridOtros.Items.Add(equipo);
            equipo = new Equipos();
            this.DataContext = equipo;

        }

        private void RbtnQuitar_Click(object sender, RoutedEventArgs e)
        {
            RgridOtros.Items.Remove(RgridOtros.SelectedItem);
        }

        private void RbtnTodos_Click(object sender, RoutedEventArgs e)
        {
            RgridOtros.Items.Clear();
            ConstVariables.ListaSubEquipos.Clear();
        }

        private void RbtnAceptar_Click(object sender, RoutedEventArgs e)
        {
            foreach (Equipos eq in RgridOtros.Items)
            {
                ConstVariables.ListaSubEquipos.Add(eq);
            }
            this.Close();
        }

        private void RbtnCancelar_Click(object sender, RoutedEventArgs e)
        {
            ConstVariables.ListaSubEquipos.Clear();
            this.Close();
        }
    }
}