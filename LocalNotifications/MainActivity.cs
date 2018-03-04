using Android.App;
using Android.Widget;
using Android.OS;
using Android.Support.V4.App;
using TaskStackBuilder = Android.Support.V4.App.TaskStackBuilder;
using System;
using Android.Content;

namespace LocalNotifications
{
    [Activity(Label = "LocalNotifications", MainLauncher = true)]
    public class MainActivity : Activity
    {
        private static readonly int ButtonClickNotificationId = 1000;
        public int count;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
            Button button = FindViewById<Button>(Resource.Id.button);
            button.Click += ButtonOnClick;
        }

        private void ButtonOnClick(object sender, EventArgs eventArgs)
        {

            // Pass the current button press count value to the next activity:
            Bundle valuesForActivity = new Bundle();
            valuesForActivity.PutInt("count", count);

            // When the user clicks the notification, SecondActivity will start up.
            Intent resultIntent = new Intent(this, typeof(SecondActivity));

            // Pass some values to SecondActivity:
            resultIntent.PutExtras(valuesForActivity);

            // Construct a back stack for cross-task navigation:
            TaskStackBuilder stackBuilder = TaskStackBuilder.Create(this);
            stackBuilder.AddParentStack(Java.Lang.Class.FromType(typeof(SecondActivity)));
            stackBuilder.AddNextIntent(resultIntent);

            // Create the PendingIntent with the back stack:            
            PendingIntent resultPendingIntent =
                stackBuilder.GetPendingIntent(0, (int)PendingIntentFlags.UpdateCurrent);

            // Build the notification:
            NotificationCompat.Builder builder = new NotificationCompat.Builder(this)
                .SetAutoCancel(true)                    // Dismiss from the notif. area when clicked
                .SetContentIntent(resultPendingIntent)  // Start 2nd activity when the intent is clicked.
                .SetContentTitle("Button Clicked")      // Set its title
                .SetNumber(count)                       // Display the count in the Content Info
                .SetSmallIcon(Resource.Drawable.ic_stat_button_click)  // Display this icon
                .SetContentText(String.Format(
                    "The button has been clicked {0} times.", count)); // The message to display.

            // Finally, publish the notification:
            NotificationManager notificationManager =
                (NotificationManager)GetSystemService(Context.NotificationService);
            notificationManager.Notify(ButtonClickNotificationId, builder.Build());

            // Increment the button press count:
            count++;
        }
    }
}

