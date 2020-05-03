using SuperDogApp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SuperDogApp.Services
{
    public interface IMapService
    {
        Task<SearchResultModel> GetTextSearch(SearchModel model);
    }
}
