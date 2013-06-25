using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Inventario.Dao;
using Inventario.Model;
using Inventario.Singleton;
using Telerik.Windows.Controls;

namespace Inventario.Formularios.EquiposFolder
{
    /// <summary>
    /// Interaction logic for DeleteEquipo.xaml
    /// </summary>
    public partial class DeleteEquipo
    {
        private List<Equipos> equipos;
        private int currentIndex = 0;
        private Equipos equipo;

        private bool? observacionesResult = false;
        private String observacionesDelete = String.Empty;

        public DeleteEquipo()
        {
            InitializeComponent();
        }

        public DeleteEquipo(Equipos equipo)
        {
            InitializeComponent();
            this.equipo = equipo;
            this.DataContext = equipo;
            RcbTipoEquipo.Text = equipo.TipoEquipo;
            //TxtScEquipo.Text = equipo.ScEquipo;
            //RbtnBuscar_Click(null, null);
        }

        private void RadWindow_Loaded(object sender, RoutedEventArgs e)
        {
            RcbTipoEquipo.ItemsSource = TiposEquiposSingleton.Tipos;
        }

        private void RbtnEliminar_Click(object sender, RoutedEventArgs e)
        {
            EquiposModel model = new EquiposModel();

            DialogParameters parameters = new DialogParameters();
            parameters.Content = "Observaciones de la baja:";
            parameters.Header = "Atención:";
            parameters.Closed = this.OnClosed;
            parameters.Owner = this;

            RadWindow.Prompt(parameters);

            if (ChkOtros.IsChecked == true)
            {
                model.BajaEquipo(equipos, observacionesDelete);
                //ServidoresSingleton.RemoveEquipoUsuario(equipo.Expediente, equipos);
            }
            else
            {
                List<Equipos> lEquipos = new List<Equipos>() { equipos[currentIndex] };
                model.BajaEquipo(lEquipos, observacionesDelete);
                //ServidoresSingleton.RemoveEquipoUsuario(equipos[currentIndex].Expediente, lEquipos);

            }

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

        private void RbtnBuscar_Click(object sender, RoutedEventArgs e)
        {
            currentIndex = 0;
            equipos = new EquiposModel().GetEquiposPorParametro("SC_Equipo", TxtScEquipo.Text);

            if (equipos.Count > 0)
            {
                this.DataContext = equipos[currentIndex];
                RcbTipoEquipo.Text = equipos[currentIndex].TipoEquipo;

                if (equipos.Count > 1)
                {
                    this.ButtonPanelVisibility(Visibility.Visible);
                    RbtnPrevious.IsEnabled = false;
                    TxtNumEquipos.Text = (currentIndex + 1) + "/" + equipos.Count + " equipos";
                }
                else
                {
                    this.ButtonPanelVisibility(Visibility.Hidden);
                    TxtNumEquipos.Text = "1/1 equipos";
                }
                RbtnEliminar.IsEnabled = true;
            }
            else
            {
                MessageBox.Show("No existen equipos con ese número de inventario");
            }

        }

        private void TxtScEquipo_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            RbtnEliminar.IsEnabled = false;
        }

        private void ButtonPanelVisibility(Visibility isVisible)
        {
            ChkOtros.Visibility = isVisible;
            RbtnFirst.Visibility = isVisible;
            RbtnPrevious.Visibility = isVisible;
            RbtnNext.Visibility = isVisible;
            RbtnLast.Visibility = isVisible;
        }

        private void ButtonPanel_Click(object sender, RoutedEventArgs e)
        {
            RadButton button = sender as RadButton;

            switch (button.Tag.ToString())
            {
                case "-1":
                    currentIndex = currentIndex - 1;
                    break;
                case "0":
                    currentIndex = 0;
                    break;
                case "1":
                    currentIndex = currentIndex + 1;
                    break;
                case "2":
                    currentIndex = equipos.Count - 1;

                    break;
                default:
                    break;
            }

            this.DataContext = equipos[currentIndex];
            TxtNumEquipos.Text = (currentIndex + 1) + "/" + equipos.Count + " equipos";
            RcbTipoEquipo.Text = equipos[currentIndex].TipoEquipo;

            RbtnPrevious.IsEnabled = (currentIndex - 1 < 0) ? false : true;
            RbtnNext.IsEnabled = (currentIndex + 1 == equipos.Count) ? false : true;
        }


    }
}