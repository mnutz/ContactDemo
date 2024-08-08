namespace MNutz.ContactDemo.Ui.ViewModels;

using CommunityToolkit.Mvvm.ComponentModel;

using Data;

using Factories;

/// <summary>
/// Represents a view model for a contact.
/// </summary>
public partial class ContactCardViewModel(IContact contact) : ObservableObject
{
    [ObservableProperty] private ObservableObject? currentStateViewModel;

    [ObservableProperty] private bool isEditing;

    /// <summary>
    /// Gets or sets the factory for creating view models.
    /// This property is intended to be set by the dependency injection container.
    /// </summary>
    public required IViewModelFactory ViewModelFactory { get; init; }

    /// <summary>
    /// Lazy initialization of the view model.
    /// </summary>
    public void Initialize() => SetStateDisplay();

    private void SetStateDisplay()
    {
        var viewModel = ViewModelFactory.GetContactCardDisplayViewModel(contact);
        viewModel.Editing -= HandleEditing;
        viewModel.Editing += HandleEditing;

        CurrentStateViewModel = viewModel;
    }

    private void HandleEditing(object? sender, EventArgs args) => SetStateEdit();

    private void SetStateEdit()
    {
        var viewModel = ViewModelFactory.CreateContactCardEditViewModel(contact);
        viewModel.Displaying -= HandleDisplaying;
        viewModel.Displaying += HandleDisplaying;

        CurrentStateViewModel = viewModel;
    }

    private void HandleDisplaying(object? sender, EventArgs e) => SetStateDisplay();
}
