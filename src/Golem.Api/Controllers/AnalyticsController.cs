using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Golem.Core.Models.Dto.Responses;
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
        private readonly UserRepository userRepository;
        private readonly QueryRepository queryRepository;
        private readonly IMapper mapper;

        public AnalyticsController(UserRepository userRepository,
            QueryRepository queryRepository,
            IMapper mapper)
        {
            this.userRepository = userRepository;
            this.queryRepository = queryRepository;
            this.mapper = mapper;
        }

        /// <summary>
        ///     Returns users' info
        /// </summary>
        [HttpGet("users")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<UserResponse>>> GetUsers(
            [FromQuery] int take = 20,
            [FromQuery] int skip = 0)
        {
            var result = await userRepository.Get(skip, take);
            return Ok(mapper.Map<IEnumerable<User>, IEnumerable<UserResponse>>(result));
        }
        
        /// <summary>
        ///     Returns users' queries
        /// </summary>
        [HttpGet("users/{userId:guid}/queries")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<QueryResponse>>> GetUserQueries(
            [FromRoute] Guid userId,
            [FromQuery] int take = 20,
            [FromQuery] int skip = 0)
        {
            var result = await queryRepository.GetByUserId(userId, skip, take);
            return Ok(mapper.Map<IEnumerable<Query>, IEnumerable<QueryResponse>>(result));
        }
    }
}
