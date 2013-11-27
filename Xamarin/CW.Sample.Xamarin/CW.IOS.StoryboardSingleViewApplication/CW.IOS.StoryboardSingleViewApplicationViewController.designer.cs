// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using MonoTouch.Foundation;
using System.CodeDom.Compiler;

namespace CW.IOS.StoryboardSingleViewApplication
{
	[Register ("CW_IOS_StoryboardSingleViewApplicationViewController")]
	partial class CW_IOS_StoryboardSingleViewApplicationViewController
	{
		[Outlet]
		MonoTouch.UIKit.UIButton _updateButton { get; set; }

		[Outlet]
		MonoTouch.UIKit.UITextField _updateTextField { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (_updateTextField != null) {
				_updateTextField.Dispose ();
				_updateTextField = null;
			}

			if (_updateButton != null) {
				_updateButton.Dispose ();
				_updateButton = null;
			}
		}
	}
}
