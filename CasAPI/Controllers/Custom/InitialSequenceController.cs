﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EntityCore.DTO;
using EntityCore.DTO.General;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CasAPI.Controllers.Custom
{
	[Route("initialequence")]
	public class InitialSequenceController : ControllerBase
	{
		private readonly ILogger<InitialSequenceController> _logger;
		private readonly DataContext _context;

		public InitialSequenceController(ILogger<InitialSequenceController> logger, DataContext context)
		{
			_logger = logger;
			_context = context;
		}

		[HttpGet]
		public async Task<ActionResult<int>> GetNextSequence()
		{
			try
			{
				var res = _context.ObtainId();
				return Ok(res);
			}
			catch (Exception e)
			{
				_logger.LogError(e.Message);
				return BadRequest();
			}
		}
	}
}