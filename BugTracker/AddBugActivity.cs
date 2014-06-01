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
    [Activity(Label = "New Bug")]
    public class AddBugActivity : Activity
    {
        private long _projectId;
        private BugModel _bug;
        private Spinner prioritySpinner;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.AddBugLayout);

            _projectId = Intent.GetLongExtra("project_id", 0);
            _bug = new BugModel {Project = _projectId};

            prioritySpinner = FindViewById<Spinner>(Resource.Id.priority);

            var priorityAdapter = ArrayAdapter.CreateFromResource(this, Resource.Array.Priorities,
                Android.Resource.Layout.SimpleSpinnerItem);

            priorityAdapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            prioritySpinner.Adapter = priorityAdapter;

            prioritySpinner.ItemSelected += SpinnerOnItemClick;

            var buttonSave = FindViewById<Button>(Resource.Id.saveBug);

            buttonSave.Click += buttonSave_Click;

        }

        void SpinnerOnItemClick(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            var senderSpinner = (Spinner) sender;
        }

        void buttonSave_Click(object sender, EventArgs e)
        {
            _bug.ExpectedBehavior = FindViewById<EditText>(Resource.Id.expectedBehavior).Text;
            _bug.ObservedBehavior = FindViewById<EditText>(Resource.Id.observedBehavior).Text;
            _bug.Steps2Reproduce = FindViewById<EditText>(Resource.Id.steps2Reproduce).Text;
            _bug.Assigned2 = FindViewById<EditText>(Resource.Id.assigned2).Text;

            _bug.Priority = prioritySpinner.GetItemAtPosition(prioritySpinner.SelectedItemPosition).ToString();

            try
            {
                BugRepository.Save(_bug);
                Toast.MakeText(this, Resource.String.bugSaved, ToastLength.Long).Show();
            }
            catch (Exception exception)
            {
                Toast.MakeText(this, exception.Message, ToastLength.Long).Show();
            }

            Finish();
        }
    }
}