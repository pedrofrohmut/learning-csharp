using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExpenseTracker.Web.Models;

public class Transaction
{
    [Key]
    public int TransactionId { get; set; }

    public int Amount { get; set; }

    [Column(TypeName = "varchar(75)")]
    public string? Note { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public int CategoryId { get; set; }
    public Category? Category { get; set; }

}
