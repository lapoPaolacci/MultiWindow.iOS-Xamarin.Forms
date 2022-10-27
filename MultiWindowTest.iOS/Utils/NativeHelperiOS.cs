using System.Collections.Generic;
using System.Threading.Tasks;
using CoreGraphics;
using MultiWindowTest.iOS.Utils;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(NativeHelperiOS))]
namespace MultiWindowTest.iOS.Utils
{
    public class NativeHelperiOS : INativeHelper
    {
        UIWindow windowToast = new UIWindow(UIApplication.SharedApplication.KeyWindow.Bounds);

        public void ShowToast(string message, double duration)
        {
            ShowToastView(message, duration);
        }

        public void ShowToastView(string text, double duration)
        {
            var tagToast = (System.nint)9132580124;

            if (windowToast.ViewWithTag(tagToast) == null)
            {
                var toastView = ReturnToastView(text);
                toastView.Tag = tagToast;

                AnimateToastView(toastView, duration);
            }
        }

        public UIView ReturnToastView(string text)
        {
            var width = windowToast.Frame.Size.Width;
            var height = windowToast.Frame.Size.Height;
            var correctWidth = Device.Idiom == TargetIdiom.Phone ? width - 20 : width / 3;
            var correctX = Device.Idiom == TargetIdiom.Phone ? 10 : width / 2 - correctWidth / 2;

            var toastView = new UIView(frame: new CGRect(correctX, height, correctWidth, 50));
            toastView.BackgroundColor = UIColor.DarkGray;
            toastView.Layer.CornerRadius = 12;
            toastView.TranslatesAutoresizingMaskIntoConstraints = true;

            var label = new UILabel(frame: toastView.Frame);
            label.Text = text;
            label.TextColor = UIColor.White;
            label.Font = UIFont.SystemFontOfSize(13);
            label.Lines = 0;
            label.TranslatesAutoresizingMaskIntoConstraints = false;

            toastView.AddSubview(label);

            var icon = UIImage.GetSystemImage("info.circle").ImageWithRenderingMode(UIImageRenderingMode.AlwaysTemplate);
            var imageView = new UIImageView(frame: toastView.Frame);

            imageView.Image = icon;
            imageView.TintColor = UIColor.White;
            imageView.TranslatesAutoresizingMaskIntoConstraints = false;
            toastView.AddSubview(imageView);

            label.LeftAnchor.ConstraintEqualTo(imageView.RightAnchor, 8).Active = true;
            label.CenterYAnchor.ConstraintEqualTo(toastView.CenterYAnchor).Active = true;
            label.HeightAnchor.ConstraintEqualTo(40).Active = true;
            label.WidthAnchor.ConstraintEqualTo(toastView.Frame.Size.Width).Active = true;

            imageView.LeftAnchor.ConstraintEqualTo(toastView.LeftAnchor, 12).Active = true;
            imageView.CenterYAnchor.ConstraintEqualTo(toastView.CenterYAnchor).Active = true;
            imageView.HeightAnchor.ConstraintEqualTo(25).Active = true;
            imageView.WidthAnchor.ConstraintEqualTo(25).Active = true;


            toastView.UserInteractionEnabled = false;

            windowToast.WindowLevel = UIWindowLevel.Alert;
            windowToast.AddSubview(toastView);
            windowToast.MakeKeyAndVisible();
            windowToast.Hidden = false;
            windowToast.UserInteractionEnabled = false;


            return toastView;
        }

        public int CalcNumberOfLines(UILabel label)
        {
            var maxSize = new CGSize(width: label.Frame.Size.Width, height: 1000);
            var textHeight = label.SizeThatFits(maxSize).Height;
            var lineHeight = label.Font.LineHeight;
            return (int)(textHeight / lineHeight);
        }

        public async void AnimateToastView(UIView view, double duration)
        {
            var oldFrame = view.Frame;
            UIView.Animate(0.3, () =>
            {
                view.Frame = new CGRect(oldFrame.X, oldFrame.Y - 100, oldFrame.Width, 50);
            });
            await Task.Delay((int)duration * 1000);
            UIView.Animate(0.3, () =>
            {
                view.Frame = new CGRect(oldFrame.X, oldFrame.Y, oldFrame.Width, 50);
            });
            await Task.Delay(300);
            view.RemoveFromSuperview();
            windowToast.Hidden = true;
        }
    }
}