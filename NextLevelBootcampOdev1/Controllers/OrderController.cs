using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NextLevelBootcampOdev1.Models;

namespace NextLevelBootcampOdev1.Controllers
{
    public class OrderController : Controller
    {
        private readonly ILogger<EmployeeController> _logger;
        private readonly NorthwindDbContext _context;
        private readonly IMapper _mapper;

        public OrderController(ILogger<EmployeeController> logger, NorthwindDbContext context, IMapper mapper)
        {
            _logger = logger;
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Orders(int id)
        {
            List<Order> orders = _context.Orders.Where(x => x.EmployeeId == id).ToList();                           //EmployeeID'ye göre filtrelenen siparişler List'eye aktarıldı.
            return View(orders);
        }

        [HttpGet]
        public IActionResult OrderDetails(int id)
        {
            List<OrderDetail> order = _context.OrderDetails.Where(x => x.OrderId == id).ToList();                   //Veritabanın ilgili siparişe ait verileri List'eye çektik.

            decimal amount = 0, discount = 0;
            foreach (var item in order)
            {
                amount+= item.UnitPrice*item.Quantity;                                                             //Siperişler için toplam ücret ve indirimler hesaplandı.
                discount += item.UnitPrice * (decimal)item.Discount;
            }
            ViewBag.Discount = discount.ToString("#0.00");                                                         //Hesaplanan toplam ücret, indirim ve nihai ücret ViewBag'ler ile frontend tarafına taşındı
            ViewBag.TotalWithoutDiscount = amount.ToString("#.00");
            ViewBag.Total = (amount-discount).ToString("#.00");
            return View(order);                                                                                    //ViewBag'lerle taşınan veriler dışında sipariş içerisindeki ürünlerde Frontend tarafına aktarıldı.
        }
    }
}
