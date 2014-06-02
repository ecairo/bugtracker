using System;
using System.Linq;

using Android.App;
using Android.Content;
using Android.Views;
using Android.Widget;
using Android.OS;

using BugTracker.Data;

namespace BugTracker
{
    [Activity(Label = "BugTracker", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : ListActivity
    {
        private long _selectedProject;
        private const int ALERT_DELETE_DIALOG = 0;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetDefaultKeyMode(DefaultKey.Shortcut);

            // Inform the list we provide context menus for items
            ListView.SetOnCreateContextMenuListener(this);

            PopulateList();
        }

        private void PopulateList()
        {
            _selectedProject = 0;

            var projects = ProjectRepository.GetAllProjects();
            var adapter = new ProjectAdapter(this, this, Resource.Layout.ProjectListRow, projects.ToArray());
            ListAdapter = adapter;
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
                    StartAddProject(0, typeof(AddProjectActivity));
                    break;
            }

            return base.OnOptionsItemSelected(item);
        }

        public override void OnCreateContextMenu(IContextMenu menu, View v, IContextMenuContextMenuInfo menuInfo)
        {
            base.OnCreateContextMenu(menu, v, menuInfo);

            MenuInflater.Inflate(Resource.Menu.contextMenu, menu);
        }

        public override bool OnContextItemSelected(IMenuItem item)
        {
            var info = (AdapterView.AdapterContextMenuInfo)item.MenuInfo;
            var project = (ProjectModel)ListAdapter.GetItem(info.Position);

            switch (item.ItemId)
            {
                case Resource.Id.editProject:
                    StartAddProject(project.Id, typeof(AddProjectActivity));
                    break;

                case Resource.Id.deleteProject:
                    _selectedProject = project.Id;
                    ShowDialog(ALERT_DELETE_DIALOG);
                    break;
            }
            
            return base.OnContextItemSelected(item);
        }

        protected override Dialog OnCreateDialog(int id)
        {
            // TODO: Move strings to strings.xml
            switch (id)
            {
                case ALERT_DELETE_DIALOG:
                    var dialogBuilder = new AlertDialog.Builder(this);
                    return dialogBuilder.SetTitle("Delete?")
                        .SetMessage("Are you sure you want to delete?")
                        .SetCancelable(false)
                        .SetPositiveButton("Yes", (sender, args) =>
                        {
                            ProjectRepository.Delete(_selectedProject);
                            
                            PopulateList();
                        })
                        .SetNegativeButton("No", (sender, args) => { })
                        .Create();
            }

            return null;
        }

        private void StartAddProject(long id, Type activity)
        {
            var intent = new Intent(this, activity);
            intent.PutExtra("project_id", id);

            StartActivity(intent);
        }

        protected override void OnListItemClick(ListView l, View v, int position, long id)
        {
            var selected = (ProjectModel)ListAdapter.GetItem(position);

            // Launch activity to view/edit the currently selected item
            StartAddProject(selected.Id, typeof(BugListActivity));
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);

            PopulateList();
        }

        protected override void OnResume()
        {
            base.OnResume();

            PopulateList();
        }
    }
}

