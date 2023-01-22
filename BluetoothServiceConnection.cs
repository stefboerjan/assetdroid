using Android.Bluetooth;
using Android.Content;
using Android.OS;
using Plugin.BLE.Abstractions.Contracts;

namespace assetdroid
{
    public class BluetoothServiceConnection : Java.Lang.Object, IServiceConnection
    {
        public bool IsConnected { get; set; }
        public BluetoothBinder Binder { get; set; }
        MainActivity mainActivity;
        public BluetoothServiceConnection(MainActivity activity)
        {
            IsConnected = false;
            Binder = null;
            mainActivity = activity;
        }

        public void OnServiceConnected(ComponentName name, IBinder service)
        {
            Binder = service as BluetoothBinder;
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

        public async Task<List<IDevice>> GetBluetoothScanResults()
        {
            if (!IsConnected)
            {
                return null;
            }

            return await Binder?.GetBluetoothScanResults();
        }

        //without Plugin.BLE
        /*public List<BluetoothDevice>? GetBluetoothScanResults()
        {
            if (!IsConnected)
            {
                return null;
            }

            return Binder?.GetDevices();
        }*/
    }
}
