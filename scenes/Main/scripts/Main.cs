using Godot;
using System;

public partial class Main : Control
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Button sdButton = GetNode<Button>("MainPanel/MarginContainer/VBoxContainer3/VBoxContainer/2DP300Button");
		Button vrButton = GetNode<Button>("MainPanel/MarginContainer/VBoxContainer3/VBoxContainer/3DP300Button");
		Button exitButton = GetNode<Button>("MainPanel/MarginContainer/VBoxContainer3/VBoxContainer2/ExitButton");
		

		sdButton.Pressed += On2DButtonPressed;
		vrButton.Pressed += OnVRButtonPressed;
		exitButton.Pressed += OnExitButtonPressed;
	}

	private void OnVRButtonPressed()
	{
		GetTree().ChangeSceneToFile("res://scenes/VR/VR.tscn");
	}

	private void On2DButtonPressed()
	{
		GetTree().ChangeSceneToFile("res://scenes/2D/2D.tscn");
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
