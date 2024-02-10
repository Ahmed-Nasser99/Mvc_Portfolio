using Core.Interfaces;
using Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Web.ViewModels;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace Web.Controllers
{
    public class PortfolioItemsController : Controller
    {
        private readonly IUnitOfWork<PortfolioItem> _portfolio;
        private readonly IHostingEnvironment _hosting;
        public PortfolioItemsController(IUnitOfWork<PortfolioItem> portfolio ,IHostingEnvironment hosting) {
          _portfolio = portfolio;
            _hosting = hosting;
        }
        public ActionResult Index()
        {
            return View(_portfolio.Entity.GetAll());
        }

        // GET: PortfolioItemController/Details/5
        public ActionResult Details(Guid id)
        {
            return View(_portfolio.Entity.GetById(id));
        }

        public ActionResult Create()
        {
            return View();
        }

        // POST: PortfolioItemController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PortfolioViewModel model)
        {
           
                if (model != null)
                {
                    if (model.File != null)
                    {
                        var upload = Path.Combine(_hosting.WebRootPath, @"img\portfolio");
                        var fullPath = Path.Combine(upload, model.File.FileName);
                        model.File.CopyTo(new FileStream(fullPath, FileMode.Create));

                    }

                var portfolioItem = new PortfolioItem
                {
                    ProjectName = model.ProjectName,
                    Description = model.Description,
                    ImageUrl = model.File.FileName
                };
                _portfolio.Entity.Insert(portfolioItem);
                _portfolio.Complete();
                return RedirectToAction(nameof(Index));
                }

            return View(model);
     
        }

        // GET: PortfolioItemController/Edit/5
        public ActionResult Edit(Guid id)

        {


            var portfolioItem = _portfolio.Entity.GetById(id);
            if (portfolioItem == null)
            {
                return NotFound();
            }

            PortfolioViewModel portfolioViewModel = new PortfolioViewModel
            {
                id = portfolioItem.id,
                Description = portfolioItem.Description,
                ImageUrl = portfolioItem.ImageUrl,
                ProjectName = portfolioItem.ProjectName
            };
            return View(portfolioViewModel);
        }

        // POST: PortfolioItemController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Guid id,PortfolioViewModel model)
        {
            if (model != null)
            {
                if (model.File != null)
                {
                    var upload = Path.Combine(_hosting.WebRootPath, @"img\portfolio");
                    var fullPath = Path.Combine(upload, model.File.FileName);
                    model.File.CopyTo(new FileStream(fullPath, FileMode.Create));

                }

                var portfolioItem = new PortfolioItem
                {
                    id = id,
                    ProjectName = model.ProjectName,
                    Description = model.Description,
                    ImageUrl = model.File.FileName
                };
                _portfolio.Entity.Update(portfolioItem);
                _portfolio.Complete();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // GET: PortfolioItemController/Delete/5
        public ActionResult Delete(Guid id)
        {
            return View(_portfolio.Entity.GetById(id));
        }

        // POST: PortfolioItemController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteItem(Guid id)
        {
            try
            {
                _portfolio.Entity.Delete(id);
                _portfolio.Complete();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return RedirectToAction(nameof(Delete));
            }
        }
    }
}
