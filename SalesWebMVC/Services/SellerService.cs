﻿using Microsoft.EntityFrameworkCore;
using SalesWebMVC.Data;
using SalesWebMVC.Models;
using SalesWebMVC.Services.Exceptions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWebMVC.Services
{
	public class SellerService
	{
		private readonly SalesWebMVCContext _context;

		public SellerService(SalesWebMVCContext context)
		{
			_context = context;
		}

		public async Task<List<Seller>> FindAllAsync()
		{
			return await _context.Seller.ToListAsync();
		}

		public void Insert(Seller obj)
		{
			_context.Add(obj);
			_context.SaveChanges();
		}

		public Seller FindSellerById(int id)
		{
			return _context.Seller.Include(obj => obj.Department).FirstOrDefault(x => x.Id == id);
		}

		public void DeleteSeller(int id)
		{
			var seller = _context.Seller.Find(id);
			_context.Remove(seller);
			_context.SaveChanges();
		}

		public void Update(Seller obj)
		{
			if (!_context.Seller.Any(x => x.Id == obj.Id))
			{
				throw new NotFoundException("Id not found");
			}
			try
			{
				_context.Update(obj);
				_context.SaveChanges();
			} 
			catch (DbUpdateConcurrencyException e)
			{
				throw new DbConcurrencyException(e.Message);
			}
		}
	}
}
