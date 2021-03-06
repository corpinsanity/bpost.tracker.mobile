﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace TestApplication
{
    [Activity(Label = "ManageListActivity")]
    public class ManageListActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.manage_list);

            Button backButton = FindViewById<Button>(Resource.Id.backButton);

            backButton.Click += delegate
            {
                Intent intent = new Intent(this, typeof(MainActivity));
                StartActivity(intent);
            };

            FillTrackingCodes();
        }
        protected void FillTrackingCodes()
        {
            TextView resultBox = FindViewById<TextView>(Resource.Id.trackingView);
            var currentList = new List<string>();
            currentList = MainActivity.TrackingCodeList;
            if (currentList.Any())
            {
                resultBox.Text = "";
                foreach (string code in currentList)
                    resultBox.Text += code + "\r\n";
            }
        }
    }
}