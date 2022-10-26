using BankingAppTest.Data;
using BankingAppTest.Models;
using BankingAppTest.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BankingAppTest.Controllers;

public class TransferController : Controller
{
    private readonly ApplicationDbContext _db;

    public TransferController(ApplicationDbContext db)
    {
        _db = db;
    }
    public IActionResult Index()
    {
        IEnumerable<Transaction> objList = _db.Transactions;
        return View(objList);
    }

    // GET-Create
    public IActionResult Create()
    {
        var expenseVM = new ExpenseVM
        {
            Transfer = new Transfer(),
            TypeDropDown = _db.BankAccounts.Select(i => new SelectListItem
            {
                Text = i.Name,
                Value = i.Id.ToString()
            })
        };

        return View(expenseVM);
    }

    // POST-Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(ExpenseVM obj)
    {
        
        if ( obj.FromAccount.Equals(0) || obj.ToAccount.Equals(0))
        {
            return View(obj);
        }
        if (!ModelState.IsValid)
        {
            var fromAccount = _db.BankAccounts.FirstOrDefault(u => u.Id == obj.FromAccount);
            var toAccount = _db.BankAccounts.FirstOrDefault(u => u.Id == obj.ToAccount);
            
            //Create new fund transaction
            var newTransaction = new Transaction
            {
                FromAccount = fromAccount.Name,
                ToAccount = toAccount.Name,
                TransactionTime = DateTime.Now,
                AmountDebited = obj.Transfer.Amount,
                FromAccountBalance = fromAccount.Balance,
                ToAccountBalance = toAccount.Balance
            };
            //if not enough balance on from account, or trying to transfer to oneself
            if (fromAccount.Balance < obj.Transfer.Amount || obj.FromAccount.Equals(obj.ToAccount))
            {
                return View(obj);
            }
            //update balances
            fromAccount.Balance = fromAccount.Balance - obj.Transfer.Amount;
            toAccount.Balance = toAccount.Balance + obj.Transfer.Amount;

            //write new balances to database
            _db.BankAccounts.Update(fromAccount);
            _db.BankAccounts.Update(toAccount);

            //add new transaction
            _db.Transactions.Add(newTransaction);
            
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        return View(obj);
    }

    // GET Update
    public IActionResult Update(int? id)
    {
        var expenseVM = new ExpenseVM
        {
            Transfer = new Transfer(),
            TypeDropDown = _db.BankAccounts.Select(i => new SelectListItem
            {
                Text = i.Name,
                Value = i.Id.ToString()
            })
        };
        var viewTransaction = new Transaction();
        if (id == null || id == 0) return NotFound();
        viewTransaction = _db.Transactions.Find(id);
      
        return View(viewTransaction);
    }
}