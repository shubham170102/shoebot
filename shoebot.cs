using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace ConsoleApp {
    class Program 
    {
        static async Task Main(string[] args) 
        {
            Find();
        }

        public static async void Find()
        {
            uri site = new uri('https://www.socialstatuspgh.com/collections/nike/products.json?limit=50');
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(site);
        }

        async void Buy(string link1) 
        {

        }
    }

}