using ReactorSim.Models;
using ReactorSim.ViewModels;

namespace ReactorSim
{
    public partial class MainPageView : ContentPage
    {

        private MainPageViewModel _viewModel;
        public MainPageView(EntitysList entitysList, MainPageViewModel vm)
        {
            InitializeComponent();
            _viewModel = vm;
            BindingContext = vm;

            vm.GenerateStartingSetup();

            // Pobieramy instancję SimulationDrawable z zasobów (zgodnie z kluczem "drawable" w XAML)
            if (Resources.TryGetValue("drawable", out object drawableObj) && drawableObj is Views.SimulationDrawable drawable)
            {
                drawable.EntitysList = entitysList;
            }

            //MAIN LOOP
            var timer = new System.Timers.Timer(20);
            timer.Elapsed += (sender, e) =>
            {
                vm.SimulationUpdate();
                RedrawSimulation();
            };
            timer.Start();
        }

        private void RedrawSimulation()
        {
            try
            {
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    SimulationGraphicsView.Invalidate();
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}