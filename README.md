# Hololens4Labs



<details open="open">
<summary>Table of Contents</summary>

- [About](#about)
  - [Key Features](#key-features)
  - [Built With](#built-with)
- [Contents](#contents)
- [Prerequisites](#prerequisites)
- [Set-up the project](#set-up-the-project)
- [Contributing](#contributing)
- [License](#license)
- [Acknowledgements](#acknowledgements)

</details>

---

## About

<table>
<tr>
<td>
  
![License](https://img.shields.io/badge/license-MIT-green.svg)
  
HoloLens4Labs is a mixed reality application designed for scientists working in chemical laboratories. This application was built in Unty and leverages the Microsoft HoloLens 2 to facilitate efficient and intuitive note-taking directly within the lab environment, syncing the results to an azure server backend.

<details open>
<summary>Additional info</summary>
<br>

This project was developed as part of my thesis and is currently in a stage of a prototype that will be further enhanced and used for a real world lab study in the near future. Although it was developed with the aim of being used primarely in the lab, the UI flow should be congruent with any work requiring note taking and hand free use. 

</details>

</td>
</tr>
</table>

### Key Features 
- **Hands-Free Note-Taking:** Use voice commands or simple gestures to create and manage notes without interrupting lab work.
- **Mixed Reality Integration:** Seamlessly overlay digital notes onto the physical lab space, enhancing information accessibility.
- **Image capture:** Capture images from your the surrounding envinronment and integrate them into notes. 
- **Text Transcription:** Quickly trancribe text from any objecte and edit it before it is saved as a note for future reference.
- **User-Centric Design:** Developed with a focus on ease of use and minimal disruption to existing lab workflow

### Built With
Supported Unity versions | Built with XR configuration
:------------------: | :---------------: |
Unity 2021 or higher | Windows XR Plugin |

- [Unity](https://unity.com/)
- [Microsoft Mixed Reality Toolkit](https://learn.microsoft.com/en-us/windows/mixed-reality/mrtk-unity/mrtk2/?view=mrtkunity-2022-05)
- [Microsoft Visual Studio](https://visualstudio.microsoft.com/)

## Contents

| File/folder | Description |
|-------------|-------------|
| `Hololens4Labs/Assets` | Unity assets, MRTK assets, scenes, prefabs, scripts and unit tests. |
| `Hololens4Labs/Documentation` | Documentation for the project. |
| `Hololens4Labs/Packages` | Project manifest and packages list. |
| `Hololens4Labs/ProjectSettings` | Unity asset setting files. |
| `Hololens4Labs/UserSettings` | Generated user settings from Unity. |
| `.gitignore` | Define what to ignore at commit time. |
| `README.md` | This README file. |
| `LICENSE`   | The license for this project. |


## Prerequisites
Although unity is fully support on OSX we recommend to use MS Windows for any development activity on this project.

Install the [latest Mixed Reality tools](https://docs.microsoft.com/windows/mixed-reality/develop/install-the-tools?tabs=unity)
  
### Instal Unity

To start off please install unity by following the instructions:
1. Dowload and install the [Unity Hub] (https://unity.com/download)
2. Once Unity Hub has been installed select the Installs section in the tab on the left
   ![unity select ](https://github.com/havlep/Hololens4Labs/assets/4102880/b6cbce0d-d34d-4188-88f9-b6673eb7948c)
4. Inside of the 'Installs' section click on the Install Editor Button
5. Select the latest LTS install version of the Unity Engine (the project was last tested on version 2021.3.25f1)
6. Select the following components in the 'Platform' section:
    - Universal Windows Platform Build Support
    - Windows Build Support (IL2CPP)
  ![platform select](https://github.com/havlep/Hololens4Labs/assets/4102880/bb08e16b-882b-47db-8e2b-eca88051d607)
7. Click on the Install button to finish the installation 

### Azure Service Accounts
All the endpoints in the images below and the within the source code are no longer valid, please make sure to setup your own and not commit them to the project. As this is a limited use prototype that will be used with test resources, key vault was not implemented.

#### Azure Tables & Blob Repository Services
The current version of this application uses Azure Tables and Azure Blob repositories for long term storage. You will need to create both by creating an Azure Storage Account.

Please refer to the Microsoft documentation on how to [Create an Azure Storage Acount](https://learn.microsoft.com/en-us/azure/storage/common/storage-account-create?tabs=azure-portal).

#### Azure Cognitive Services
The application uses Azure Computer Vision resource For image to text transcription. 

Please refer to the Microsoft documentation on how to [Create a multi-service resource for Azure AI services](https://learn.microsoft.com/en-us/azure/ai-services/multi-service-resource?tabs=linux&pivots=azportal)

## Set-up the project

### Import the project into Unity
Please follow these steps to import the project into your unity engine:
  1. Clone or download this sample repository.
  2. Open the **Hololens4Labs** folder in Unity Hub and launch the project

In case you get some import errors, reimport the directories that have these issues. 

### Configure the build environment in Unity
You will need to switch your build evnironment to windows universal to build for the HoloLens 4. 

Take the following steps in Unity once the HoloLens4Labs project is open:
  1. Go to File->Build Settings
  2. In the platfrom section on the left hand side select Universal Windows Platform
  3. Select the following settings for the Unviersal Windows Platform:
    * Architecture: ARM-64bit
    * Build Type: D3D Project
    * Target SDK Version: Latest Installed
    * Minimum Platform version: 10.0.1024
    * Visual Studio Version: Latest Installed
    * Build Configuration: Release
     
     <img width="959" alt="UWP platform settings" src="https://github.com/havlep/Hololens4Labs/assets/4102880/8589f371-f71d-48d0-a9e4-60c6a566f916">

  5. Click on the Switch Plafrom button in the lower left hand corner

Once you have complete the above steps the project will reimport and recompile for the UWP platform.

### Configuration the Azure Storage Endpoint
#### Getting the endpoint for storage
You will first need to retrieve the azure storage endpoint from your Azure storage account
  1. Go to your [Azure services portal](https://portal.azure.com/#home) and log in
    * If you created an azure storage resource you should see the account under resources 
  2. Under resources Select the storage resource that you would like to use
    * The storage resource page opens up
  3. In the left hand toolbar under your storage resource select 'AccessKeys' in the 'Secturity + networking' section
    * You should see the keys and endpoints available for your resource
     <img width="778" alt="Azure Storage Resource Enpoints and Keys" src="https://github.com/havlep/Hololens4Labs/assets/4102880/846b5034-1283-4f21-aef2-30afc4b58e6f">
  4. Copy either of the connection strings available into your clipboard

#### Configuring the project to use your storage resource
Inside of your the unity project that you imported do the following:
  1. Inside of the Unity project browser select 'Assets'->'Scenes'->'MainScene'
     * The scene should load and open up inside of the heirarchy window
  2. Inside of the hierachy window select 'SceneController'->'DataManager'->'AzureRepository'
     * You should see the properties for the 'AzureRepository' gameobject in the inspector
  3. Inside of the inspector copy your storage endpoint into the 'Connection String' property under the 'Base Settings' section
     <img width="960" alt="Unity Azure Storage Settings" src="https://github.com/havlep/Hololens4Labs/assets/4102880/5603338e-241d-4ca1-ac2d-b4821538de73">
     * You should see the enpoint url under the 'Connection String' property  
  4. Save your project

### Configuration the Computer Vision resource
#### Getting the endpoint for computer vision
You will first need to retrieve the azure storage endpoint from your Azure storage account
  1. Go to your [Azure services portal](https://portal.azure.com/#home) and log in
    * If you created an azure computer vision resource you should see the account under resources 
  2. Under 'Resources' select the vision resource that you would like to use
    * The Computer Vision resource page opens up
  3. In the left hand toolbar under your Computer vision resource acount select 'Keys and Endpoint' in the 'Resource Management' section
    * You should see the keys, endpoint, and location for your resource
     <img width="789" alt="Azure Portal Computer Vision Keys and Enpoint" src="https://github.com/havlep/Hololens4Labs/assets/4102880/a1d483cc-b75c-4783-8fc8-a42f289cfe7b">
  5. You will need to copy one key, endpoint in the following section

#### Configuring the project to use your computer vision resource
Inside of your the unity project that you imported do the following:
  1. Inside of the Unity project browser select 'Assets'->'Scenes'->'MainScene'
     * The scene should load and open up inside of the heirarchy window
  2. Inside of the hierachy window select 'SceneController'->'ImageAnalysisManadger'
     * You should see the properties for the 'ImageAnalysis' gameobject in the inspector
  3. Inside of the inspector input the key and endpoint that you obtained in the previous section into the 'Subscription Key' and 'Endpoint' propeties under the 'Azure Image Settings' section
     * You should see the enpoint url and key under the 'endpoint' and 'Subscription Key' properties
     <img width="960" alt="Unity Azure Image Settings" src="https://github.com/havlep/Hololens4Labs/assets/4102880/a0cb61d4-220c-46e4-b883-c2ad56de5bc4">
  4. Save your project

## Contributing

First off, thanks for taking the time to contribute! Contributions are what makes the open-source community such an amazing place to learn, inspire, and create. Any contributions you make will benefit everybody else and are **greatly appreciated**.

Please try to create bug reports that are:
- _Reproducible._ Include steps to reproduce the problem.
- _Specific._ Include as much detail as possible: which version, what environment, etc.
- _Unique._ Do not duplicate existing opened issues.
- _Scoped to a Single Bug._ One bug per report.

## License

This project is licensed under the **MIT license**. 

See [LICENSE](LICENSE) for more information.

## Acknowledgements

Thanks for these awesome resources that were used during the development of the **Hololens4Labs**:

