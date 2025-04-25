using Godot;
using System.Collections.Generic;

public partial class P300SettingsAutoload : Node
{
    public static P300SettingsAutoload Instance { get; private set; }
    public int NumberOfTrials { get; set; } = 20;
    public int NumberOfStimuli { get; set; } = 6;
    public int TargetNumber { get; set; } = 2;

    public override void _Ready()
    {
        Instance = this;
    }
}