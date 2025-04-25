using Godot;
using System;

public partial class VRNode : Node3D
{
    [Export]
    public bool emulateVR = false;

    private XRInterface _xrInterface;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {


        var xrSimulator = GetNode("XRSimulator");
        xrSimulator.Set("enabled", emulateVR);

        _xrInterface = XRServer.FindInterface("OpenXR");
        if (_xrInterface != null && _xrInterface.IsInitialized())
        {

            // Turn off v-sync!
            DisplayServer.WindowSetVsyncMode(DisplayServer.VSyncMode.Disabled);

            // Change our main viewport to output to the HMD
            GetViewport().UseXR = true;
        }
    }

    public override void _ExitTree()
    {
        base._ExitTree();
        GetViewport().UseXR = false;
        _xrInterface = null;
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
    }
}
