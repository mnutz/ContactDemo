namespace MNutz.ContactDemo.Ui.Data;

using System.ComponentModel;

/// <summary>
/// Represents a contact with a name, email, and phone number.
/// </summary>
public class ContactDto(long id, string name, string email, string phone) : IContact
{
    /// <inheritdoc />
    public event PropertyChangedEventHandler? PropertyChanged;

    /// <inheritdoc />
    public long Id { get; } = id;

    /// <summary>
    /// Gets or sets the name of the contact.
    /// </summary>
    public string Name { get; set; } = name;

    /// <summary>
    /// Gets or sets the email address of the contact.
    /// </summary>
    public string Email { get; set; } = email;

    /// <summary>
    /// Gets or sets the phone number of the contact.
    /// </summary>
    public string Phone { get; set; } = phone;

    /// <summary>
    /// Updates the contact.
    /// </summary>
    public void Update(string name, string email, string phone)
    {
        ArgumentNullException.ThrowIfNull(nameof(name));
        ArgumentNullException.ThrowIfNull(nameof(email));
        ArgumentNullException.ThrowIfNull(nameof(phone));
        (Name, Email, Phone) = (name, email, phone);
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null));
    }

    /// <summary>
    /// Tells the Dto to invoke the PropertyChanged event.
    /// </summary>
    public void NotifyPropertyChanged() => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null));
}
