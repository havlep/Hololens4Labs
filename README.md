# Hololens4Labs

<details open="open">
<summary>Table of Contents</summary>

- [About](#about)
  - [Built With](#built-with)
- [Getting Started](#getting-started)
  - [Prerequisites](#prerequisites)
  - [Usage](#usage)
    - [Cookiecutter template](#cookiecutter-template)
    - [Manual setup](#manual-setup)
    - [Variables reference](#variables-reference)
- [Roadmap](#roadmap)
- [Contributing](#contributing)
- [Support](#support)
- [License](#license)
- [Acknowledgements](#acknowledgements)

</details>

---

## About

<table>
<tr>
<td>

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

- [Unity](https://unity.com/)
- [Microsoft Mixed Reality Toolkit](https://learn.microsoft.com/en-us/windows/mixed-reality/mrtk-unity/mrtk2/?view=mrtkunity-2022-05)
- [Microsoft Visual Studio](https://visualstudio.microsoft.com/)

## Getting Started

### Prerequisites
Although unity is fully support on OSX we recommend to use MS Windows for any development activity on this project.
#### Unity Installation

To start off please install unity by following the instructions:
1. Dowload and install the [Unity Hub] (https://unity.com/download)
2. Once Unity Hub has been installed select the Installs section in the tab on the left
3. Inside of the 'Installs' section click on the Install Editor Button
4. Select the latest LTS install version of the Unity Engine (the project was last tested on version 2021.3.25f1)
5. Select the following components in the 'Platform' section:
  -- Universal Windows Platform Build Support
  -- Windows Build Support (IL2CPP)
6. Finish the installation 
 
#### 


### Usage

#### Cookiecutter template

After installing Cookiecutter, all you need to do is to run the following command:

```sh
cookiecutter gh:dec0dOS/amazing-github-template
```

You will get an interactive prompt where you'll specify relevant options for your project (or the default value will be used).

![Preview](docs/images/preview.svg)

#### Manual setup

Please follow these steps for manual setup:

1. [Download the precompiled template](https://github.com/dec0dOS/amazing-github-template/releases/download/latest/template.zip)
2. Replace all the [variables](#variables-reference) to your desired values
3. Initialize the repo in the precompiled template folder

    `or`

    Move the necessary files from precompiled template folder to your existing project directory. Don't forget the `.github` directory that may be hidden by default in your operating system

#### Variables reference

Please note that entered values are case-sensitive.
Default values are provided as an example to help you figure out what should be entered.

> On manual setup, you need to replace only values written in **uppercase**.

| Name                       | Default value      | Description                                                                 |
| -------------------------- | ------------------ | --------------------------------------------------------------------------- |
| PROJECT_NAME               | My Amazing Project | Your project name                                                           |
| REPO_SLUG                  | my-amazing-project | Repo slug must match the GitHub repo URL slug part                          |
| GITHUB_USERNAME            | dec0dOS            | Your GitHub username **without @**                                          |
| FULL_NAME                  | Alexey Potapov     | Your full name                                                              |
| OPEN_SOURCE_LICENSE        | MIT license        | Full OSS license name                                                       |
| modern_header              | y                  | Use HTML to prettify your header                                            |
| table_in_about             | n                  | Use table to wrap around About section                                      |
| include_logo               | y                  | Include Logo section. Only valid when `modern_header == y`          |
| include_badges             | y                  | Include section for badges                                                  |
| include_toc                | y                  | Include Table of Contents                                                   |
| include_screenshots        | y                  | Include Screenshots section                                                 |
| include_project_assistance | y                  | Include Project assistance section                                          |
| include_authors            | y                  | Include Authors & contributors section                                      |
| include_security           | y                  | Include Security section and SECURITY.md file                               |
| include_acknowledgements   | y                  | Include Acknowledgements section                                            |
| include_code_of_conduct    | y                  | Include CODE_OF_CONDUCT.md file                                             |
| include_workflows          | y                  | Include .github/workflows directory                                         |
| use_codeql                 | y                  | Use [CodeQL](https://securitylab.github.com/tools/codeql/)                  |
| use_conventional_commits   | y                  | Add [Conventional Commits](https://www.conventionalcommits.org) notice      |
| use_github_discussions     | n                  | Use [GitHub Discussions](https://docs.github.com/en/discussions/quickstart) |

> NOTICE: to use GitHub Discussions, you have to [enable it first](https://docs.github.com/en/discussions/quickstart).

## Roadmap

See the [open issues](https://github.com/dec0dOS/amazing-github-template/issues) for a list of proposed features (and known issues).

- [Top Feature Requests](https://github.com/dec0dOS/amazing-github-template/issues?q=label%3Aenhancement+is%3Aopen+sort%3Areactions-%2B1-desc) (Add your votes using the 👍 reaction)
- [Top Bugs](https://github.com/dec0dOS/amazing-github-template/issues?q=is%3Aissue+is%3Aopen+label%3Abug+sort%3Areactions-%2B1-desc) (Add your votes using the 👍 reaction)
- [Newest Bugs](https://github.com/dec0dOS/amazing-github-template/issues?q=is%3Aopen+is%3Aissue+label%3Abug)

## Contributing

First off, thanks for taking the time to contribute! Contributions are what makes the open-source community such an amazing place to learn, inspire, and create. Any contributions you make will benefit everybody else and are **greatly appreciated**.

Please try to create bug reports that are:

- _Reproducible._ Include steps to reproduce the problem.
- _Specific._ Include as much detail as possible: which version, what environment, etc.
- _Unique._ Do not duplicate existing opened issues.
- _Scoped to a Single Bug._ One bug per report.

Please adhere to this project's [code of conduct](docs/CODE_OF_CONDUCT.md).

You can use [markdownlint-cli](https://github.com/igorshubovych/markdownlint-cli) to check for common markdown style inconsistency.

## Support

Reach out to the maintainer at one of the following places:

- [GitHub discussions](https://github.com/dec0dOS/amazing-github-template/discussions)
- The email which is located [in GitHub profile](https://github.com/dec0dOS)

## License

This project is licensed under the **MIT license**. Feel free to edit and distribute this template as you like.

See [LICENSE](LICENSE) for more information.

## Acknowledgements

Thanks for these awesome resources that were used during the development of the **Amazing GitHub template**:

- <https://github.com/cookiecutter/cookiecutter>
- <https://github.github.com/gfm/>
- <https://tom.preston-werner.com/2010/08/23/readme-driven-development.html>
- <https://changelog.com/posts/top-ten-reasons-why-i-wont-use-your-open-source-project>
- <https://thoughtbot.com/blog/how-to-write-a-great-readme>
- <https://www.makeareadme.com>
- <https://github.com/noffle/art-of-readme>
- <https://github.com/noffle/common-readme>
- <https://github.com/RichardLitt/standard-readme>
- <https://github.com/matiassingers/awesome-readme>
- <https://github.com/LappleApple/feedmereadmes>
- <https://github.com/othneildrew/Best-README-Template>
- <https://github.com/mhucka/readmine>
- <https://github.com/badges/shields>
- <https://github.com/cjolowicz/cookiecutter-hypermodern-python>
- <https://github.com/stevemao/github-issue-templates>
- <https://github.com/devspace/awesome-github-templates>
- <https://github.com/cezaraugusto/github-template-guidelines>
- <https://github.com/frenck?tab=repositories>
