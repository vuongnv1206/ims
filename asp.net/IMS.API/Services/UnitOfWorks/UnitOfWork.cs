﻿
using IMS.Api.EnityFrameworkCore;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IMS.Api.Common.UnitOfWorks;

namespace IMS.Api.Services.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IMSDbContext _context;


        private readonly IHttpContextAccessor _httpContextAccessor;

        //private IProductRepository _productRepository;


        public UnitOfWork(IMSDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        //public IProductRepository ProductRepository => _productRepository ?? new ProductRepository();



        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task SaveChangesAsync()
        {
            var username = _httpContextAccessor.HttpContext.User.Identity.Name;

            await _context.SaveChangesAsync(username);
        }
    }
}
