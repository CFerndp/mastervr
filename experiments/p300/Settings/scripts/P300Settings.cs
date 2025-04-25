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

        var numberOfTrials = GetNode<LineEdit>("MainPanel/MarginContainer/GridContainer/Content/Trials/LineEdit");
        numberOfTrials.Text = P300SettingsAutoload.Instance.NumberOfTrials.ToString();
        numberOfTrials.TextChanged += (value) =>
        {
            P300SettingsAutoload.Instance.NumberOfTrials = int.Parse(value);
        };

        var numberOfStimuli = GetNode<LineEdit>("MainPanel/MarginContainer/GridContainer/Content/Stimulus/LineEdit");
        numberOfStimuli.Text = P300SettingsAutoload.Instance.NumberOfStimuli.ToString();
        numberOfStimuli.TextChanged += (value) =>
        {
            P300SettingsAutoload.Instance.NumberOfStimuli = int.Parse(value);
        };

        var numberTarget = GetNode<LineEdit>("MainPanel/MarginContainer/GridContainer/Content/Target/LineEdit");
        numberTarget.Text = P300SettingsAutoload.Instance.TargetNumber.ToString();
        numberTarget.TextChanged += (value) =>
        {
            P300SettingsAutoload.Instance.TargetNumber = int.Parse(value);
        };
    }

    private void OnBackButtonPressed()
    {
        GetTree().ChangeSceneToFile("res://scenes/Main/Main.tscn");
    }

    private void OnStartButtonPressed()
    {
        var vrEnabled = vrEnabledCheck.IsPressed();

        if (vrEnabled)
        {
            GetTree().ChangeSceneToFile("res://scenes/experiments/p300/VR/VR.tscn");
        }
        else
        {
            GetTree().ChangeSceneToFile("res://scenes/experiments/p300/2D/2D.tscn");
        }
    }
}
