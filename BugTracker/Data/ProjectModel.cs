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
    public class ProjectModel
    {
        public int Id { get; set; }
     
        public string ProjectName { get; set; }

        public string ProjectDescription { get; set; }
    }
}