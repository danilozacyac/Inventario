using System;
using System.Linq;
using System.Windows;
using DaoProject.Dao;
using DaoProject.Model;
using DaoProject.Singleton;
using DaoProject.Utilities;

namespace Inventario.Formularios.EquiposFolder
{
    /// <summary>
    /// Interaction logic for AddEquipos.xaml
    /// </summary>
    public partial class AddEquipos
    {
        private Equipos equipo;
        private int expediente = 10;
        private bool isServidorExist = false;

        public AddEquipos()
        {
            InitializeComponent();
            this.equipo = new Equipos();
        }



        private void RadWindow_Loaded(object sender, RoutedEventArgs e)
        {
            RcbTipoEquipo.ItemsSource = TiposEquiposSingleton.TiposComputo;
            RcbTitulos.ItemsSource = TitulosSingleton.Titulos;
            RcbAreas.ItemsSource = AreasSingleton.Areas;
            RcbUbicacion.ItemsSource = UbicacionesSingleton.Ubicaciones;
            this.DataContext = equipo;
        }

        private void ChkAsignar_Checked(object sender, RoutedEventArgs e)
        {
            Usuario.Visibility = System.Windows.Visibility.Visible;
        }

        private void ChkAsignar_Unchecked(object sender, RoutedEventArgs e)
        {
            Usuario.Visibility = System.Windows.Visibility.Collapsed;
            this.expediente = 10;
        }

        private void RbtnVerOtros_Click(object sender, RoutedEventArgs e)
        {
            AddSubEquipos sub = new AddSubEquipos(10);
            sub.ShowDialog();
        }

        private void RbtnAceptar_Click(object sender, RoutedEventArgs e)
        {
            if (ChkAsignar.IsChecked == true && !isServidorExist)
            {
                MessageBox.Show("No ha ingresado un número de expediente válido para asignar el equipo, si no tiene el número de expediente o no desea asignar el equipo en este momento quite la selección de la casilla \"Asignar equipo\"", "Error:", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                if (ConstVariables.ListaSubEquipos.Count > 0)
                {
                    MessageBoxResult result = MessageBox.Show("Se agregaran " + ConstVariables.ListaSubEquipos.Count + " componentes con el equipo principal, deseas continuar?",
                        "Atención:", MessageBoxButton.OKCancel, MessageBoxImage.Question);
                    if (result == MessageBoxResult.Cancel)
                        return;
                }

                this.expediente = (ChkAsignar.IsChecked == true) ? Convert.ToInt32(TxtExpediente.Text) : 10;

                equipo.Expediente = expediente;
                equipo.IdTipo = Convert.ToInt32(RcbTipoEquipo.SelectedValue);

               int userId =  new EquiposModel(equipo).SetNewEquipo();

               if (userId == -1)
               {
                   MessageBox.Show("El equipo que intenta ingresar ya esta registrado. Verifique por favor");
               }else
               {
                   //Agregamos cada uno de los subequipos
                   foreach (Equipos equipoAlta in ConstVariables.ListaSubEquipos)
                   {
                       if (ChkAsignar.IsChecked == true && Convert.ToInt32(TxtExpediente.Text) != expediente)
                       {
                           equipoAlta.Expediente = Convert.ToInt32(TxtExpediente.Text);
                       }

                      userId =  new EquiposModel(equipoAlta).SetNewEquipo();

                      if (userId == -1)
                      {
                          MessageBox.Show("El equipo que intenta ingresar ya esta registrado. Verifique por favor");
                      }
                   }

                   ConstVariables.ListaSubEquipos.Add(equipo);
                   ServidoresSingleton.AddEquiposAUsuario(expediente, ConstVariables.ListaSubEquipos);

                   //Al final limpiamos la Lista de subequipos de ConstVariables
                   ConstVariables.ListaSubEquipos.Clear();
                   DialogResult = true;
                   this.Close();
               }

                
            }
        }

        private void RbtnCancelar_Click(object sender, RoutedEventArgs e)
        {
            //Al final limpiamos la Lista de subequipos de ConstVariables
            ConstVariables.ListaSubEquipos.Clear();
            DialogResult = false;
            this.Close();
        }

        private void RbtnBuscar_Click(object sender, RoutedEventArgs e)
        {
            ServidoresPublicos servidor = new ServidoresModel().GetUsuarioPorExpediente(Convert.ToInt32(TxtExpediente.Text));
            Usuario.DataContext = servidor;

            if (servidor != null)
                isServidorExist = true;
        }

        private void TxtExpediente_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            // Usuario.DataContext = new ServidoresPublicos();
        }

        private void RcbTipoEquipo_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            int val = Convert.ToInt32(RcbTipoEquipo.SelectedValue);

            if (val == 1 || val == 8)
                RbtnVerOtros.Visibility = System.Windows.Visibility.Visible;
            else
                RbtnVerOtros.Visibility = System.Windows.Visibility.Hidden;
        }
    }
}