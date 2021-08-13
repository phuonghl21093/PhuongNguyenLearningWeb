using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.Mvc;
using Google.Apis.Analytics.v3;
using Google.Apis.Analytics.v3.Data;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;

namespace Web365Admin.Controllers
{
    public class GoogleAnalyticsController : Controller
    {
        private readonly AnalyticsService _service;

        public GoogleAnalyticsController()
        {
            string[] scopes = new string[] { AnalyticsService.Scope.Analytics };

            const string keyFilePath = @"F:\GHD\HiconProject\Hicon\Code\Web365Admin\App_Data\GoogleAnalytics\HICONVN-a60ef1cb858a.p12";
            const string serviceAccountEmail = "admin-145@hiconvn-158009.iam.gserviceaccount.com";

            //loading the Key file
            var certificate = new X509Certificate2(keyFilePath, "notasecret", X509KeyStorageFlags.Exportable);
            var credential =
                new ServiceAccountCredential(new ServiceAccountCredential.Initializer(serviceAccountEmail)
                {
                    Scopes = scopes
                }.FromCertificate(certificate));

            _service = new AnalyticsService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = "HICONVN"
            });
        }


        // GET: GoogleAnalytics
        public ActionResult Index()
        {

            return View();
        }


        public AnalyticDataPoint GetAnalyticsData(string profileId, string[] dimensions, string[] metrics, DateTime? startDate, DateTime? endDate)
        {
            AnalyticDataPoint data = new AnalyticDataPoint();
            if (!profileId.Contains("ga:"))
                profileId = string.Format("ga:{0}", profileId);

            //Make initial call to service.
            //Then check if a next link exists in the response,
            //if so parse and call again using start index param.
            GaData response = null;
            do
            {
                int startIndex = 1;
                if (response != null && !string.IsNullOrEmpty(response.NextLink))
                {
                    Uri uri = new Uri(response.NextLink);
                    var paramerters = uri.Query.Split('&');
                    string s = paramerters.First(i => i.Contains("start-index")).Split('=')[1];
                    startIndex = int.Parse(s);
                }

                var request = BuildAnalyticRequest(profileId, dimensions, metrics, startDate, endDate, startIndex);
                response = request.Execute();
                data.ColumnHeaders = response.ColumnHeaders;
                data.Rows.AddRange(response.Rows);

            } while (!string.IsNullOrEmpty(response.NextLink));

            return data;
        }

        private DataResource.GaResource.GetRequest BuildAnalyticRequest(string profileId, string[] dimensions, string[] metrics,
                                                                            DateTime? startDate, DateTime? endDate, int startIndex)
        {
            string _startDate = startDate == null ? "30daysAgo" : startDate.Value.ToString("yyyy-MM-dd");
            string _endDate = endDate == null ? DateTime.Now.ToString("yyyy-MM-dd") : endDate.Value.ToString("yyyy-MM-dd");   

            DataResource.GaResource.GetRequest request = _service.Data.Ga.Get(profileId, _startDate, _endDate, string.Join(",", metrics));
            request.Dimensions = string.Join(",", dimensions);
            request.StartIndex = startIndex;
            return request;
        }

        public IList<Profile> GetAvailableProfiles()
        {
            var response = _service.Management.Profiles.List("~all", "~all").Execute();
            return response.Items;
        }

        public class AnalyticDataPoint
        {
            public AnalyticDataPoint()
            {
                Rows = new List<IList<string>>();
            }

            public IList<GaData.ColumnHeadersData> ColumnHeaders { get; set; }
            public List<IList<string>> Rows { get; set; }
        }

        public ActionResult PageViewReport()
        {
            AnalyticDataPoint result = GetAnalyticsData("ga:139854820", new[] { "ga:pagePath" }, new[] { "ga:pageviews" }, null, null);
            return Json(new { result }, JsonRequestBehavior.AllowGet);
        }
    }
}