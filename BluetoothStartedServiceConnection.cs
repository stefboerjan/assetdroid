using Android.Content;
using Android.OS;
using Plugin.BLE.Abstractions.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace assetdroid
{
    public class BluetoothStartedServiceConnection : Java.Lang.Object, IServiceConnection
    {
        public bool IsConnected { get; set; }
        public BluetoothStartedServiceBinder Binder { get; set; }
        MainActivity mainActivity;
        public BluetoothStartedServiceConnection(MainActivity activity)
        {
            IsConnected = false;
            Binder = null;
            mainActivity = activity;
        }

        public void OnServiceConnected(ComponentName name, IBinder service)
        {
            Binder = service as BluetoothStartedServiceBinder;
            IsConnected = Binder != null;

            if (IsConnected)
            {
                mainActivity.UpdateUiForBoundService();
            }
            else
            {
                mainActivity.UpdateUiForBoundService();
            }
        }

        public void OnServiceDisconnected(ComponentName? name)
        {
            IsConnected = false;
            Binder = null;
            mainActivity.UpdateUiForUnboundService();
        }

        public List<IDevice> GetBluetoothScanResults()
        {
            if (!IsConnected)
            {
                return null;
            }

            return  Binder?.GetBluetoothScanResults();
        }
    }
}
