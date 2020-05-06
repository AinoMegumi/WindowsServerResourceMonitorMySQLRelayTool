using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WindowsServerResourceMonitorMySQLRelayTool
{
    internal static class AsyncHelper
    {
        private static readonly TaskFactory _myTaskFactory = new TaskFactory(CancellationToken.None, TaskCreationOptions.None, TaskContinuationOptions.None, TaskScheduler.Default);

        public static TResult RunSync<TResult>(Func<Task<TResult>> func)
        {
            return _myTaskFactory.StartNew<Task<TResult>>(func).Unwrap<TResult>().GetAwaiter().GetResult();
        }

        public static void RunSync(Func<Task> func)
        {
            _myTaskFactory.StartNew<Task>(func).Unwrap().GetAwaiter().GetResult();
        }
    }
    class RequestManager
    {
        private HttpClient client;
        private int LastStatusCode;
        public RequestManager() { client = new HttpClient(); }

        private async Task<string> RequestSender(string URL, string Request, HttpMethod method)
        {
            HttpResponseMessage res = await client.SendAsync(
                new HttpRequestMessage(method, URL)
                {
                    Content = new StringContent(Request, Encoding.UTF8, @"application/json")
                }
            );
            string resContent = await res.Content.ReadAsStringAsync();
            LastStatusCode = (int)res.StatusCode;
            return string.IsNullOrEmpty(resContent) ? "" : resContent;
        }

        public async Task<string> RequestSender(string URL)
        {
            HttpResponseMessage res = await client.GetAsync(URL);
            string resContent = await res.Content.ReadAsStringAsync();
            LastStatusCode = (int)res.StatusCode;
            return string.IsNullOrEmpty(resContent) ? "" : resContent;
        }

        public string Get(string URL)
        {
            return AsyncHelper.RunSync(() => RequestSender(URL));
        }

        public string Post(string URL, string Request = "")
        {
            return AsyncHelper.RunSync(() => RequestSender(URL, Request, HttpMethod.Post));
        }

        public string Delete(string URL, string Request = "")
        {
            return AsyncHelper.RunSync(() => RequestSender(URL, Request, HttpMethod.Delete));
        }

        public int GetLastStatusCode() { return LastStatusCode; }
    }
}
