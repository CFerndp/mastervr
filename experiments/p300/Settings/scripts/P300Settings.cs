using Godot;
using System;

public partial class P300Settings : Control
{
    private CheckBox vrEnabledCheck;

    public override void _Ready()
    {

        Button backButton = GetNode<Button>("MainPanel/MarginContainer/GridContainer/FooterButtons/BackButton");
        Button startButton = GetNode<Button>("MainPanel/MarginContainer/GridContainer/FooterButtons/StartButton");
        vrEnabledCheck = GetNode<CheckBox>("MainPanel/MarginContainer/GridContainer/Content/VR/CheckVR");

        backButton.Pressed += OnBackButtonPressed;
        startButton.Pressed += OnStartButtonPressed;
    }

    private void OnBackButtonPressed()
    {
        GetTree().ChangeSceneToFile("res://scenes/Main.tscn");
    }

    private void OnStartButtonPressed()
    {
        var vrEnabled = vrEnabledCheck.IsPressed();

        if(vrEnabled){
            GetTree().ChangeSceneToFile("res://scenes/experiments/p300/VR/VR.tscn");
        } else {
            GetTree().ChangeSceneToFile("res://scenes/experiments/p300/2D/2D.tscn");
        }
    }
}
