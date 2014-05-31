using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace BugTracker
{
    [Activity(Label = "New Bug")]
    public class AddBugActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.AddBugLayout);

            var prioritySpinner = FindViewById<Spinner>(Resource.Id.priority);

            var priorityAdapter = ArrayAdapter.CreateFromResource(this, Resource.Array.Priorities,
                Android.Resource.Layout.SimpleSpinnerItem);

            priorityAdapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            prioritySpinner.Adapter = priorityAdapter;

            prioritySpinner.ItemSelected += SpinnerOnItemClick;
        }

        void SpinnerOnItemClick(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            var senderSpinner = (Spinner) sender;
        }
    }
}