using System;
using System.Linq;
using System.Windows.Data;
using DaoProject.Singleton;

namespace Inventario.Converters
{
    class ExpedienteConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value != null)
            {
                int number = 0;
                int.TryParse(value.ToString(), out number);

                if (number > 0)
                    return (from n in ServidoresSingleton.Servidores
                            where n.Expediente == number
                            select n.Nombre).ToList()[0];
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