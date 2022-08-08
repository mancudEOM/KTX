using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using KTX.Models;

namespace KTX.Controllers;

public class ListRelativesController : Controller
{

    private readonly KtxDbContext _context;

    public ListRelativesController(KtxDbContext context)
    {
        _context = context;
    }
    public IActionResult Index()
    {
        var model = _context.RelativeUsers.ToList();
        return View(model);
    }
}