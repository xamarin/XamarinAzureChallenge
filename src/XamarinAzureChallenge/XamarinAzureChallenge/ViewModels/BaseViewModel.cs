using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace XamarinAzureChallenge.ViewModels
{
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void SetAndRaisePropertyChanged<TRef>(ref TRef field, TRef value, Action onChanged = null, [CallerMemberName] string propertyName = "")
        {
            if (EqualityComparer<TRef>.Default.Equals(field, value))
                return;

            field = value;

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

            onChanged?.Invoke();
        }

        protected Task NavigateToPage(Page page) => MainThread.InvokeOnMainThreadAsync(() => Application.Current.MainPage.Navigation.PushAsync(page));

        protected Task NavigateBack() => MainThread.InvokeOnMainThreadAsync(() => Application.Current.MainPage.Navigation.PopAsync());

        protected Task ShowFeatureNotAvailable() => MainThread.InvokeOnMainThreadAsync(() => Application.Current.MainPage.DisplayAlert("Feature not available", "", "Ok"));

        protected Task DisplayInvalidFieldAlert(string message) => MainThread.InvokeOnMainThreadAsync(() => Application.Current.MainPage.DisplayAlert("Invalid Field(s)", message, "Ok"));
    }
}
