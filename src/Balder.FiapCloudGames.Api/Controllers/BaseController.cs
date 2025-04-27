using System.Net;
using Balder.FiapCloudGames.Application.DTOs.Response;
using Microsoft.AspNetCore.Mvc;

namespace Balder.FiapCloudGames.Api.Controllers;

public class BaseController : Controller
{
		public async Task<IActionResult> MakeSafeCallAsync<TResponse>(
			Func<Task<TResponse>> serviceMethod)
			where TResponse : BaseResponse, new()
		{
			try
			{
				TResponse response = await serviceMethod();				
				return this.StatusCode((int)response.StatusCode, response);
			}
			catch (Exception)
			{
				return this.StatusCode((int)HttpStatusCode.InternalServerError, null);
			}
		}
}