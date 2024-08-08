namespace MNutz.ContactDemo.Ui;

using Autofac;

using Data;

using ViewModels;
using ViewModels.Factories;

/// <summary>
/// Defines a module that registers types with the Autofac container.
/// </summary>
public class AutofacModule : Module
{
    /// <summary>
    /// Adds registrations to the container.
    /// </summary>
    /// <param name="builder">The builder through which components can be registered.</param>
    protected override void Load(ContainerBuilder builder)
    {
        _ = builder.RegisterType<ViewModelFactory>().AsImplementedInterfaces().SingleInstance();

        _ = builder.RegisterType<ContactsRepository>().AsImplementedInterfaces().SingleInstance();

        _ = builder.RegisterType<MainWindowModel>().SingleInstance();
        _ = builder.RegisterType<MainViewModel>().OnActivating(args => args.Instance.Initialize());
        _ = builder.RegisterType<ContactCardViewModel>().OnActivating(args => args.Instance.Initialize());
        _ = builder.RegisterType<ContactCardEditViewModel>().OnActivating(args => args.Instance.Initialize());
        _ = builder.RegisterType<ContactCardDisplayViewModel>()
            .OnActivating(args => args.Instance.Initialize());
        _ = builder.RegisterType<ContactViewModel>().OnActivating(args => args.Instance.Initialize());
        _ = builder.RegisterType<MenuViewModel>();
        _ = builder.RegisterType<StatusViewModel>().OnActivating(args => args.Instance.Initialize());
    }
}
