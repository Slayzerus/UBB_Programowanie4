using SmartERP.Development.Application.Avalonia;
using System;

namespace SmartERP.ModuleEditor.ReactiveUI.Static
{
    public sealed class DependencyResolver
    {
        private DependencyResolver(IServiceProvider services) 
        { 
            _services = services;
        }

        private static DependencyResolver _instance;

        private static IServiceProvider _services;

        private static readonly object _lock = new object();

        public static DependencyResolver Instance 
        { 
            get 
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                        {
                            IServiceProvider serviceProvider = DependencyInjection
                                //.BuildServiceProvider("Data Source=AERO16;Initial Catalog=SmartERP;Integrated Security=True;Trust Server Certificate=True");
                                //.BuildServiceProvider("Server=localhost\\SQLEXPRESS;Database=SmartERP;Integrated Security=True;Trusted_Connection=True;");
                                .BuildServiceProvider("Data Source=AERO16; Database=SmartERP; Integrated Security=true;TrustServerCertificate=True");
                            _instance = new DependencyResolver(serviceProvider);
                        }
                    }
                }
                return _instance;
            } 
        }

        public T Get<T>() => (T)_services.GetService(typeof(T));
    }
}
