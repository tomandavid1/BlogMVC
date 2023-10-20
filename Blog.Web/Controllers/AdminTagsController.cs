using Blog.Web.Data;
using Blog.Web.Models.Domain;
using Blog.Web.Models.ViewModels;
using Blog.Web.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Blog.Web.Controllers
{
	public class AdminTagsController : Controller
	{
		private readonly ITagRepository tagRepository;

		public AdminTagsController(ITagRepository tagRepository)
		{
			this.tagRepository = tagRepository;
		}

		[HttpGet]
		public IActionResult Add()
		{
			return View();
		}

		[HttpPost]
		[ActionName("Add")]
		public async Task<IActionResult> Add(AddTagRequest addTagRequest)
		{

			// Mapping AddTagRequest to Tag domain model
			var tag = new Tag
			{
				Name = addTagRequest.Name,
				DisplayName = addTagRequest.DisplayName
			};

			await tagRepository.AddAsync(tag);

			return RedirectToAction("List");
		}

		[HttpGet]
		[ActionName("List")]
		public async Task<IActionResult> List()
		{
			// Read the tags

			var tags = await tagRepository.GetAllAsync();

			return View(tags);
		}

		[HttpGet]
		[ActionName("Edit")]
		public async Task<IActionResult> Edit(Guid id)
		{
			var tag = await tagRepository.GetAsync(id);

			if (tag != null)
			{
				var editTagRequest = new EditTagRequest
				{
					Id = tag.Id,
					Name = tag.Name,
					DisplayName = tag.DisplayName
				};

				return View(editTagRequest);
			}

			return View(null);
		}

		[HttpPost]
		[ActionName("Edit")]
		public async Task<IActionResult> Edit(EditTagRequest editTagRequest)
		{
			var tag = new Tag
			{
				Id = editTagRequest.Id,
				Name = editTagRequest.Name,
				DisplayName = editTagRequest.DisplayName
			};

			// Find tag
			var updatedTag = await tagRepository.UpdateAsync(tag);

			if (updatedTag != null)
			{
				// Show success notification
				return RedirectToAction("List");
			}
			else
			{
				//Show error notification
			}

			return RedirectToAction("Edit", new { id = editTagRequest.Id });
		}

		[HttpPost]
		[ActionName("Delete")]
		public async Task<IActionResult> Delete(EditTagRequest editTagRequest)
		{
			var deletedTag = await tagRepository.DeleteAsync(editTagRequest.Id);

			if (deletedTag != null)
			{
				// Show success notification
				return RedirectToAction("List");
			}

			// Show error notification
			return RedirectToAction("Edit", new { id = editTagRequest.Id });
		}
	}
}
