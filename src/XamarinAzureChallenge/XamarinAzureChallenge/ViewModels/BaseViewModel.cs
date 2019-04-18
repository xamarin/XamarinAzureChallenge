using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace XamarinAzureChallenge.ViewModels
{
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        private bool isBusy;

        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand FeatureNotAvailableCommand { get; } = new Command(async () => await ShowFeatureNotAvailableAsync());

        public bool IsBusy
        {
            get => isBusy;
            set => SetAndRaisePropertyChanged(ref isBusy, value);
        }

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

        protected Task NavigateToAsync(Page page, bool clearStack = false)
        {
            var tcs = new TaskCompletionSource<object>();

            Device.BeginInvokeOnMainThread(async () =>
            {
                if (clearStack)
                {
                    Application.Current.MainPage = new NavigationPage(page);
                }
                else
                {
                    await Application.Current.MainPage.Navigation.PushAsync(page);
                }

                tcs.SetResult(null);
            });

            return tcs.Task;
        }

        protected Task NavigateBackAsync()
        {
            var tcs = new TaskCompletionSource<object>();

            Device.BeginInvokeOnMainThread(async () =>
            {
                await Application.Current.MainPage.Navigation.PopAsync();
                tcs.SetResult(null);
            });

            return tcs.Task;
        }

        protected static async Task ShowFeatureNotAvailableAsync()
        {
            await Application.Current.MainPage.DisplayAlert(
                "Feature not available",
                "",
                "Ok");
        }
    }
}
