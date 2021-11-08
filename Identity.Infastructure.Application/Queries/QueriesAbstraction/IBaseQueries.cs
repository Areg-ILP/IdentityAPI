using Identity.Infastructure.Application.Models;
using Identity.Infastructure.Application.Models.DetailsModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Identity.Infastructure.Application.Queries.QueriesAbstraction
{
    public interface IBaseQueries<T> where T : BaseDetailsModel
    {
        Task<ResultModel<T>> SingleAsync(int id);
        Task<ResultModel<List<T>>> GetAllAsync(PaginationModel paginationModel);
    }
}
