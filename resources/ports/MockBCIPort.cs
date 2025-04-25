using Godot;

public class MockBCIPort : IBCIPort
{
    private void _Print(string msg)
    {
        var time = Time.GetTicksMsec();
        GD.Print($"[{time}] {msg}");
    }
    public void SendEvent(int evnt)
    {
        _Print($"SendEvent {evnt}");
    }
    public void Start(int? evnt)
    {
        _Print($"MockBCIPort: Start {evnt}");
    }
    public void Stop(int? evnt)
    {
        _Print($"MockBCIPort: Stop {evnt}");
    }

}