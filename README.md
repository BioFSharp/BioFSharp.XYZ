# BioFSharp.XYZ

![Logo](docs/img/Logo_large.png)

A template repository for creating an extension package for BioFSharp.

## Content

- `src/BioFSharp.XYZ`: the library, targeting .NET Standard 2.0 and referencing BioFSharp 2.0.0.
- `tests/BioFSharp.XYZ.Tests`: an xUnit test project targeting .NET 10.
- `build/build.fsproj`: a FAKE build project targeting .NET 10 that handles building, testing, packaging, and publishing.
- `docs`: literate F# documentation and images.

## Setup

1. Install the .NET 10 SDK. `global.json` selects the SDK family and is also used by CI.
2. Replace `BioFSharp.XYZ` with your package name throughout the tracked file contents. This includes the solution, project references, source and test namespaces, `build/ProjectInfo.fs`, documentation, and package metadata.
3. Rename `BioFSharp.XYZ.sln`, `src/BioFSharp.XYZ` and its `.fsproj`, and `tests/BioFSharp.XYZ.Tests` and its `.fsproj`. Check that the solution entries, test project reference, and `testProjects` in `build/ProjectInfo.fs` point to the renamed files.
4. Fill in the library's NuGet metadata, including authors and description. Update `gitOwner` in `build/ProjectInfo.fs` and all repository, documentation, license, and release-notes URLs if your GitHub owner differs from `BioFSharp`.
5. Update `docs/index.fsx`: its heading, library DLL name, include directory, and opened namespace must match the renamed project. If you change the library's target framework, update the framework directory in its `#I` directive too. Keep its BioFSharp package reference aligned with the library project.
6. Replace the sample library, test, documentation, and release notes with your own content. Replace the images in `docs/img` as needed.
7. Run `dotnet tool restore` from the repository root to install the pinned documentation tool. Documentation build/watch targets also restore it automatically.
8. For coverage uploads, configure the `CODECOV_TOKEN` repository secret. The workflow derives the repository name automatically. For documentation hosting, configure GitHub Pages to deploy from the `gh-pages` branch, root directory; the first successful docs deployment creates that branch.

The library can retain `netstandard2.0` even though development tools and tests use .NET 10. Choose a different library target only when its API requirements call for it.

## Development

Run commands from the repository root. On Linux/macOS use `bash ./build.sh`; on Windows use `.\build.cmd`.

### Build

```bash
bash ./build.sh
```

```powershell
.\build.cmd
```

### Test

```bash
bash ./build.sh RunTests
```

```powershell
.\build.cmd RunTests
```

### Create a NuGet package

```bash
bash ./build.sh Pack
```

```powershell
.\build.cmd Pack
```

`Pack` builds, tests, asks for confirmation of the version from `RELEASE_NOTES.md`, and writes packages to `pkg`. It does not require a publishing key.

### Documentation

Build the site with `bash ./build.sh BuildDocs` or `.\build.cmd BuildDocs`. Watch locally with hot reload:

```bash
bash ./build.sh WatchDocs
```

```powershell
.\build.cmd WatchDocs
```

The docs workflow builds and deploys on every push to `main`, including API and release-version changes. It can also be started manually from GitHub Actions. Its deployment job requests `contents: write` to update the `gh-pages` branch.

### Publishing

Set `NUGET_API_KEY` in the environment of the shell running the release. Use a NuGet API key authorized to publish your package; do not put its value in tracked files.

Check the configuration without publishing anything:

```bash
bash ./build.sh ValidateNugetApiKey
```

```powershell
.\build.cmd ValidateNugetApiKey
```

This checks that the variable is nonempty; NuGet validates the key's permissions when publishing. Release targets validate its presence before pushing tags and mask it in build traces.

`ReleaseNoDocs` builds, tests, packages, pushes a Git tag, and publishes the stable NuGet package after interactive confirmations. `PreReleaseNoDocs` does the same for a prerelease and asks for a suffix. Use these targets when the GitHub workflow handles documentation deployment.

`Release` and `PreRelease` additionally publish documentation locally and require an existing `gh-pages` branch. Set the release version and notes in `RELEASE_NOTES.md` before running a release target.
