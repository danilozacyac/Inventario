using System;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using DaoProject.Singleton;

namespace Inventario.Splash
{
    public class LaunchSplash
    {
        public LaunchSplash()
        {
            Splashes splasher = new Splashes();
            splasher.Show();

            DoBackgroundWork();

            for (int i = 0; i < 1000; i++)
            {
                MessageListener.Instance.ReceiveMessage(string.Format("Cargando módulos {0}", i));
                Thread.Sleep(1);
            }

            splasher.Close();
        }

        /// <summary>
        /// Creates a BackgroundWorker class to do work
        /// on a background thread.
        /// </summary>
        private void DoBackgroundWork()
        {
            BackgroundWorker worker = new BackgroundWorker();

            // Tell the worker to report progress.
            worker.WorkerReportsProgress = true;

            worker.ProgressChanged += ProgressChanged;
            worker.DoWork += DoWork;
            worker.RunWorkerCompleted += WorkerCompleted;
            worker.RunWorkerAsync();
        }


        /// <summary>
        /// The work for the BackgroundWorker to perform.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 
        void DoWork(object sender, DoWorkEventArgs e)
        {
            object obj = AreasSingleton.Areas;
            obj = UbicacionesSingleton.Ubicaciones;
            obj = ServidoresSingleton.Servidores;
            obj = null;
        }

        /// <summary>
        /// Occurs when the BackgroundWorker reports a progress.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            //pbLoad.Value = e.ProgressPercentage;
        }

        /// <summary>
        /// Occurs when the BackgroundWorker has completed its work.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void WorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //_backgroundButton.IsEnabled = true;
            //pbLoad.Visibility = Visibility.Collapsed;
        }
    }
}
