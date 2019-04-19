using System.Threading.Tasks;
using Geocodificacion.ViewModel;
using Xamarin.Forms;

namespace Geocodificacion.View
{
    public class BasePage : ContentPage
    {
        public BasePage()
        {
            NavigationPage.SetBackButtonTitle(this, "Atras");
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            SetupBinding(BindingContext);
        }

        protected override void OnDisappearing()
        {
            TearDownBinding(BindingContext);

            base.OnDisappearing();
        }

        protected void SetupBinding(object bindingContext)
        {
            if (bindingContext is BaseViewModel vm)
            {
                vm.DoDisplayAlert += OnDisplayAlert;
                vm.OnAppearing();
            }
        }

        protected void TearDownBinding(object bindingContext)
        {
            if (bindingContext is BaseViewModel vm)
            {
                vm.OnDisappearing();
                vm.DoDisplayAlert -= OnDisplayAlert;
            }
        }

        Task OnDisplayAlert(string message)
        {
            return DisplayAlert(Title, message, "OK");
        }
    }
}
