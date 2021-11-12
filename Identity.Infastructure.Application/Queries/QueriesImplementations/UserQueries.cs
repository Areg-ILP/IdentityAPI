using AutoMapper;
using AutoMapper.QueryableExtensions;
using Identity.Domain.RepositoryAbstraction;
using Identity.Infastructure.Application.Models;
using Identity.Infastructure.Application.Models.DetailsModels;
using Identity.Infastructure.Application.Queries.QueriesAbstraction;
using Identity.Infastructure.Application.Utilities.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.Infastructure.Application.Queries.QueriesImplementations
{
    public class UserQueries : IBaseQueries<UserDetailsModel>, IUserQueries
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserQueries(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<ResultModel<List<UserDetailsModel>>> GetAllAsync(PaginationModel paginationModel)
        {
            var usersDetailModel = await _userRepository.Table.ProjectTo<UserDetailsModel>(_mapper.ConfigurationProvider)
                                                                       .Skip((paginationModel.PageNumber - 1) * paginationModel.PageSize)
                                                                       .Take(paginationModel.PageSize)
                                                                       .ToListAsync();

            if (usersDetailModel != null && usersDetailModel.Count != 0)
            {
                return ResultModel<List<UserDetailsModel>>.Done(usersDetailModel);
            }

            return ResultModel<List<UserDetailsModel>>.NoData(CustomErrorMessage.NoData);
        }

        public async Task<ResultModel<UserDetailsModel>> SingleAsync(int id)
        {
            var userDetailModel = await _userRepository.Table.ProjectTo<UserDetailsModel>(_mapper.ConfigurationProvider)
                                                                      .FirstOrDefaultAsync(a => a.Id == id);

            if (userDetailModel != null)
            {
                return ResultModel<UserDetailsModel>.Done(userDetailModel);
            }

            return ResultModel<UserDetailsModel>.NoData(CustomErrorMessage.UserDoesntExist);
        }
    }
}
