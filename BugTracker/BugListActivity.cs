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
    [Activity(Label = "Bug List")]
    public class BugListActivity : ListActivity
    {
        private long _projectId;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            var projectId = Intent.GetLongExtra("project_id", 0);

            _projectId = projectId;

            PopulateList();
        }

        private void PopulateList()
        {
            var bugs = BugRepository.GetAllBugs(_projectId);
            var adapter = new BugAdapter(this, this, Resource.Layout.ProjectListRow, bugs.ToArray());
            ListAdapter = adapter;
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.bugsOptionsMenu, menu);

            return base.OnCreateOptionsMenu(menu);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.addBug:
                    StartActivity(typeof(AddBugActivity));
                    break;
            }

            return base.OnOptionsItemSelected(item);
        }

    }
}