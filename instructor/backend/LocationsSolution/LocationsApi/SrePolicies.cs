using Polly;
using Polly.Extensions.Http;

namespace LocationsApi;

public static  class SrePolicies
{
    // Retry policy - 
    public static IAsyncPolicy<HttpResponseMessage> GetDefaultRetyPolicyAsync()
    {
        return HttpPolicyExtensions
            .HandleTransientHttpError()
            .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.NotFound)
            .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));
    }

    // Circuit Breaker - QUIT CALLING FOR A WHILE.
    public static IAsyncPolicy<HttpResponseMessage> GetDefaultCircuitBreaker()
    {
        return HttpPolicyExtensions
            .HandleTransientHttpError()
            .CircuitBreakerAsync(2, TimeSpan.FromSeconds(30));
    }

}
