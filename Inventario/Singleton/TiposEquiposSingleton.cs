using System;
using System.Collections.Generic;
using System.Linq;
using Inventario.Dao;
using Inventario.Model;

namespace Inventario.Singleton
{
    public class TiposEquiposSingleton
    {
        private static List<CommonProperties> tipos;

        private TiposEquiposSingleton()
        {
        }

        public static List<CommonProperties> Tipos
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

        public static void AddTipos(CommonProperties properties)
        {
            TiposEquiposSingleton.tipos.Add(properties);
        }

        public static void RemoveTipos(CommonProperties properties)
        {
            TiposEquiposSingleton.tipos.Remove(properties);
        }

        public static void UpdateTipos(CommonProperties currentProperty, CommonProperties updatedProperty)
        {
            int index = TiposEquiposSingleton.Tipos.FindLastIndex(s => s.IdElemento == currentProperty.IdElemento);

            TiposEquiposSingleton.tipos[index] = updatedProperty;
        }
    }
}
