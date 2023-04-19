using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Fiorello.Models;
using Fiorello.Data;
using Microsoft.EntityFrameworkCore;
using Fiorello.ViewModels;
using Newtonsoft.Json;
using Fiorello.Services;
using Fiorello.Services.Interfaces;

namespace Fiorello.Controllers;

public class HomeController : Controller
{
    private readonly AppDbContext _context;
    private readonly ILayoutService _layoutService;
    private readonly ICartService _cartService;
    private readonly IProductService _productService;

    public HomeController(AppDbContext context,
                          ILayoutService layoutService,
                          ICartService cartService,
                          IProductService productService)
    {
        _context = context;
        _layoutService = layoutService;
        _cartService = cartService;
        _productService = productService;
    }



    public async Task<IActionResult> Index()
    {
        IEnumerable<Slider> sliders = await _context.Sliders.Where(s => !s.SoftDelete).ToListAsync();
        SliderInfo sliderInfo = await _context.SliderInfo.Where(si => !si.SoftDelete).FirstOrDefaultAsync();
        About about = await _context.Abouts.Where(a => !a.SoftDelete).Include(a => a.Advantages).FirstOrDefaultAsync();
        Expert expert = await _context.Experts.Where(e => !e.SoftDelete).Include(e => e.Persons).FirstOrDefaultAsync();
        Subscribe subscribe = await _context.Subscribes.Where(s => !s.SoftDelete).FirstOrDefaultAsync();
        IEnumerable<Quote> quotes = await _context.Quotes.Where(q => !q.SoftDelete).ToListAsync();
        IEnumerable<Instagram> instagrams = await _context.Instagrams.Where(i => !i.SoftDelete).ToListAsync();
        IEnumerable<Category> categories = await _context.Categories.Where(c => !c.SoftDelete).ToListAsync();
        IEnumerable<Product> products = await _productService.GetAll();


        HomeVM homeVM = new HomeVM
        {
            Sliders = sliders,
            SliderInfo = sliderInfo,
            About = about,
            Expert = expert,
            Subscribe = subscribe,
            Quotes = quotes,
            Instagrams = instagrams,
            Categories = categories,
            Products = products
        };

        return View(homeVM);
    }

    [ActionName("Add")]
    [HttpPost]
    public async Task<IActionResult> AddToCart(int? id)
    {
        if (id is null) return BadRequest();
        Product dbProduct = await _productService.GetById(id);

        if (dbProduct is null) return NotFound();

        List<CartVM> cart = _cartService.GetCartDatas();

        CartVM existedProduct = cart.FirstOrDefault(cv => cv.Id == dbProduct.Id);

        _cartService.AddProductToCart(existedProduct, dbProduct, cart);

        int productsCount = cart.Sum(p => p.Count);

        return Ok(productsCount);
    }
}