using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChronoTap.Core.Utils
{
    internal class LocationService
    {

        public static async Task<string> GetCachedLocation()
        {
            try
            {
                Location? location = await Geolocation.Default.GetLastKnownLocationAsync();

                if (location != null)
                    return $"Lat: {location.Latitude}, Lon: {location.Longitude}, Alt: {location.Altitude}";
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                // Handle not supported on device exception
            }
            catch (FeatureNotEnabledException fneEx)
            {
                // Handle not enabled on device exception
            }
            catch (PermissionException pEx)
            {
                // Handle permission exception
            }
            catch (Exception ex)
            {
                // Unable to get location
            }

            return "None";
        }
    }
}
