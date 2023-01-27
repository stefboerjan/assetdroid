using Android.OS;
using Plugin.BLE.Abstractions.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace assetdroid
{
    public class BluetoothStartedServiceBinder : Binder
    {
        public BluetoothStartedService Service { get; set; }
        public BluetoothStartedServiceBinder(BluetoothStartedService service)
        {
            Service = service;
        }

        public List<IDevice> GetBluetoothScanResults()
        {
            return Service?.GetBluetoothScanResults();
        }
    }
}
