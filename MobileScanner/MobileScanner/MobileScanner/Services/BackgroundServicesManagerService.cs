using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace MobileScanner.Services
{
    public interface IBackgroundServicesManagerService
    {
        void StartService(string name);
        void StopService(string name);
    }
    public class BackgroundServicesManagerService : IBackgroundServicesManagerService
    {
        private void SendMessage(string name, ServiceStateEnum state)
        {
            MessagingCenter.Send(this, name, state.ToString("d"));
        }

        public void StartService(string name)
        {
            this.SendMessage(name, ServiceStateEnum.START);
        }
        public void StopService(string name)
        {
            this.SendMessage(name, ServiceStateEnum.STOP);
        }
    }

    public enum ServiceStateEnum
    {
        STOP = 0,
        START = 1
    }
}
