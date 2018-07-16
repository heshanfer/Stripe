using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace StripeTest.Controllers
{
    public class StripeController : Controller
    {
        private static readonly HttpClient client = new HttpClient();

        // GET: Stripe
        public async System.Threading.Tasks.Task<ActionResult> Index()
        {
            var CLIENT_ID = "";
            var API_KEY = "";
            var TOKEN_URI = "https://connect.stripe.com/oauth/token";
            var AUTHORIZE_URI = "https://connect.stripe.com/oauth/authorize";

            var code = Request["code"];
            string data = "None";

            if (code != null)
            {

                var values = new Dictionary<string, string>
                    {
                       { "client_secret", API_KEY },
                       { "code", code },
                       { "grant_type" , "authorization_code" },
                       { "client_id" , CLIENT_ID}
                    };

                var content = new FormUrlEncodedContent(values);

                var response = await client.PostAsync(TOKEN_URI, content);

                var responseString = await response.Content.ReadAsStringAsync();
                data = responseString;



            }

            ViewData["data"] = data;

            return View();
        }



        /*      public async System.Threading.Tasks.Task<ActionResult> Connect<ActionResult>(string code)
              {
                  var CLIENT_ID = "ca_D1d3N5fNh9cPUH1uw46caErCpGrh6La4";
                  var API_KEY = "sk_test_Z8x4M7wWPiv3rJcfQQ83Xfai";
                  var TOKEN_URI = "https://connect.stripe.com/oauth/token";
                  var AUTHORIZE_URI = "https://connect.stripe.com/oauth/authorize";

                  string data = "None";

                  if (code != null)
                  {

                      var values = new Dictionary<string, string>
                          {
                             { "client_secret", API_KEY },
                             { "code", code },
                             { "grant_type" , "authorization_code" },
                             { "client_id" , CLIENT_ID}
                          };

                      var content = new FormUrlEncodedContent(values);

                      var response = await client.PostAsync(TOKEN_URI, content);

                      var responseString = await response.Content.ReadAsStringAsync();
                      data = responseString;



                  }

                  var list = JsonConvert.SerializeObject(data);


                  return Content(list,"application/json");
              }
      */

        public string StripeConnect(string code)
        {
            var CLIENT_ID = "ca_D1d3N5fNh9cPUH1uw46caErCpGrh6La4";
            var API_KEY = "sk_test_Z8x4M7wWPiv3rJcfQQ83Xfai";
            var TOKEN_URI = "https://connect.stripe.com/oauth/token";
            var AUTHORIZE_URI = "https://connect.stripe.com/oauth/authorize";

            string data = "None";

            if (code != null)
            {

                //
                WebClient wc = new WebClient();

                wc.QueryString.Add("client_secret", API_KEY);
                wc.QueryString.Add("code", code);
                wc.QueryString.Add("grant_type", "authorization_code");
                wc.QueryString.Add("client_id", CLIENT_ID);

                var request_data = wc.UploadValues(TOKEN_URI, "POST", wc.QueryString);
                var responseString = UnicodeEncoding.UTF8.GetString(request_data);
                data = responseString;

            }

            return data;

        }


        public ActionResult Connect(string code)
        {
            var responseString = JsonConvert.DeserializeObject(StripeConnect(code));
            return Content(responseString.ToString(), "application/json");
        }





    }
}
