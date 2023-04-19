using System;
using Fiorello.Models;

namespace Fiorello.Services.Interfaces
{
	public interface ISliderService
	{
		Task<IEnumerable<Slider>> GetAll();
		Task<SliderInfo> GetInfo();
	}
}