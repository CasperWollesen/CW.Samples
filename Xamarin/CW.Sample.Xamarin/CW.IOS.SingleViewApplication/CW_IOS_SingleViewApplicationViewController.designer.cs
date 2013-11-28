// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using MonoTouch.Foundation;
using System.CodeDom.Compiler;

namespace CW.IOS.SingleViewApplication
{
	[Register ("CW_IOS_SingleViewApplicationViewController")]
	partial class CW_IOS_SingleViewApplicationViewController
	{
		[Outlet]
		MonoTouch.UIKit.UIButton _addControlToDynamicViewButton { get; set; }	

		[Outlet]
		MonoTouch.UIKit.UIButton _clickOnMeButton { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIView _dynamicView { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIButton _notificationButton { get; set; }

		[Outlet]
		MonoTouch.UIKit.UITextField _notificationMesssageTextField { get; set; }

		[Outlet]
		MonoTouch.UIKit.UITextField _notificationTextField { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (_clickOnMeButton != null) {
				_clickOnMeButton.Dispose ();
				_clickOnMeButton = null;
			}

			if (_notificationButton != null) {
				_notificationButton.Dispose ();
				_notificationButton = null;
			}

			if (_notificationMesssageTextField != null) {
				_notificationMesssageTextField.Dispose ();
				_notificationMesssageTextField = null;
			}

			if (_notificationTextField != null) {
				_notificationTextField.Dispose ();
				_notificationTextField = null;
			}

			if (_dynamicView != null) {
				_dynamicView.Dispose ();
				_dynamicView = null;
			}

			if (_addControlToDynamicViewButton != null) {
				_addControlToDynamicViewButton.Dispose ();
				_addControlToDynamicViewButton = null;
			}
		}
	}
}
