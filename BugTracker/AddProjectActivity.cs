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
            _project.ProjectDescription = FindViewById<EditText>(Resource.Id.projectDescription).Text;

            ProjectRepository.SaveProject(_project);

            Toast.MakeText(this, Resource.String.projectSaved, ToastLength.Long).Show();
            SetResult(Result.Ok);
        }

        protected override void OnPause()
        {
            base.OnPause();

            // If this was a new project and no title was added, don't save it
            if (IsFinishing && _project.Id == 0 && _projectNameView.Text.Length == 0)
                return;

            _project.ProjectName = _projectNameView.Text;
            _project.ProjectDescription = _projectDescriptionView.Text;
            ProjectRepository.SaveProject(_project);
        }
    }
}