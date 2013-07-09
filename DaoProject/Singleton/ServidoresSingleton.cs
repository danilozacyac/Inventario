using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using DaoProject.Dao;
using DaoProject.Model;

namespace DaoProject.Singleton
{
    public class ServidoresSingleton
    {
        private static List<ServidoresPublicos> servidores;

        private ServidoresSingleton()
        {
        }

        public static List<ServidoresPublicos> Servidores
        {
            get
            {
                if (servidores == null)
                    servidores = new ServidoresModel().GetUsuarios();

                return servidores;
            }
        }

        public static void AddUsuario(ServidoresPublicos servidor)
        {
            servidores.Add(servidor);
        }

        public static void AddEquiposAUsuario(int expediente, ObservableCollection<Equipos> nuevosEquipos)
        {
            ServidoresPublicos servidor = (from n in servidores
                                           where n.Expediente == expediente
                                           select n).ToList()[0];

            int index = servidores.IndexOf(servidor);

            foreach (Equipos equipo in nuevosEquipos)
                servidores[index].Equipos.Add(equipo);
        }

        public static void RemoveEquipoUsuario(int expediente, ObservableCollection<Equipos> equiposBaja)
        {
            ServidoresPublicos servidor = (from n in servidores
                                           where n.Expediente == expediente
                                           select n).ToList()[0];

            int index = servidores.IndexOf(servidor);


            foreach (Equipos mobil in equiposBaja)
            {
                int indexes = -1;

                for (int x = 0; x < servidores[index].Equipos.Count; x++)
                {
                    if (servidores[index].Equipos[x].ScEquipo == mobil.ScEquipo && servidores[index].Equipos[x].IdTipo == mobil.IdTipo)
                    {
                        indexes = x;
                    }
                }

                if(indexes != -1)
                servidores[index].Equipos.RemoveAt(indexes);

            }
        }

        public static void AddMobiliarioUsuario(int expediente, Mobiliario nuevoMobiliario)
        {
            ServidoresPublicos servidor = (from n in servidores
                                           where n.Expediente == expediente
                                           select n).ToList()[0];

            int index = servidores.IndexOf(servidor);

            servidores[index].Mobiliario.Add(nuevoMobiliario);
        }
    }
}