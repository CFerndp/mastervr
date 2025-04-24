using Godot;
using Chickensoft.LogicBlocks;
using System;

public partial class P300 : Node3D
{
    private ExperimentState experimentState;
    private LogicBlock<ExperimentState.State>.IBinding binding;

    public override void _Ready()
    {
        var numberLabel = GetNode<Label3D>("Label3D");
        numberLabel.Text = "How many times appears target number?";

        experimentState = new ExperimentState();

        binding = experimentState.Bind();

        binding.When((ExperimentState.State.Running _) => {
            numberLabel.Text = "Running...";
        });

        binding.When((ExperimentState.State.End _) => {
            numberLabel.Text = "Thanks";
        });

        experimentState.Start();
    }

    public override void _Process(double delta)
    {
        if (Input.IsActionJustPressed("ui_cancel"))
        {
            GetTree().ChangeSceneToFile("res://experiments/p300/settings/P300Settings.tscn");
        }

        if (Input.IsActionJustPressed("ui_accept"))
        {
            var status = experimentState.Value;

            if(status is ExperimentState.State.Instructions) {
                experimentState.Input(new ExperimentState.Input.StartExperiment());
            } else if (status is ExperimentState.State.Running) {
                experimentState.Input(new ExperimentState.Input.EndExperiment());
            }
        }
    }

}
