using System;
using Fiorello.Data;
using Fiorello.Services.Interfaces;
using Fiorello.ViewModels;
using Newtonsoft.Json;

namespace Fiorello.Services
{
	public class LayoutService: ILayoutService
	{
        private readonly AppDbContext _context;
        private readonly ICartService _cartService;

        public LayoutService(AppDbContext context,
                             ICartService cartService)
        {
            _context = context;
            _cartService = cartService;
        }



        public LayoutVM GetSettingDatas()
        {
            Dictionary<string, string> settings = _context.Settings.AsEnumerable().ToDictionary(s => s.Key, s => s.Value);

            List<CartVM> cartDatas = _cartService.GetCartDatas();

            return new LayoutVM { Settings = settings, CartCount = cartDatas.Sum(p => p.Count) };
        }
    }
}