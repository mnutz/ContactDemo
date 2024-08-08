namespace MNutz.ContactDemo.Ui;

using System.Windows;

using ViewModels;

using Views;

/// <summary>
/// Interaction logic for App.xaml.
/// </summary>
public partial class App
{
    private void OnStartup(object sender, StartupEventArgs args)
    {
        Bootstrapper bootstrapper = new();
        bootstrapper.Initialize();

        var mainVm = bootstrapper.Resolve<MainWindowModel>();
        MainWindow mainView = new() { DataContext = mainVm };
        mainView.Show();
    }
}
