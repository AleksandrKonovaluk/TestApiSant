using System;
using System.Collections.Generic;
using System.Text.Json;
using WebApiTestForSantander.Infrastructure.RequestModels;
using WebApiTestForSantander.Models;

namespace WebApiTestForSantander.Infrastructure
{
	public class StoriesRequestorService : IStoriesRequestorService
    {
        private const string BASIC_URL = "baseUrl";
        private const string DETAILS_URL = "detalilUrl";

        private readonly IConfiguration _configuration;

        public StoriesRequestorService(IConfiguration configuration1)
		{
            _configuration = configuration1;
		}


        public async Task<List<int>> GetCountOfAllStories()
        {

            try
            {
                string url = _configuration.GetValue<string>("baseUrl");

                HttpClient client = new HttpClient();
                var result = await client.GetFromJsonAsync<List<int>>(url);

                return result;
            }
            catch (Exception ex)
            {
                throw new NotImplementedException();
            }
            
        }

        public async Task<string> GetNumberOfStoriesAsync(int number)
        {
            string detailsUrl  = _configuration.GetValue<string>(DETAILS_URL);
            string baseListOfStoresUrl = _configuration.GetValue<string>(BASIC_URL);

            try
            {
                HttpClient client = new HttpClient();
                List<StoryResponceModel> storyModels = new List<StoryResponceModel>();
                var result = await client.GetFromJsonAsync<List<int>>(baseListOfStoresUrl);

                for (int i = 0; i < number; i++)
                {
                    var e = string.Format(detailsUrl, result[i]);
                    var detailsItemRequestResult = await client.GetStringAsync(e);

                    StoryRequestModel story = JsonSerializer.Deserialize<StoryRequestModel>(detailsItemRequestResult);
                    StoryResponceModel storyResponce = new StoryResponceModel()
                    {
                        Title = story.Title,
                        Uri = story.Url,
                        PostedBy = story.By,
                        //Time = story.
                        Score = story.Score,
                        CommentCount = story.Descendants
                    };
                    storyModels.Add(storyResponce);
                   
                }
                var sortedList = storyModels.OrderByDescending(itm => itm.Score).ToList();
                var m = JsonSerializer.Serialize<List<StoryResponceModel>>(sortedList);
                return m;
                    
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public async Task<string> GetNumberOfStoriesParrallel(int number)
        {
            string detailsUrl = _configuration.GetValue<string>(DETAILS_URL);
            string baseListOfStoresUrl = _configuration.GetValue<string>(BASIC_URL);

            HttpClient client = new HttpClient();
            List<StoryResponceModel> storyModels = new List<StoryResponceModel>();
            var result = await client.GetFromJsonAsync<List<int>>(baseListOfStoresUrl);
            var u = result.Take(number).ToList();

            await Parallel.ForEachAsync(u , async (item, cancellationToken) =>
            { 
                HttpClient detailsClient = new HttpClient();
                var e = string.Format(detailsUrl, item);
                var detailsItemRequestResult = await detailsClient.GetStringAsync(e);

                StoryRequestModel story = JsonSerializer.Deserialize<StoryRequestModel>(detailsItemRequestResult);
                StoryResponceModel storyResponce = new StoryResponceModel()
                {
                    Title = story.Title,
                    Uri = story.Url,
                    PostedBy = story.By,
                    //Time = story.
                    Score = story.Score,
                    CommentCount = story.Descendants
                };
                storyModels.Add(storyResponce);
            }).ConfigureAwait(true);

            var sortedList = storyModels.OrderByDescending(itm => itm.Score).ToList();
            var m = JsonSerializer.Serialize<List<StoryResponceModel>>(sortedList);
            return m;
        }
    }
}

