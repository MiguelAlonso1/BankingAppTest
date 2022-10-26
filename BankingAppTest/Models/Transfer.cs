using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankingAppTest.Models;

public class Transfer
{
    [Key] public int Id { get; set; }

    [DisplayName("Expense")] [Required] public string ExpenseName { get; set; }

    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "Amount must be greater than 0!")]
    public double Amount { get; set; }

   // [DisplayName("Expense Type")] public int ExpenseTypeId { get; set; }

    //[ForeignKey("ExpenseTypeId")]
    //public int ExpenseTypeId2 { get; set; }
    //[ForeignKey("ExpenseTypeId2")]
    public virtual BankAccount ExpenseType { get; set; }
}