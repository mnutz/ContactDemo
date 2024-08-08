namespace MNutz.ContactDemo.Ui.Data;

using System.ComponentModel;

/// <summary>
/// Represents a contact.
/// </summary>
public interface IContact : INotifyPropertyChanged
{
    /// <summary>
    /// Gets the id of the contact.
    /// </summary>
    long Id { get; }

    /// <summary>
    /// Gets or sets the name of the contact.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the email address of the contact.
    /// </summary>
    public string Email { get; set; }

    /// <summary>
    /// Gets or sets the phone number of the contact.
    /// </summary>
    public string Phone { get; set; }

    /// <summary>
    /// Updates the contact.
    /// </summary>
    void Update(string name, string email, string phone);
}
