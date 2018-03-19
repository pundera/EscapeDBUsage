using EscapeDBUsage.Views;
using System.Windows;
using Prism.Modularity;
using Autofac;
using Prism.Autofac;
using Prism.Regions;

namespace EscapeDBUsage
{
    class Bootstrapper : AutofacBootstrapper
    {
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
    }
}
