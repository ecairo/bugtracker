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
    [Activity(Label = "Preferences")]
    public class PreferencesActivity : Android.Preferences.PreferenceActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            AddPreferencesFromResource(Resource.Xml.Preferences);
        }
    }
}