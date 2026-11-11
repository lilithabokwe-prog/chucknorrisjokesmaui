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

#How I attempted to add the DadJokesApiUrl to the existing ChuckNorrisJokes 
Models
These are just classes that hold the data I get back from the APIs.

ChuckNorrisJoke.cs – this is where I put the stuff that comes back from the Chuck Norris API. Basically just a property for the joke text.

DadJoke.cs – same idea but for the dad joke API. Their response had a slightly different shape, so I needed a separate class.

View
JokesView.xaml – this is the screen you actually see. It shows both jokes, has a "Get New Jokes" button, a little loading spinner (ActivityIndicator) while it's fetching, and some text in case something goes wrong.

ViewModel
JokesViewModel.cs – honestly this was the trickiest part to wrap my head around. It's the middleman between the View and the data. It has properties that the View binds to (the joke strings, an IsLoading flag, an error message) and a Command that runs when you tap the button. It also asks the service for jokes and handles any errors.

API Service
ChuckNorrisApiHttpService.cs – this is the class that actually talks to the internet. I used HttpClient to call both joke APIs, and then JsonSerializer to turn the JSON responses into my C# objects. It took me a while to get the JSON property names matching up right!

How to Run It
I'm writing this as if you're on a fresh machine, because that's what I had to deal with:

Open the project in Visual Studio 2026.

Make sure the .NET MAUI workload is installed (you can check in the Visual Studio Installer – I forgot this step the first time and was very confused).

Pick an Android emulator or plug in an Android device.

Build the solution.

Run the app.

Tap the Get New Jokes button to fetch a fresh pair of jokes.
