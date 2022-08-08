using KTX.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace KTX.Controllers;


public class ListUsersController : Controller
{

    private readonly KtxDbContext _context;

    public ListUsersController(KtxDbContext context)
    {
        _context = context;
    }
    
    public IActionResult Index()
    {
        var listofData = _context.Users.ToList();
        return View(listofData);
    }
    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create(User model)
    {
        _context.Users.Add(model);
        _context.SaveChanges();
        ViewBag.Message = "Data Insert Successfully";
        return View();
    }
    [HttpGet]
    public IActionResult Edit(int id)
    {
        var data = _context.Users.Where(x => x.Id == id).FirstOrDefault();
        return View(data);
    }
    [HttpPost]
    public IActionResult Edit(User Model)
    {
        var data = _context.Users.Where(x => x.Id == Model.Id).FirstOrDefault();
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
            _context.SaveChanges();
        }

        return RedirectToAction("index");
    }
    public IActionResult Detail(int id)
    {
        var data = _context.Users.Where(x => x.Id == id).Include(x => x.RelativeUsers).FirstOrDefault();
        return View(data);
    }
    public IActionResult Detail2(int id)
    {
        var data = _context.HistoryRents.Where(x => x.UserId == id).Include(y => y.Rents).FirstOrDefault();
        return View(data);
    }
  
    

    //public IActionResult Details(int id)
    //{
    //    var model = _context.Users.Where(i => i.Id == id).Include(x => x.RelativeUsers).FirstOrDefault();
    //    return View(model);
    //}
    public IActionResult Delete(int id)
    {
        var data = _context.Users.Where(x => x.Id == id).FirstOrDefault();
        _context.Users.Remove(data);
        _context.SaveChanges();
        ViewBag.Messsage = "Record Delete Successfully";
        return RedirectToAction("index");
    }
    


}
