using System.Collections.Generic;
using Chickensoft.Introspection;
using Chickensoft.LogicBlocks;
using Godot;

[Meta, LogicBlock(typeof(State), Diagram = true)]
public partial class ExperimentState : LogicBlock<ExperimentState.State>
{

     private const int initialNumberOfStimuli = 5;
     private const int initialNumberOfTrials = 20;

     public override Transition GetInitialState() => To<State.Instructions>();

     public sealed record ExperimentData
     {
          public int numberOfTrials { get; set; }
          public int numberOfStimuli { get; set; }
     }

     public sealed record RunningData
     {
          public int runningTrial { get; set; }

          public List<int> stimuliArray { get; set; }

          public int currentStimulus { get; set; }
     }

     public ExperimentState()
     {


          var stimuliArray = P300Utils.GenerateStimuliArray(initialNumberOfStimuli);
          var currentStimulus = stimuliArray[0];
          stimuliArray.RemoveAt(0);

          Set(new ExperimentData() { numberOfTrials = initialNumberOfTrials, numberOfStimuli = initialNumberOfStimuli });
          Set(new RunningData()
          {
               runningTrial = 0,
               stimuliArray = stimuliArray,
               currentStimulus = currentStimulus
          });
     }



     public static class Input
     {
          public readonly record struct StartExperiment;
          public readonly record struct EndExperiment;
          public readonly record struct TimerEnd;
     }
     public static class Output
     {
          public readonly record struct NumberSwitched(int? stimulus);
     }
     public abstract partial record State : StateLogic<State>
     {

          private void _GenerateNextStimuli()
          {
               var experimentData = Get<ExperimentData>();
               var runningData = Get<RunningData>();

               // Get the array of stimuli
               var stimuliArray = runningData.stimuliArray;

               // If array finished, trial finished. Go to next trial 
               if (stimuliArray.Count == 0)
               {
                    stimuliArray = P300Utils.GenerateStimuliArray(experimentData.numberOfStimuli);
                    runningData.stimuliArray = stimuliArray;
                    runningData.runningTrial++;
               }

               // Get the first stimulus from the array and remove it from the array
               runningData.currentStimulus = stimuliArray[0];
               runningData.stimuliArray.RemoveAt(0);
          }

          public record Instructions : State, IGet<Input.StartExperiment>
          {
               public Transition On(in Input.StartExperiment input)
               {
                    _GenerateNextStimuli();
                    return To<Running.ShowNumber>();
               }
          }

          public record Running : State, IGet<Input.EndExperiment>, IGet<Input.TimerEnd>
          {
               public Transition On(in Input.EndExperiment input) => To<End>();

               public Transition On(in Input.TimerEnd input)
               {

                    var experimentData = Get<ExperimentData>();
                    var runningData = Get<RunningData>();



                    // If all trials have been completed, go to end
                    if (runningData.runningTrial > experimentData.numberOfTrials - 1)
                    {
                         return To<End>();
                    }

                    if (this is ShowNumber)
                    {
                         return To<HideNumber>();
                    }
                    else if (this is HideNumber)
                    {
                         _GenerateNextStimuli();

                         // Go to show number
                         return To<ShowNumber>();
                    }

                    return ToSelf();

               }

               public record ShowNumber : Running
               {
                    public ShowNumber()
                    {
                         this.OnEnter(() =>
                              Output(new Output.NumberSwitched(Get<RunningData>().currentStimulus))
                         );
                    }
               }
               public record HideNumber : Running
               {
                    public HideNumber()
                    {
                         this.OnEnter(() =>
                              Output(new Output.NumberSwitched(null))
                         );
                    }
               }
          }

          public record End : State;
     }
}
