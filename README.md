# Xamarin Azure Challenge

Welcome to the Xamarin Azure Challenge.

The goal is to create an serverless [Azure Function](https://azure.microsoft.com/services/functions?WT.mc_id=xamarinazurechallenge-github-bramin) and connect it to a [Xamarin](https://dotnet.microsoft.com/apps/xamarin?WT.mc_id=xamarinazurechallenge-github-bramin) mobile app.

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
* Clone the repository
* Download the source code

#### 1a. Clone the repository

To [clone](https://git-scm.com/docs/git-clone) this repository, run this command in your favorite terminal:

``` bash
git clone https://github.com/xamarin/XamarinAzureChallenge.git
```

#### 1b. Download Source Code

To download the source clode, click this link: https://github.com/xamarin/XamarinAzureChallenge/archive/master.zip

### 2. Create Azure Function

After cloning the repository, we have 3 options to create and publish the Azure Function:

1. Use [Azure Portal to create the Azure Function](https://docs.microsoft.com/azure/azure-functions/functions-create-function-app-portal)
2. Use [Azure CLI to create the Azure Function](https://docs.microsoft.com/azure/azure-functions/functions-create-first-azure-function-azure-cli?WT.mc_id=xamarinazurechallenge-github-bramin)


#### 2a. Use Azure Portal

1. In your browser, naviagte to the [Azure Portal](http://portal.azure.com?WT.mc_id=xamarinazurechallenge-github-bramin) 

2. In the **Azure Portal**, on the left-hand menu, click **+ Create a resource**

> Note: If the toolbar is collapsed, it will be shown as a green **+**

![Create new resource](https://user-images.githubusercontent.com/13558917/64928580-1a3e3f80-d7cf-11e9-84e7-01ecf565de81.png)

3. In the **New** dashboard, in the search bar, type **functions**

4. In the **New** dashboard, in the search bar tap the **[Enter]** key

![Marketplace Search Functions](https://user-images.githubusercontent.com/13558917/64928584-1d393000-d7cf-11e9-852d-fad28d656ae3.png)

5. On the **Marketplace** search results, click **Function App**

![Function App](https://user-images.githubusercontent.com/13558917/64928586-1e6a5d00-d7cf-11e9-9334-08eabee77898.png)

6. On the **Marketplace > Function App** window, select **Create** 

![Create Functions App](https://user-images.githubusercontent.com/13558917/64928581-1ad6d600-d7cf-11e9-9805-829a665e5d88.png)

7. On the **Function App Create** page, enter the following information:

- **App name**: XamarinAzureChallenge-[Your Name]
    - Note: The app name must be unique because it is used for the Azure Functions Url. This is why we'll append our name.
    - In this example, I'm using "XamarinAzureChallenge-Brandon"
- **Subscription**: [Select your Azure Subscription]
- **Resource Group**:
    - [x] **Create new**
    - XamarinAzureChallenge
- **OS**: Windows
- **Hosting Plan**: Consumption Plan
- **Location**: [Select the Azure Datacenter closest to you]
- **Runtime Stack**: .NET Core
- **Storage**:
    - [x] **Create new**
    - xamarinazure[Your Name]
        - Note: The Storage name must be unique, which is why we append our name
        - In this example, I'm using "xamarinazurebrandon"

7. On the **Function App Create** page, Click **Create**

![Enter Functions App Data](https://user-images.githubusercontent.com/13558917/64928583-1ca09980-d7cf-11e9-83ad-df824d193d66.png)

8. On the **Azure Portal**, at the top of the page, tap the notifications button which is shaped like a bell

9. In the **Notifications** window, ensure it says **Deployment in progress...**

![Deployment in progress](https://user-images.githubusercontent.com/13558917/64928694-ce4cc400-d7e9-11e9-8b87-c57a5695d6b2.png)

10. Stand by until the deployment has suceeded

![Deployment Succeeded](https://user-images.githubusercontent.com/13558917/64928721-3b605980-d7ea-11e9-9996-89f7d0ff8ef1.png)

#### 2b. Use Azure CLI

> As a prerequisite, you must install [Azure Core Tools version 2.x](https://docs.microsoft.com/azure/azure-functions/functions-run-local?WT.mc_id=xamarinazurechallenge-github-bramin#v2) and [Azure CLI](https://docs.microsoft.com/cli/azure/install-azure-cli?WT.mc_id=xamarinazurechallenge-github-bramin)) or [Azure Cloud Shell](https://shell.azure.com/bash?WT.mc_id=xamarinazurechallenge-github-bramin).

1. Open the terminal
  - [How to open the macOS Terminal](https://macpaw.com/how-to/use-terminal-on-mac)
  - [How to open the Windows Terminal](https://www.quora.com/How-do-I-open-terminal-in-windows)

2. In the terminal, enter the following command to login into Azure CLI:

```bash
az login
```

3. In the terminal, enter the following command to create a new Azure Resource Group:

```bash
az group create --name XamarinAzureChallenge --location westeurope
```

> **Note:** If you have more than one subscription you will need to especify the subscription where the resource groups will be created with `--subscription [your Azure Subscription ID]`
> - [How to find your Azure Subscription ID ](https://blogs.msdn.microsoft.com/mschray/2016/03/18/getting-your-azure-subscription-guid-new-portal?WT.mc_id=xamarinazurechallenge-github-bramin)

4. In the terminal, enter the following command to create a new Azure Storage Account:

```bash
az storage account create --name xamarinazure[Your Name] --location westeurope --resource-group XamarinAzureChallenge --sku Standard_LRS --subscription [Your Azure Subscription ID]
```
> Note: The storage name must be unique, which is why we append our name

5. In the terminal, enter the following command to navigate to the project folder `XamarinAzureChallenge.Functions` project folder.

```bash
cd [Your Path to XamarinAzureChallengeSource Code]\src\XamarinAzureChallenge\XamarinAzureChallenge.Functions
```

6. In the terminal, enter the following command to create a function app:

```bash
az functionapp create --resource-group XamarinAzureChallenge --consumption-plan-location westeurope --name XamarinAzureChallenge-[Your Name] --storage-account  xamarinazure[Your Name] --runtime dotnet --subscription [Your Azure Subscription ID]
```

> **Note:** The Azure Function name must be unique, which is why we append our name

## 3: Publish Code to Azure Function

After creating the Azure Function, it's time to publish our code to the cloud. For this, we have two options:

1. Use [Visual Studio on PC to publish the Azure Function](https://blogs.msdn.microsoft.com/benjaminperkins/2018/04/05/deploy-an-azure-function-created-from-visual-studio?WT.mc_id=xamarinazurechallenge-github-bramin).
2. Use [Visual Studio for Mac to publish the Azure Function](https://docs.microsoft.com/visualstudio/mac/publish-app-svc?WT.mc_id=xamarinazurechallenge-github-bramin)
3. Use [Azure CLI to publish the Azure Function](https://docs.microsoft.com/azure/azure-functions/functions-create-first-azure-function-azure-cli?WT.mc_id=xamarinazurechallenge-github-bramin)


### 3a. Use Visual Studio on PC

1. In Visual Studio on PC, open `XamarinAzureChallenge.sln`

2. In to Visual Studio, in the Solution Explorer, right-click on the `XamarinAzureChallenge.Functions` project

2. In the right-click menu, select in **Publish**

![publish](https://user-images.githubusercontent.com/13558917/65265720-82e33000-dadf-11e9-9a23-d910ac159790.png)

4. In the **Pick a publish target** window, select the following: 

- **Azure Functions Consumption Plan**
- [x] **Create New**

5. In the **Pick a publish target** window, select **Publish**

![Pick a publish target](https://user-images.githubusercontent.com/13558917/65265726-84145d00-dadf-11e9-9c14-716aaf2e3f18.png)

6. In the **Create New** window, enter the following information:
- **Name:** XamarinAzureChallenge-[Your Name]
    -  **Note:** The Azure Function name must be unique, which is why we append our name
- **Subscription:** [Select your Azure Subscription]
- **Resource Group**
    - **New**
        - **New resource group name**: XamarinAzureChallenge
        - **OK**
- **Location:** [Select the Azure Data Center Closest To You]
- **Azure Storage**
    - **New**
        - **Account Name:** xamarinazure[Your Name]
            -  **Note:** The Azure Storage name must be unique, which is why we append our name
        - **Location:** [Select the Azure Data Center Closest To You]
        - **Account type:** Standard - Locally Redundant Storage
        - **OK**

7. In the **Create New** window, click **Create** 

![Create New App Service](https://user-images.githubusercontent.com/13558917/65271517-52a18e80-daeb-11e9-8854-972bce47134e.png)

8. Standby while Visual Studio publishes our code to our Azure Function

![Deploying](https://user-images.githubusercontent.com/13558917/65271519-533a2500-daeb-11e9-9a54-3a4b6afad613.png)

### 3c. Use Visual Studio for Mac

1. In Visual Studio for Mac, open `XamarinAzureChallenge.sln`

2. In Visual Studio for Mac, in the Solution Explorer, right-click on `XamarinAzureChallenge.Functions`

3. In the **Publish to Azure App Service** window, select your Azure Account

4. In the **Publish to Azure App Service** window, select **New**

![Create New App Service](https://user-images.githubusercontent.com/13558917/65275675-276f6d00-daf4-11e9-9dac-3b23c50964cc.png)

5. In the right-click menu, select **Publish** > **Publish to Azure...**

![Publish to Azure](https://user-images.githubusercontent.com/13558917/65273393-1112e280-daef-11e9-9555-c3d47582ab18.png)

5. In the **Create New App Service on Azure** window, enter the following information:

- **App Service Name:** XamarinAzureChallenge-[Your Name]
    -  **Note:** The Azure Function name must be unique, which is why we append our name
- **Subscription:** [Select your Azure Subscription]
- **Resource Group**
    - Click the **+** symbol
    - XamarinAzureChallenge
- **Service Plan:** Custom
- **Plan Name:** XamarinAzureChallenge
- **Region:** [Select the Azure Data Center Closest to you]
- **Pricing:** Consumption

6. In the **Create New App Service on Azure** window, select **Next**

![Create New App Service](https://user-images.githubusercontent.com/13558917/65274970-8f24b880-daf2-11e9-8715-4b5fb7d86746.png)

7. In the **Configure Storage Account** window, enter the following information:

- **Storage Account:** Custom
- **Account Name** xamarinazure[Your Name]
    -  **Note:** The Storage Account name must be unique, which is why we append our name
- **Account Type** Standard - Locally Redundant Storage

8. In the **Configure Storage Account** window, select **Create**

![Configure Storage Account](https://user-images.githubusercontent.com/13558917/65274742-0c9bf900-daf2-11e9-90af-da04b5539d75.png)

9. In the **Create Azure App Service** pop up, select **OK**

![OK](https://user-images.githubusercontent.com/13558917/65279318-f135eb80-dafb-11e9-97d3-351a247348ee.png)

10. In Visual Studio, in the menu bar, select **View** > **Pads** > **Azure**

![Azure Pad](https://user-images.githubusercontent.com/13558917/65274737-0b6acc00-daf2-11e9-85bd-552ff9b01049.png)

11. In Visual Studio, in the **Azure** pad, ensure the code is **Deploying...**

![Deploying](https://user-images.githubusercontent.com/13558917/65274736-0ad23580-daf2-11e9-94e6-531d22f2e0d1.png)

12. Stand by while Visual Studio for Mac publishes our code to our Azure Function

### 3c. Use Azure CLI

1. Open the terminal
  - [How to open the macOS Terminal](https://macpaw.com/how-to/use-terminal-on-mac)
  - [How to open the Windows Terminal](https://www.quora.com/How-do-I-open-terminal-in-windows)


2. In the terminal, enter the following command to navigate to the project folder `XamarinAzureChallenge.Functions` project folder.

```bash
cd [Your Path to XamarinAzureChallengeSource Code]\src\XamarinAzureChallenge\XamarinAzureChallenge.Functions
```

3.  In the terminal, enter the following command to publish our code our Azure Function:

```bash
func azure functionapp publish XamarinAzureChallenge-[Your Name]
```

## Task 2: Configure the Xamarin App

### 1. Retrieve Azure Function URL

After publishing our Azure Function, we are ready to configure our Xamarin app with the Azure Function URL.

1. In your browser, naviagte to the [Azure Portal](http://portal.azure.com?WT.mc_id=xamarinazurechallenge-github-bramin) 

2. In the Azure Portal, on the left-hand menu, select the cube-shaped **Resource Groups** icon

![Resource Groups Icon](https://user-images.githubusercontent.com/13558917/65279226-bb910280-dafb-11e9-8691-68c08204a84e.png)

3. In the **Resource Groups** window, in the filter box, enter `XamarinAzureChallenge`

4. In the **Resource Groups** window, select the **XamarinAzureChallenge** Resource Group

![XamarinAzureChallenge Resource Group](https://user-images.githubusercontent.com/13558917/65279226-bb910280-dafb-11e9-8691-68c08204a84e.png)

5. In the **XamarinAzureChallenge Resource Group**, select the function app **XamarinAzureChallenge-[Your Name]**

![Open Function App](https://user-images.githubusercontent.com/13558917/65279223-ba5fd580-dafb-11e9-8144-431d8cfad5f0.png)

6. In the **Function Apps** window, select **XamarinAzureChallenge** > **Functions** > **SubmitChallengeFunction**

![Submit Challenge Function](https://user-images.githubusercontent.com/13558917/65279519-6b667000-dafc-11e9-8aa8-9db24dc02553.png)

7. In the **SubmitChallengeFunction** window, select **Get function url**

![Get function url](https://user-images.githubusercontent.com/13558917/65279218-b8961200-dafb-11e9-8634-f33db35197e7.png)

8. In the **Get function url** window, select **Copy**

![Copy](https://user-images.githubusercontent.com/13558917/65279215-b7fd7b80-dafb-11e9-9e2a-badb0f869cb2.png)

### 2. Add the Azure Function URL to Xamarin App

1. In Visual Studio, open `XamarinAzureChallenge.sln`

2. In Visual Studio, in the Solution Explorer, open **Mobile** > **XamarinAzureChallenge** > **ViewModels** > **UserDataViewModel.cs**

3. In the UserDataViewModel editor, append `//` to the `#error` compiler directive:

```csharp
//#error Missing Azure Function Endpoint Url. Replace "Enter Your Function API Url Here" with your Azure Function Endopint Url
```

4. In the UserDataViewModel editor, add your Azure Function Url to `private const string endpoint`:

```csharp
private const string endpoint = "[Enter your Azure Function URL]";
```

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

![Success](https://user-images.githubusercontent.com/13558917/65280916-5ccd8800-daff-11e9-93d7-dc824ec1ffae.png)


## Next Steps

Please, read the Terms and Conditions of this challenge to know more about the next steps. 

## Report an issue

If you found an issue with this challenge, please open an issue in this GitHub repo

## Additional Resources

If you are interested in learning more about this topic, you can refer to the following resources:

* [Azure Function Documentation](https://docs.microsoft.com/azure/azure-functions?WT.mc_id=xamarinazurechallenge-github-bramin)
* [Xamarin Documentation](https://docs.microsoft.com/xamarin?WT.mc_id=xamarinazurechallenge-github-bramin)
* [Azure Samples + Xamarin](https://github.com/Azure-Samples?utf8=%E2%9C%93&q=Xamarin&type=&language=&WT.mc_id=xamarinazurechallenge-github-bramin)
