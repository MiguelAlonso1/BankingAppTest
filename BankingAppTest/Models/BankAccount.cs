using System.ComponentModel.DataAnnotations;

namespace BankingAppTest.Models;

public class BankAccount
{
    [Key] public int Id { get; set; }

    [Required] public string Name { get; set; }

    public double Balance { get; set; }
}