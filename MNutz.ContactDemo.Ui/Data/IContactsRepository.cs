namespace MNutz.ContactDemo.Ui.Data;

using System.Collections.Concurrent;
using System.Collections.ObjectModel;

/// <summary>
/// The repository for managing contacts.
/// Provides methods to retrieve, insert, update, and delete contacts, as well as save changes to the data store.
/// </summary>
public class ContactsRepository : IContactsRepository
{
    private readonly ICollection<DataContact> contacts;

    private readonly ConcurrentDictionary<DataContact, ContactDto> dtoMap = new();

    /// <summary>
    /// Initializes the <see cref="ContactsRepository" />
    /// </summary>
    public ContactsRepository() =>
        contacts =
        [
            new DataContact(0, "Sophie Müller", "sophie.mueller@example.com", "+49-30-123456"),
            new DataContact(1, "Hans Schmidt", "hans.schmidt@example.com", "+49-30-654321"),
            new DataContact(2, "Laura Schneider", "laura.schneider@example.com", "+49-30-789012"),
            new DataContact(3, "Peter Fischer", "peter.fischer@example.com", "+49-30-345678"),
            new DataContact(4, "Anna Weber", "anna.weber@example.com", "+49-30-901234"),
            new DataContact(5, "Klaus Meyer", "klaus.meyer@example.com", "+49-30-567890"),
            new DataContact(6, "Julia Wagner", "julia.wagner@example.com", "+49-30-234567"),
            new DataContact(7, "Michael Becker", "michael.becker@example.com", "+49-30-678901"),
            new DataContact(8, "Monika Hoffmann", "monika.hoffmann@example.com", "+49-30-345678"),
            new DataContact(9, "Stefan Schäfer", "stefan.schaefer@example.com", "+49-30-789012")
        ];

    /// <inheritdoc />
    public event EventHandler? CollectionChanged;

    /// <inheritdoc />
    public IReadOnlyCollection<IContact> GetContacts() =>
        new ReadOnlyCollection<IContact>(contacts.Select(GetContactDto).ToArray());

    /// <inheritdoc />
    public IContact? GetContactById(long id)
    {
        var contact = GetDataContactById(id);
        return contact is null ? null : GetContactDto(contact);
    }

    /// <inheritdoc />
    public void InsertContact(IContact contact)
    {
        ArgumentNullException.ThrowIfNull(contact, nameof(contact));
        var newId = contacts.Max(c => c.Id) + 1;
        var newContact = new DataContact(newId, contact.Name, contact.Email, contact.Phone);
        contacts.Add(newContact);
        CollectionChanged?.Invoke(this, EventArgs.Empty);
    }

    /// <inheritdoc />
    public void UpdateContact(IContact contact)
    {
        ArgumentNullException.ThrowIfNull(contact, nameof(contact));
        var original = GetDataContactById(contact.Id);
        original!.Update(contact.Name, contact.Email, contact.Phone);
        GetContactDto(original).NotifyPropertyChanged();
    }

    /// <inheritdoc />
    public void DeleteContact(IContact contact)
    {
        ArgumentNullException.ThrowIfNull(contact, nameof(contact));
        var toRemove = GetDataContactById(contact.Id);
        if (toRemove is null)
        {
            return;
        }

        _ = contacts.Remove(toRemove);
        CollectionChanged?.Invoke(this, EventArgs.Empty);
    }

    /// <inheritdoc />
    /// <remarks>
    /// For this demo, I will not persist any data.
    /// </remarks>
    public void SaveChanges() => throw new NotImplementedException();

    /// <inheritdoc />
    public IContact CreateContact() => new ContactDto(-1, string.Empty, string.Empty, string.Empty);

    private DataContact? GetDataContactById(long id) => contacts.SingleOrDefault(c => c.Id == id);

    private ContactDto GetContactDto(DataContact contact) => dtoMap.GetOrAdd(contact, CreateContactDto);

    private ContactDto CreateContactDto(DataContact contact) =>
        new(contact.Id, contact.Name, contact.Email, contact.Phone);
}

/// <summary>
/// Defines a repository interface for managing contacts.
/// Provides methods to retrieve, insert, update, and delete contacts, as well as save changes to the data store.
/// </summary>
public interface IContactsRepository
{
    /// <summary>
    /// An event which indicates that the collection of users has been changed.
    /// </summary>
    public event EventHandler CollectionChanged;

    /// <summary>
    /// Retrieves all contacts from the repository.
    /// </summary>
    IReadOnlyCollection<IContact> GetContacts();

    /// <summary>
    /// Gets a contact by its Id.
    /// Returns null, if the user is not found.
    /// </summary>
    IContact? GetContactById(long id);

    /// <summary>
    /// Inserts a new contact into the repository.
    /// </summary>
    void InsertContact(IContact contact);

    /// <summary>
    /// Updates an existing contact in the repository.
    /// </summary>
    void UpdateContact(IContact contact);

    /// <summary>
    /// Deletes a contact from the repository.
    /// </summary>
    void DeleteContact(IContact contact);

    /// <summary>
    /// Saves all changes made to the repository.
    /// </summary>
    void SaveChanges();

    /// <summary>
    /// Creates a new, empty object of type <see cref="IContact" />
    /// </summary>
    /// <returns></returns>
    IContact CreateContact();
}
