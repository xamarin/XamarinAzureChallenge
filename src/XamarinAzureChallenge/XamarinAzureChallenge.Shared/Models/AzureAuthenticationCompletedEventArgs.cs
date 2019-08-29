using System;
namespace XamarinAzureChallenge.Shared
{
    public class AzureAuthenticationCompletedEventArgs : EventArgs
    {
        public AzureAuthenticationCompletedEventArgs(bool isAuthenticationSuccessful) =>
            IsAuthenticationSuccessful = isAuthenticationSuccessful;

        public bool IsAuthenticationSuccessful { get; }
    }
}
