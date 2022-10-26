using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BankingAppTest.Models;

public class Transaction
{
    [Key] public int Id { get; set; }

    [DisplayName("From Account")] public string FromAccount { get; set; }

    [DisplayName("To Account")] public string ToAccount { get; set; }

    [DisplayName("Transaction Time")] public DateTime TransactionTime { get; set; }

    [DisplayName("Amount Debited")] public double AmountDebited { get; set; }

    [DisplayName("From Account Balance")] public double FromAccountBalance { get; set; }

    [DisplayName("To Account Balance")] public double ToAccountBalance { get; set; }
}