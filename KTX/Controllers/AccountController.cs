using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using KTX.Models;

namespace KTX.Controllers;

public class AccountController : Controller
{
    
    private readonly KtxDbContext db;
    public AccountController( KtxDbContext context)
    {
        
        db = context;
    }
    public IActionResult Index()
    {

        return View();
    }
    public IActionResult Login()
    {
        return View();
    }
    [HttpPost]
    public IActionResult Login(Admin obj)
    {
        try
        {
            if (db.Admins.Any(a => a.Email == obj.Email && a.Password == obj.Password))
            {
                TempData["user"] = obj.Email;
                return RedirectToAction("Index");
            }

            else
            {
                ViewBag.error = "!!Tên đăng nhập hoặc mật khẩu không chính xác";
            }
            db.SaveChanges();
        }
        catch (Exception ex)
        {
            ViewBag.error = "!!There is some error.";
        }
        return View();
    }

    public IActionResult Registration()
    {
        return View();
    }
    [HttpPost]
    public IActionResult Registration(Admin obj)
    {
        try
        {
            if (ModelState.IsValid)
            {
                db.Admins.Add(obj);
                db.SaveChanges();
                ViewBag.success = "Sign Up successfully.";
                return View();
            }
            else
            {
                ViewBag.error = "!!There is some error.";
            }
            db.SaveChanges();
        }
        catch (Exception ex)
        {
            ViewBag.error = "!!There is some error.";
        }
        return View();
    }
    //Logout
    public ActionResult Logout()
    {       
        return RedirectToAction("Login");
    }
}
