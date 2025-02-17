using System.Net;

namespace AntiPhishingAPI.SerVices.ServiceInterfaces;

public interface ICheckStatus
{
    Task<HttpStatusCode>  GetStatus(string link);
}