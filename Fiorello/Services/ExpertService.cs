﻿using System;
using System.Threading.Tasks;
using Fiorello.Data;
using Fiorello.Models;
using Fiorello.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Fiorello.Services
{
	public class ExpertService: IExpertService
	{
        private readonly AppDbContext _context;

        public ExpertService(AppDbContext context)
        {
            _context = context;
        }



        public async Task<Expert> GetAll()
        {
            return await _context.Experts.Include(e => e.Persons).FirstOrDefaultAsync();
        }
    }
}