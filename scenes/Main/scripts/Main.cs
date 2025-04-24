using Godot;
using System;
using ExperimentConstants;

public partial class Main : Control
{
	// Called when the node enters the scene tree for the first time.
	/// <summary>
	/// Called when the node is ready, i.e. when it's children and resources are fully loaded.
	/// </summary>
	public override void _Ready()
	{
		BoxContainer experimentsPanel = GetNode<BoxContainer>("MainPanel/MarginContainer/GridContainer/Experiments");

		foreach (var experiment in ExperimentRouter.MainRoutes){
			Button button = new Button();
			button.Text = experiment.Key;
			button.Pressed += () => GetTree().ChangeSceneToFile(experiment.Value);
			experimentsPanel.AddChild(button);
		}

		Button exitButton = GetNode<Button>("MainPanel/MarginContainer/GridContainer/Footer/ExitButton");
		

		exitButton.Pressed += OnExitButtonPressed;
	}

	private void OnExitButtonPressed()
	{
		GetTree().Quit();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
