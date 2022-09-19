using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StronglyTypedIdFastEndpoint;

public sealed record Book
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public BookId Id { get; private set; }
    public string Title { get; set; }
} 
