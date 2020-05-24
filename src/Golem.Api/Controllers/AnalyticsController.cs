using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Golem.Core.Models.Dto.Responses;
using Golem.Core.Services;
using Golem.Data.PostgreSql.Models;
using Golem.Data.PostgreSql.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Golem.Api.Controllers
{
    [ApiController]
    [Route("api/analytics")]
    public class AnalyticsController : ControllerBase
    {
        private readonly AnalyticsService analyticsService;
        private readonly UserRepository userRepository;
        private readonly QueryRepository queryRepository;
        private readonly IMapper mapper;

        public AnalyticsController(UserRepository userRepository,
            QueryRepository queryRepository,
            IMapper mapper,
            AnalyticsService analyticsService)
        {
            this.userRepository = userRepository;
            this.queryRepository = queryRepository;
            this.mapper = mapper;
            this.analyticsService = analyticsService;
        }

        /// <summary>
        ///     Returns users' info
        /// </summary>
        [HttpGet("users")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetUsers(
            [FromQuery] int take = 20,
            [FromQuery] int skip = 0)
        {
            var users = await userRepository.Get(skip, take);
            var result = new
            {
                users = mapper.Map<IEnumerable<User>, IEnumerable<UserResponse>>(users),
                totalCount = await userRepository.GetCount()
            };
            return Ok(result);
        }

        /// <summary>
        ///     Returns users' queries
        /// </summary>
        [HttpGet("users/{userId:guid}/queries")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetUserQueries(
            [FromRoute] Guid userId,
            [FromQuery] int take = 20,
            [FromQuery] int skip = 0)
        {
            var queries = await queryRepository.GetByUserId(userId, skip, take);
            var result = new
            {
                queries = mapper.Map<IEnumerable<Query>, IEnumerable<QueryResponse>>(queries),
                totalCount = await queryRepository.GetCount(userId)
            };
            return Ok(result);
        }

        /// <summary>
        ///     Returns dashboard overview
        /// </summary>
        [HttpGet("dashboard-overview")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<DashboardOverviewResponse>> GetDashboardOverview()
        {
            var result = await analyticsService.GetDashboardOverview();
            return Ok(result);
        }
    }
}
