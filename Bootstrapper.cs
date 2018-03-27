using EscapeDBUsage.Views;
using System.Windows;
using Prism.Modularity;
using Autofac;
using Prism.Autofac;
using Prism.Regions;
using Prism.Mvvm;
using EscapeDBUsage.ViewModels;

namespace EscapeDBUsage
{
    class Bootstrapper : AutofacBootstrapper
    {
        protected override void ConfigureContainerBuilder(ContainerBuilder builder)
        {
            base.ConfigureContainerBuilder(builder);
            // just one instance in all application - combos etc..
            builder.RegisterType<MainViewModel>().As<MainViewModel>().SingleInstance();
        }

        protected override DependencyObject CreateShell()
        {            
            return Container.Resolve<MainWindow>();
        }

        protected override void InitializeShell()
        {
            Application.Current.MainWindow.Show();

            // View discovery
            var regionManager = Container.Resolve<IRegionManager>();
            regionManager.RegisterViewWithRegion("ContentRegion", () => Container.Resolve<MainView>());
            regionManager.RegisterViewWithRegion("Sprints", () => Container.Resolve<SprintsView>());
            regionManager.RegisterViewWithRegion("DatabaseSchemaView", () => Container.Resolve<DatabaseSchemaView>());
        }

        protected override void ConfigureModuleCatalog()
        {
            var moduleCatalog = (ModuleCatalog)ModuleCatalog;
            //moduleCatalog.AddModule(typeof(YOUR_MODULE));
        }

        protected override void ConfigureViewModelLocator()
        {
            base.ConfigureViewModelLocator();
            ViewModelLocationProvider.Register<MainWindow, MainWindowViewModel>();
        }

    }
}
