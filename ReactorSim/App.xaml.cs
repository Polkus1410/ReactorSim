using ReactorSim.Models;

namespace ReactorSim
{
    public partial class App : Application
    {
        public App(IServiceProvider serviceProvider)
        {
            InitializeComponent();

            var s = serviceProvider.GetRequiredService<EntitysList>();


            MainPage = new MainPageView(s);
        }
    }
}
