# Solution

We hope you tried to complete this challenge with all your effort and enthusiasm.

If you did, and are still unable to complete the challenge, here you have the solution. We assume you have followed and completed the task 1 and task 2.

## Fixing the errors

1. By POST not by GET

When you were trying make a POST request from Xamarin App to your Azure Function, it answered you with a **404 not found**.

This error is allocated in the signature of the function. This Azure Function will only be triggered by a POST request, but in this code, we specified that the function only accepts GET requests.

Change it to this:

```c#
public static async Task<HttpResponseMessage> Run([HttpTrigger(AuthorizationLevel.Function, "post", Route = null)]HttpRequestMessage req)
```


2. Encoding.UTF8 not Encoding.UTF7

At this point, it seems that all your code is correct. However, when you try to make a request you get a 500 error, even if the JSON data is correct.

`SendToApi` is throwing 500, simply because the String Encoding is wrong.

Change it to this:


```c#
return await client.PostAsync(_uri, new StringContent(body, Encoding.UTF8, "application/json"));
```

3. Fix Binding on *UserDataPage.xaml.cs*

The BindingContext of the page is not configure so the bindings and commands that are invoked from the xaml code do not work. To solve this add as the last line of the *UserDataPage* constructor:

```c#
BindingContext = new UserDataViewModel();
```

4. Fix serialization issue

The model used to send the data to the Azure Function do not serialize the *Email* to Json. In User.cs class, locate the property *Email* and replace the

 ```c#
 [JsonIgnore]
 ```

with

 ```c#
 [JsonProperty(PropertyName="email")]
 ```

> Remember to deploy your Azure Function with the fixes!