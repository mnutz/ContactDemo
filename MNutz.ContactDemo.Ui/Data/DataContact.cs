namespace MNutz.ContactDemo.Ui.Data;

using System.ComponentModel;
using System.Runtime.CompilerServices;

/// <summary>
/// Data model for a contact
/// </summary>
public class DataContact(long id, string name, string email, string phone)
    : IContact
{
    /// <inheritdoc />
    public long Id { get; } = id;

    /// <summary>
    /// Gets or sets the name of the contact.
    /// </summary>
    public string Name
    {
        get => name;
        set => SetField(ref name, value);
    }

    /// <summary>
    /// Gets or sets the email address of the contact.
    /// </summary>
    public string Email
    {
        get => email;
        set => SetField(ref email, value);
    }

    /// <summary>
    /// Gets or sets the phone number of the contact.
    /// </summary>
    public string Phone
    {
        get => phone;
        set => SetField(ref phone, value);
    }

    /// <summary>
    /// Lazy initialization of the view model.
    /// </summary>
    public event PropertyChangedEventHandler? PropertyChanged;

    /// <summary>
    /// Updates the contact.
    /// </summary>
    public void Update(string name, string email, string phone)
    {
        ArgumentNullException.ThrowIfNull(nameof(name));
        ArgumentNullException.ThrowIfNull(nameof(email));
        ArgumentNullException.ThrowIfNull(nameof(phone));
        (Name, Email, Phone) = (name, email, phone);
    }

    /// <summary>
    /// Notifies property changed event.
    /// </summary>
    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    /// <summary>
    /// Sets a field and notifies property changed event.
    /// </summary>
    protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value))
        {
            return false;
        }

        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }
}
