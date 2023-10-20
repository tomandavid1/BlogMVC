using Blog.Web.Models.Domain;

namespace Blog.Web.Repositories
{
	public interface ITagRepository
	{
		// Get All Tags Async
		Task<IEnumerable<Tag>> GetAllAsync();
		// Get Single Tag Async
		Task<Tag?> GetAsync(Guid id);
		// Add Single Tag Async
		Task<Tag> AddAsync(Tag tag);
		// Add Single Tag Async
		Task<Tag?> UpdateAsync(Tag tag);
		// Delete Single Tag Async
		Task<Tag?> DeleteAsync(Guid id);

	}
}
