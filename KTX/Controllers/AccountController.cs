﻿using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using KTX.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.IO;
using System.Data;
using System.Linq;
using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
//using SampleExcel.Models




namespace KTX.Controllers;

public class AccountController : Controller
{
    
    private readonly KtxDbContext db;
    public AccountController( KtxDbContext context)
    {
        
        db = context;
    }
    
    public async Task<IActionResult> Index(string sortOrder,
    string currentFilter,
    string searchString,
    int? pageNumber)
    {
        ViewData["CurrentSort"] = sortOrder;
        ViewData["FullNameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "fullname_desc" : "";       
        ViewData["IdenSortParm"] = sortOrder == "Iden" ? "iden_desc" : "Iden";
        if (searchString != null)
        {
            pageNumber = 1;
        }
        else
        {
            searchString = currentFilter;
        }
        ViewData["CurrentFilter"] = searchString;
        var users = from s in db.Users
                       select s;
        if (!String.IsNullOrEmpty(searchString))
        {
            users = users.Where(s => s.Fullname!.Contains(searchString) || s.IdentityId!.Contains(searchString));
        }
        switch (sortOrder)
        {
            case "fullname_desc":
                users = users.OrderByDescending(s => s.IdentityId);
                break;
            case "Iden":
                users = users.OrderBy(s => s.Fullname);
                break;
            case "iden_desc":
                users = users.OrderByDescending(s => s.Fullname);
                break;
            default:
                users = users.OrderBy(s => s.IdentityId);
                break;
        }
        int pageSize = 8;
        return View(await PaginatedList<User>.CreateAsync(users.AsNoTracking(), pageNumber ?? 1, pageSize));
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


    public IActionResult ExportToExcel()
    {
        // Getting the information from our mimic db
        //var users = GetUserList();
        var users = from s in db.Users
                    select s;

        // Start exporting to Excel
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        var stream = new MemoryStream();

        using (var xlPackage = new ExcelPackage(stream))
        {
            // Define a worksheet
            var worksheet = xlPackage.Workbook.Worksheets.Add("Users");

            // Styling
            var customStyle = xlPackage.Workbook.Styles.CreateNamedStyle("CustomStyle");
            customStyle.Style.Font.UnderLine = true;
            customStyle.Style.Font.Color.SetColor(Color.Red);

            // First row
            var startRow = 5;
            var row = startRow;

            worksheet.Cells["A1"].Value = "Sample User Export";
            using (var r = worksheet.Cells["A1:C1"])
            {
                r.Merge = true;
                r.Style.Font.Color.SetColor(Color.Green);
                r.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                r.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(23, 55, 93));
            }

            worksheet.Cells["A4"].Value = "CMND";
            worksheet.Cells["B4"].Value = "Họ và tên";
            worksheet.Cells["C4"].Value = "Email";
            worksheet.Cells["A4:C4"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
            worksheet.Cells["A4:C4"].Style.Fill.BackgroundColor.SetColor(Color.Yellow);

            row = 5;
            foreach (var user in users)
            {
                worksheet.Cells[row, 1].Value = user.IdentityId;
                worksheet.Cells[row, 2].Value = user.Fullname;
                worksheet.Cells[row, 3].Value = user.Email;

                row++; // row = row + 1;
            }

            xlPackage.Workbook.Properties.Title = "User list";
            xlPackage.Workbook.Properties.Author = "Mohamad";

            xlPackage.Save();
        }

        stream.Position = 0;
        return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "users.xlsx");
    }
    public async Task<IActionResult> Index1(string searchString)
    {
        var users = from m in db.Users
                         select m;

        if (!String.IsNullOrEmpty(searchString))
        {
            users = users.Where(s => s.IdentityId!.Contains(searchString));
        }

        return View(await users.ToListAsync());
    }



    [HttpGet]
    public IActionResult BatchUserUpload()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult BatchUserUpload(IFormFile batchUsers)
    {
        if (ModelState.IsValid)
        {
            if (batchUsers?.Length > 0)
            {
                // convert to a stream
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                var stream = batchUsers.OpenReadStream();

                List<User> users = new List<User>();
                //List<User> users = new List<User>();

                try
                {
                    using (var package = new ExcelPackage(stream))
                    {
                        var worksheet = package.Workbook.Worksheets.First();
                        var rowCount = worksheet.Dimension.Rows;

                        for (var row = 2; row <= rowCount; row++)
                        {
                            try
                            {
                                var identityid = worksheet.Cells[row, 1].Value?.ToString();
                                var fullname = worksheet.Cells[row, 2].Value?.ToString();
                                var email = worksheet.Cells[row, 3].Value?.ToString();

                                var user = new User()
                                {
                                    IdentityId = identityid,
                                    Fullname = fullname,
                                    Email = email
                                };

                                users.Add(user);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                        }
                    }

                    return View("Index1", users);


                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        return View();
    }

    //Logout
    public ActionResult Logout()
    {       
        return RedirectToAction("Login");
    }
}
