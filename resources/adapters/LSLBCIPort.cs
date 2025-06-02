using SharpLSL;

public enum MarkerBasics
{
    Start = 1,
    Stop = 2,
}

public class LSLBCIPort : IBCIPort
{
    private StreamInfo streamInfo;
    private StreamOutlet streamOutlet;

    public LSLBCIPort()
    {
        streamInfo = new StreamInfo("Markers", "Markers");
        streamOutlet = new StreamOutlet(streamInfo);
    }

    public void _SendData(int data)
    {
        this.streamOutlet.PushSample(new int[] { data });
    }

    public void SendEvent(int evnt)
    {
        this._SendData(evnt);
    }

    public void Start(int? evnt)
    {
        this._SendData(evnt ?? (int)MarkerBasics.Start);
    }
    public void Stop(int? evnt)
    {
        this._SendData(evnt ?? (int)MarkerBasics.Stop);
    }
}