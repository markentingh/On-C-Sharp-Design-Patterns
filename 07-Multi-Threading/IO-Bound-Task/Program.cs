using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace IO_Bound_Task
{
    class Program
    {
        private const string URL = "https://github.com/Datasilk/Saber/";

        static void Main(string[] args)
        {
            DoCalc();
            var download = FileDownloader.DownloadFileAsync(URL);
            DoOtherCalc();
            Console.ReadLine();
        }

        public static void DoCalc()
        {
            Console.WriteLine("1. Calculating synchronously");
        }

        static void DoOtherCalc()
        {
            Console.WriteLine("6. Calculating stuff while downloading...");
        }
    }

    public static class FileDownloader
    {
        public static async Task DownloadFileAsync(string url)
        {
            using (var httpClient = new HttpClient())
            {
                Console.WriteLine("2. Begin downloading file...");
                string result = await httpClient.GetStringAsync(url);
                Console.WriteLine("3. Finished downloading file!");
                Console.WriteLine($"4. Reading length of http Get for {url}");
                Console.WriteLine($"5. {result.Length} character(s)");
            }
        }
    }
}