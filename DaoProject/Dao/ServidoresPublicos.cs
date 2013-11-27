using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace DaoProject.Dao
{
    public class ServidoresPublicos : INotifyPropertyChanged
    {
        private int expediente;
        private int idTitulo;
        private String tituloStr;
        private String nombre;
        private int idUbicacion;
        private String ubicacionStr;
        private String puerta;
        private int? extension;
        private int idArea;
        private String areaStr;
        private int idAdscripcion;
        private String adscripcionStr;
        private bool isActivated;
        private ObservableCollection<Equipos> equipos;
        private ObservableCollection<Mobiliario> mobiliario;
        private String image = "/Inventario;component/Resources/noPic.png";

        public bool IsActivated
        {
            get
            {
                return this.isActivated;
            }
            set
            {
                this.isActivated = value;
            }
        }

        public String AdscripcionStr
        {
            get
            {
                return this.adscripcionStr;
            }
            set
            {
                this.adscripcionStr = value;
            }
        }

        public String AreaStr
        {
            get
            {
                return this.areaStr;
            }
            set
            {
                this.areaStr = value;
            }
        }

        public int Expediente
        {
            get
            {
                return this.expediente;
            }
            set
            {
                this.expediente = value;
            }
        }

        public int IdTitulo
        {
            get
            {
                return this.idTitulo;
            }
            set
            {
                this.idTitulo = value;
            }
        }

        public String TituloStr
        {
            get
            {
                return this.tituloStr;
            }
            set
            {
                this.tituloStr = value;
            }
        }

        public String Nombre
        {
            get
            {
                return this.nombre;
            }
            set
            {
                this.nombre = value;
                this.OnPropertyChanged("Nombre");
            }
        }

        public int IdUbicacion
        {
            get
            {
                return this.idUbicacion;
            }
            set
            {
                this.idUbicacion = value;
            }
        }

        public String UbicacionStr
        {
            get
            {
                return this.ubicacionStr;
            }
            set
            {
                this.ubicacionStr = value;
            }
        }

        public String Puerta
        {
            get
            {
                return this.puerta;
            }
            set
            {
                this.puerta = value;
            }
        }

        public int? Extension
        {
            get
            {
                return this.extension;
            }
            set
            {
                this.extension = value;
            }
        }

        public int IdArea
        {
            get
            {
                return this.idArea;
            }
            set
            {
                this.idArea = value;
            }
        }

        public int IdAdscripcion
        {
            get
            {
                return this.idAdscripcion;
            }
            set
            {
                this.idAdscripcion = value;
            }
        }

        public ObservableCollection<Equipos> Equipos
        {
            get
            {
                return this.equipos;
            }
            set
            {
                this.equipos = value;
                this.OnPropertyChanged("Equipos");
            }
        }

        public ObservableCollection<Mobiliario> Mobiliario
        {
            get
            {
                return this.mobiliario;
            }
            set
            {
                this.mobiliario = value;
                this.OnPropertyChanged("Mobiliario");
            }
        }

        public String Image
        {
            get
            {
                return this.image;
            }
            set
            {
                this.image = value;
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