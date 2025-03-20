using ReactorSim.Models;
using System.Runtime.CompilerServices;
using System.Timers;

namespace ReactorSim
{
    public partial class MainPageView : ContentPage
    {
        public MainPageView(EntitysList entitysList)
        {
            InitializeComponent();

            //BindingContext = entitysList;

            entitysList.neutronList.Add(new Neutron(100, 100, 0, 0, false));

            var timer = new System.Timers.Timer(20);
            timer.Elapsed += new ElapsedEventHandler(RedrawSimulation);
            timer.Start();
        }

        public void RedrawSimulation(object source, ElapsedEventArgs e)
        {
            var graphicsView = this.SimulationGraphicsView;
            try
            {
                if (graphicsView != null) graphicsView.Invalidate();
            }
            catch (Exception ex)
            {
                DisplayAlert("Error", $"{ex.Message}", "Ok");
            }
        }
    }
}
