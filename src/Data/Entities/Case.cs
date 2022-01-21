namespace MinimalApis.Data.Entities;

/// <summary>
/// Case Status
/// </summary>
public enum CaseStatus
{
    /// <summary>
    /// The invalid
    /// </summary>
    Invalid = 0,
    /// <summary>
    /// The open
    /// </summary>
    Open,
    /// <summary>
    /// The closed
    /// </summary>
    Closed,
    /// <summary>
    /// The rejected
    /// </summary>
    Rejected,
    /// <summary>
    /// The referred
    /// </summary>
    Referred,
    /// <summary>
    /// The scheduled
    /// </summary>
    Scheduled,
    /// <summary>
    /// The settled
    /// </summary>
    Settled
}

/// <summary>
/// Case
/// </summary>
public class Case
{
    /// <summary>
    /// Gets or sets the identifier.
    /// </summary>
    /// <value>
    /// The identifier.
    /// </value>
    public int Id { get; set; }
    /// <summary>
    /// Gets or sets the file number.
    /// </summary>
    /// <value>
    /// The file number.
    /// </value>
    public string FileNumber { get; set; } = "";
    /// <summary>
    /// Gets or sets the status.
    /// </summary>
    /// <value>
    /// The status.
    /// </value>
    public CaseStatus Status { get; set; }

    /// <summary>
    /// Gets or sets the client.
    /// </summary>
    /// <value>
    /// The client.
    /// </value>
    public Client? Client { get; set; }
}