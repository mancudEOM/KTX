using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using KTX.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace KTX.Controllers;

public class AccountController : Controller
{
    
    private readonly KtxDbContext db;
    public AccountController( KtxDbContext context)
    {
        
        db = context;
    }
    //public IActionResult Index()
    //{

    //    return View();
    //}
    public IActionResult Index4()
    {
        var listofData = db.Users.ToList();
        return View(listofData);
    }
    public async Task<IActionResult> Index1(string searchString)
    {
        var listofData = from m in db.Users
                         select m;

        if (!String.IsNullOrEmpty(searchString))
        {
            listofData = listofData.Where(s => s.IdentityId!.Contains(searchString));
        }

        return View(await listofData.ToListAsync());
    }

    public IActionResult Index(string userEmail, string searchString)
    {
        var EmailLst = new List<string>();

        var EmailQry = from d in db.Users
                       orderby d.Email
                       select d.Email;

        EmailLst.AddRange(EmailQry.Distinct());
        ViewBag.userEmail = new SelectList(EmailLst);

        var data = from m in db.Users
                   select m;

        if (!String.IsNullOrEmpty(searchString))
        {
            data = data.Where(s => s.IdentityId!.Contains(searchString));
        }

        if (!string.IsNullOrEmpty(userEmail))
        {
            data = data.Where(x => x.Email == userEmail);
        }

        return View(data);
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
    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create(User model)
    {
        db.Users.Add(model);
        db.SaveChanges();
        ViewBag.Message = "Data Insert Successfully";
        //return View();
        return RedirectToAction("index");
    }
    [HttpGet]
    public IActionResult Edit(int id)
    {
        var data = db.Users.Where(x => x.Id == id).FirstOrDefault();
        return View(data);
    }
    [HttpPost]
    public IActionResult Edit(User Model)
    {
        var data = db.Users.Where(x => x.Id == Model.Id).FirstOrDefault();
        if (data != null)
        {
            data.IdentityId = Model.IdentityId;
            data.BirthDay = Model.BirthDay;
            data.Fullname = Model.Fullname;
            data.Address = Model.Address;
            data.ImageUrl = Model.ImageUrl;
            data.Gender = Model.Gender;
            data.TelephoneNumber = Model.TelephoneNumber;
            data.Email = Model.Email;
            db.SaveChanges();
        }

        return RedirectToAction("index");
    }
    public IActionResult Detail(int id)
    {
        var data = db.Users.Where(x => x.Id == id).Include(x => x.RelativeUsers).FirstOrDefault();
        return View(data);
    }
    public IActionResult DetailRent(int id)
    {
        var data = db.HistoryRents.Where(x => x.UserId == id).Include(y => y.Rents).FirstOrDefault();
        return View(data);
    }


    public IActionResult Delete(int id)
    {
        var data = db.Users.Where(x => x.Id == id).FirstOrDefault();
        db.Users.Remove(data);
        db.SaveChanges();
        ViewBag.Messsage = "Record Delete Successfully";
        return RedirectToAction("index");
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
