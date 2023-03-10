using Android.Bluetooth;
using Android.Bluetooth.LE;
using Android.Content;
using Android.OS;
using Plugin.BLE;
using Plugin.BLE.Abstractions.Contracts;
using Plugin.BLE.Android;
using System.Timers;
using static Android.Bluetooth.BluetoothAdapter;
using IAdapter = Plugin.BLE.Abstractions.Contracts.IAdapter;
using Timer = System.Timers.Timer;

namespace assetdroid
{
    [Service(Name = "com.stefboerjan.BluetoothService")]
    public class BluetoothService : Android.App.Service//without Plugin.BLE, ILeScanCallback
    {
        private IBluetoothLE ble;
        private IAdapter adapter;
        private List<IDevice> Devices { get; set; }
        public IBinder Binder { get; private set; }

        private Timer Timer { get; set; }

        //without Plugin.BLE
        /*public IBinder Binder { get; private set; }
        Handler handler = new Handler();
        bool scanning = false;
        BluetoothManager managerLE;
        BluetoothAdapter adapterLE;
        private int SCAN_PERIOD;
        private List<BluetoothDevice> BluetoothDevices { get; set; }*/

        public override async void OnCreate()
        {
            //without Plugin.BLE
            /*managerLE = (BluetoothManager)GetSystemService(BluetoothService);
            adapterLE = managerLE.Adapter;
            SCAN_PERIOD = 2000;
            BluetoothDevices = new List<BluetoothDevice>();*/

            //plugin.ble
            base.OnCreate();
            ble = CrossBluetoothLE.Current;
            adapter = CrossBluetoothLE.Current.Adapter;
            adapter.ScanTimeout = 2000;
            Devices = new List<IDevice>();
            adapter.DeviceDiscovered += (s, a) => Devices.Add(a.Device);
            
            Timer = new Timer();
            Timer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            Timer.Interval = 5000;
            Timer.Enabled = true;
        }

        private async void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            try
            {
                Devices.Clear();
                await adapter.StartScanningForDevicesAsync();
                string list = "";
                Devices.ForEach(d =>
                {
                    list = list + ", " + d.ToString();
                });
                Console.WriteLine(list);
            } catch (Exception ex)
            {

            }
        }

        public override IBinder? OnBind(Intent? intent)
        {
            Binder = new BluetoothBinder(this);
            return Binder;
        }

        public override bool OnUnbind(Intent? intent)
        {
            return base.OnUnbind(intent);
        }

        public override void OnDestroy()
        {
            //Plugin.BLE
            ble = null;
            adapter = null;
            Devices = null;
            //without Plugin.BLE
            /*handler = null;
            scanning = false;
            managerLE = null;
            adapterLE = null;
            SCAN_PERIOD = 0;*/

            Binder = null;
            Timer.Dispose();
            base.OnDestroy();
        }

        public async Task<List<IDevice>> GetBluetoothScanResults()
        {
            try
            { 
                return Devices;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        //without Plugin.BLE
        /*public void Scan()
        {
            if (!scanning)
            {
                // Stops scanning after a pre-defined scan period.
                handler.PostDelayed(new Action(delegate {
                    scanning = false;
                    adapterLE.StopLeScan(this);
                }), SCAN_PERIOD);

                scanning = true;
                adapterLE.StartLeScan(this);
            }
            else
            {
                scanning = false;
                adapterLE.StopLeScan(this);
            }
        }

        public void OnLeScan(BluetoothDevice? device, int rssi, byte[]? scanRecord)
        {
            BluetoothDevices.Add(device);
        }

        public List<BluetoothDevice> GetDevices()
        {
            BluetoothDevices.Clear();
            Scan();
            return BluetoothDevices;
        }*/
    }
}
