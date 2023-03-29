using Microsoft.AspNetCore.Http.Connections;
using Microsoft.EntityFrameworkCore;
using SalesWebMVC.Data;
using SalesWebMVC.Models;
using SalesWebMVC.Services.Exceptions;
using System.Collections.Generic;
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

		public async Task InsertAsync(Seller obj)
		{
			_context.Add(obj);
			await _context.SaveChangesAsync();
		}

		public async Task<Seller> FindSellerByIdAsync(int id)
		{
			return await _context.Seller.Include(obj => obj.Department).FirstOrDefaultAsync(x => x.Id == id);
		}

		public async Task DeleteSellerAsync(int id)
		{
			try
			{
				var seller = await _context.Seller.FindAsync(id);
				_context.Remove(seller);
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateException)
			{
				throw new IntegrityException("Can't delete because he/she has sales");
			}
		}

		public async Task UpdateAsync(Seller obj)
		{
			bool hasAny = await _context.Seller.AnyAsync(x => x.Id == obj.Id);
			if (!hasAny)
			{
				throw new NotFoundException("Id not found");
			}
			try
			{
				_context.Update(obj);
				await _context.SaveChangesAsync();
			} 
			catch (DbUpdateConcurrencyException e)
			{
				throw new DbConcurrencyException(e.Message);
			}
		}
	}
}
