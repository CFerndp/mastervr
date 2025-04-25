

using Godot;

public class MockBCIPort : IBCIPort
{
    public void SendEvent(int evnt)
    {
        GD.Print($"MockBCIPort: SendEvent {evnt}");
    }

    public void Start()
    {
        GD.Print("MockBCIPort: Start");
    }

    public void Stop()
    {
        GD.Print("MockBCIPort: Stop");
    }
}