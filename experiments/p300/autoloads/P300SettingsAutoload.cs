using Godot;
using System.Collections.Generic;

public partial class P300SettingsAutoload : Node
{
    public static P300SettingsAutoload Instance { get; private set; }   
    public int numberOfTrials { get; set; } = 20;
    public int numberOfStimuli { get; set; } = 6;

    public override void _Ready()
    {
        Instance = this;
    }
}