namespace MinimalApis.Models;

/// <summary>
/// Case Model
/// </summary>
public class CaseModel
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
    public string Status { get; set; } = "";
}