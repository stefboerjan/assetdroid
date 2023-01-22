package crc643a1c912150a3202e;


public class BluetoothBinder
	extends android.os.Binder
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("assetdroid.BluetoothBinder, assetdroid", BluetoothBinder.class, __md_methods);
	}


	public BluetoothBinder ()
	{
		super ();
		if (getClass () == BluetoothBinder.class) {
			mono.android.TypeManager.Activate ("assetdroid.BluetoothBinder, assetdroid", "", this, new java.lang.Object[] {  });
		}
	}


	public BluetoothBinder (java.lang.String p0)
	{
		super (p0);
		if (getClass () == BluetoothBinder.class) {
			mono.android.TypeManager.Activate ("assetdroid.BluetoothBinder, assetdroid", "System.String, System.Private.CoreLib", this, new java.lang.Object[] { p0 });
		}
	}

	public BluetoothBinder (com.stefboerjan.BluetoothService p0)
	{
		super ();
		if (getClass () == BluetoothBinder.class) {
			mono.android.TypeManager.Activate ("assetdroid.BluetoothBinder, assetdroid", "assetdroid.BluetoothService, assetdroid", this, new java.lang.Object[] { p0 });
		}
	}

	private java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
