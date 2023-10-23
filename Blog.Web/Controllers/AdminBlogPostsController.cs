using Blog.Web.Models.Domain;
using Blog.Web.Models.ViewModels;
using Blog.Web.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Blog.Web.Controllers
{
	public class AdminBlogPostsController : Controller
	{

		private readonly ITagRepository tagRepository;
		private readonly IBlogPostRepository blogPostRepository;

		// Injection of tagrepository
		public AdminBlogPostsController(ITagRepository tagRepository, IBlogPostRepository blogPostRepository)
		{
			this.tagRepository = tagRepository;
			this.blogPostRepository = blogPostRepository;
		}

		// Get method for displaying available tags on page

		[HttpGet]
		public async Task<IActionResult> AddAsync()
		{
			// Get Tags from repository as list

			var tags = await tagRepository.GetAllAsync();

			var model = new AddBlogPostRequest
			{
				Tags = tags.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() })
			};

			return View(model);
		}

		// Post method for saving to database

		[HttpPost]
		public async Task<IActionResult> AddAsync(AddBlogPostRequest addBlogPostRequest)
		{
			// Map view model to domain model

			var blogPost = new BlogPost
			{
				Heading = addBlogPostRequest.Heading,
				PageTitle = addBlogPostRequest.PageTitle,
				Content = addBlogPostRequest.Content,
				ShortDescription = addBlogPostRequest.ShortDescription,
				FeaturedImageUrl = addBlogPostRequest.FeaturedImageUrl,
				UrlHandle = addBlogPostRequest.UrlHandle,
				PublishedDate = addBlogPostRequest.PublishedDate,
				Author = addBlogPostRequest.Author,
				IsVisible = addBlogPostRequest.IsVisible,

			};

			// Map Tags from selected tags
			var selectedTags = new List<Tag>();
			foreach (var selectedTagId in addBlogPostRequest.SelectedTags)
			{
				var selectedTagIdAsGuid = Guid.Parse(selectedTagId);
				var existingTag = await tagRepository.GetAsync(selectedTagIdAsGuid);

				if (existingTag != null)
				{
					selectedTags.Add(existingTag);
				}
			}
			// Mapping tags to domain model after looping
			blogPost.Tags = selectedTags;

			await blogPostRepository.AddAsync(blogPost);

			return RedirectToAction("Add");
		}
	}
}
