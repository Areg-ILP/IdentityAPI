using AutoMapper;
using AutoMapper.QueryableExtensions;
using Identity.Domain.RepositoryAbstraction;
using Identity.Infastructure.Application.Models;
using Identity.Infastructure.Application.Models.DetailsModels;
using Identity.Infastructure.Application.Queries.QueriesAbstraction;
using Identity.Infastructure.Application.Utilities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Infastructure.Application.Queries.QueriesImplementations
{
    public class RoleQueries : IBaseQueries<RoleDetailsModel>, IRoleQueries
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;

        public RoleQueries(IRoleRepository roleRepository, IMapper mapper)
        {
            _roleRepository = roleRepository;
            _mapper = mapper;
        }

        public async Task<ResultModel<List<RoleDetailsModel>>> GetAllAsync(PaginationModel paginationModel)
        {
            var rolesDetailModel = await _roleRepository.Table.ProjectTo<RoleDetailsModel>(_mapper.ConfigurationProvider)
                                                                       .Skip((paginationModel.PageNumber - 1) * paginationModel.PageSize)
                                                                       .Take(paginationModel.PageSize)
                                                                       .ToListAsync();

            if (rolesDetailModel != null && rolesDetailModel.Count != 0)
            {
                return ResultModel<List<RoleDetailsModel>>.Done(rolesDetailModel);
            }

            return ResultModel<List<RoleDetailsModel>>.Failed(CustomErrorMessage.NoData);
        }

        public async Task<ResultModel<RoleDetailsModel>> SingleAsync(int id)
        {
            var bookDetailModel = await _roleRepository.Table.ProjectTo<RoleDetailsModel>(_mapper.ConfigurationProvider)
                                                                      .FirstOrDefaultAsync(a => a.Id == id);
            
            if (bookDetailModel != null)
            {
                return ResultModel<RoleDetailsModel>.Done(bookDetailModel);
            }
            
            return ResultModel<RoleDetailsModel>.Failed(CustomErrorMessage.RoleDoesntExist);
        }
    }
}
