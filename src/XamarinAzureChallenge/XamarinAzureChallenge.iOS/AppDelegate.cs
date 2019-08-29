using System;
using System.Threading.Tasks;
using Foundation;
using SafariServices;
using UIKit;
using Xamarin.Forms;
using XamarinAzureChallenge.ViewModels;

namespace XamarinAzureChallenge.iOS
{
    [Register(nameof(AppDelegate))]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        public override bool FinishedLaunching(UIApplication uiApplication, NSDictionary launchOptions)
        {
            global::Xamarin.Forms.Forms.Init();
            LoadApplication(new App());

            return base.FinishedLaunching(uiApplication, launchOptions);
        }

        public override bool OpenUrl(UIApplication app, NSUrl url, NSDictionary options)
        {
            var callbackUri = new Uri(url.AbsoluteString);

            HandleCallbackUri(callbackUri);

            return true;
        }

        async void HandleCallbackUri(Uri callbackUri)
        {
            await CloseSFSafariViewController();
            await AzureAuthenticationService.AuthorizeSession(callbackUri);
        }

        async Task CloseSFSafariViewController()
        {
            while (await GetVisibleViewController() is SFSafariViewController sfSafariViewController)
            {
                await Device.InvokeOnMainThreadAsync(async () =>
                {
                    await sfSafariViewController.DismissViewControllerAsync(true);
                    sfSafariViewController.Dispose();
                    sfSafariViewController = null;
                });
            }
        }

        Task<UIViewController> GetVisibleViewController()
        {
            return Device.InvokeOnMainThreadAsync(() =>
            {
                var rootController = UIApplication.SharedApplication.KeyWindow.RootViewController;

                switch (rootController.PresentedViewController)
                {
                    case UINavigationController navigationController:
                        return navigationController.TopViewController;
                    case UITabBarController tabBarController:
                        return tabBarController.SelectedViewController;
                    case null:
                        return rootController;
                    default:
                        return rootController.PresentedViewController;
                }
            });
        }
    }
}
