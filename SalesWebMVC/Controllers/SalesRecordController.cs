using Microsoft.AspNetCore.Mvc;

namespace SalesWebMVC.Controllers
{
	public class SalesRecordController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}

		public IActionResult SimpleSearch()
		{
			return View();
		}

		public IActionResult GroupingSearch()
		{
			return View();
		}
	}
}
