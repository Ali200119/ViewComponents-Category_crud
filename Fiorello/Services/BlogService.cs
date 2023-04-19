using System;
using Fiorello.Data;
using Fiorello.Models;
using Fiorello.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Fiorello.Services
{
    public class BlogService : IBlogService
    {
        private readonly AppDbContext _context;

        public BlogService(AppDbContext context)
        {
            _context = context;
        }



        public async Task<Blog> GetAll() => await _context.Blogs.Include(b => b.BlogPosts).FirstOrDefaultAsync();
    }
}