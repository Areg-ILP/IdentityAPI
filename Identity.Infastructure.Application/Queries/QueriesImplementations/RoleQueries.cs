using AutoMapper;
using AutoMapper.QueryableExtensions;
using Identity.Domain.RepositoryAbstraction;
using Identity.Infastructure.Application.Models;
using Identity.Infastructure.Application.Models.DetailsModels;
using Identity.Infastructure.Application.Queries.QueriesAbstraction;
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

            var res = new ResultModel<List<RoleDetailsModel>>();

            if (rolesDetailModel != null && rolesDetailModel.Count != 0)
            {
                res.Done(rolesDetailModel);
            }
            else
            {
                res.Failed("Empty page");
            }

            return res;
        }

        public async Task<ResultModel<RoleDetailsModel>> SingleAsync(int id)
        {
            var bookDetailModel = await _roleRepository.Table.ProjectTo<RoleDetailsModel>(_mapper.ConfigurationProvider)
                                                                      .FirstOrDefaultAsync(a => a.Id == id);
            var res = new ResultModel<RoleDetailsModel>();

            if (bookDetailModel != null)
            {
                res.Done(bookDetailModel);
            }
            else
            {
                res.Failed("Role does not exist");
            }

            return res;
        }
    }
}
