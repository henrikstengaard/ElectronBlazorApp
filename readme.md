
# Electron Blazor App

Blazor server app created with NET 5 SDK.

Inspired by https://jimbuck.io/building-desktop-apps-with-blazor.

# Run

## Browser

Press F5 to run Blazor server app in browser.

## Electron

Open command prompt run following command from Blazor app csproj directory to launch Blazor app in Electron:

```
dotnet electronize start
```

# Debug

Run and attach to process.

# Publish

Publish application for Windows with following command:

```
dotnet electronize build /target win
```

Publish application for MacOS with following command:

```
dotnet electronize build /target osx
```

Publish application for Linux with following command:

```
dotnet electronize build /target linux
```
