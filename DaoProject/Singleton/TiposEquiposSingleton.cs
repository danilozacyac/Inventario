using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using DaoProject.Dao;
using DaoProject.Model;

namespace DaoProject.Singleton
{
    public class TiposEquiposSingleton : INotifyPropertyChanged
    {
        private static readonly TiposEquiposSingleton mySingletonInstance = new TiposEquiposSingleton();

        private TiposEquiposSingleton(){}

        public static TiposEquiposSingleton MySingletonInstance
        {
            get
            {
                return mySingletonInstance;
            }
        }

        private List<CommonProperties> tipos;
        public List<CommonProperties> Tipos
        {
            get
            {
                if (tipos == null)
                    tipos = new TiposEquiposModel().GetTiposEquipos();

                return (from n in tipos
                        where n.Corto == AccesoUsuarioModel.Grupo.ToString()
                        orderby n.Descripcion
                        select n).ToList();
            }
        }

        public void AddTipos(CommonProperties properties)
        {
            TiposEquiposSingleton.MySingletonInstance.Tipos.Add(properties);
            OnPropertyChanged("AddTipos");
        }

        public void RemoveTipos(CommonProperties properties)
        {
            TiposEquiposSingleton.MySingletonInstance.Tipos.Remove(properties);
            OnPropertyChanged("RemoveTipos");
        }

        public void UpdateTipos(CommonProperties currentProperty, CommonProperties updatedProperty)
        {
            int index = MySingletonInstance.Tipos.FindLastIndex(s => s.IdElemento == currentProperty.IdElemento);

            MySingletonInstance.Tipos[index] = updatedProperty;
            OnPropertyChanged("UpdateTipos");
        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion // INotifyPropertyChanged Members
    }

    //public class TiposEquiposSingleton
    //{
    //    private static List<CommonProperties> tipos;

    //    private TiposEquiposSingleton()
    //    {
    //    }

    //    public static List<CommonProperties> Tipos
    //    {
    //        get
    //        {
    //            if (tipos == null)
    //                tipos = new TiposEquiposModel().GetTiposEquipos();

    //            return (from n in tipos
    //                    where n.Corto == AccesoUsuarioModel.Grupo.ToString()
    //                    orderby n.Descripcion
    //                    select n).ToList();
    //        }
    //    }

    //    public static void AddTipos(CommonProperties properties)
    //    {
    //        TiposEquiposSingleton.tipos.Add(properties);
    //    }

    //    public static void RemoveTipos(CommonProperties properties)
    //    {
    //        TiposEquiposSingleton.tipos.Remove(properties);
    //    }

    //    public static void UpdateTipos(CommonProperties currentProperty, CommonProperties updatedProperty)
    //    {
    //        int index = TiposEquiposSingleton.Tipos.FindLastIndex(s => s.IdElemento == currentProperty.IdElemento);

    //        TiposEquiposSingleton.tipos[index] = updatedProperty;
    //    }


    //}
}
