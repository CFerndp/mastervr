using Godot;
using SharpLSL;
using System;

	


public partial class p300Text : Label3D
{
	private Random random = new Random();

	private StreamInfo streamInfo;
	private StreamOutlet streamOutlet;

	enum MarkerType	{
		Show,
		Hide
	}

	private void sendLSLEvent(MarkerType @event) {
			
		if(this.streamOutlet == null || this.streamOutlet.IsInvalid) {
			return;
		}

		try{
			this.streamOutlet.PushSample(new int[] { @event == MarkerType.Show ? 10 :11 });
			GD.Print("Sending sample: " + @event);
		} catch(Exception e) {
			GD.Print("Error: " + e.Message);
		}
		
	}


	private async void ShowAndHideText()
	{
		while (true)
		{
			this.Visible = true;
			this.sendLSLEvent(MarkerType.Show);
			await ToSignal(GetTree().CreateTimer(0.3f), "timeout");

			this.Visible = false;
			this.sendLSLEvent(MarkerType.Hide);
			this.Text = random.Next(0, 10).ToString(); // Cambia el texto a un número aleatorio
			await ToSignal(GetTree().CreateTimer(0.1f), "timeout");
		}
	}

	public override void _Ready()
	{

		// Stream info name & typo has to be the same than Markers Lab Streaming Label 1 in NIC
		this.streamInfo = new StreamInfo("Markers", "Markers");
		this.streamOutlet = new StreamOutlet(streamInfo);

		ShowAndHideText();
	}
}
