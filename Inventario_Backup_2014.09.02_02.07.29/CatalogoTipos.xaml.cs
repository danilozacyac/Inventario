﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using DaoProject.Dao;
using DaoProject.Model;
using DaoProject.Singleton;
using Telerik.Windows.Controls;

namespace Inventario
{
    /// <summary>
    /// Interaction logic for CatalogoTipos.xaml
    /// </summary>
    public partial class CatalogoTipos
    {
        private CommonProperties tipoSeleccionado;

        public CatalogoTipos()
        {
            InitializeComponent();
        }

        private void RadWindow_Loaded_1(object sender, RoutedEventArgs e)
        {
            RlstTipos.DataContext = TiposEquiposSingleton.MySingletonInstance.Tipos;
        }

        private void RbtnAgregar_Click_1(object sender, RoutedEventArgs e)
        {
            DialogParameters parameters = new DialogParameters();
            parameters.Content = "Ingresa la nueva descripcion";
            parameters.Closed = this.OnAddClosed;
            parameters.Header = "Agregar";
            parameters.Owner = this;
            RadWindow.Prompt(parameters);
        }

        private void OnAddClosed(object sender, WindowClosedEventArgs e)
        {
            RadWindow wind = sender as RadWindow;

            if (String.IsNullOrWhiteSpace(wind.PromptResult))
                return;
            else
            {
                CommonProperties common = new CommonProperties();
                common.Descripcion = wind.PromptResult;

                List<CommonProperties> existentes = new TiposEquiposModel().GetTiposEquiposForInsert(common.Descripcion);

                if (existentes.Count != 0)
                {
                    VerificaInsertaTipos verifica = new VerificaInsertaTipos(existentes);
                    verifica.ShowDialog();

                    if (verifica.DialogResult == true)
                        return;
                    else
                    {
                        TiposEquiposModel model = new TiposEquiposModel(common);
                        common = model.SetNewTipoEquipo();

                        TiposEquiposSingleton.MySingletonInstance.AddTipos(common);

                        RlstTipos.Items.Refresh();
                    }
                }
                else
                {
                    TiposEquiposModel model = new TiposEquiposModel(common);
                    common = model.SetNewTipoEquipo();

                    TiposEquiposSingleton.MySingletonInstance.AddTipos(common);

                    RlstTipos.Items.Refresh();
                }
            }

            RlstTipos.DataContext = TiposEquiposSingleton.MySingletonInstance.Tipos;

        }

        private void RbtnActualizar_Click(object sender, RoutedEventArgs e)
        {
            if (RlstTipos.SelectedIndex == -1)
            {
                MessageBox.Show("Seleccione el tipo de equipo que desea actualizar");
                return;
            }

            tipoSeleccionado = RlstTipos.SelectedItem as CommonProperties;

            DialogParameters parameters = new DialogParameters();
            parameters.Content = "Ingresa la nueva descripcion";
            parameters.Closed = this.OnUpdateClosed;
            parameters.Header = "Actualizar";
            parameters.DefaultPromptResultValue = tipoSeleccionado.Descripcion;
            parameters.Owner = this;

            RadWindow.Prompt(parameters);
        }

        private void OnUpdateClosed(object sender, WindowClosedEventArgs e)
        {
            RadWindow wind = sender as RadWindow;

            if (String.IsNullOrWhiteSpace(wind.PromptResult))
                return;
            else
            {
                CommonProperties common = tipoSeleccionado;
                common.Descripcion = wind.PromptResult;

                TiposEquiposModel model = new TiposEquiposModel(common);
                common = model.UpdateTipoEquipo();

                TiposEquiposSingleton.MySingletonInstance.UpdateTipos(tipoSeleccionado, common);

                RlstTipos.Items.Refresh();
            }

        }

        private void RbtnEliminar_Click(object sender, RoutedEventArgs e)
        {
            if (RlstTipos.SelectedIndex == -1)
            {
                MessageBox.Show("Seleccione el tipo de equipo que desea eliminar");
                return;
            }

            tipoSeleccionado = RlstTipos.SelectedItem as CommonProperties;
            RadWindow.Confirm("¿Estas seguro de eliminar este elemento?", OnDeleteClosed);
        }

        private void OnDeleteClosed(object sender, WindowClosedEventArgs e)
        {
            RadWindow wind = sender as RadWindow;

            if (wind.DialogResult == true)
            {
                TiposEquiposModel model = new TiposEquiposModel(tipoSeleccionado);
                model.DeleteTipoEquipo();

                RlstTipos.SelectedIndex = -1;
                TiposEquiposSingleton.MySingletonInstance.RemoveTipos(tipoSeleccionado);
                RlstTipos.Items.Refresh();
            }

        }

        private void RbtnCerrar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    }
}