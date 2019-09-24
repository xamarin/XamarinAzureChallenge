# Xamarin Azure Challenge

Welcome to the Xamarin Azure Challenge!

The goal is to create a serverless [Azure Function](https://azure.microsoft.com/services/functions?WT.mc_id=xamarinazurechallenge-github-bramin) and connect it to a [Xamarin](https://dotnet.microsoft.com/apps/xamarin?WT.mc_id=xamarinazurechallenge-github-bramin) mobile app.


### Challenge Info

The challenge begins 23 September 2019, and ends at 2359 PT on 23 October 2019.

- Six (6) Grand Prizes:
  - Each winner will receive Microsoft Surface Headphones

- One Thousand (1,000) Prizes:
  - Each winner will receive a 3-month Xbox Game Pass
  
Learn more about the challenge: https://github.com/xamarin/XamarinAzureChallenge/blob/master/TermsAndConditions.md


### Challenge Objectives

1. Create and publish a serverless [Azure Function](https://azure.microsoft.com/services/functions?WT.mc_id=xamarinazurechallenge-github-bramin)
2. Add the Azure Function url to the [Xamarin.Forms](https://docs.microsoft.com/xamarin/xamarin-forms?WT.mc_id=xamarinazurechallenge-github-bramin) application
3. Submit your entry from the Xamarin.Forms application to your Azure Function

## Task 0: Prerequisites

1. Create Azure Suscription
    * If you do not currently have an Azure subscription, sign up for a [free Azure account](https://azure.microsoft.com/free?WT.mc_id=xamarinazurechallenge-github-bramin) that includes a $200 Azure Credit
2. Install Visual Studio + Xamarin Tools
    * On PC, [follow these steps to install Visual Studio with Xamarin](https://docs.microsoft.com/xamarin/get-started/installation/windows?WT.mc_id=xamarinazurechallenge-github-bramin)
    * On Mac, [follow these steps to install Visual Studio for Mac with Xamarin](https://docs.microsoft.com/visualstudio/mac/installation?view=vsmac-2019&WT.mc_id=xamarinazurechallenge-github-bramin)


## Task 1: Create an Azure Function resource in Azure

### 1. Retrieve Source Code

We have two options to retrieve the code for the Xamarin Azure Challenge:
* [Clone the repository](#1a-clone-the-repository)
* [Download the source code](#1b-download-source-code)

#### 1a. Clone the repository

To [clone](https://git-scm.com/docs/git-clone) this repository, run this command in your favorite terminal:

``` bash
git clone https://github.com/xamarin/XamarinAzureChallenge.git
```

#### 1b. Download Source Code

To download the source clode, click this link: https://github.com/xamarin/XamarinAzureChallenge/archive/master.zip

> **Note:** On a PC, save the source code in a folder close to your root directory, e.g. `C:\Code\`. If the folder path is too many characters long, you may get errors while publishing the code to the Azure Functions.

### 2. Publish Azure Function

After cloning the repository, we have 3 options to create our Azure Function:

1. Use [Visual Studio on PC](#2a-use-visual-studio-on-pc)
    - [Learn More](https://blogs.msdn.microsoft.com/benjaminperkins/2018/04/05/deploy-an-azure-function-created-from-visual-studio?WT.mc_id=xamarinazurechallenge-github-bramin)
2. Use [Visual Studio for Mac](#2b-use-visual-studio-for-mac)
    - [Learn More](https://docs.microsoft.com/visualstudio/mac/publish-app-svc?WT.mc_id=xamarinazurechallenge-github-bramin)
3. Use [Azure CLI](#2c-use-azure-cli)
    - [Learn More](https://docs.microsoft.com/azure/azure-functions/functions-create-first-azure-function-azure-cli?WT.mc_id=xamarinazurechallenge-github-bramin)

#### 2a. Use Visual Studio on PC

1. In Visual Studio on PC, open `XamarinAzureChallenge.sln`

2. In Visual Studio, in the top toolbar, select **File** > **Account Settings...**

![Account Settings...](https://user-images.githubusercontent.com/13558917/65535259-eab9c200-deb5-11e9-823c-17d4f49769bc.png)

3. In the **Account Settings** window, select **Add an account...**

> **Note:** If your Azure account is already visible, you may skip this step

![Add an account](https://user-images.githubusercontent.com/13558917/65535258-ea212b80-deb5-11e9-84a8-c2c722f6b64a.png)

4. In the **Sign in to your account** pop-up, log in to your Azure account

> **Note:** If your Azure account was already visible, you may skip this step

![Sign in to your account](https://user-images.githubusercontent.com/13558917/65535257-ea212b80-deb5-11e9-9ad6-a33bfea6b534.png)

5. In the **Account Settings** window, select **Close**

![Close](https://user-images.githubusercontent.com/13558917/65535255-ea212b80-deb5-11e9-9748-6e646c032028.png)

6. In Visual Studio, in the **Solution Explorer**, right-click on **Backend** >  **XamarinAzureChallenge.Functions**

7. In the right-click menu, select in **Publish**

![publish](https://user-images.githubusercontent.com/13558917/65265720-82e33000-dadf-11e9-9a23-d910ac159790.png)

8. In the **Pick a publish target** window, select the following: 

- **Azure Functions Consumption Plan**
    - [x] **Create New**

9. In the **Pick a publish target** window, select **Publish**

![Pick a publish target](https://user-images.githubusercontent.com/13558917/65265726-84145d00-dadf-11e9-9c14-716aaf2e3f18.png)

10. In the **Create New** window, enter the following information:
- **Name:** XamarinAzureChallenge-[Your Name]
    - **Note:** Replace `[Your Name]` with your name to ensure the Azure Function name is unique 
    - In this example, I'm using `XamarinAzureChallenge-Brandon`
- **Subscription:** [Select your Azure Subscription]
- **Resource Group**
    - **New**
        - **New resource group name**: XamarinAzureChallenge
        - **OK**
- **Location:** [Select the Azure Data Center Closest To You]
- **Azure Storage**
    - **New**
        - **Account Name:** xamarinazure[Your Name]
            - **Note:** Replace `[Your Name]` with your name to ensure the Azure Storage name is unique 
            - In this example, I'm using "xamarinazurebrandon"
        - **Location:** [Select the Azure Data Center Closest To You]
        - **Account type:** Standard - Locally Redundant Storage
        - **OK**

11. In the **Create New** window, click **Create** 

![Create New App Service](https://user-images.githubusercontent.com/13558917/65271517-52a18e80-daeb-11e9-8854-972bce47134e.png)

12. Standby while Visual Studio creates our Azure Function resources

![Deploying](https://user-images.githubusercontent.com/13558917/65271519-533a2500-daeb-11e9-9a54-3a4b6afad613.png)

13. In the **Publish** window, to push our code to the newly created Azure Function, select **Publish**

![Publish](https://user-images.githubusercontent.com/13558917/65555679-24082700-dee2-11e9-9bcb-746b9f03470b.png)

14. In the **Visual Studio Toolbar**, select **View** > **Output**

![Output](https://user-images.githubusercontent.com/13558917/65556074-3afb4900-dee3-11e9-8b64-e41bf4e2a8f2.png)

15. In the **Output** window, ensure it says `Publish: 1 succeeded`

![Publish Succeeded](https://user-images.githubusercontent.com/13558917/65556072-3a62b280-dee3-11e9-982a-9e313b2ac34d.png)

#### 2b. Use Visual Studio for Mac

1. In Visual Studio for Mac, open `XamarinAzureChallenge.sln`

2. In the **Apple Menu Bar**, select **Visual Studio** > **Account...**

![Visual Studio Accounts](https://user-images.githubusercontent.com/13558917/65533492-94974f80-deb2-11e9-9ea1-045ec4b2aa0a.png)

3. In the **Account** window, click **Add...**

> **Note:** If your Azure account is already visible, you may skip this step

![Add Account](https://user-images.githubusercontent.com/13558917/65533509-9e20b780-deb2-11e9-888c-cffb8ef6c48b.png)

4. In the  **Sign in to your account** pop-up, sign into your Azure account

> **Note:** If your Azure account was already visible, you may skip this step

![Sign in to your account](https://user-images.githubusercontent.com/13558917/65533966-7b42d300-deb3-11e9-87bc-d0820f879ec5.png)


5. After logging into your Azure Account, in the **Account** window, select the red close button, **X**

![Close Accounts](https://user-images.githubusercontent.com/13558917/65534155-c1983200-deb3-11e9-99de-b9e0b5049f75.png)

6. In Visual Studio for Mac, in the **Solution Explorer**, right-click on **Backend** > **XamarinAzureChallenge.Functions**

7. In the right-click menu, select **Publish** > **Publish to Azure...**

![Publish to Azure](https://user-images.githubusercontent.com/13558917/65273393-1112e280-daef-11e9-9555-c3d47582ab18.png)

8. In the **Publish to Azure App Service** window, select your Azure Account

9. In the **Publish to Azure App Service** window, select **New**

![Create New App Service](https://user-images.githubusercontent.com/13558917/65275675-276f6d00-daf4-11e9-9dac-3b23c50964cc.png)

10. In the **Create New App Service on Azure** window, enter the following information:

- **App Service Name:** XamarinAzureChallenge-[Your Name]
    - **Note:** Replace `[Your Name]` with your name to ensure the App Service Name is unique 
    - In this example, I'm using `XamarinAzureChallenge-Brandon`
- **Subscription:** [Select your Azure Subscription]
- **Resource Group**
    - Click the **+** symbol
    - XamarinAzureChallenge
- **Service Plan:** 
    - [x] Custom
- **Plan Name:** XamarinAzureChallenge
- **Region:** [Select the Azure Data Center Closest to you]
- **Pricing:** Consumption

11. In the **Create New App Service on Azure** window, select **Next**

![Create New App Service](https://user-images.githubusercontent.com/13558917/65282499-09f5cf80-db03-11e9-9cd5-0fe898ceb173.png)

12. In the **Configure Storage Account** window, enter the following information:

- **Storage Account:** 
    - [x] Custom
- **Account Name:** xamarinazure[Your Name]
    - **Note:** Replace `[Your Name]` with your name to ensure the Storage Account Name is unique 
    - In this example, I'm using "xamarinazurebrandon"
- **Account Type:** Standard - Locally Redundant Storage

13. In the **Configure Storage Account** window, select **Create**

![Configure Storage Account](https://user-images.githubusercontent.com/13558917/65274742-0c9bf900-daf2-11e9-90af-da04b5539d75.png)

14. In the **Create Azure App Service** pop up, select **OK**

![OK](https://user-images.githubusercontent.com/13558917/65279318-f135eb80-dafb-11e9-97d3-351a247348ee.png)

15. In Visual Studio, in the menu bar, select **View** > **Pads** > **Azure**

![Azure Pad](https://user-images.githubusercontent.com/13558917/65274737-0b6acc00-daf2-11e9-85bd-552ff9b01049.png)

16. In Visual Studio, in the **Azure** pad, ensure the code is **Deploying...**

![Deploying](https://user-images.githubusercontent.com/13558917/65274736-0ad23580-daf2-11e9-94e6-531d22f2e0d1.png)

17. Stand by while Visual Studio for Mac publishes our code to our Azure Function

#### 2c. Use Azure CLI

> As a prerequisite, you must install [Azure Core Tools version 2.x](https://docs.microsoft.com/azure/azure-functions/functions-run-local?WT.mc_id=xamarinazurechallenge-github-bramin#v2) and [Azure CLI](https://docs.microsoft.com/cli/azure/install-azure-cli?WT.mc_id=xamarinazurechallenge-github-bramin). 
>
> Alternatively, if you do not wish to install these tools locally, you can use these CLI tools pre-installed in a browser via the [Azure Cloud Shell](https://shell.azure.com/bash?WT.mc_id=xamarinazurechallenge-github-bramin).

1. Open the terminal
  - [How to open the macOS Terminal](https://macpaw.com/how-to/use-terminal-on-mac)
  - [How to open the Windows Terminal](https://www.quora.com/How-do-I-open-terminal-in-windows)

2. In the terminal, enter the following command to login into Azure CLI:

```bash
az login
```
> **Note:** Stand by until the Azure CLI opens your browser to the Azure Login page

3. In the automatically-opened browser window, on the Azure Login page, log into your Azure Account

4. In the terminal, enter the following command to create a new Azure Resource Group:

```bash
az group create --name XamarinAzureChallenge --location westeurope
```

> **Note:** If you have more than one subscription you will need to especify the subscription in which the resource group will be created using `--subscription [your Azure Subscription ID]`
> 
> [How to find your Azure Subscription ID ](https://blogs.msdn.microsoft.com/mschray/2016/03/18/getting-your-azure-subscription-guid-new-portal?WT.mc_id=xamarinazurechallenge-github-bramin)

5. In the terminal, enter the following command to create a new Azure Storage Account:

```bash
az storage account create --name xamarinazure[Your Name] --location westeurope --resource-group XamarinAzureChallenge --sku Standard_LRS
```
> **Note:** Replace `[Your Name]` with your name to ensure the storage name is unique
>
> **Note:** If you have more than one subscription you will need to especify the subscription in which the resource group will be created using `--subscription [your Azure Subscription ID]`
> 
> [How to find your Azure Subscription ID ](https://blogs.msdn.microsoft.com/mschray/2016/03/18/getting-your-azure-subscription-guid-new-portal?WT.mc_id=xamarinazurechallenge-github-bramin)

6. In the terminal, enter the following command to create a function app:

```bash
az functionapp create --resource-group XamarinAzureChallenge --consumption-plan-location westeurope --name XamarinAzureChallenge-[Your Name] --storage-account  xamarinazure[Your Name] --runtime dotnet
```
> **Note:** Replace `[Your Name]` with your name to ensure the functionapp name is unique 
>
> **Note:** For `--storage-account`, use the storage account created in the previous step
>
> **Note:** If you have more than one subscription you will need to especify the subscription in which the resource group will be created using `--subscription [your Azure Subscription ID]`
> 
> [How to find your Azure Subscription ID ](https://blogs.msdn.microsoft.com/mschray/2016/03/18/getting-your-azure-subscription-guid-new-portal?WT.mc_id=xamarinazurechallenge-github-bramin)

> **Note:** The Azure Function name must be unique, which is why we append our name

7. In the terminal, enter the following command to navigate to the project folder `XamarinAzureChallenge.Functions` project folder.

- Windows
    - `cd [Your Path to XamarinAzureChallengeSource Code]\src\XamarinAzureChallenge\XamarinAzureChallenge.Functions`
- macOS
    - `cd [Your Path to XamarinAzureChallengeSource Code]/src/XamarinAzureChallenge/XamarinAzureChallenge.Functions`

8.  In the terminal, enter the following command to publish our code to our Azure Function:

```bash
func azure functionapp publish XamarinAzureChallenge-[Your Name]
```
> **Note:** Replace `[Your Name]` with your name, using the same account name [created earlier](#2b-use-azure-cli)


## Task 2: Configure Azure Function

Before our Azure Function can submit our entry for the XamarinAzureChallenge, we'll need to configure a few items.

### 1. Add Submission Url to Environment Variables

Azure Functions allow us to store Environment Variables in the cloud that our Functions app can retrieve at runtime. In this step, we'll add the submission url as an Environment Variable.

1. In your browser, navigate to the [Azure Portal](http://portal.azure.com?WT.mc_id=xamarinazurechallenge-github-bramin) 

2. In the Azure Portal, on the left-hand menu, select the cube-shaped **Resource Groups** icon

![Resource Groups Icon](https://user-images.githubusercontent.com/13558917/65279226-bb910280-dafb-11e9-8691-68c08204a84e.png)

3. In the **Resource Groups** window, in the filter box, enter `XamarinAzureChallenge`

4. In the **Resource Groups** window, select the **XamarinAzureChallenge** Resource Group

![XamarinAzureChallenge Resource Group](https://user-images.githubusercontent.com/13558917/65279225-baf86c00-dafb-11e9-9536-549e24b4ae24.png)

5. In the **XamarinAzureChallenge Resource Group**, select the function app **XamarinAzureChallenge-[Your Name]** 

![Open Function App](https://user-images.githubusercontent.com/13558917/65279223-ba5fd580-dafb-11e9-8144-431d8cfad5f0.png)

6. In the XamarinAzureChallenge Function, in the **Configured features** frame, select **Configuration**

![Configuration](https://user-images.githubusercontent.com/13558917/65397032-a3beb600-dd69-11e9-9204-13e4f4cbd9c2.png)

7. In the **Application Settings** window, select **+ New application setting** 

![New Application Setting](https://user-images.githubusercontent.com/13558917/65397028-a3261f80-dd69-11e9-8ab0-347c39e6f612.png)

8. In the **Add/Edit application setting** window, enter the following data:
- **Name:** END_POINT
- **Value:** https://xamarinazurechallenge-backend.azurewebsites.net/api/SubmitChallengeResult

9. In the **Add/Edit application setting** window, select **OK**

![Add/Edit Application Setting](https://user-images.githubusercontent.com/13558917/65477931-c241b180-de3c-11e9-9111-2fe516ef4a5a.png)

10. In the **Add/Edit application setting** window, select the **Save**

![Save Application Settings](https://user-images.githubusercontent.com/13558917/65440738-0c477a80-ddde-11e9-81b2-9c1f5f4bb4cb.png)

11. Stand by while the settings are saved

![Updating App Settings](https://user-images.githubusercontent.com/13558917/65441097-aefff900-ddde-11e9-9456-e638a77efc5c.png)

12. In the **Add/Edit application setting** window, select the close button, **X**

![Close Application Settings](https://user-images.githubusercontent.com/13558917/65399076-f3a47980-dd77-11e9-9e21-e1d3abda31ad.jpg)


### 2. Enable Access Control (IAM)

To ensure a valid submisison, the Function App will verify the Azure Subscription ID using [Managed Identity](https://docs.microsoft.com/azure/app-service/overview-managed-identity#adding-a-system-assigned-identity) & [Access Control](https://docs.microsoft.com/azure/role-based-access-control/role-assignments-portal#overview-of-access-control-iam?WT.mc_id=xamarinazurechallenge-github-bramin). Let's enable both.

1. In the **XamarinAzureChallenge** window, select **Platform features**

![Platform Features](https://user-images.githubusercontent.com/13558917/65397766-ff8c3d80-dd6f-11e9-98d4-c8640da32e37.png)

2. In the **Platform features** window, select **Identity**

![Identity](https://user-images.githubusercontent.com/13558917/65397882-db7d2c00-dd70-11e9-82a7-d1dfe8351574.png)

3. In the **Identity** window, in the **System assigned** tab, set the **Status** to **On**

4. In the **Identity** window, select **Save**

![System assigned identity](https://user-images.githubusercontent.com/13558917/65441645-9b08c700-dddf-11e9-9e30-6f23f171eac8.png)

5. In the confirmation popup, select **Yes**

![Confirmation Popup](https://user-images.githubusercontent.com/13558917/65478063-22d0ee80-de3d-11e9-9f8e-467e8d1e2113.png)

6. Stand by while System Assigned Identity is enabled

![Enable Identity](https://user-images.githubusercontent.com/13558917/65441491-541ad180-dddf-11e9-9646-ced9576f9178.png)

7. In the **Identity** window, click the close button, **X**

![Close Identity](https://user-images.githubusercontent.com/13558917/65397024-a28d8900-dd69-11e9-8080-a83a78429035.png)

8. In the **Platform features** tab, select **Access control (IAM)**

![IAM](https://user-images.githubusercontent.com/13558917/65398031-eedcc700-dd71-11e9-9610-ec28e148b58d.png)

9. In the **Access Control** window, select **+Add** > **Add role assignment**

![Add role assignment](https://user-images.githubusercontent.com/13558917/65397022-a1f4f280-dd69-11e9-93a3-7b9172cddff3.png)

10. In the right-hand fly-out menu **Add role assignment**, make the following selections:
- **Role:** Owner
- **Assign Access to:** Azure AD user, group or service principal
- **Select:** xamarinazurechallenge

11. In the right-hand fly-out menu **Add role assignment**, select **XamarinAzureChallenge-[Your Name]**

![Add role assignement, 1](https://user-images.githubusercontent.com/13558917/65398164-f486dc80-dd72-11e9-9e5c-8ce2b3672ccb.png)

12. In the right-hand fly-out menu **Add role assignment**, select **Save**

![Save role assignment](https://user-images.githubusercontent.com/13558917/65398260-6eb76100-dd73-11e9-894a-8bb75e93a406.png)

## Task 3: Configure the Xamarin App

### 1. Retrieve Azure Function URL

After publishing our Azure Function, we are ready to configure our Xamarin app with our Azure Function's URL.

1. In your browser, navigate to the [Azure Portal](http://portal.azure.com?WT.mc_id=xamarinazurechallenge-github-bramin) 

2. In the Azure Portal, on the left-hand menu, select the cube-shaped **Resource Groups** icon

![Resource Groups Icon](https://user-images.githubusercontent.com/13558917/65279226-bb910280-dafb-11e9-8691-68c08204a84e.png)

3. In the **Resource Groups** window, in the filter box, enter `XamarinAzureChallenge`

4. In the **Resource Groups** window, select the **XamarinAzureChallenge** Resource Group

![XamarinAzureChallenge Resource Group](https://user-images.githubusercontent.com/13558917/65279225-baf86c00-dafb-11e9-9536-549e24b4ae24.png)

5. In the **XamarinAzureChallenge Resource Group**, select the function app **XamarinAzureChallenge-[Your Name]**

![Open Function App](https://user-images.githubusercontent.com/13558917/65279223-ba5fd580-dafb-11e9-8144-431d8cfad5f0.png)

6. In the **Function Apps** window, select **XamarinAzureChallenge** > **Functions** > **SubmitChallengeFunction**

![Submit Challenge Function](https://user-images.githubusercontent.com/13558917/65282159-3a893980-db02-11e9-8331-57a34cfab44a.png)

7. In the **SubmitChallengeFunction** window, select **Get function url**

![Get function url](https://user-images.githubusercontent.com/13558917/65279218-b8961200-dafb-11e9-8634-f33db35197e7.png)

8. In the **Get function url** window, select **Copy**

![Copy](https://user-images.githubusercontent.com/13558917/65279215-b7fd7b80-dafb-11e9-9e2a-badb0f869cb2.png)

### 2. Add the Azure Function URL to Xamarin App

1. In Visual Studio, open `XamarinAzureChallenge.sln`

2. In Visual Studio, in the Solution Explorer, open **Mobile** > **XamarinAzureChallenge** > **ViewModels** > **UserDataViewModel.cs**

3. In the **UserDataViewModel** editor, comment out the `#error` compiler directive by prepending `//`:

```csharp
//#error Missing Azure Function Endpoint Url. Replace "Enter Your Function API Url Here" with your Azure Function Endopint Url
```

4. In the **UserDataViewModel** editor, paste your Azure Function Url to `private const string endpoint`:

```csharp
private const string endpoint = "[Enter your Azure Function URL]";
```
> **Note:** Replace `[Enter your Azure Function URL]` with the [URL retrieved in the previous step](#1-retrieve-azure-function-url)

## Task 4: Run the Xamarin App

1. In Visual Studio, in the Solution Explorer, right-click on `XamarinAzureChallenge.Android`

    > **Note:** To run the iOS app, right-click on `XamarinAzureChallenge.iOS`

2. In the right-click menu, select **Set As Startup Project**

![Set Startup Project](https://user-images.githubusercontent.com/13558917/65280449-5f7bad80-dafe-11e9-9333-687fbb827d32.png)

3. In Visual Studio, at the top, select the arrow icon to build/deploy the app

![Build/Deploy](https://user-images.githubusercontent.com/13558917/65280451-60acda80-dafe-11e9-96fa-64abd26309d5.png)

4. Ensure the app launches on your mobile device

![Xamarin Azure Challenge App](https://user-images.githubusercontent.com/13558917/65280918-5d661e80-daff-11e9-87e6-006f7428175f.png)

5. Follow the instructions on the mobile app to complete the Xamarin Azure Challenge

6. Upon completing the challenge, ensure the success screen is displayed

![Success](https://user-images.githubusercontent.com/13558917/65402551-543db180-dd8c-11e9-973c-fd42486fde82.png)

## Congratulations! 

You've successfully completed the Xamarin Azure Challenge. Keep an eye out for an email shortly!

## Report an issue

If you found an issue with this challenge, please open an issue in this GitHub repo

## Additional Resources

If you are interested in learning more about this topic, you can refer to the following resources:

* [Azure Function Documentation](https://docs.microsoft.com/azure/azure-functions?WT.mc_id=xamarinazurechallenge-github-bramin)
* [Xamarin Documentation](https://docs.microsoft.com/xamarin?WT.mc_id=xamarinazurechallenge-github-bramin)
* [Azure Samples + Xamarin](https://github.com/Azure-Samples?utf8=%E2%9C%93&q=Xamarin&type=&language=&WT.mc_id=xamarinazurechallenge-github-bramin)
