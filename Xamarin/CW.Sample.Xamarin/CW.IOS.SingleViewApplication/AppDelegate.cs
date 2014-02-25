using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace CW.IOS.SingleViewApplication
{
	// The UIApplicationDelegate for the application. This class is responsible for launching the
	// User Interface of the application, as well as listening (and optionally responding) to
	// application events from iOS.
	[Register ("AppDelegate")]
	public partial class AppDelegate : UIApplicationDelegate
	{
		// class-level declarations
		UIWindow window;
		CW_IOS_SingleViewApplicationViewController viewController;
		//
		// This method is invoked when the application has loaded and is ready to run. In this
		// method you should instantiate the window, load the UI into it and then make the window
		// visible.
		//
		// You have 17 seconds to return from this method, or iOS will terminate your application.
		//
		public override bool FinishedLaunching (UIApplication app, NSDictionary options)
		{
			window = new UIWindow (UIScreen.MainScreen.Bounds);
			
			viewController = new CW_IOS_SingleViewApplicationViewController ();
			window.RootViewController = viewController;
			window.MakeKeyAndVisible ();

			// check for a notification
			if (options != null)
			{
				// check for a local notification
				if (options.ContainsKey(UIApplication.LaunchOptionsLocalNotificationKey))
				{
					var localNotification = options[UIApplication.LaunchOptionsLocalNotificationKey] as UILocalNotification;
					if (localNotification != null)
					{
						new UIAlertView(localNotification.AlertAction, localNotification.AlertBody, null, "OK", null).Show();
						// reset our badge
						UIApplication.SharedApplication.ApplicationIconBadgeNumber = 0;
					}
				}
			}

			return true;
		}
        
	    public override void ReceivedLocalNotification(UIApplication application, UILocalNotification notification)
		{
			// show an alert
			new UIAlertView(notification.AlertAction, notification.AlertBody, null, "OK", null).Show();

			// reset our badge
			UIApplication.SharedApplication.ApplicationIconBadgeNumber = 0;
		}
	}
}

