using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Randevu_Sistemi.Data;
using Randevu_Sistemi.Models;
using Randevu_Sistemi.ViewModels;

namespace Randevu_Sistemi.Controllers
{
	public class HastaController : ControllerBase
	{
		private readonly UygulamaDbContext _uygulamaDbcontext;//veritabanına erişmek için kullanlılan kod

		public HastaController(UygulamaDbContext context) : base(context)// Constructor
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
			var ses = JsonConvert.DeserializeObject<SessionVM>(value);
			ViewBag.Kullanici = ses.UserRole + " :" + ses.Fullname;

			List<HastaListVM> list = new List<HastaListVM>();
			foreach (var item in _uygulamaDbcontext.Hastalar.Include(x => x.hekim).Include(x=>x.hastalikTuru).ToList())
			{
				var listitem = new HastaListVM { Id = item.Id, HastaAd = item.HastaAd, HekimName = item.hekim.HekimAd, HastalikName = item.hastalikTuru.HastalikAdi };

				list.Add(listitem);
			}
			return View(list);
		}
		public IActionResult Ekle()
		{
		    HastaCE_VM model = new HastaCE_VM();
			model.HastalikTuruList =GetHastalikTuruList();
			model.HekimList = GetHekimList();
			return View(model);
		}
		public List<SelectItem> GetHekimList()
		{
			List<SelectItem> list = new List<SelectItem>();

			foreach (var item in _uygulamaDbcontext.Hekimler.ToList())
			{
				var listitem = new SelectItem { Id = item.Id.ToString(), Name = item.HekimAd };
				list.Add(listitem);
			}
			return list;
		}
		public List<SelectItem> GetHastalikTuruList()
		{
			List<SelectItem> list = new List<SelectItem>();

			foreach (var item in _uygulamaDbcontext.HastalikTurleri.ToList())
			{
				var listitem = new SelectItem { Id = item.Id.ToString(), Name = item.HastalikAdi };
				list.Add(listitem);
			}
			return list;
		}
		[HttpPost]

		public IActionResult Ekle(HastaCE_VM vm)
		{
			var hekim = _uygulamaDbcontext.Hekimler.Find(vm.HekimId);
			var hastalikTur =_uygulamaDbcontext.HastalikTurleri.Find(vm.HastalikTuruId);

			Hasta model = new Hasta();
			
			model.Id = vm.Id;
			model.HastaAd = vm.HastaAd;
			
			

			model.hekim = hekim;
			model.hastalikTuru = hastalikTur;

			_uygulamaDbcontext.Hastalar.Add(model); //LINQ
			_uygulamaDbcontext.SaveChanges();

			return RedirectToAction("Index");

		}

		public IActionResult Guncelle(int id)
		{
			var hasta = _uygulamaDbcontext.Hastalar.Include(x => x.hekim).Include(x=>x.hastalikTuru).Where(x => x.Id == id).FirstOrDefault();
			HastaCE_VM vm = new HastaCE_VM();
			vm.Id = hasta.Id;
			vm.HekimId = hasta.hekim.Id;
			vm.HastalikTuruId = hasta.hastalikTuru.Id;
			vm.HastaAd = hasta.HastaAd;
			vm.HekimList = GetHekimList();
			vm.HastalikTuruList=GetHastalikTuruList();

			return View(vm);

		}

		[HttpPost]
		public IActionResult Guncelle(HastaCE_VM vm)
		{
			var hekim = _uygulamaDbcontext.Hekimler.Find(vm.HekimId);
			var hastalikTuru = _uygulamaDbcontext.HastalikTurleri.Find(vm.HastalikTuruId);
			var model = _uygulamaDbcontext.Hastalar.Find(vm.Id);
			model.HastaAd = vm.HastaAd;
			model.hekim = hekim;
			model.hastalikTuru = hastalikTuru;

			_uygulamaDbcontext.SaveChanges();
			return RedirectToAction("Index");

		}

		//Get action
		public IActionResult Sil(int? id)//null olabilir
		{
			var model = _uygulamaDbcontext.Hastalar.Find(id);
			_uygulamaDbcontext.Hastalar.Remove(model);
			_uygulamaDbcontext.SaveChanges();
			return RedirectToAction("Index");

		}

		//[HttpPost, ActionName("Sil")]
		//public IActionResult Sill(int? id)
		//{
		//	Hasta? hasta = _uygulamaDbcontext.Hastalar.Find(id);
		//	if (hasta == null)
		//	{
		//		return NotFound();
		//	}
		//	_uygulamaDbcontext.Hastalar.Remove(hasta);
		//	_uygulamaDbcontext.SaveChanges();
		//	TempData["basarili"] = "Yeni Hasta başarıyla silindi";
		//	return RedirectToAction("Index");

		//}

		public IActionResult Search()
		{
			HastaSearchVM vm = new HastaSearchVM();
			vm.HekimList = GetHekimList();
			vm.HastalikTuruList = GetHastalikTuruList();
			return View(vm);
		}
		[HttpPost]
		public IActionResult Search(HastaSearchVM vm)
		{
			var hastalar = _uygulamaDbcontext.Hastalar.Include(x => x.hekim).Include(x=>x.hastalikTuru).ToList();

			if (vm.HastaAd != null)
			{
				hastalar = hastalar.Where(x => x.HastaAd.Contains(vm.HastaAd)).ToList();
			}
			
			if (vm.HekimId > 0)
			{
				hastalar = hastalar.Where(x => x.hekim.Id == vm.HekimId).ToList();
			}
			if (vm.HastalikTuruId > 0)
			{
				hastalar = hastalar.Where(x => x.hastalikTuru.Id == vm.HastalikTuruId).ToList();
			}

			List<HastaListVM> list = new List<HastaListVM>();
			foreach (var item in hastalar)
			{
				var listitem = new HastaListVM { Id = item.Id, HastaAd = item.HastaAd, HastalikName = item.hastalikTuru.HastalikAdi, HekimName = item.hekim.HekimAd };

				list.Add(listitem);
			}
			vm.ResultList = list;
			vm.HekimList = GetHekimList();
			vm.HastalikTuruList = GetHastalikTuruList();
			return View(vm);
		}





	}
}
