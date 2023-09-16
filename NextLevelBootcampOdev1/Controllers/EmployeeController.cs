using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NextLevelBootcampOdev1.Models;
using NextLevelBootcampOdev1.ViewModel;
using System.Diagnostics;

namespace NextLevelBootcampOdev1.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly ILogger<EmployeeController> _logger;
        private readonly NorthwindDbContext _context;
        private readonly IMapper _mapper;

        public EmployeeController(ILogger<EmployeeController> logger, NorthwindDbContext context, IMapper mapper)
        {
            _logger = logger;
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {

            return View(await _context.Employees.ToListAsync());                                                    //Employee'lerimizi veri tabanından çekip frontend tarafına List olarak gönderdik. Frontend tarafında da bu list'i @model ile aldık.
        }

        [HttpGet]
        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AddEmployee()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddEmployee([FromForm] AddEmployeeModel model)
        {
            _context.Employees.Add(_mapper.Map<Employee>(model));                                                   //Cilent'tan aldığımız verileri bir model'e attık ardından o modeli elimizdeki gerçek Employee entity'sine mapledik ve veri tabanına eklemek için gönderdik.
            _context.SaveChanges();                                                                                 //Gönderdiğimiz ekleme işleminin gerçekleşmesi için SaveChanges metodunu kullandık.
            return View("/Employee/Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}