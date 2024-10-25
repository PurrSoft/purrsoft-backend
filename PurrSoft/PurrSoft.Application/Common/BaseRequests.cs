using MediatR;
using System.Text.Json.Serialization;
using PurrSoft.Common.Identity;

namespace PurrSoft.Application.Common;

public class BaseRequest<T> : IRequest<T>
{
    [JsonIgnore]
    public CurrentUser? User { get; set; }
}