using SmartERP.Development.Application.Avalonia;
using SmartERP.ModuleEditor.ReactiveUI.ViewModels;
using SmartERP.ModuleEditor.ReactiveUI.Views;

namespace SmartERP.ModuleEditor.ReactiveUI.Static
{
    public sealed class AppWindows
    {
        private static MainWindow _mainWindow;

        private static readonly object _lock = new object();

        public static MainWindow MainWindow
        {
            get
            {
                if (_mainWindow == null)
                {
                    lock (_lock)
                    {
                        if (_mainWindow == null)
                        {
                            _mainWindow = new MainWindow
                            {
                                DataContext = new MainWindowViewModel()
                            };
                        }
                    }
                }
                return _mainWindow;
            }
        }
    }
}
