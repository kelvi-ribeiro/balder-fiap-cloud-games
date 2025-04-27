using System.Net;
using System.Text.Json.Serialization;

namespace Balder.FiapCloudGames.Application.DTOs.Response;

public class BaseResponse
{
	[JsonIgnore]
	public virtual bool IsSuccessful => Errors is null || Errors.Count == 0;
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
	public virtual List<ErrorInfo>? Errors { get; set; }
	[JsonIgnore]
	public virtual HttpStatusCode StatusCode { get; set; } = HttpStatusCode.OK;
	
	public BaseResponse()
	{
		
	}
	public void AddError(string code, string message, string? field = null)
	{
		Errors ??= [];
		Errors.Add(new ErrorInfo(code, message, field));
	}
}

public record ErrorInfo(
		string Code, 
		string Message, 
		string? Field = null)
{
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
	public string? Field { get; init; } = Field;
}