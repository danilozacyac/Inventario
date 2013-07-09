using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using DaoProject.Dao;

namespace Inventario
{
    /// <summary>
    /// Interaction logic for VerificaInsertaTipos.xaml
    /// </summary>
    public partial class VerificaInsertaTipos
    {
        private readonly List<CommonProperties> existentes;

        public VerificaInsertaTipos(List<CommonProperties> existentes)
        {
            InitializeComponent();
            this.existentes = existentes;
        }

        private void WinVerifica_Loaded(object sender, RoutedEventArgs e)
        {
            this.RlstSimilitudes.DataContext = existentes;
        }

        private void RbtnExiste_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            this.Close();
        }

        private void RbtnNoExiste_Click_1(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            this.Close();
        }
    }
}
