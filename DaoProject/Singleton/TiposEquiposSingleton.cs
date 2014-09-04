using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using DaoProject.Dao;
using DaoProject.Model;

namespace DaoProject.Singleton
{
    public class TiposEquiposSingleton : INotifyPropertyChanged
    {
        private static  TiposEquiposSingleton mySingletonInstance = new TiposEquiposSingleton();

        private TiposEquiposSingleton(){}

        public static TiposEquiposSingleton MySingletonInstance
        {
            get
            {
                return mySingletonInstance;
            }
        }

        private ObservableCollection<CommonProperties> tipos;
        public ObservableCollection<CommonProperties> Tipos
        {
            get
            {
                if (tipos == null)
                    tipos = new TiposEquiposModel().GetTiposEquipos();

                List<CommonProperties> obs = (from n in tipos
                                                                     where n.Corto == AccesoUsuarioModel.Grupo.ToString()
                                                                     orderby n.Descripcion
                                                                     select n).ToList();


                ObservableCollection<CommonProperties> newObs = new ObservableCollection<CommonProperties>();

                foreach (CommonProperties item in obs)
                    newObs.Add(item);

                return newObs;
            }
        }

        public void AddTipos(CommonProperties properties)
        {
            MySingletonInstance.Tipos.Add(properties);
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
            //int index = MySingletonInstance.Tipos.FindLastIndex(s => s.IdElemento == currentProperty.IdElemento);

            int index = MySingletonInstance.Tipos.IndexOf( MySingletonInstance.Tipos.Where(s => s.IdElemento == currentProperty.IdElemento).LastOrDefault());

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

}
