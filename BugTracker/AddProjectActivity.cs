using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.OS;
using Android.Provider;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using BugTracker.Data;

namespace BugTracker
{
    [Activity(Label = "New Project")]
    public class AddProjectActivity : Activity
    {
        private ProjectModel _project;
        private EditText _projectNameView;
        private EditText _projectDescriptionView;
        private const int ALERT_SAVE_DIALOG = 0;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.AddProjectLayout);

            var projectId = Intent.GetLongExtra("project_id", 0);

            _project = projectId == 0 ? new ProjectModel() : ProjectRepository.GetProject(projectId);

            _projectNameView = FindViewById<EditText>(Resource.Id.projectName);
            _projectDescriptionView = FindViewById<EditText>(Resource.Id.projectDescription);

            _projectNameView.Text = _project.ProjectName;
            _projectDescriptionView.Text = _project.ProjectDescription;

            var buttonSave = FindViewById<Button>(Resource.Id.saveProject);

            buttonSave.Click += buttonSave_Click;
        }

        void buttonSave_Click(object sender, EventArgs e)
        {
            _project.ProjectName = FindViewById<EditText>(Resource.Id.projectName).Text;

            if (string.IsNullOrEmpty(_project.ProjectName))
            {
                ShowDialog(ALERT_SAVE_DIALOG);
                return;
            }

            _project.ProjectDescription = FindViewById<EditText>(Resource.Id.projectDescription).Text;

            try
            {
                ProjectRepository.SaveProject(_project);
                Toast.MakeText(this, Resource.String.projectSaved, ToastLength.Short).Show();
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
                        .SetMessage("Project must have a Name")
                        .SetPositiveButton("OK", (sender, args) => {})
                        .Create();
            }

            return null;
        }
    }
}