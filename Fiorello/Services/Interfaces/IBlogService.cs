using System;
using Fiorello.Models;

namespace Fiorello.Services.Interfaces
{
	public interface IBlogService
	{
		Task<Blog> GetAll();
	}
}