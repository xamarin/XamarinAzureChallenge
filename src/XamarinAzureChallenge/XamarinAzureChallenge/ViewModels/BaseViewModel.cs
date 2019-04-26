using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace XamarinAzureChallenge.ViewModels
{
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void SetAndRaisePropertyChanged<TRef>(
            ref TRef field, TRef value, [CallerMemberName] string propertyName = null)
        {
            field = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected void SetAndRaisePropertyChangedIfDifferentValues<TRef>(
            ref TRef field, TRef value, [CallerMemberName] string propertyName = null)
            where TRef : class
        {
            if (EqualityComparer<TRef>.Default.Equals(field, value))
                return;

            SetAndRaisePropertyChanged(ref field, value, propertyName);
        }

        protected Task NavigateToPage(Page page)
        {
            return RunOnUIThread(async () =>
                await Application.Current.MainPage.Navigation.PushAsync(page));
        }

        protected Task NavigateBack()
        {
            return RunOnUIThread(async () =>
                await Application.Current.MainPage.Navigation.PopAsync());
        }

        protected Task ShowFeatureNotAvailable()
        {
            return RunOnUIThread(async () =>
                await Application.Current.MainPage.DisplayAlert("Feature not available", "", "Ok"));
        }

        protected Task DisplayInvalidFieldAlert(string message)
        {
            return RunOnUIThread(async () =>
                await Application.Current.MainPage.DisplayAlert("Invalid Field", message, "Ok"));
        }

        protected Task RunOnUIThread(System.Action action)
        {
            var tcs = new TaskCompletionSource<object>();

            Device.BeginInvokeOnMainThread(() =>
            {
                try
                {
                    action?.Invoke();
                    tcs.SetResult(null);
                }
                catch (System.Exception e)
                {
                    tcs.SetException(e);
                }
            });

            return tcs.Task;
        }
    }
}
