using Android.App;
using Android.Content;
using Android.Widget;
using BugTracker.Data;
using Java.Lang;

namespace BugTracker
{
    public class BugAdapter: ArrayAdapter
    {
        private readonly Activity _activity;

        public BugAdapter(Activity activity, Context context, int textViewResourceId, object[] objects)
            : base(context, textViewResourceId, objects)
        {
            this._activity = activity;
        }

        public override Android.Views.View GetView(int position, Android.Views.View convertView, Android.Views.ViewGroup parent)
        {
            //Get our object for this position
            var item = (BugModel)this.GetItem(position);

            // Try to reuse convertView if it's not null, otherwise inflate it from our item layout
            // This gives us some performance gains by not always inflating a new view
            var view = (convertView ?? _activity.LayoutInflater.Inflate(Resource.Layout.BugListRow, parent, false)) as LinearLayout;

            if (view != null)
            {
                view.FindViewById<TextView>(Resource.Id.bug_priority).Text = item.Priority;
                view.FindViewById<TextView>(Resource.Id.bug_assigned2).Text = Left(item.Assigned2, 30);
                view.FindViewById<TextView>(Resource.Id.bugs_steps2reproduce).Text = Left(item.Steps2Reproduce, 70);

                return view;
            }
            return null;
        }

        private string Left(string text, int length)
        {
            if (text.Length <= length)
                return text;

            return text.Substring(0, length - 3) + "...";
        }
    }
}