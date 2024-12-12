using Godot;
using System;

public partial class p300Text : Label3D
{
	private Random random = new Random();

	private async void ShowAndHideText()
	{
		while (true)
		{
			this.Visible = true;
			await ToSignal(GetTree().CreateTimer(0.3f), "timeout");
			this.Visible = false;
			this.Text = random.Next(0, 10).ToString(); // Cambia el texto a un número aleatorio
			await ToSignal(GetTree().CreateTimer(0.1f), "timeout");
		}
	}

	public override void _Ready()
	{
		ShowAndHideText();
	}
}
