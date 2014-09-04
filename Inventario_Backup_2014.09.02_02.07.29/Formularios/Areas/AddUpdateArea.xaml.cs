using System;
using System.Linq;
using System.Windows;
using DaoProject.Dao;
using DaoProject.Model;

namespace Inventario.Formularios.Areas
{
    /// <summary>
    /// Interaction logic for AddUpdateArea.xaml
    /// </summary>
    public partial class AddUpdateArea
    {
        private readonly CommonProperties area;
        private readonly bool isUpdating = false;
        private readonly bool isDeleting = false;
        private readonly int action = 0;

        public AddUpdateArea()
        {
            InitializeComponent();
            area = new CommonProperties();
        }

        public AddUpdateArea(CommonProperties area, int action)
        {
            InitializeComponent();
            this.area = area;
            this.action = action;
            this.isUpdating = (action == 1) ? true : false;
            this.isDeleting = (action == 1) ? false : true;
        }

        private void RadWindow_Loaded_1(object sender, RoutedEventArgs e)
        {
            this.DataContext = area;

            RBtnAceptar.Content = (action == 1) ? "Actualizar" : "Eliminar";
        }

        private void RBtnAceptar_Click_1(object sender, RoutedEventArgs e)
        {
            AreasModel model = new AreasModel(area);
            if (isUpdating || isDeleting)
            {
                if (action == 1)
                    model.UpdateArea();
                else if (action == 2)
                    model.DeleteArea();
            }
            else
            {
                model.SetNewArea();
            }

            DialogResult = true;
            this.Close();
        }

        private void RBtnCancelar_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            this.Close();
        }
    }
}