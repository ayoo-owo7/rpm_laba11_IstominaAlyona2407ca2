using Microsoft.Extensions.DependencyInjection;
using Rpm_Lab11.Services;
using Rpm_Lab11.ViewModels;
using System.Configuration;
using System.Data;
using System.Windows;

namespace Rpm_Lab11
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            var services = new ServiceCollection();

            // 🔹 SINGLETON: Один экземпляр на всё время работы приложения.
            // Сервисы не хранят состояние UI, поэтому Singleton оптимален.
            services.AddSingleton<INavigationService, NavigationService>();
            services.AddSingleton<IDialogService, DialogService>();
            services.AddSingleton<IContactsStore, ContactsStore>();

            // ViewModel оболочки (Shell) тоже Singleton, так как управляет всей навигацией.
            services.AddSingleton<MainWindowViewModel>();

            // 🔹 TRANSIENT: Новый экземпляр при каждом вызове GetRequiredService<T>().
            // Экраны создаются заново при переходе. Данные вынесены в Singleton IContactsStore,
            // поэтому Transient для VM безопасен и соответствует паттерну ViewModel-First.
            services.AddTransient<ContactsListViewModel>();
            services.AddTransient<ContactEditViewModel>();
            services.AddTransient<AboutViewModel>();

            // Главное окно создаётся один раз, DataContext привязывается к Shell VM.
            services.AddSingleton<MainWindow>(sp =>
            {
                var window = new MainWindow();
                window.DataContext = sp.GetRequiredService<MainWindowViewModel>();
                return window;
            });

            var serviceProvider = services.BuildServiceProvider();
            serviceProvider.GetRequiredService<MainWindow>().Show();
        }
    }

}
