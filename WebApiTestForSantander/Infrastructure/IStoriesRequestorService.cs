using System;
using WebApiTestForSantander.Models;

namespace WebApiTestForSantander.Infrastructure
{
	public interface IStoriesRequestorService
	{
        Task<string> GetNumberOfStoriesAsync(int number);
        Task<List<int>> GetCountOfAllStories();
        Task<string> GetNumberOfStoriesParrallel(int number);
    }
}

	