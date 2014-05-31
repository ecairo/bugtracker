using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace BugTracker
{
    [Activity(Label = "BugTracker", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.optionsMenu, menu);

            return base.OnCreateOptionsMenu(menu);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.editPreferences:
                    StartActivity(typeof(PreferencesActivity));
                    break;
                case Resource.Id.addProject:
                    StartActivity(typeof(AddProjectActivity));
                    break;
                case Resource.Id.addBug:
                    StartActivity(typeof(AddBugActivity));
                    break;
            }

            return base.OnOptionsItemSelected(item);
        }
    }
}

