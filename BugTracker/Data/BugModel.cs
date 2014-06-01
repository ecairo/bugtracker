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
    public class BugModel : Java.Lang.Object
    {
        public BugModel(int id, string foundby, string expectedBehavior, string observedBehavior, string steps2Reproduce, bool fix, DateTime found, string priority, string assigned2)
        {
            this.Id = id;
            this.FoundBy = foundby;
            this.ExpectedBehavior = expectedBehavior;
            this.ObservedBehavior = observedBehavior;
            this.Steps2Reproduce = steps2Reproduce;
            this.Fixed = fix;
            this.Found = found;
            this.Assigned2 = assigned2;
            this.Priority = priority;
        }

        public BugModel()
        {
            
        }

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