using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace BugTracker.Data
{
    public class ProjectModel: Java.Lang.Object
    {

        public ProjectModel(long id, string projectName, string description)
        {
            this.Id = id;
            this.ProjectName = projectName;
            this.ProjectDescription = description;
        }

        public ProjectModel()
        {
            Id = 0;
            ProjectName = "";
            ProjectDescription = "";
        }

        public long Id { get; set; }
     
        public string ProjectName { get; set; }

        public string ProjectDescription { get; set; }

        public long Bugs
        {
            get
            {
                return ProjectRepository.GetProjectBugs(this.Id);
            }
        }
    }
}