

using Godot;

public class MockBCIPort : IBCIPort
{
    public void SendEvent(int evnt)
    {
        GD.Print($"MockBCIPort: SendEvent {evnt}");
    }
    public void Start(int? evnt)
    {
        GD.Print($"MockBCIPort: Start {evnt}");
    }
    public void Stop(int? evnt)
    {
        GD.Print($"MockBCIPort: Stop {evnt}");
    }

}