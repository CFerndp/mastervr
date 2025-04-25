using Godot;

public partial class P300PortAutoload : Node
{
    public static P300PortAutoload Instance { get; private set; }
    public IBCIPort Port { get; set; }
    public override void _Ready()
    {
        // Dependency injection
        Port = new LSLBCIPort();
        // Port = new MockBCIPort();
        Instance = this;
    }
}