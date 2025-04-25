


public interface IBCIPort
{
    /// <summary>Start recording</summary>
    void Start();

    /// <summary>Send event</summary>
    void SendEvent(int evnt);

    /// <summary>Stop recording</summary>
    void Stop();


}

