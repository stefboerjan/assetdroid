using Android;
using Android.App.Job;
using Android.Content;
using Android.Content.PM;
using Android.Views;

namespace assetdroid
{
    [Activity(Label = "@string/app_name", MainLauncher = true)]
    public class MainActivity : ListActivity
    {
        BluetoothStartedServiceConnection serviceConnection;
        List<string> Devices { get; set; } = new List<string>();
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
        }

        protected override void OnStart()
        {
            base.OnStart();
            //Bound service
            /*if (serviceConnection == null)
            {
                serviceConnection = new BluetoothServiceConnection(this);
            }
            Intent serviceToStart = new Intent(this, typeof(BluetoothService));
            BindService(serviceToStart, serviceConnection, Bind.AutoCreate);*/

            /****************************************************************************************************************************************************************/
            // Sample usage - creates a JobBuilder for a BluetoothJob and sets the Job ID to 1.
            /*var jobBuilder = this.CreateJobBuilderUsingJobId<BluetoothJob>(1);

            var jobInfo = jobBuilder.SetPeriodic(1000).Build();  // creates a JobInfo object.

            var jobScheduler = (JobScheduler)GetSystemService(JobSchedulerService);
            var scheduleResult = jobScheduler.Schedule(jobInfo);

            if (JobScheduler.ResultSuccess == scheduleResult)
            {
                Toast.MakeText(Application, "success", ToastLength.Short).Show();
            }
            else
            {
                Toast.MakeText(Application, "Failed", ToastLength.Short).Show();
            }*/

            /****************************************************************************************************************************************************************/
            //Started service
            var intent = new Intent(this, typeof(BluetoothStartedService));

            if (serviceConnection == null)
            {
                serviceConnection = new BluetoothStartedServiceConnection(this);
            }

            BindService(intent, serviceConnection, Bind.AutoCreate);

            if (Android.OS.Build.VERSION.SdkInt >= Android.OS.BuildVersionCodes.O)
            {
                StartForegroundService(intent);
            }
            else
            {
                StartService(intent);
            }
        }

        protected override void OnDestroy()
        {
            StopService(new Intent(this, typeof(BluetoothStartedService)));
            base.OnDestroy();
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.top_menus, menu);
            return base.OnCreateOptionsMenu(menu);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            if (item.TitleFormatted.ToString() == "Scan")
            {
                ScanButtonPressed();
            }
            return base.OnOptionsItemSelected(item);
        }

        public async Task UpdateUiForBoundService()
        {
            CheckPermissions();
            var devices = serviceConnection.Binder.GetBluetoothScanResults();

            foreach (var device in devices)
            {
                Devices.Add(device.Id.ToString() + ": " + device.Rssi.ToString() + "dBm");
            }

            ListAdapter = new ArrayAdapter<string>(this, Resource.Layout.list_item, Devices);

            ListView.TextFilterEnabled = true;

            ListView.ItemClick += delegate (object sender, AdapterView.ItemClickEventArgs args)
            {
                Toast.MakeText(Application, ((TextView)args.View).Text, ToastLength.Short).Show();
            };
        }

        public void UpdateUiForUnboundService()
        {

        }

        public async void ScanButtonPressed()
        {
            var devices = serviceConnection.Binder.GetBluetoothScanResults();
            Devices.Clear();
            foreach (var device in devices)
            {
                Devices.Add(device.Id.ToString() + ": " + device.Rssi.ToString() + "dBm");
            }
            ListAdapter = new ArrayAdapter<string>(this, Resource.Layout.list_item, Devices);
        }

        private void CheckPermissions()
        {
            var allowed = CheckSelfPermission(Manifest.Permission.AccessFineLocation);
            if (allowed == Permission.Denied)
            {
                string[] permissions = new string[] { Manifest.Permission.AccessFineLocation };
                RequestPermissions(permissions, 0);
            }
            var again = CheckSelfPermission(Manifest.Permission.AccessFineLocation);
        }
    }
}