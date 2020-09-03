using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Media;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace BasketballCounterAndroid
{
	class AudioFocusManager : Activity, AudioManager.IOnAudioFocusChangeListener
	{
		private Context context;

		public AudioFocusManager(Context context)
		{
			this.context = context;
			RequestAudioFocus();
		}
		public bool RequestAudioFocus()
		{
			var audioManager = (AudioManager) context.GetSystemService(Context.AudioService);
			AudioFocusRequest audioFocusRequest = audioManager.RequestAudioFocus(this, Stream.Music, AudioFocus.Gain);

			if (audioFocusRequest == AudioFocusRequest.Granted)
			{
				System.Diagnostics.Debug.WriteLine("REQUESTED AND RECIEVED");
				return true;
			}
			System.Diagnostics.Debug.WriteLine("REQUESTED AND REJECTED");
			return false;
		}

		public void OnAudioFocusChange([GeneratedEnum] AudioFocus focusChange)
		{
			System.Diagnostics.Debug.WriteLine("AUDIO FOCUS SWITCH");
			switch (focusChange)
			{
				case AudioFocus.Gain:
					System.Diagnostics.Debug.WriteLine("GOT IT");
					//Gain when other Music Player app releases the audio service   

					break;
				case AudioFocus.Loss:
					System.Diagnostics.Debug.WriteLine("LOST IT");
					//We have lost focus stop!   

					break;
				case AudioFocus.LossTransient:
					System.Diagnostics.Debug.WriteLine("LOST IT, BUT BRB");
					//We have lost focus for a short time, but likely to resume so pause   

					break;
				case AudioFocus.LossTransientCanDuck:
					System.Diagnostics.Debug.WriteLine("DUCKING");
					//We have lost focus but should till play at a muted 10% volume   

					break;
			}
		}
	}
}