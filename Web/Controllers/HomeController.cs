using Core.Interfaces;
using Core.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Web.Models;
using Web.ViewModels;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUnitOfWork<Owner> _owner;
        private readonly IUnitOfWork<PortfolioItem> _portfolio;

        public HomeController(
            IUnitOfWork<Owner> owner,
            IUnitOfWork<PortfolioItem> portfolio)
        {
            _owner = owner;
            _portfolio = portfolio;
        }

        public IActionResult Index()
        {
            var homeView = new HomeViewModel
            {
                Owner = _owner.Entity.GetAll().First(),
                PortfolioItem = _portfolio.Entity.GetAll().ToList()
            };
            return View(homeView);
        }

    }
}