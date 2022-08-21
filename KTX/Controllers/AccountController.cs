using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using KTX.Models;
//using Microsoft.AspNetCore.Mvc.Rendering;
//using System.IO;
using System.Data;

using System.Drawing;

using OfficeOpenXml;
using KTX.ViewModels;
using Microsoft.AspNetCore.Hosting;

namespace KTX.Controllers;

public class AccountController : Controller
{
    private readonly IWebHostEnvironment webHostEnvironment;

    private readonly KtxDbContext db;
    public AccountController( KtxDbContext context, IWebHostEnvironment webHostEnvironment)
    {

        db = context;
        this.webHostEnvironment = webHostEnvironment;
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

    public IActionResult Index2()

    {

        return View();

    }
    [HttpPost]

    public IActionResult Index2(IFormFile formFile)

    {

        try

        {



            Guid guid = Guid.NewGuid();

            string newfileName = guid.ToString();

            string fileextention = Path.GetExtension(formFile.FileName);

            string fileName = newfileName + fileextention;

            string uploadpath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images", fileName);

            var stream = new FileStream(uploadpath, FileMode.Create);

            formFile.CopyToAsync(stream);

            ViewBag.Message = "File uploaded successfully.";

        }

        catch

        {

            ViewBag.Message = "Error while uploading the files.";

        }

        return View();

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

    public async Task<IActionResult> Details(int? id)
    {
        
        {

            if (id == null)
            {
                return NotFound();
            }

            //var user = await db.Users
            //    .FirstOrDefaultAsync(m => m.Id == id);
            var user = await db.Users.Where(x => x.Id == id).Include(x => x.RelativeUsers).FirstOrDefaultAsync();

            var userViewModel = new UserViewModel()
            {
                Id = user.Id,
                Fullname = user.Fullname,
                BirthDay = user.BirthDay,
                Address = user.Address,
                Gender = user.Gender,
                TelephoneNumber = user.TelephoneNumber,
                Email = user.Email,
                ExistingImage = user.ImageUrl
            };

            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }
    }
    public IActionResult Create1()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Create1(UserViewModel model)
    {
        if (ModelState.IsValid)
        {
            string uniqueFileName = ProcessUploadedFile(model);
            User user = new()
            {
                IdentityId=model.IdentityId,
                Fullname = model.Fullname,
                BirthDay = model.BirthDay,
                Address = model.Address,
                Gender = model.Gender,
                TelephoneNumber = model.TelephoneNumber,
                Email = model.Email,                
                ImageUrl = uniqueFileName
            };

            db.Users.Add(user);
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(model);
    }
    public async Task<IActionResult> Edit1(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var user = await db.Users.FindAsync(id);
        var speakerViewModel = new UserViewModel()
        {
            IdentityId = user.IdentityId,
            Fullname = user.Fullname,
            BirthDay = user.BirthDay,
            Address = user.Address,
            Gender = user.Gender,
            TelephoneNumber = user.TelephoneNumber,
            Email = user.Email,
            ExistingImage = user.ImageUrl
        };

        if (user == null)
        {
            return NotFound();
        }
        return View(speakerViewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit1(int id, UserViewModel model)
    {
        if (ModelState.IsValid)
        {
            var user = await db.Users.FindAsync(model.Id);
            user.IdentityId = model.IdentityId;
            user.Fullname = model.Fullname;
            user.BirthDay = model.BirthDay;
            user.Address = model.Address;
            user.Gender = model.Gender;
            user.Email = model.Email;

            if (model.ImageUrl != null)
            {
                if (model.ExistingImage != null)
                {
                    string filePath = Path.Combine(webHostEnvironment.WebRootPath, "Uploads", model.ExistingImage);
                    System.IO.File.Delete(filePath);
                }

                user.ImageUrl = ProcessUploadedFile(model);
            }
            db.Update(user);
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View();
    }


    private bool SpeakerExists(int id)
    {
        return db.Users.Any(e => e.Id == id);
    }

    private string ProcessUploadedFile(UserViewModel model)
    {
        string uniqueFileName = null;
        string path = Path.Combine(webHostEnvironment.WebRootPath, "Uploads");
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }

        if (model.ImageUrl != null)
        {
            string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "Uploads");
            uniqueFileName = Guid.NewGuid().ToString() + "_" + model.ImageUrl.FileName;
            string filePath = Path.Combine(uploadsFolder, uniqueFileName);
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                model.ImageUrl.CopyTo(fileStream);
            }
        }

        return uniqueFileName;
    }
    public IActionResult Delete(int id)
    {
        var data = db.Users.Where(x => x.Id == id).FirstOrDefault();
        db.Users.Remove(data);
        db.SaveChanges();
        ViewBag.Messsage = "Record Delete Successfully";
        return RedirectToAction("index");
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

            worksheet.Cells["A1"].Value = "Danh sách sinh viên năm học 2021-20022";
            using (var r = worksheet.Cells["A1:F1"])
            {
                r.Merge = true;
                r.Style.Font.Color.SetColor(Color.Green);
                r.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                r.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(23, 55, 93));
            }

            worksheet.Cells["A4"].Value = "CMND";
            worksheet.Cells["B4"].Value = "Họ và tên";
            worksheet.Cells["C4"].Value = "Email";
            worksheet.Cells["D4"].Value = "Ngày sinh";
            worksheet.Cells["E4"].Value = "Địa chỉ";
            worksheet.Cells["F4"].Value = "Điện thoại";
            worksheet.Cells["G4"].Value = "Link Ảnh";
            worksheet.Cells["A4:G4"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
            worksheet.Cells["A4:G4"].Style.Fill.BackgroundColor.SetColor(Color.Yellow);

            row = 5;
            foreach (var user in users)
            {
                worksheet.Cells[row, 1].Value = user.IdentityId;
                worksheet.Cells[row, 2].Value = user.Fullname;
                worksheet.Cells[row, 3].Value = user.Email;
                worksheet.Cells[row, 4].Value = user.BirthDay;
                worksheet.Cells[row, 5].Value = user.Address;
                worksheet.Cells[row, 6].Value = user.TelephoneNumber;
                worksheet.Cells[row, 7].Value = user.ImageUrl;

                row++; // row = row + 1;
            }

            //xlPackage.Workbook.Properties.Title = "User list";
            //xlPackage.Workbook.Properties.Author = "Mohamad";

            xlPackage.Save();
        }

        stream.Position = 0;
        return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "DANHSACHSINHVIEN.xlsx");
    }
   

    //public async Task<IActionResult> Index1(string searchString)
    //{
    //    var users = from m in db.Users
    //                     select m;

    //    if (!String.IsNullOrEmpty(searchString))
    //    {
    //        users = users.Where(s => s.IdentityId!.Contains(searchString));
    //    }

    //    return View(await users.ToListAsync());
    //}



    [HttpGet]
    public IActionResult BatchUserUpload()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> BatchUserUpload(IFormFile batchUsers)
    {
        if (ModelState.IsValid)
        {
            if (batchUsers?.Length > 0)
            {
                // convert to a stream
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                var stream = batchUsers.OpenReadStream();

                List<User> users = new List<User>();
                List<HistoryRent> users1 = new List<HistoryRent>();
                //List<User> users = new List<User>();

                try
                {
                    using (var package = new ExcelPackage(stream))
                    {
                        var worksheet = package.Workbook.Worksheets.First();
                        var rowCount = worksheet.Dimension.Rows;
                        
                        for (var row = 5; row <= rowCount; row++)
                        {
                            try
                            {
                                var identityid = worksheet.Cells[row, 1].Value?.ToString();
                                var fullname = worksheet.Cells[row, 2].Value?.ToString();
                                var email = worksheet.Cells[row, 3].Value?.ToString();
                                //var birthday = Convert.ToDateTime.Cells[row, 4].Value?.ToString("dd-MM-yyyy");
                                var address = worksheet.Cells[row, 4].Value?.ToString();
                                var telephonenumber = worksheet.Cells[row, 5].Value?.ToString();
                                
                                var user = new User()

                                {
                                    IdentityId = identityid,
                                    Fullname = fullname,
                                    Email = email,
                                    //BirthDay = birthday,
                                    Address = address,
                                    TelephoneNumber = telephonenumber
                                };
                                
                                


                                users.Add(user);
                                
                                db.Users.Add(user);
                                
                                db.SaveChanges();
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                        }
                    }

                    return RedirectToAction("Index");


                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        return View();
    }
    public IActionResult Changepassword(Admin login)
    {

        var detail = db.Admins.Where(log => log.Password == login.Password && log.Email == login.Email).FirstOrDefault();

        if (detail != null && login.NewPassword == login.ComfirmNewPassword)
        {

            detail.Password = login.NewPassword;

            db.SaveChanges();

            ViewBag.Message = "Password updated Successfully!";
            return RedirectToAction("Login", "Account");

        }
        else
        {
            ViewBag.Message = "Password not Updated!";
        }


        return View(login);
    }

    //Logout
    public ActionResult Logout()
    {       
        return RedirectToAction("Login");
    }

    //[Route("login")]
    //public IActionResult Login1()
    //{
    //    return View();
    //}

    //[Route("login")]
    //[HttpPost]
    //public async Task<IActionResult> Login(SignInModel signInModel, string returnUrl)
    //{
    //    if (ModelState.IsValid)
    //    {
    //        var result = await _accountRepository.PasswordSignInAsync(signInModel);
    //        if (result.Succeeded)
    //        {
    //            if (!string.IsNullOrEmpty(returnUrl))
    //            {
    //                return LocalRedirect(returnUrl);
    //            }
    //            return RedirectToAction("Index", "Home");
    //        }
    //        if (result.IsNotAllowed)
    //        {
    //            ModelState.AddModelError("", "Not allowed to login");
    //        }
    //        else if (result.IsLockedOut)
    //        {
    //            ModelState.AddModelError("", "Account blocked. Try after some time.");
    //        }
    //        else
    //        {
    //            ModelState.AddModelError("", "Invalid credentials");
    //        }

    //    }

    //    return View(signInModel);
    //}
    //[HttpPost]
    //public FileResult ExportPdf(string ExportData)
    //{
    //    using (MemoryStream stream = new System.IO.MemoryStream())
    //    {
    //        StringReader reader = new StringReader(ExportData);
    //        Document PdfFile = new Document(PageSize.A4);
    //        PdfWriter writer = PdfWriter.GetInstance(PdfFile, stream);
    //        PdfFile.Open();
    //        XMLWorkerHelper.GetInstance().ParseXHtml(writer, PdfFile, reader);
    //        PdfFile.Close();
    //        return File(stream.ToArray(), "application/pdf", "ExportData.pdf");
    //    }
    //}

    //[HttpPost]
    //public FileResult ExportExcel(string ExportData)
    //{
    //    using (MemoryStream stream = new System.IO.MemoryStream())
    //    {
    //        StringReader reader = new StringReader(ExportData);
    //        Document PdfFile = new Document(PageSize.A4);
    //        PdfWriter writer = PdfWriter.GetInstance(PdfFile, stream);
    //        PdfFile.Open();
    //        XMLWorkerHelper.GetInstance().ParseXHtml(writer, PdfFile, reader);
    //        PdfFile.Close();
    //        return File(stream.ToArray(), "application/pdf", "ExportData.pdf");
    //    }
    //}


}
