using Microsoft.Extensions.DependencyInjection;
using Rpm_Lab11.Basic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rpm_Lab11.Services
{
    public interface INavigationService
    {
        object? CurrentViewModel { get; }
        void NavigateTo<TViewModel>(object? parameter = null) where TViewModel : class;
    }

    public interface INavigationAware
    {
        void OnNavigatedTo(object? parameter);
    }

    public class NavigationService : ObservableObject, INavigationService
    {
        private readonly IServiceProvider _serviceProvider;
        private object? _currentViewModel;

        public NavigationService(IServiceProvider serviceProvider) => _serviceProvider = serviceProvider;

        public object? CurrentViewModel
        {
            get => _currentViewModel;
            private set { _currentViewModel = value; OnPropertyChanged(); }
        }

        public void NavigateTo<TViewModel>(object? parameter = null) where TViewModel : class
        {
            var vm = _serviceProvider.GetRequiredService<TViewModel>();
            if (vm is INavigationAware aware) aware.OnNavigatedTo(parameter);
            CurrentViewModel = vm;
        }
    }
}
