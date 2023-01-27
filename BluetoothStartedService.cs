using Android.Content;
using Android.OS;
using Plugin.BLE;
using Plugin.BLE.Abstractions.Contracts;
using System.Timers;
using IAdapter = Plugin.BLE.Abstractions.Contracts.IAdapter;
using Timer = System.Timers.Timer;

namespace assetdroid
{
    [Service(Name = "com.stefboerjan.BluetoothStartedService")]
    public class BluetoothStartedService : Service
    {
        public const int SERVICE_RUNNING_NOTIFICATION_ID = 10000;

        private Timer Timer { get; set; }
        private List<IDevice> Devices { get; set; }
        private IAdapter adapter;
        public IBinder Binder { get; private set; }
        public override void OnCreate()
        {
            base.OnCreate();
            adapter = CrossBluetoothLE.Current.Adapter;
            adapter.ScanTimeout = 2000;
            Devices = new List<IDevice>();
            adapter.DeviceDiscovered += (s, a) => Devices.Add(a.Device);
        }

        public override StartCommandResult OnStartCommand(Android.Content.Intent intent, StartCommandFlags flags, int startId)
        {
            Timer = new Timer();
            Timer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            Timer.Interval = 5000;
            Timer.Enabled = true;


            var notification = new Notification.Builder(this)
            .SetContentTitle("Bluetooth Low Energy Service")
            .SetContentText("Device is scanning for BLE devices.")
            .SetSmallIcon(Resource.Mipmap.appicon)
            .SetOngoing(true)
            .Build();

            // Enlist this instance of the service as a foreground service
            StartForeground(SERVICE_RUNNING_NOTIFICATION_ID, notification);

            return StartCommandResult.Sticky;
        }

        public override IBinder? OnBind(Intent? intent)
        {
            Binder = new BluetoothStartedServiceBinder(this);
            return Binder;
        }

        public override bool OnUnbind(Intent? intent)
        {
            Timer.Dispose();
            return base.OnUnbind(intent);
        }

        public override void OnDestroy()
        {
            adapter = null;
            Devices = null;
            Binder = null;
            
            base.OnDestroy();
        }

        public List<IDevice> GetBluetoothScanResults()
        {
            return Devices;
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
                Console.WriteLine(list.Split(",").Length);
            }
            catch (Exception ex)
            {

            }
        }
    }
}
