using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using NewlineRemover.Views;
using NewlineRemover.ViewModels;

namespace NewlineRemover
{
    /// <summary>
    /// App.xaml の相互作用ロジック
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            MainWindow window = new MainWindow();
            window.DataContext = new MainWindowViewModel();
            /*
             * ViewとViewModelの連結は外側で。
             * View-ViewModelが多対多対応しうるため。
             * Viewはあくまで穴ぼこが開いた枠 
             */
            window.Show();
            Console.WriteLine("show main window.");
        }
    }
}
