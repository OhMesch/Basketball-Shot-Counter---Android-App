using Android.App;
using Android.Content;
using Android.Media;
using Android.OS;
using Android.Widget;
using System.Diagnostics;

namespace BasketballCounterAndroid
{
	[Activity(Label = "BasketballCounterAndroid", MainLauncher = true, Icon = "@mipmap/icon")]
	public class MainActivity : Activity
	{
		int count = 1;
		int hit = 0;
		int total = 0;

		MediaReceiver mr = new MediaReceiver();

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			SetContentView(Resource.Layout.Main);

			Button button = FindViewById<Button>(Resource.Id.myButton);
			button.Click += delegate { button.Text = string.Format("{0} clicks!", count++); };

			TextView counter = FindViewById<TextView>(Resource.Id.text_counter);
			FindViewById<Button>(Resource.Id.btn_miss).Click += delegate { counter.Text = string.Format("{0}/{1}", hit, ++total); };
			FindViewById<Button>(Resource.Id.btn_hit).Click += delegate { counter.Text = string.Format("{0}/{1}", ++hit, ++total); };
			FindViewById<Button>(Resource.Id.btn_reset).Click += delegate { counter.Text = string.Format("{0}/{1}", hit = 0, total = 0); };
			FindViewById<Button>(Resource.Id.btn_dummy).Click += delegate { SendBroadcast(new Intent("DUMMY")); };

			AudioFocusManager afm = new AudioFocusManager(Android.App.Application.Context);

			System.Diagnostics.Debug.WriteLine("OnCreate");
		}
		protected override void OnStart()
		{
			base.OnStart();

			System.Diagnostics.Debug.WriteLine("OnStart");
		}
		protected override void OnResume()
		{
			base.OnResume();
			RegisterReceiver(mr, getIntentFiler());
			System.Diagnostics.Debug.WriteLine("OnResume");
		}
		protected override void OnStop()
		{
			base.OnStop();
			System.Diagnostics.Debug.WriteLine("OnStop");
		}
		protected override void OnDestroy()
		{
			UnregisterReceiver(mr);
			System.Diagnostics.Debug.WriteLine("OnDestroy");
			base.OnDestroy();
		}
		private IntentFilter getIntentFiler()
		{
			IntentFilter intentFilter = new IntentFilter();
			intentFilter.AddAction("android.intent.action.MEDIA_BUTTON");
			intentFilter.AddAction("android.intent.action.HEADSET_PLUG");
			intentFilter.AddAction("android.net.conn.CONNECTIVITY_CHANGE");
			intentFilter.AddAction("DUMMY");
			intentFilter.Priority = 999;
			return intentFilter;
		}
		public override bool OnKeyDown(Android.Views.Keycode keyCode, Android.Views.KeyEvent e) {
			System.Diagnostics.Debug.WriteLine("CAPTURED KEYCODE EVENT");
			System.Diagnostics.Debug.WriteLine(keyCode);
			System.Diagnostics.Debug.WriteLine(e);
			//if (keyCode == KeyEvent.KEYCODE_HEADSETHOOK)
			//{
			//	//handle click
			//	return true;
			//}
			return base.OnKeyDown(keyCode, e);
		}
	}
}