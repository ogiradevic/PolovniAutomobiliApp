using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using PolovniAutomobiliFromScratch.Data;
using PolovniAutomobiliFromScratch.Models;
using PolovniAutomobiliFromScratch.ViewModels;

namespace PolovniAutomobiliFromScratch.Controllers
{
    public class AutomobiliController : Controller
    {
        private readonly AutomobiliDbContext _dbContext;

        public AutomobiliController(AutomobiliDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult PregledAutomobila()
        {
            var model = new SviAutomobiliViewModel();
            var sviAutomobili = _dbContext.Automobil.ToList();
            model.SviAutomobili = sviAutomobili;
            return View(model);
        }

        public IActionResult DetaljiOAutomobilu(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Bad request");
            }

            var automobil = _dbContext.Automobil.FirstOrDefault(x => x.Id == id);

            if (automobil == null)
            {
                return BadRequest("Trazeni automobil ne postoji");
            }

            return View(automobil);
        }

        public IActionResult NoviAutomobil()
        {
            var model = new Automobil();

            return View(model);
        }

        public IActionResult DodajNoviAutomobil(Automobil automobil)
        {
            if (automobil == null)
            {
                return BadRequest("BadRequest");
            }

            _dbContext.Automobil.Add(automobil);
            _dbContext.SaveChanges();

            return RedirectToAction("PregledAutomobila");
        }

        [Authorize]
        public IActionResult ObrisiAutomobil(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Bad request");
            }

            var automobil = _dbContext.Automobil.FirstOrDefault(x => x.Id == id);

            if (automobil == null)
            {
                return BadRequest("Trazeni automobil ne postoji");
            }

            _dbContext.Automobil.Remove(automobil);
            _dbContext.SaveChanges();

            return RedirectToAction("PregledAutomobila");
        }

        public IActionResult SetCulture(string culture, string sourceUrl)
        {
            Response.Cookies.Append(
            CookieRequestCultureProvider.DefaultCookieName,
            CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
            new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
            );
            return Redirect(sourceUrl);
        }
    }
}
