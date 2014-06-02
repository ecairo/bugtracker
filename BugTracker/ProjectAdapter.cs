using System.Globalization;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Widget;
using BugTracker.Data;
using Java.Lang;

namespace BugTracker
{
    public class ProjectAdapter: ArrayAdapter
    {
        private readonly Activity _activity;

        public ProjectAdapter(Activity activity, Context context, int textViewResourceId, object[] objects)
            : base(context, textViewResourceId, objects)
        {
            this._activity = activity;
        }

        public override Android.Views.View GetView(int position, Android.Views.View convertView, Android.Views.ViewGroup parent)
        {
            //Get our object for this position
            var item = (ProjectModel)this.GetItem(position);

            // Try to reuse convertView if it's not null, otherwise inflate it from our item layout
            // This gives us some performance gains by not always inflating a new view
            var view = (convertView ?? _activity.LayoutInflater.Inflate(Resource.Layout.ProjectListRow, parent, false)) as LinearLayout;

            if (view != null)
            {
                var projectHeaderView = view.FindViewById<TextView>(Resource.Id.project_name);
                projectHeaderView.Text = Left(item.ProjectName, 30);

                view.FindViewById<TextView>(Resource.Id.project_description).Text = Left(item.ProjectDescription, 150);

                var projectBugs = item.Bugs;
                var projectBugsView = view.FindViewById<TextView>(Resource.Id.project_bugs);
                var projectColor = Color.LightGreen;

                projectBugsView.Text = projectBugs == 0
                    ? ""
                    : projectBugs.ToString(CultureInfo.InvariantCulture);

                if (projectBugs > 0 && projectBugs <= 10)
                {
                    projectColor = Color.Orange;
                }else if (projectBugs > 10 && projectBugs <= 20)
                {
                    projectColor = Color.OrangeRed;
                }
                else if(projectBugs > 20)
                {
                    projectColor = Color.Red;
                }

                projectBugsView.SetTextColor(projectColor);
                projectHeaderView.SetTextColor(projectColor);

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