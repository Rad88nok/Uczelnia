using System;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace cw1
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            if (args.Length == 0)
            {
                throw new ArgumentNullException();
            }
            else if (Regex.IsMatch(args[0], @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase) != true)
            {
                throw new ArgumentException();
            }

            string url = args[0];
            using (var httpClient = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await httpClient.GetAsync(url);

                    if (response.IsSuccessStatusCode)//200
                    {
                        string content = await response.Content.ReadAsStringAsync();
                        //Regex
                        Regex emailRegex = new Regex(@"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", RegexOptions.IgnoreCase);
                        MatchCollection emailMatches = emailRegex.Matches(content);
                        StringBuilder sb = new StringBuilder();
                        foreach (Match emailMatch in emailMatches)
                        {
                            sb.AppendLine(emailMatch.Value);
                        }
                        if (sb.Length == 0)
                        {
                            Console.WriteLine("Nie znaleziono adresów email");
                        }
                        Console.WriteLine(sb);
                    }
                }
                catch (HttpRequestException exception)
                {
                    System.Diagnostics.Debug.WriteLine("Błąd w czasie pobierania strony");
                    System.Diagnostics.Debug.WriteLine(exception);
                }

            }

            Console.WriteLine("Hello World!");
        }
    }
}