using Android.Bluetooth;
using Android.OS;
using Plugin.BLE.Abstractions.Contracts;

namespace assetdroid
{
    public class BluetoothBinder : Binder
    {
        public BluetoothService Service { get; set; }
        public BluetoothBinder(BluetoothService service)
        {
            Service = service;
        }

        public async Task<List<IDevice>> GetBluetoothScanResults()
        {
            return await Service?.GetBluetoothScanResults();
        }

        //without Plugin.BLE
        /*public List<BluetoothDevice>? GetDevices()
        {
            return Service?.GetDevices();
        }*/
    }
}
