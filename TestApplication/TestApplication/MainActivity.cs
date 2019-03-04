using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using Android.Webkit;
using Android.Content;
using System.Net.Http;
using System.Net;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text;
using System.Threading;
using System.Linq;
using TestApplication.Resources.models;
using System;

namespace TestApplication
{
    [Activity(Label = "Postman", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        public static List<string> TrackingCodeList = new List<string>();

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_main);
            Button searchButton = FindViewById<Button>(Resource.Id.searchButton);
            Button manageButton = FindViewById<Button>(Resource.Id.manageListButton);

            searchButton.Click += delegate
            {
                ///Searchbutton has been pressed, calling function Submit
                Submit();
            };
            manageButton.Click += delegate
            {
                ///Showing new page to manage the trackinglist
                Intent intent = new Intent(this, typeof(ManageListActivity));
                StartActivity(intent);
            };

        }

        protected void Submit()
        {
            Android.App.AlertDialog.Builder alert = new Android.App.AlertDialog.Builder(this);

            alert.SetTitle("Confirmation");
            alert.SetMessage(string.Format("Are you sure you want to track the following codes: ?"));
            alert.SetPositiveButton("Hell yea", (senderAlert, args) =>
            {
                Toast.MakeText(this, "Searching...", ToastLength.Short).Show();

                TrackingCodeList.Add("RP163186281CN");
                TrackingCodeList.Add("LM482350324CN");
                Search(TrackingCodeList);
            });

            alert.SetNegativeButton("Fuck no", (senderAlert, args) =>
            {
                Toast.MakeText(this, "Cancelled!", ToastLength.Short).Show();
            });

            Dialog dialog = alert.Create();
            dialog.Show();
        }

        protected void Search(List<string> trackingCodes)
        {
            List<string> plainTextsJson = new List<string>();
            TextView resultBox = FindViewById<TextView>(Resource.Id.resultView);
            resultBox.Text = "";
            var apiUrl = "https://track.bpost.be/btr/api/items?itemIdentifier=";

            foreach (var trackingCode in trackingCodes)
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(apiUrl + trackingCode);
                request.AutomaticDecompression = DecompressionMethods.GZip;
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                using (var sr = new StreamReader(response.GetResponseStream()))
                    plainTextsJson.Add(sr.ReadToEnd());
            }

            int index = 0;
            resultBox.Text = "---------------RESULTS--------------- \r\n \r\n";
            foreach (var jsonResult in plainTextsJson)
            {
                resultBox.Text += trackingCodes[index] + "\r\n";
                resultBox.Text += "------------------------------------- \r\n";

                Item trackingResult = new Item();
                JObject json = JObject.Parse(jsonResult);
                RootObject bpostResult = JsonConvert.DeserializeObject<RootObject>(jsonResult);
                trackingResult = bpostResult.items[0];

                if (trackingResult.processOverview.activeStepTextKey.ToLower() == "delivered")
                {
                    resultBox.Text += "\r\n Your parcel has been delivered on: " + trackingResult.events[0].date;
                }
                else
                {
                    resultBox.Text += "\r\n The status of your parcel is: " + trackingResult.processOverview.activeStepTextKey;
                    var currentStep = trackingResult.processOverview.processSteps.FirstOrDefault(s => s.status.Equals("active"));
                    resultBox.Text += string.Format("\r\n      -{0} {1}", currentStep.label.main, currentStep.label.detail);
                }

                resultBox.Text += "\r\n \r\n";
                index++;
            }
        }

    }
}