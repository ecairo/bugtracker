using Android.App;
using Android.Content;
using Android.Graphics;
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

            var bugTextColor = Color.DarkSalmon;
            if (view != null)
            {
                switch (item.Priority)
                {
                    case "Very High":
                        bugTextColor = Color.Red;
                        break;
                    case "High":
                        bugTextColor = Color.OrangeRed;
                        break;
                    case "Medium":
                        bugTextColor = Color.Orange;
                        break;
                    default:
                        break;
                }
                var header = view.FindViewById<TextView>(Resource.Id.bug_header);
                header.Text = string.Format("BR-{0}, assigned to: {1}", item.Id, item.Assigned2);
                header.SetTextColor(bugTextColor);

                var description = view.FindViewById<TextView>(Resource.Id.bugs_description);
                description.Text = Left(item.ObservedBehavior, 255);
                description.SetTextColor(bugTextColor);
                

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