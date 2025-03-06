using Microsoft.AspNetCore.Mvc;
using Randevu_Sistemi.Data;
using Randevu_Sistemi.Models;

namespace Randevu_Sistemi.Controllers
{
	public class HastalikTuruController : Controller
	{
		private readonly UygulamaDbContext _uygulamaDbcontext;//veritabanına erişmek için kullanlılan kod

		public HastalikTuruController(UygulamaDbContext context)//Constructor
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
			List<HastalikTuru> objHastalikTuruList = _uygulamaDbcontext.HastalikTurleri.ToList();//index çağrıldığında veritabanına giderek hastaliktürlerini çeker
			return View(objHastalikTuruList);
			 
		}

		public IActionResult Ekle()
		{
			return View();
		}


		[HttpPost]
        public IActionResult Ekle(HastalikTuru hastalikTuru)
        {
			if (ModelState.IsValid)//HastalikTuru Modelindeyazdığımız koşul sağlanırsa ekleme yapılacak
			{
                _uygulamaDbcontext.HastalikTurleri.Add(hastalikTuru);
                _uygulamaDbcontext.SaveChanges(); //bu kod sayesinde bilgiler veri tabanına eklenir
                TempData["basarili"] = "Yeni Hasta Türü başarıyla oluşturuldu";
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
            HastalikTuru? hastalikTuruVt = _uygulamaDbcontext.HastalikTurleri.Find(id);
            if (hastalikTuruVt == null) 
            { 
                return NotFound(); 
            }
            return View(hastalikTuruVt);
            
        }
        


        [HttpPost]
        public IActionResult Guncelle(HastalikTuru hastalikTuru)
        {
            if (ModelState.IsValid)//HastalikTuru Modelindeyazdığımız koşul sağlanırsa ekleme yapılacak
            {
                _uygulamaDbcontext.HastalikTurleri.Update(hastalikTuru);
                _uygulamaDbcontext.SaveChanges(); //bu kod sayesinde bilgiler veri tabanına eklenir
                TempData["basarili"] = "Yeni Hasta Türü başarıyla güncellendi";
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
            HastalikTuru? hastalikTuruVt = _uygulamaDbcontext.HastalikTurleri.Find(id);
            if (hastalikTuruVt == null)
            {
                return NotFound();
            }
            return View(hastalikTuruVt);

        }


        [HttpPost, ActionName("Sil")]
        public IActionResult Sill(int? id)
        {
            HastalikTuru? hastalikTuru = _uygulamaDbcontext.HastalikTurleri.Find(id);
            if(hastalikTuru == null)
            {
                return NotFound();
            }
            _uygulamaDbcontext.HastalikTurleri.Remove(hastalikTuru);
            _uygulamaDbcontext.SaveChanges();
            TempData["basarili"] = "Yeni Hasta Türü başarıyla silindi";
            return RedirectToAction("Index");

        }




    }
}
