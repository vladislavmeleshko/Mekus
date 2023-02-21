using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Net.Http.Json;

namespace Mekus.belarusneft
{
    public class Test_1
    {
        public string startDate { get; set; }
        public string endDate { get; set; }
        public int cardNumber { get; set; }
        public int subDivisnNumber { get; set; }
        public int flChoice { get; set; }
    }

    public class EnterAPI
    {
        public string access_token { get; set; }
        public int expires_in { get; set; }
        public string token_type { get; set; }
        public string scope { get; set; }
    }

    public class API
    {
        public Cardlist[] cardList { get; set; }
        public Total total { get; set; }
    }

    public class Total
    {
        public int subdivision { get; set; }
        public DateTime dateTimeIssue { get; set; }
        public int productType { get; set; }
        public int productCode { get; set; }
        public object productName { get; set; }
        public float productQuantity { get; set; }
        public object productMeasure { get; set; }
        public float productUnitPrice { get; set; }
        public float productCost { get; set; }
        public float vatAmount { get; set; }
        public float discountWithVAT { get; set; }
        public float percentageForServices { get; set; }
        public float costServiceWithVAT { get; set; }
        public float costTotalWithVAT { get; set; }
        public float vatAmountTotal { get; set; }
        public object ownerName { get; set; }
        public int azsNumber { get; set; }
        public int trkNumber { get; set; }
        public int operCode { get; set; }
        public object carNum { get; set; }
        public object driverName { get; set; }
        public int docNumber { get; set; }
        public int emtCodeFrm { get; set; }
    }

    public class Cardlist
    {
        public string cardNumber { get; set; }
        public Issuerow[] issueRows { get; set; }
        public Total1 total { get; set; }
        public float totalOil { get; set; }
        public float totalGas { get; set; }
        public float totalOilGas { get; set; }
        public float totalProduct { get; set; }
        public float totalService { get; set; }
        public float totalProductService { get; set; }
        public float totalRoad { get; set; }
        public float totalQuantity { get; set; }
        public float totalWithVATWithDiscountOilGas { get; set; }
        public float totalDiscountWithVATOilGas { get; set; }
        public float totalWithVATWithDiscountProductService { get; set; }
        public float totalDiscountWithVATProductService { get; set; }
        public float totalVATOilGas { get; set; }
        public float totalVATProductService { get; set; }
        public float totalWithVATOilGas { get; set; }
        public float totalWithVATProductService { get; set; }
        public float totalTotalVATOilGas { get; set; }
        public float totalTotalVATProductService { get; set; }
    }

    public class Total1
    {
        public int subdivision { get; set; }
        public DateTime dateTimeIssue { get; set; }
        public int productType { get; set; }
        public int productCode { get; set; }
        public string productName { get; set; }
        public float productQuantity { get; set; }
        public object productMeasure { get; set; }
        public float productUnitPrice { get; set; }
        public float productCost { get; set; }
        public float vatAmount { get; set; }
        public float discountWithVAT { get; set; }
        public float percentageForServices { get; set; }
        public float costServiceWithVAT { get; set; }
        public float costTotalWithVAT { get; set; }
        public float vatAmountTotal { get; set; }
        public object ownerName { get; set; }
        public int azsNumber { get; set; }
        public int trkNumber { get; set; }
        public int operCode { get; set; }
        public object carNum { get; set; }
        public object driverName { get; set; }
        public int docNumber { get; set; }
        public int emtCodeFrm { get; set; }
    }

    public class Issuerow
    {
        public int subdivision { get; set; }
        public DateTime dateTimeIssue { get; set; }
        public int productType { get; set; }
        public int productCode { get; set; }
        public string productName { get; set; }
        public float productQuantity { get; set; }
        public string productMeasure { get; set; }
        public float productUnitPrice { get; set; }
        public float productCost { get; set; }
        public float vatAmount { get; set; }
        public float discountWithVAT { get; set; }
        public float percentageForServices { get; set; }
        public float costServiceWithVAT { get; set; }
        public float costTotalWithVAT { get; set; }
        public float vatAmountTotal { get; set; }
        public string ownerName { get; set; }
        public int azsNumber { get; set; }
        public int trkNumber { get; set; }
        public int operCode { get; set; }
        public string carNum { get; set; }
        public string driverName { get; set; }
        public int docNumber { get; set; }
        public int emtCodeFrm { get; set; }
    }

    public class Belarusneft
    {
        public Belarusneft()
        {

        }

        public static async Task<EnterAPI> auth(string url)
        {
            IEnumerable<KeyValuePair<string, string>> queries = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("grant_type", "password"),
                new KeyValuePair<string, string>("client_id", "rcp.web"),
                new KeyValuePair<string, string>("client_secret", "secret"),
                new KeyValuePair<string, string>("scope", "rcp.api"),
                new KeyValuePair<string, string>("username", "650.6404319"),
                new KeyValuePair<string, string>("password", "De122020")
            };

            HttpContent q = new FormUrlEncodedContent(queries);

            using (HttpClient client = new HttpClient())
            {
                using (HttpResponseMessage response = await client.PostAsync(url, q))
                {
                    using (HttpContent content = response.Content)
                    {
                        string mycontent = await content.ReadAsStringAsync();
                        Console.WriteLine(mycontent);

                        EnterAPI enter = new EnterAPI();
                        enter = JsonSerializer.Deserialize<EnterAPI>(mycontent);

                        return enter;
                    }
                }
            }
        }

        public static async Task<API> getAPI(string url, string token, Test_1 test_1)
        {

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

                JsonContent content = JsonContent.Create(test_1);

                var response = await client.PostAsync(url, content);

                API check = await response.Content.ReadFromJsonAsync<API>();

                return check;
            }
        }
    }
}
