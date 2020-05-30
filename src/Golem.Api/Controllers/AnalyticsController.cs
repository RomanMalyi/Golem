﻿using System;
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
        private readonly SessionRepository sessionRepository;
        private readonly IMapper mapper;

        public AnalyticsController(UserRepository userRepository,
            QueryRepository queryRepository,
            IMapper mapper,
            AnalyticsService analyticsService,
            SessionRepository sessionRepository)
        {
            this.userRepository = userRepository;
            this.queryRepository = queryRepository;
            this.mapper = mapper;
            this.analyticsService = analyticsService;
            this.sessionRepository = sessionRepository;
        }

        /// <summary>
        ///     Returns users' info
        /// </summary>
        [HttpGet("users")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetUsers(
            [FromQuery] DateTime? lastVisitDateFrom,
            [FromQuery] DateTime? lastVisitDateTo,
            [FromQuery] int take = 20,
            [FromQuery] int skip = 0)
        {
            var users = await userRepository.Get(lastVisitDateFrom, lastVisitDateTo, skip, take);
            var result = new
            {
                users = mapper.Map<IEnumerable<User>, IEnumerable<UserResponse>>(users),
                totalCount = await userRepository.GetCount(lastVisitDateFrom, lastVisitDateTo)
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
            [FromQuery] DateTime? creationDateFrom,
            [FromQuery] DateTime? creationDateTo,
            [FromQuery] bool showEmpty,
            [FromQuery] int take = 20,
            [FromQuery] int skip = 0)
        {
            var queries =
                await queryRepository.GetByUserId(userId, creationDateFrom, creationDateTo, showEmpty, skip, take);
            var result = new
            {
                queries = mapper.Map<IEnumerable<Query>, IEnumerable<QueryResponse>>(queries),
                totalCount = await queryRepository.GetCount(userId, creationDateFrom, creationDateTo, showEmpty)
            };
            return Ok(result);
        }

        /// <summary>
        ///     Returns users' sessions
        /// </summary>
        [HttpGet("users/{userId:guid}/sessions")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetUserSessions(
            [FromRoute] Guid userId,
            [FromQuery] DateTime? startDateFrom,
            [FromQuery] DateTime? startDateTo,
            [FromQuery] int take = 20,
            [FromQuery] int skip = 0)
        {
            var sessions = await sessionRepository.GetByUserId(userId, startDateFrom, startDateTo, skip, take);
            var result = new
            {
                sessions = mapper.Map<IEnumerable<Session>, IEnumerable<SessionResponse>>(sessions),
                totalCount = await sessionRepository.GetCount(userId, startDateFrom, startDateTo)
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
