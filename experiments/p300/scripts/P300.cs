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
        var stimulus = 0;

        port = P300PortAutoload.Instance.Port;

        showTimer.Timeout += OnTimerTimeout;
        hideTimer.Timeout += OnTimerTimeout;

        var numberLabel = GetNode<Label3D>("Label3D");
        numberLabel.Text = $"How many times appears the number {P300SettingsAutoload.Instance.TargetNumber}?";

        experimentState = new ExperimentState();
        binding = experimentState.Bind();

        binding.Handle((in ExperimentState.Output.NumberSwitched output) =>
        {
            if (output.stimulus.HasValue)
            {
                stimulus = output.stimulus.Value;
            }

            numberLabel.Text = output.stimulus.ToString();
        });


        binding.When((ExperimentState.State.Running.ShowNumber _) =>
        {
            port.SendEvent((int)P300Markers.ShowNumber + stimulus);
            showTimer.Start();
        });

        binding.When((ExperimentState.State.Running.HideNumber _) =>
        {
            port.SendEvent((int)P300Markers.HideNumber + stimulus);
            hideTimer.Start();
        });

        binding.When((ExperimentState.State.End _) =>
        {
            numberLabel.Text = "Thanks";
            port.Stop(null);
        });

        experimentState.Start();
        experimentState.Input(new ExperimentState.Input.Setup(new ExperimentState.ExperimentData()
        {
            numberOfTrials = P300SettingsAutoload.Instance.NumberOfTrials,
            numberOfStimuli = P300SettingsAutoload.Instance.NumberOfStimuli
        }));

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
                port.Start(P300SettingsAutoload.Instance.TargetNumber);
                experimentState.Input(new ExperimentState.Input.StartExperiment());
            }
            else if (status is ExperimentState.State.Running)
            {
                experimentState.Input(new ExperimentState.Input.EndExperiment());
            }
        }
    }

}
