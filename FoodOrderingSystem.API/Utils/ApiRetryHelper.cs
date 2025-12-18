using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace FoodOrderingSystem.API.Utils
{
    /// <summary>
    /// Provides a helper method to execute an operation with a retry policy for handling rate limiting errors (HTTP 429).
    /// Implements exponential backoff to increase the delay between retries.
    /// </summary>
    public static class ApiRetryHelper
    {
        /// <summary>
        /// Executes a function and retries it with exponential backoff if a rate limiting error is detected.
        /// </summary>
        /// <typeparam name="T">The return type of the function to execute.</typeparam>
        /// <param name="operation">The asynchronous function to execute.</param>
        /// <param name="maxRetries">The maximum number of retries.</param>
        /// <returns>The result of the successful operation.</returns>
        public static async Task<T> ExecuteWithRetryAsync<T>(Func<Task<T>> operation, int maxRetries = 5)
        {
            int backoffDelay = 1000; // start with 1 second
            var random = new Random();

            for (int i = 0; i < maxRetries; i++)
            {
                try
                {
                    return await operation();
                }
                catch (Exception ex) when (IsRateLimitingException(ex))
                {
                    if (i == maxRetries - 1)
                    {
                        // Max retries reached, rethrow the exception.
                        throw;
                    }

                    // Add a random jitter to the delay to avoid a "thundering herd" scenario.
                    int jitter = random.Next(0, 1000);
                    await Task.Delay(backoffDelay + jitter);

                    // Increase the backoff delay for the next retry.
                    backoffDelay *= 2;
                }
            }
            
            // This line should not be reachable. It's for compiler satisfaction.
            throw new InvalidOperationException("This code path should be unreachable.");
        }

        /// <summary>
        /// Inspects an exception to determine if it is related to rate limiting (HTTP 429).
        /// This method may need to be customized based on the specific exceptions thrown by your API client library.
        /// </summary>
        private static bool IsRateLimitingException(Exception ex)
        {
            // Case 1: For modern HttpClient, check for HttpRequestException with a 429 status code.
            if (ex is HttpRequestException httpEx && httpEx.StatusCode == HttpStatusCode.TooManyRequests)
            {
                return true;
            }

            // Case 2: For older HttpWebRequest, check for WebException.
            if (ex is WebException webEx && webEx.Response is HttpWebResponse response && response.StatusCode == HttpStatusCode.TooManyRequests)
            {
                return true;
            }

            // Case 3: If you use a Google Cloud client library, the exception is likely Google.GoogleApiException.
            // You would need to add a reference to the Google API client library and check the type.
            // Example:
            // if (ex.GetType().FullName == "Google.GoogleApiException")
            // {
            //     dynamic apiEx = ex;
            //     if (apiEx.Error != null && apiEx.Error.Code == 429)
            //     {
            //         return true;
            //     }
            // }

            // Case 4: Fallback to checking the exception message. This is less reliable but can be a good catch-all.
            string message = ex.ToString();
            return message.Contains("429") || message.Contains("Resource exhausted") || message.Contains("rateLimitExceeded");
        }
    }
}
