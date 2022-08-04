using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using KTX.Models;

namespace KTX.Controllers;

public class ListRentsController : Controller
{

    private readonly KtxDbContext _context;

    public ListRentsController(KtxDbContext context)
    {
        _context = context;
    }
    public IActionResult Index()
    {
        var model = _context.Rents.ToList();
        return View(model);
    }
}