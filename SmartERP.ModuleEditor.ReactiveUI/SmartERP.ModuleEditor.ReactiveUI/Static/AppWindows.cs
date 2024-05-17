using SmartERP.Development.Application.Avalonia;
using SmartERP.ModuleEditor.ReactiveUI.ViewModels;
using SmartERP.ModuleEditor.ReactiveUI.Views;

namespace SmartERP.ModuleEditor.ReactiveUI.Static
{
    public sealed class AppWindows
    {
        private static MainWindow _mainWindow;

        private static MainWindowViewModel _mainWindowViewModel;

        private static readonly object _lock = new object();

        public static MainWindow MainWindow
        {
            get
            {
                if (_mainWindow == null)
                {
                    lock (_lock)
                    {
                        CreateMainWindow();
                    }
                }
                return _mainWindow;
            }
        }

        public static MainWindowViewModel MainWindowViewModel
        {
            get
            {
                if (_mainWindowViewModel == null)
                {
                    lock (_lock)
                    {
                        CreateMainWindow();
                    }
                }
                return _mainWindowViewModel;
            }
        }

        private static void CreateMainWindow()
        {
            _mainWindowViewModel = new MainWindowViewModel();
            _mainWindow = new MainWindow
            {
                DataContext = _mainWindowViewModel
            };
        }
    }
}
