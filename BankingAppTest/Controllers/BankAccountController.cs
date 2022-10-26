using BankingAppTest.Data;
using BankingAppTest.Models;
using Microsoft.AspNetCore.Mvc;

namespace BankingAppTest.Controllers;

public class BankAccountController : Controller
{
    private readonly ApplicationDbContext _db;

    public BankAccountController(ApplicationDbContext db)
    {
        _db = db;
    }


    public IActionResult Index()
    {
        IEnumerable<BankAccount> objList = _db.BankAccounts;
        return View(objList);
    }

    // GET-Create
    public IActionResult Create()
    {
        return View();
    }

    // POST-Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(BankAccount obj)
    {
        if (ModelState.IsValid)
        {
            _db.BankAccounts.Add(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        return View(obj);
    }


    // GET Delete
    public IActionResult Delete(int? id)
    {
        if (id == null || id == 0) return NotFound();
        var obj = _db.BankAccounts.Find(id);
        if (obj == null) return NotFound();
        return View(obj);
    }

    // POST Delete
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult DeletePost(int? id)
    {
        var obj = _db.BankAccounts.Find(id);
        if (obj == null) return NotFound();

        _db.BankAccounts.Remove(obj);
        _db.SaveChanges();
        return RedirectToAction("Index");
    }

    // GET Update
    public IActionResult Update(int? id)
    {
        if (id == null || id == 0) return NotFound();
        var obj = _db.BankAccounts.Find(id);
        if (obj == null) return NotFound();
        return View(obj);
    }

    // POST UPDATE
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Update(BankAccount obj)
    {
        if (ModelState.IsValid)
        {
            _db.BankAccounts.Update(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        return View(obj);
    }
}