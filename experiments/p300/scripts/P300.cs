using Godot;
using Chickensoft.LogicBlocks;
using System;

public partial class P300 : Node3D
{
    private ExperimentState experimentState;
    private LogicBlock<ExperimentState.State>.IBinding binding;

    private IBCIPort port;

    public override void _Ready()
    {
        var showTimer = GetNode<Timer>("TimerShow");
        var hideTimer = GetNode<Timer>("TimerHide");

        port = P300PortAutoload.Instance.Port;

        showTimer.Timeout += OnTimerTimeout;
        hideTimer.Timeout += OnTimerTimeout;

        var numberLabel = GetNode<Label3D>("Label3D");
        numberLabel.Text = "How many times appears target number?";

        experimentState = new ExperimentState();
        experimentState.Get<ExperimentState.ExperimentData>().numberOfTrials = P300SettingsAutoload.Instance.numberOfTrials;
        experimentState.Get<ExperimentState.ExperimentData>().numberOfStimuli = P300SettingsAutoload.Instance.numberOfStimuli;


        binding = experimentState.Bind();

        binding.Handle((in ExperimentState.Output.NumberSwitched output) =>
        {
            numberLabel.Text = output.stimulus.HasValue ? output.stimulus.Value.ToString() : "";
        });


        binding.When((ExperimentState.State.Running.ShowNumber _) =>
        {
            showTimer.Start();
            port.SendEvent((int)P300Markers.ShowNumber);
        });

        binding.When((ExperimentState.State.Running.HideNumber _) =>
        {
            hideTimer.Start();
            port.SendEvent((int)P300Markers.HideNumber);
        });

        binding.When((ExperimentState.State.End _) =>
        {
            numberLabel.Text = "Thanks";
            port.Stop();
        });

        experimentState.Start();
    }

    private void OnTimerTimeout()
    {
        experimentState.Input(new ExperimentState.Input.TimerEnd());
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

            if (status is ExperimentState.State.Instructions)
            {
                port.Start();
                experimentState.Input(new ExperimentState.Input.StartExperiment());
            }
            else if (status is ExperimentState.State.Running)
            {
                experimentState.Input(new ExperimentState.Input.EndExperiment());
            }
        }
    }

}
