﻿using System;
using Fiorello.Models;

namespace Fiorello.Services.Interfaces
{
	public interface IExpertService
	{
		Task<Expert> GetAll();
	}
}