using Android.App.Job;
using Android.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace assetdroid
{
    [Service(Name = "com.stefboerjan.BluetoothJob",
         Permission = "android.permission.BIND_JOB_SERVICE")]
    public class BluetoothJob : JobService
    {
        public override bool OnStartJob(JobParameters? @params)
        {
            Task.Run(() =>
            {
                // Work is happening asynchronously
                Console.WriteLine("job executed");
                // Have to tell the JobScheduler the work is done. 
                JobFinished(@params, true);
            });

            // Return true because of the asynchronous work
            return true;
        }

        public override bool OnStopJob(JobParameters? @params)
        {
            // we don't want to reschedule the job if it is stopped or cancelled.
            return false;
        }
    }

    public static class JobSchedulerHelpers
    {
        public static JobInfo.Builder CreateJobBuilderUsingJobId<T>(this Context context, int jobId) where T : JobService
        {
            var javaClass = Java.Lang.Class.FromType(typeof(T));
            var componentName = new ComponentName(context, javaClass);
            return new JobInfo.Builder(jobId, componentName);
        }
    }
}
