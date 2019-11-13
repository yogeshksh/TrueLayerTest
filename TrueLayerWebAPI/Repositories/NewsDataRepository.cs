using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrueLayerWebAPI.Models;

namespace TrueLayerWebAPI.Repositories
{
    /// <summary>
    /// 
    /// </summary>
    public class NewsDataRepository : INewsDataRepository
    {   
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<List<Post>> GetAllNewsDataAsync()
        {
            return await Task.FromResult(HackerNewsDataProvider.GetData(1000));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="numberofpost"></param>
        /// <returns></returns>
        public async Task<List<Post>> GetNewsDataAsync(int numberofpost)
        {
            return await Task.FromResult(HackerNewsDataProvider.GetData(numberofpost));
        }
    }
}
