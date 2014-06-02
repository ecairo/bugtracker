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
        private Spinner _prioritySpinner;
        private const int ALERT_SAVE_DIALOG = 0;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.AddBugLayout);

            _projectId = Intent.GetLongExtra("project_id", 0);
            _bug = new BugModel {Project = _projectId};

            _prioritySpinner = FindViewById<Spinner>(Resource.Id.priority);

            var priorityAdapter = ArrayAdapter.CreateFromResource(this, Resource.Array.Priorities,
                Android.Resource.Layout.SimpleSpinnerItem);

            priorityAdapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            _prioritySpinner.Adapter = priorityAdapter;

            var buttonSave = FindViewById<Button>(Resource.Id.saveBug);

            buttonSave.Click += buttonSave_Click;
        }

        void buttonSave_Click(object sender, EventArgs e)
        {
            _bug.ExpectedBehavior = FindViewById<EditText>(Resource.Id.expectedBehavior).Text;
            _bug.ObservedBehavior = FindViewById<EditText>(Resource.Id.observedBehavior).Text;
            _bug.Steps2Reproduce = FindViewById<EditText>(Resource.Id.steps2Reproduce).Text;
            _bug.Assigned2 = FindViewById<EditText>(Resource.Id.assigned2).Text;

            if (string.IsNullOrEmpty(_bug.ExpectedBehavior) || string.IsNullOrEmpty(_bug.ObservedBehavior) || string.IsNullOrEmpty(_bug.Steps2Reproduce) || string.IsNullOrEmpty(_bug.Assigned2))
            {
                // abort save
                ShowDialog(ALERT_SAVE_DIALOG);
                return;
            }

            _bug.Priority = _prioritySpinner.GetItemAtPosition(_prioritySpinner.SelectedItemPosition).ToString();

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


        protected override Dialog OnCreateDialog(int id)
        {
            // TODO: Move strings to strings.xml
            switch (id)
            {
                case ALERT_SAVE_DIALOG:
                    var dialogBuilder = new AlertDialog.Builder(this);
                    return dialogBuilder.SetTitle("Attention")
                        .SetMessage("Fill all fields to save the bug.")
                        .SetCancelable(false)
                        .SetPositiveButton("OK", (sender, args) => { })
                        .Create();
            }

            return null;
        }
    }
}