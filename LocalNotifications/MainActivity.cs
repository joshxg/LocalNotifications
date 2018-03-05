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
            SetContentView(Resource.Layout.Main);
            Button button = FindViewById<Button>(Resource.Id.button);
            button.Click += ButtonOnClick;
        }

        private void ButtonOnClick(object sender, EventArgs eventArgs)
        {
            //start the timer on button click
            Remind();
        }

        public void Remind()
        {
            //DAta to pass to the second activity
            var message = "hello";

            //Create the alarm intent
            Intent alarmIntent = new Intent(this, typeof(AlarmReceiver));
            
            //create and fill the bundle of data to pass to the second activity, add it to our intent
            Bundle valuesForActivity = new Bundle();
            valuesForActivity.PutString("message", message);
            alarmIntent.PutExtras(valuesForActivity);

            //Wrap the intent in a pendingintent
            PendingIntent PendingIntent = PendingIntent.GetBroadcast(this, 0, alarmIntent, PendingIntentFlags.UpdateCurrent);

            //create and set the alarm (currently fires 5 seconds after button press
            AlarmManager alarmManager = (AlarmManager)GetSystemService(AlarmService);
            alarmManager.Set(AlarmType.ElapsedRealtime, SystemClock.ElapsedRealtime () + 5 * 1000, PendingIntent);
        }
    }
}

