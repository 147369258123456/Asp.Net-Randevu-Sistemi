using Microsoft.AspNetCore.Mvc;
using Randevu_Sistemi.Data;
using Randevu_Sistemi.Models;

namespace Randevu_Sistemi.Controllers
{
    public class HekimController : Controller
    {
        private readonly UygulamaDbContext _uygulamaDbcontext;//veritabanına erişmek için kullanlılan kod

        public HekimController(UygulamaDbContext context)//Constructor
        {
            _uygulamaDbcontext = context;//private readonly alanına atar
        }
        public IActionResult Index()
        {
			var value = HttpContext.Session.GetString("UserSessionInfo");
			if (value == null)
			{
				return RedirectToAction("Index", "Login");
			}
			List<Hekim> objHekimList = _uygulamaDbcontext.Hekimler.ToList();//index çağrıldığında veritabanına giderek hastaları çeker
            return View(objHekimList);
        }

        public IActionResult Ekle()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Ekle(Hekim hekim)
        {
            if (ModelState.IsValid)//HastalikTuru Modelindeyazdığımız koşul sağlanırsa ekleme yapılacak
            {
                _uygulamaDbcontext.Hekimler.Add(hekim);
                _uygulamaDbcontext.SaveChanges(); //bu kod sayesinde bilgiler veri tabanına eklenir
                TempData["basarili"] = "Yeni Hekim  başarıyla oluşturuldu";
                return RedirectToAction("Index");
            }
            return View();

        }

        public IActionResult Guncelle(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Hekim? hekimVt = _uygulamaDbcontext.Hekimler.Find(id);
            if (hekimVt == null)
            {
                return NotFound();
            }
            return View(hekimVt);

        }

        [HttpPost]
        public IActionResult Guncelle(Hekim hekim)
        {
            if (ModelState.IsValid)//HastalikTuru Modelindeyazdığımız koşul sağlanırsa ekleme yapılacak
            {
                _uygulamaDbcontext.Hekimler.Update(hekim);
                _uygulamaDbcontext.SaveChanges(); //bu kod sayesinde bilgiler veri tabanına eklenir
                TempData["basarili"] = "Yeni hekim  başarıyla güncellendi";
                return RedirectToAction("Index");
            }
            return View();

        }

        //Get action
        public IActionResult Sil(int? id)//null olabilir
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Hekim? hekimVt = _uygulamaDbcontext.Hekimler.Find(id);
            if (hekimVt == null)
            {
                return NotFound();
            }
            return View(hekimVt);

        }

        [HttpPost, ActionName("Sil")]
        public IActionResult Sill(int? id)
        {
            Hekim? hekim = _uygulamaDbcontext.Hekimler.Find(id);
            if (hekim == null)
            {
                return NotFound();
            }
            _uygulamaDbcontext.Hekimler.Remove(hekim);
            _uygulamaDbcontext.SaveChanges();
            TempData["basarili"] = "Yeni Hekim başarıyla silindi";
            return RedirectToAction("Index");

        }

    }
}
