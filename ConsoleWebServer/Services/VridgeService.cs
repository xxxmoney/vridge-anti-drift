using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VRE.Vridge.API.Client.Remotes;

namespace ConsoleWebServer.Services
{
    public interface IVridgeService
    {
        void Recenter();
    }
    public class VridgeService : IVridgeService
    {
        private readonly VridgeRemote vridgeRemote;
        public VridgeService()
        {
            this.vridgeRemote = new VridgeRemote(
                "localhost",
                "VridgeAntiDrift",
                Capabilities.Controllers | Capabilities.HeadTracking);
        }

        public void Recenter()
        {
            this.vridgeRemote.Head?.Recenter();
        }
    }
}
