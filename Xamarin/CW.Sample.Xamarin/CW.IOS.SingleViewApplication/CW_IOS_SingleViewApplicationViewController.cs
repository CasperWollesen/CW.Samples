using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace CW.IOS.SingleViewApplication
{
	public partial class CW_IOS_SingleViewApplicationViewController : UIViewController
	{
		static bool UserInterfaceIdiomIsPhone {
			get { return UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone; }
		}

		public CW_IOS_SingleViewApplicationViewController ()
			: base (UserInterfaceIdiomIsPhone ? "CW_IOS_SingleViewApplicationViewController_iPhone" : "CW_IOS_SingleViewApplicationViewController_iPad", null)
		{
		}

		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();
			
			// Release any cached data, images, etc that aren't in use.
		}

	    int _counter;
		System.Threading.Thread _thread;
		void ActionClickOnMeTouchDown (object sender, EventArgs e)
		{
			if (_thread == null) {
				ThreadStart ();
			} else {
				ThreadStop ();
			}
		}

		void ThreadStop()
		{
			_threadActive = false;
			_thread = null;
		}

		void ThreadStart()
		{
			_thread = new System.Threading.Thread (ActionThread);
			_thread.IsBackground = true;
			_thread.Start ();
		}

		bool _threadActive;
		void ActionThread()
		{
			_threadActive = true;
			while (_threadActive) {

				InvokeOnMainThread (() => {
						_counter++;
						_clickOnMeButton.SetTitle (_counter.ToString(), UIControlState.Normal);
					});

				System.Threading.Thread.Sleep (1000);
			}
		}

        

	    public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			
			// Perform any additional setup after loading the view, typically from a nib.
			_clickOnMeButton.TouchDown += ActionClickOnMeTouchDown;
			_notificationButton.TouchDown += ActionNotificationTouchDown;
			_addControlToDynamicViewButton.TouchDown += ActionAddToDynamicViewTouchDown;
		}

		int _dynamicButtonCount = 0;
		void ActionAddToDynamicViewTouchDown (object sender, EventArgs e)
		{
			var btn = new UIButton (UIButtonType.System);
			btn.TouchDown += HandleTouchDown;
			btn.SetTitle ("Dynamic Button", UIControlState.Normal);
			btn.Frame = new RectangleF (0, (30 * _dynamicButtonCount), 300, 30);
			_dynamicView.AddSubview ( btn );

			_dynamicButtonCount++;
		}

		int _notificationCount;
		void ActionNotificationTouchDown (object sender, EventArgs e)
		{
			int seconds;
			if(int.TryParse(_notificationTextField.Text, out seconds))
			{
				// create the notification
				var notification = new UILocalNotification();

				// set the fire date (the date time in which it will fire)
				notification.FireDate = DateTime.Now.AddSeconds(seconds);

				// configure the alert stuff
				notification.AlertAction = "Time for question";
				notification.AlertBody = _notificationMesssageTextField.Text + "(" + ++_notificationCount + ")";

				// modify the badge
				notification.ApplicationIconBadgeNumber = 1;

				// set the sound to be the default sound
				notification.SoundName = UILocalNotification.DefaultSoundName;

				// schedule it
				UIApplication.SharedApplication.ScheduleLocalNotification(notification);

				_notificationButton.SetTitle("Success (" + _notificationCount + ")", UIControlState.Normal);
			}
			else
			{
				_notificationButton.SetTitle ("Failed", UIControlState.Normal);
			}
		}

		void HandleTouchDown (object sender, EventArgs e)
		{
			var uiButton = sender as UIButton;
			if(uiButton != null)
			{
				uiButton.SetTitle (DateTime.Now.ToString (), UIControlState.Normal);
			}
		}
	}
}

