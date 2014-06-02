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

            // Inform the list we provide context menus for items
            ListView.SetOnCreateContextMenuListener(this);

            _projectId = Intent.GetLongExtra("project_id", 0);

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
                    var intent = new Intent(this, typeof(AddBugActivity));
                    intent.PutExtra("project_id", _projectId);

                    StartActivity(intent);
                    break;
            }

            return base.OnOptionsItemSelected(item);
        }

        public override void OnCreateContextMenu(IContextMenu menu, View v, IContextMenuContextMenuInfo menuInfo)
        {
            base.OnCreateContextMenu(menu, v, menuInfo);

            MenuInflater.Inflate(Resource.Menu.bugsContextMenu, menu);
        }

        public override bool OnContextItemSelected(IMenuItem item)
        {
            var info = (AdapterView.AdapterContextMenuInfo)item.MenuInfo;
            var bug = (BugModel)ListAdapter.GetItem(info.Position);

            switch (item.ItemId)
            {
                case Resource.Id.markFixedBug:
                    BugRepository.MarkAsFixed(bug.Id);
                    
                    Toast.MakeText(this, Resource.String.bugSaved, ToastLength.Short).Show();

                    PopulateList();
                    break;
            }

            return base.OnContextItemSelected(item);
        }

        protected override void OnListItemClick(ListView l, View v, int position, long id)
        {
            var selected = (BugModel)ListAdapter.GetItem(position);

            // Launch activity to view/edit the currently selected item
            var intent = new Intent(this, typeof(BugDetails));
            intent.PutExtra("bug_id", selected.Id);

            StartActivity(intent);
        }

        protected override void OnResume()
        {
            base.OnResume();

            PopulateList();
        }
    }
}