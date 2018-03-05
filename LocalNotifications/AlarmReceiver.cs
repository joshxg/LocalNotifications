using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Views;
using Android.Widget;
using TaskStackBuilder = Android.Support.V4.App.TaskStackBuilder;

namespace LocalNotifications
{
    [BroadcastReceiver]
    public class AlarmReceiver : BroadcastReceiver
    {
        public override void OnReceive(Context context, Intent intent)
        {
            //getting the data passed
            var message = intent.Extras.GetString("message", "yo");
            
            //Notification intent
            var notIntent = new Intent(context, typeof(SecondActivity));
            
            
            //creating the bundle, adding data and adding to the notification intent
            Bundle ValuesForActivity = new Bundle();
            ValuesForActivity.PutString("message", message);
            notIntent.PutExtras(ValuesForActivity);

            //Create the TaskStackBuilder for cross app navigation
            TaskStackBuilder stackBuilder = TaskStackBuilder.Create(context);
            stackBuilder.AddParentStack(Java.Lang.Class.FromType(typeof(SecondActivity)));
            stackBuilder.AddNextIntent(notIntent);
            PendingIntent contentIntent = stackBuilder.GetPendingIntent(0, (int)PendingIntentFlags.UpdateCurrent);

            //Create the notification manager
            var manager = NotificationManagerCompat.From(context);

            // Build the notification
            NotificationCompat.Builder builder = new NotificationCompat.Builder(context)
                .SetAutoCancel(true)                    // Dismiss from the notif. area when clicked
                .SetContentIntent(contentIntent)  // Start 2nd activity when the intent is clicked.
                .SetContentTitle("Button Clicked")      // Set its title                    // Display the count in the Content Info
                .SetSmallIcon(Resource.Drawable.ic_stat_button_click)  // Display this icon
                .SetContentText(String.Format(
                    "The button has been clicked {0} times.", message)); // The message to display.

            // Finally, publish the notification:
            var notification = builder.Build();
            manager.Notify(0, notification);
        }
    }
}