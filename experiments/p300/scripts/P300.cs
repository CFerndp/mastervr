using Godot;
using System;

public partial class P300 : Node3D
{
    public override void _Process(double delta)
    {
        if (Input.IsActionJustPressed("ui_cancel"))
        {
            GetTree().ChangeSceneToFile("res://experiments/p300/settings/P300Settings.tscn");
        }
    }

}
