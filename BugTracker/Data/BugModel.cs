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
    public class BugModel
    {
        public int Id { get; set; }
     
        public string FoundBy { get; set; }

        public string ExpectedBehavior { get; set; }

        public string ObservedBehavior { get; set; }

        public string Steps2Reproduce { get; set; }

        public string Assigned2 { get; set; }

        public string Priority { get; set; }

        public bool Fixed { get; set; }

        public DateTime Found { get; set; }
    }
}