using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BankingAppTest.Models.ViewModels;

public class ExpenseVM
{
    public Transfer Transfer { get; set; }
    //public BankAccount LocalExpenseType { get; set; }

    [Required] public int FromAccount { get; set; }

    [Required] public int ToAccount { get; set; }

    public IEnumerable<SelectListItem> TypeDropDown { get; set; }
}