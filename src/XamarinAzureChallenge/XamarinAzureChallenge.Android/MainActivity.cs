using System;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using XamarinAzureChallenge.Pages;

namespace XamarinAzureChallenge.Droid
{
    [Activity(Label = "XamarinAzureChallenge", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ScreenOrientation = ScreenOrientation.Portrait)]
    [IntentFilter(new string[] { Intent.ActionView }, Categories = new[] { Intent.CategoryDefault, Intent.CategoryBrowsable }, DataSchemes = new[] { "xamarinazurechallenge" })]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            global::Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);

            LoadApplication(new App());

            if (Intent?.Data is Android.Net.Uri callbackUri)
                ExecuteCallbackUri(callbackUri);
        }

        async void ExecuteCallbackUri(Android.Net.Uri callbackUri)
        {
            if (Xamarin.Forms.Application.Current.MainPage is Xamarin.Forms.NavigationPage navigationPage)
            {
                navigationPage.Pushed += HandlePushed;

                await navigationPage.PushAsync(new UserDataPage());

                async void HandlePushed(object sender, Xamarin.Forms.NavigationEventArgs e)
                {
                    if (e.Page is UserDataPage)
                    {
                        navigationPage.Pushed -= HandlePushed;

                        await AzureAuthenticationService.AuthorizeSession(new Uri(callbackUri.ToString()));
                    }
                }
            }
        }
    }
}