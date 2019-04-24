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

        protected BaseViewModel()
        {
            FeatureNotAvailableCommand = new Command(async () => await ShowFeatureNotAvailable());
        }

        public ICommand FeatureNotAvailableCommand { get; }

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

        protected Task NavigateToPage(Page page, bool clearStack = false)
        {
            return RunOnUIThread(async () =>
            {
                if (clearStack)
                    Application.Current.MainPage = new NavigationPage(page);
                else
                    await Application.Current.MainPage.Navigation.PushAsync(page);
            });
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

        protected Task DisplayAlert(string message)
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
