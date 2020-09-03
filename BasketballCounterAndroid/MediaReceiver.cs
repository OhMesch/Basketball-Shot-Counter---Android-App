using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace BasketballCounterAndroid
{
	[BroadcastReceiver]
	public class MediaReceiver : BroadcastReceiver
	{
		public override void OnReceive(Context context, Intent intent)
		{
			System.Diagnostics.Debug.WriteLine("CAPTURED BROADCAST EVENT");
			System.Diagnostics.Debug.WriteLine(context);
			System.Diagnostics.Debug.WriteLine(intent);
			Toast.MakeText(context, "Received intent!", ToastLength.Short).Show();
		}
	}
}