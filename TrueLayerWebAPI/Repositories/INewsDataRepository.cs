using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrueLayerWebAPI.Models;

namespace TrueLayerWebAPI.Repositories
{
    public interface INewsDataRepository
    {
        Task<List<Post>> GetNewsDataAsync(int numberofpost);
        Task<List<Post>> GetAllNewsDataAsync();
    }
}
