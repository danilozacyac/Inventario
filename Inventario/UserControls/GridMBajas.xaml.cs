﻿using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using DaoProject.Model;

namespace Inventario.UserControls
{
    /// <summary>
    /// Lógica de interacción para GridMBajas.xaml
    /// </summary>
    public partial class GridMBajas : UserControl
    {
        public GridMBajas()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            GMBajas.DataContext = new MobiliarioModel().GetBajas().Tables[0].DefaultView;
        }
    }
}
