using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace MobileScanner.Services
{
    public interface IPermissionsService
    {
        Task<bool> CheckPermissions();
    }

    public class PermissionsService : IPermissionsService
    {
        public async Task<bool> CheckPermissions()
        {
            List<PermissionStatus> states = new List<PermissionStatus>();

            states.Add(await Permissions.RequestAsync<Permissions.Camera>());
            states.Add(await Permissions.RequestAsync<Permissions.Photos>());
            states.Add(await Permissions.RequestAsync<Permissions.Media>());
            states.Add(await Permissions.RequestAsync<Permissions.StorageRead>());
            states.Add(await Permissions.RequestAsync<Permissions.StorageWrite>());

            return states.All(x => x == PermissionStatus.Granted);
        }

    }
}
