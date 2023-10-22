using Microsoft.AspNetCore.Mvc;

namespace Blog.Web.Controllers
{
	public class AdminBlogPostsController : Controller
	{

		[HttpGet]
		public IActionResult Add()
		{
			return View();
		}
	}
}
