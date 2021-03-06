﻿using DaoProject.Dao;
using DaoProject.Model;
using DaoProject.Singleton;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Inventario.UserControls
{
    /// <summary>
    /// Interaction logic for GridReportes.xaml
    /// </summary>
    public partial class GridReportes : UserControl
    {
        public LevantaReporte selectedReporte;

        public GridReportes()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            GReporte.DataContext = LevantaReporteSingleton.Reportes;
        }

        private void GReporte_SelectionChanged(object sender, Telerik.Windows.Controls.SelectionChangeEventArgs e)
        {
            selectedReporte = GReporte.SelectedItem as LevantaReporte;
        }

        private void SearchTextBox_Search(object sender, RoutedEventArgs e)
        {
            String tempString = ((TextBox)sender).Text.ToUpper();

            if (!String.IsNullOrEmpty(tempString))
                GReporte.DataContext = (from n in LevantaReporteSingleton.Reportes
                                        where n.NumReporte.ToString().Contains(tempString) || n.Nombre.ToUpper().Contains(tempString) || n.ScEquipo.Contains(tempString)
                                        select n).ToList();
            else
                GReporte.DataContext = LevantaReporteSingleton.Reportes;
        }
    }
}
