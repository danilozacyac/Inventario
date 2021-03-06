﻿using System;
using System.Linq;
using System.Windows.Data;
using DaoProject.Singleton;

namespace Inventario.Converters
{
    class UbicacionToStringConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value != null)
            {
                int number = 0;
                int.TryParse(value.ToString(), out number);

                if (number > 0)
                    return (from n in UbicacionesSingleton.Ubicaciones
                            where n.IdElemento == number
                            select n.Descripcion).ToList()[0];
                else
                    return "";
            }

            return "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}