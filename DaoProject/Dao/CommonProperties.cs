using System;
using System.ComponentModel;
using System.Linq;

namespace DaoProject.Dao
{
    public class CommonProperties : INotifyPropertyChanged
    {
        private int idElemento;
        private String descripcion;
        private String abreviatura;
        private String corto;


        public int IdElemento
        {
            get
            {
                return this.idElemento;
            }
            set
            {
                this.idElemento = value;
            }
        }

        public String Descripcion
        {
            get
            {
                return this.descripcion;
            }
            set
            {
                this.descripcion = value;
            }
        }

        public String Abreviatura
        {
            get
            {
                return this.abreviatura;
            }
            set
            {
                this.abreviatura = value;
            }
        }

        public String Corto
        {
            get
            {
                return this.corto;
            }
            set
            {
                this.corto = value;
            }
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
