using BraintreeHttp;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PayPal.Core;
using PayPal.v1.Payments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Travel.Data.Interfaces;

namespace TravelApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PayController : Controller
    {
        private readonly string _clientIdPaypal;
        private readonly string _secretKeyPaypal;
        private readonly string _urlSandBoxAPI;
        public double TyGiaUSD = 25000;

        public PayController(IConfiguration config, ITourBooking tourBookingRes)
        {
            _clientIdPaypal = config["PaypalSettings:ClientId"];
            _secretKeyPaypal = config["PaypalSettings:SecretKey"];
            _urlSandBoxAPI = config["PaypalSettings:UrlAPI"];
        }
        //private HttpClient GetPaypalHttpClient()
        //{
        //    var http = new HttpClient
        //    {
        //        BaseAddress = new Uri(_urlSandBoxAPI),
        //        Timeout = TimeSpan.FromSeconds(30)
        //    };
        //    return http;
        //}
        //private async Task<PayPalAccessToken> GetPayPalAccessTokenAsync(HttpClient http)
        //{
        //    byte[] bytes = Encoding.GetEncoding("iso-8859-1").GetBytes($"{_clientIdPaypal}:{_secretKeyPaypal}");
        //    HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "/v1/oauth2/token");
        //    request.Headers.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(bytes));

        //    var form = new Dictionary<string, string>
        //    {
        //        ["grant_type"] = "client_credentials"
        //    };
        //    request.Content = new FormUrlEncodedContent(form);
        //    HttpResponseMessage respone = await http.SendAsync(request);

        //    string content = await respone.Content.ReadAsStringAsync();

        //    PayPalAccessToken accessToken = JsonConvert.DeserializeObject<PayPalAccessToken>(content);
        //    return accessToken;

        //}

        //private async Task<PayPalPaymentCreateResponse> CreatePaypalPaymentAsync(HttpClient http, PayPalAccessToken accessToken, double total,string currency)
        //{
        //    HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "v1/payments/payment");
        //    request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken.access_token);

        //    var payment = JObject.FromObject(new
        //    {
        //        intent = "sale",
        //        redirectUrls = new RedirectUrls()
        //        {
        //            CancelUrl = "https://localhost:4200/Paypal/CheckoutFailed",
        //            ReturnUrl = "https://youtube.com"
        //        },
        //    });
        //}

        [HttpGet]
        [AllowAnonymous]
        [Route("checkout-paypal")]
        public async Task<object> PaypalCheckout(string idTourBooking)
        {
            var environment = new SandboxEnvironment(_clientIdPaypal, _secretKeyPaypal);
            var client = new PayPalHttpClient(environment);

            var itemList = new ItemList()
            {
                Items = new List<Item>()
            };
            var total = Math.Round(1500000/TyGiaUSD,2);
            
            var item = new Item()
            {
                Name = "Tour du lịch",
                Description = "tu tu",
                Currency = "USD",
                Price = total.ToString(),
                Quantity = "1",
                Sku = "sku",
                Tax = "0"
            };
            itemList.Items.Add(item);


            var payment = new Payment()
            {
                Intent = "sale",
                Transactions = new List<Transaction>()
                {
                    new Transaction()
                    {
                        Amount = new Amount()
                        {
                            Total = total.ToString(),
                            Currency = "USD",
                            Details = new AmountDetails
                            {
                                Tax = "0",
                                Shipping = "0",
                                Subtotal = total.ToString()
                            }
                        },
                        ItemList = itemList,
                        Description = "Hàn hóa xịn",
                        InvoiceNumber = "ko ko ko ko "
                            
                    }
                },
                RedirectUrls = new RedirectUrls()
                {
                    CancelUrl = "https://localhost:4200/Paypal/CheckoutFailed",
                    ReturnUrl = "https://youtube.com"
                },
                Payer = new Payer()
                {
                    PaymentMethod = "paypal"
                }   
            };

            PaymentCreateRequest request = new PaymentCreateRequest();
            request.RequestBody(payment);

            try
            {
                var response = await client.Execute(request);
                var statusCode = response.StatusCode;
                Payment result = response.Result<Payment>();

                var links = result.Links.GetEnumerator();
                string paypalRedirectUrl = null;
                string paypalRedirectUrlExec = null;
                string paypalRedirectUrlFinal = null;
                while (links.MoveNext())
                {
                    LinkDescriptionObject lnk = links.Current;
                    if (lnk.Rel.ToLower().Trim().Equals("approval_url"))
                    {
                        paypalRedirectUrl = lnk.Href;
                    }else if (lnk.Rel.ToLower().Trim().Equals("execute"))
                    {
                        paypalRedirectUrlExec = lnk.Href;
                    }
                    else
                    {
                        paypalRedirectUrlFinal = lnk.Href;
                    }
                }
                return new { status = 1, url = paypalRedirectUrl, urlExecute = paypalRedirectUrlExec, urlFinal = paypalRedirectUrlFinal };
            }
            catch (HttpException httpException)
            {
                var statusCode = httpException.StatusCode;
                var debugId = httpException.Headers.GetValues("PayPal-Debug-Id").FirstOrDefault();

                return new { status = 1, url = payment.RedirectUrls.CancelUrl , DebugId = debugId, StatusCode= statusCode};

            }
        }


    }
}
