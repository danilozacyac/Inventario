using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Inventario.Dao;
using Inventario.Model;

namespace Inventario.Singleton
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

        public static void AddMobiliarioUsuario(int expediente, ObservableCollection<Mobiliario> nuevoMobiliario)
        {
            ServidoresPublicos servidor = (from n in servidores
                                           where n.Expediente == expediente
                                           select n).ToList()[0];

            int index = servidores.IndexOf(servidor);

            foreach (Mobiliario mobil in nuevoMobiliario)
                servidores[index].Mobiliario.Add(mobil);
        }

        public static void RemoveEquipoUsuario(int expediente, ObservableCollection<Equipos> equiposBaja)
        {
            ServidoresPublicos servidor = (from n in servidores
                                           where n.Expediente == expediente
                                           select n).ToList()[0];

            int index = servidores.IndexOf(servidor);

            foreach (Equipos mobil in equiposBaja)
                servidores[index].Equipos.Remove(mobil);
        }
    }
}