using Microsoft.Extensions.Logging;
using ReactorSim.Models;
using ReactorSim.ViewModels;
using ReactorSim.Views;
using Microsoft.Maui.LifecycleEvents;
#if WINDOWS
using Microsoft.UI.Windowing;
using Microsoft.UI;
#endif

namespace ReactorSim
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
#if WINDOWS
            builder.ConfigureLifecycleEvents(events =>
            {
                events.AddWindows(wndLifeCycleBuilder =>
                {
                    wndLifeCycleBuilder.OnWindowCreated(window =>
                    {
                        IntPtr nativeWindowHandle = WinRT.Interop.WindowNative.GetWindowHandle(window);
                        WindowId win32WindowsId = Win32Interop.GetWindowIdFromWindow(nativeWindowHandle);
                        AppWindow winuiAppWindow = AppWindow.GetFromWindowId(win32WindowsId);
                        winuiAppWindow.SetPresenter(AppWindowPresenterKind.FullScreen);
                        winuiAppWindow.SetPresenter(AppWindowPresenterKind.FullScreen);
                    });
                });
            });
#endif

      builder
          .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            builder.Services.AddSingleton<EntitysList>();
            builder.Services.AddSingleton<MainPageView>();
            builder.Services.AddTransient<MainPageViewModel>();
            builder.Services.AddSingleton<SimulationDrawable>(serviceProvider =>
            {
                var entitysList = serviceProvider.GetRequiredService<EntitysList>();
                var simulationDrawable = new SimulationDrawable();
                simulationDrawable.EntitysList = entitysList;
                return simulationDrawable;
            });

#if DEBUG
            builder.Logging.AddDebug();
#endif


            return builder.Build();
        }
    }
}