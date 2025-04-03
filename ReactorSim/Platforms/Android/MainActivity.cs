using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Views;

namespace ReactorSim
{
  [Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, LaunchMode = LaunchMode.SingleTop, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
  public class MainActivity : MauiAppCompatActivity
  {
    protected override void OnCreate(Bundle savedInstanceState)
    {
      base.OnCreate(savedInstanceState);
      RequestedOrientation = ScreenOrientation.Landscape;

      
      // Hide the status and navigation bars
      if (Build.VERSION.SdkInt >= BuildVersionCodes.R)
      {
        Window.InsetsController?.Hide(WindowInsets.Type.StatusBars());
        Window.InsetsController?.Hide(WindowInsets.Type.NavigationBars());
      }
      else
      {
        // For older Android versions
        Window.AddFlags(Android.Views.WindowManagerFlags.Fullscreen);
      }
    }
  }
}
