using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using KTX.Models;

namespace KTX.Controllers;

public class PostController : Controller
{

    private readonly KtxDbContext _context;

    public PostController(KtxDbContext context)
    {
        _context = context;
    }
   
    public IActionResult Index()
    {
        var listofData = _context.Posts.ToList();
        return View(listofData);
    }
    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create(Post model)
    {
        _context.Posts.Add(model);
        _context.SaveChanges();
        ViewBag.Message = "Data Insert Successfully";
        return View();
    }
    [HttpGet]
    public IActionResult Edit(int id)
    {
        var data = _context.Posts.Where(x => x.Id == id).FirstOrDefault();
        return View(data);
    }
    [HttpPost]
    public IActionResult Edit(Post Model)
    {
        var data = _context.Posts.Where(x => x.Id == Model.Id).FirstOrDefault();
        if (data != null)
        {
            data.DatePost = Model.DatePost;
            data.Title = Model.Title;
            data.Content = Model.Content;
            _context.SaveChanges();
        }

        return RedirectToAction("index");
    }
    public IActionResult Detail(int id)
    {
        var data = _context.Posts.Where(x => x.Id == id).FirstOrDefault();
        return View(data);
    }
    public IActionResult Delete(int id)
    {
        var data = _context.Posts.Where(x => x.Id == id).FirstOrDefault();
        _context.Posts.Remove(data);
        _context.SaveChanges();
        ViewBag.Messsage = "Record Delete Successfully";
        return RedirectToAction("index");
    }
}
