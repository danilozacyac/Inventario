using System;
using System.Linq;
using System.Threading;
using System.Windows;
using DaoProject.Singleton;

namespace Inventario.Splash
{
    public class LaunchSplash
    {
        public LaunchSplash()
        {
            Splashes splasher = new Splashes();
            splasher.Show();

            object obj = AreasSingleton.Areas;
            obj = UbicacionesSingleton.Ubicaciones;
            obj = TiposEquiposSingleton.Tipos;

            for (int i = 0; i < 5000; i++)
            {
                MessageListener.Instance.ReceiveMessage(string.Format("Load module {0}", i));
                Thread.Sleep(1);
            }

            obj = ServidoresSingleton.Servidores;
           

            splasher.Close();
        }
    }
}
