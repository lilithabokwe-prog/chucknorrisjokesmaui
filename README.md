# Chuck Norris Jokes App

> **⚠️ For educational purposes only.**
> This solution exists to demonstrate .NET MAUI, MVVM and dependency injection. It is not production software and comes with no warranty or support. It is not affiliated with or endorsed by Chuck Norris or by the [chucknorris.io](https://api.chucknorris.io) API. Jokes are fetched live from that third-party API, so content is outside this project's control.

## What it does

A small cross-platform .NET MAUI app. Tap a button and it fetches a random Chuck Norris joke from `https://api.chucknorris.io/jokes/random` and displays it. If the request fails, an error pop-up is shown.

## Solution layout

```
ChuckNorrisJokesAppSln.slnx          Solution file
└── ChuckNorrisJokesApp/             Single MAUI project
    ├── MauiProgram.cs               App startup and dependency injection registration
    ├── App.xaml / AppShell.xaml     Application and Shell navigation
    ├── Models/
    │   └── ChuckNorrisJoke.cs       DTO matching the API's JSON response
    ├── Services/
    │   ├── Interfaces/              IChuckNorrisApiService, IConfigurationService, IPopUps
    │   ├── ChuckNorrisApiHttpService.cs   Calls the API with HttpClient and deserializes the JSON
    │   ├── ConfigurationService.cs        Holds the API URL
    │   └── MauiPopupService.cs            Shows alerts through MAUI
    ├── ViewModels/
    │   ├── BaseViewModel.cs         INotifyPropertyChanged helper
    │   └── JokesViewModel.cs        Exposes the joke text and GetJokeCommand
    ├── Views/
    │   └── JokesView.xaml(.cs)      UI bound to JokesViewModel
    ├── Platforms/                   Android, iOS, Mac Catalyst and Windows entry points
    └── Resources/                   Icons, splash screen, fonts, images, styles
```

## Concepts demonstrated

- **MVVM**: the `JokesView` binds to `JokesViewModel`, which exposes `ChuckNorrisJoke` and `GetJokeCommand`.
- **Dependency injection**: services, view and view model are registered in `MauiProgram.cs` and constructor-injected.
- **Interface-based services**: the API, configuration and pop-up logic sit behind interfaces, so they can be swapped or mocked in tests.
- **Consuming a REST API**: `HttpClient` plus `System.Text.Json`.
- **Cross-platform targeting**: one project targets Android, iOS, Mac Catalyst and Windows.

## Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- The .NET MAUI workload: `dotnet workload install maui`
- Visual Studio 2026 with the ".NET Multi-platform App UI development" workload (recommended on Windows)
- Platform tooling for whichever target you run (Android SDK/emulator, Xcode on macOS for iOS and Mac Catalyst)

## Running

From Visual Studio, pick a target platform and press F5. From the command line:

```bash
dotnet build ChuckNorrisJokesApp/ChuckNorrisJokesApp.csproj -f net10.0-windows10.0.19041.0
dotnet run --project ChuckNorrisJokesApp/ChuckNorrisJokesApp.csproj -f net10.0-windows10.0.19041.0
```

Replace the `-f` value with `net10.0-android`, `net10.0-ios` or `net10.0-maccatalyst` for the other targets. iOS and Mac Catalyst require a Mac. An internet connection is needed to fetch jokes.

## Known limitations (learning opportunities)

- `HttpClient` is created directly rather than via `IHttpClientFactory`.
- `GetJokeAsync` returns `null` on a non-success status code instead of raising an error.
- The API URL is hard-coded in `ConfigurationService` rather than read from settings.
- `FetchJoke` is an `async void` command handler; an async command pattern would be more robust.
- There are no automated tests.

## Disclaimer

This project is provided "as is" for learning and demonstration only. Do not use it in production or rely on it for anything important. Review the terms of the [chucknorris.io](https://api.chucknorris.io) API before using it beyond personal learning.
