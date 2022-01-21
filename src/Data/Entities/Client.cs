namespace MinimalApis.Data.Entities;

/// <summary>
/// Client
/// </summary>
public class Client
{
    /// <summary>
    /// Gets or sets the identifier.
    /// </summary>
    /// <value>
    /// The identifier.
    /// </value>
    public int Id { get; set; } = 0;
    /// <summary>
    /// Gets or sets the name.
    /// </summary>
    /// <value>
    /// The name.
    /// </value>
    public string Name { get; set; } = "";
    /// <summary>
    /// Gets or sets the phone.
    /// </summary>
    /// <value>
    /// The phone.
    /// </value>
    public string? Phone { get; set; }
    /// <summary>
    /// Gets or sets the contact.
    /// </summary>
    /// <value>
    /// The contact.
    /// </value>
    public string? Contact { get; set; }
    /// <summary>
    /// Gets or sets the address.
    /// </summary>
    /// <value>
    /// The address.
    /// </value>
    public Address Address { get; set; } = new Address();

    /// <summary>
    /// Gets or sets the cases.
    /// </summary>
    /// <value>
    /// The cases.
    /// </value>
    public ICollection<Case>? Cases { get; set; }
}