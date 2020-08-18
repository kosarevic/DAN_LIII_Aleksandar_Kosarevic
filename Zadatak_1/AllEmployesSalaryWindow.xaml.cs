using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Zadatak_1.ViewModel;

namespace Zadatak_1
{
    /// <summary>
    /// Interaction logic for AllEmployesSalaryWindow.xaml
    /// </summary>
    public partial class AllEmployesSalaryWindow : Window
    {
        ManagerViewModel mvm = new ManagerViewModel();

        public AllEmployesSalaryWindow()
        {
            InitializeComponent();
            DataContext = mvm;
        }

        private void Confirm(object sender, RoutedEventArgs e)
        {

            BackgroundWorker worker = new BackgroundWorker();
            worker.WorkerReportsProgress = true;
            worker.DoWork += worker_DoWork;
            worker.ProgressChanged += worker_ProgressChanged;

            worker.RunWorkerAsync();
        }

        void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            for (int i = 0; i < mvm.Employes.Count(); i++)
            {
                mvm.GeneralSalary(sender, e, mvm.Employes[i].Id);
                (sender as BackgroundWorker).ReportProgress((i + 1) * (100 / mvm.Employes.Count()));
                Thread.Sleep(250);
            }
            MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("All salaries successfully updated.", "Notification");
            App.Current.Dispatcher.Invoke((Action)delegate
            {
                ManagerWindow window = new ManagerWindow();
                window.Show();
                Close();
            });
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            ManagerWindow window = new ManagerWindow();
            window.Show();
            Close();
        }

        void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            pbStatus.Value = e.ProgressPercentage;
        }
    }
}
