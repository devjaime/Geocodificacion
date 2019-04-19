using System;
using System.Linq;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Geocodificacion.ViewModel
{
    public class GeocodingViewModel : BaseViewModel
    {
        string lat = "-33.3873615";
        string lon = "-70.5665004";
        string address = "Microsoft Chile";
        string geocodeAddress;
        string geocodePosition;

        public GeocodingViewModel()
        {
            GetAddressCommand = new Command(OnGetAddress);
            GetPositionCommand = new Command(OnGetPosition);
        }

        public ICommand GetAddressCommand { get; }

        public ICommand GetPositionCommand { get; }

        public string Latitude
        {
            get => lat;
            set => SetProperty(ref lat, value);
        }

        public string Longitude
        {
            get => lon;
            set => SetProperty(ref lon, value);
        }

        public string GeocodeAddress
        {
            get => geocodeAddress;
            set => SetProperty(ref geocodeAddress, value);
        }

        public string Address
        {
            get => address;
            set => SetProperty(ref address, value);
        }

        public string GeocodePosition
        {
            get => geocodePosition;
            set => SetProperty(ref geocodePosition, value);
        }

        async void OnGetPosition()
        {
            if (IsBusy)
                return;

            IsBusy = true;
            try
            {
                var locations = await Geocoding.GetLocationsAsync(Address);
                var location = locations?.FirstOrDefault();
                if (location == null)
                {
                    GeocodePosition = "No se pueden detectar ubicaciones";
                }
                else
                {
                    GeocodePosition =
                        $"{nameof(location.Latitude)}: {location.Latitude}\n" +
                        $"{nameof(location.Longitude)}: {location.Longitude}\n";
                }
            }
            catch (Exception ex)
            {
                GeocodePosition = $"No se pueden detectar ubicaciones: {ex.Message}";
            }
            finally
            {
                IsBusy = false;
            }
        }

        async void OnGetAddress()
        {
            if (IsBusy)
                return;

            IsBusy = true;
            try
            {
                double.TryParse(lat, out var lt);
                double.TryParse(lon, out var ln);

                var placemarks = await Geocoding.GetPlacemarksAsync(lt, ln);
                var placemark = placemarks?.FirstOrDefault();
                if (placemark == null)
                {
                    GeocodeAddress = "No se pueden detectar marcas de posición.";
                }
                else
                {
                    GeocodeAddress =
                        $"{nameof(placemark.AdminArea)}: {placemark.AdminArea}\n" +
                        $"{nameof(placemark.CountryCode)}: {placemark.CountryCode}\n" +
                        $"{nameof(placemark.CountryName)}: {placemark.CountryName}\n" +
                        $"{nameof(placemark.FeatureName)}: {placemark.FeatureName}\n" +
                        $"{nameof(placemark.Locality)}: {placemark.Locality}\n" +
                        $"{nameof(placemark.PostalCode)}: {placemark.PostalCode}\n" +
                        $"{nameof(placemark.SubAdminArea)}: {placemark.SubAdminArea}\n" +
                        $"{nameof(placemark.SubLocality)}: {placemark.SubLocality}\n" +
                        $"{nameof(placemark.SubThoroughfare)}: {placemark.SubThoroughfare}\n" +
                        $"{nameof(placemark.Thoroughfare)}: {placemark.Thoroughfare}\n";
                }
            }
            catch (Exception ex)
            {
                GeocodeAddress = $"No se pueden detectar marcas de posición: {ex.Message}";
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
