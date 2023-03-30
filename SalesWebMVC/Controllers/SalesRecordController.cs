﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using SalesWebMVC.Models;
using SalesWebMVC.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SalesWebMVC.Controllers
{
	public class SalesRecordController : Controller
	{
		private readonly SalesRecordService _salesRecordService;

		public SalesRecordController(SalesRecordService salesRecordService) 
		{ 
			_salesRecordService = salesRecordService;
		}

		public IActionResult Index()
		{
			return View();
		}

		public async Task<IActionResult> SimpleSearch(DateTime? minDate, DateTime? maxDate)
		{
			if (!minDate.HasValue)
			{
				minDate = new DateTime(DateTime.Now.Year, 1, 1);
			}

			if (!maxDate.HasValue)
			{
				maxDate = DateTime.Now;
			}
			ViewData["minDate"] = minDate.Value.ToString("yyyy-MM-dd");
			ViewData["maxDate"] = maxDate.Value.ToString("yyyy-MM-dd");
			var list = await _salesRecordService.FindByDateAsync(minDate, maxDate);
			return View(list);
		}

		public IActionResult GroupingSearch()
		{
			return View();
		}
	}
}
