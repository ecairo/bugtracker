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
using BugTracker.Data;

namespace BugTracker
{
    [Activity(Label = "Bug Details")]
    public class BugDetails : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Create your application here
            SetContentView(Resource.Layout.BugDetailsLayout);

            var bugId = Intent.GetLongExtra("bug_id", 0);

            PopulateDetails(bugId);
        }

        private void PopulateDetails(long bugId)
        {
            var bug = BugRepository.GetBug(bugId);
            if (bug != null)
            {
                FindViewById<TextView>(Resource.Id.expectedBehaviorDetails).Text = bug.ExpectedBehavior;
                FindViewById<TextView>(Resource.Id.observedBehaviorDetails).Text = bug.ObservedBehavior;
                FindViewById<TextView>(Resource.Id.assigned2Details).Text = bug.Assigned2;
                FindViewById<TextView>(Resource.Id.steps2ReproduceDetails).Text = bug.Steps2Reproduce;
                FindViewById<TextView>(Resource.Id.priorityDetails).Text = bug.Priority;
                FindViewById<TextView>(Resource.Id.managerDetails).Text = bug.FoundBy;
                FindViewById<TextView>(Resource.Id.foundDetails).Text = bug.Found.ToLongDateString();
            }
        }
    }
}