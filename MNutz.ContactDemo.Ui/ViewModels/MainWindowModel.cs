namespace MNutz.ContactDemo.Ui.ViewModels;

/// <summary>
/// Represents the view model for the main window.
/// </summary>
public class MainWindowModel
{
    /// <summary>
    /// Gets or sets the <see cref="MenuViewModel" />
    /// This property is intended to be set by the dependency injection container.
    /// </summary>
    public required MenuViewModel MenuViewModel { get; init; }

    /// <summary>
    /// Gets or sets the <see cref="MainViewModel" />
    /// This property is intended to be set by the dependency injection container.
    /// </summary>
    public required MainViewModel MainViewModel { get; init; }

    /// <summary>
    /// Gets or sets the <see cref="StatusViewModel" />
    /// This property is intended to be set by the dependency injection container.
    /// </summary>
    public required StatusViewModel StatusViewModel { get; init; }
}
