using Microsoft.Extensions.Logging;
using ReactorSim.Models;
using ReactorSim.Views;

namespace ReactorSim
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();

            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            builder.Services.AddSingleton<EntitysList>();
            builder.Services.AddTransient<MainPageView>();
      /*builder.Services.AddSingleton<SimulationDrawable>();*/
      /*            builder.Services.AddSingleton<SimulationDrawable>(serviceProvider =>
                  {
                      var entitysList = serviceProvider.GetRequiredService<EntitysList>();
                      return new SimulationDrawable(entitysList);
                  });*/
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
