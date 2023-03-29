using Microsoft.EntityFrameworkCore;
using SalesWebMVC.Data;
using SalesWebMVC.Models;
using System.Collections.Generic;
using System.Linq;

namespace SalesWebMVC.Services
{
	public class SellerService
	{
		private readonly SalesWebMVCContext _context;

		public SellerService(SalesWebMVCContext context)
		{
			_context = context;
		}

		public List<Seller> FindAll()
		{
			return _context.Seller.ToList();
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
	}
}
