using Chickensoft.Introspection;
using Chickensoft.LogicBlocks;

[Meta, LogicBlock(typeof(State), Diagram = true)]
public partial class ExperimentState : LogicBlock<ExperimentState.State>
{
     public override Transition GetInitialState() => To<State.Instructions>();

     public static class Input {
          public readonly record struct StartExperiment;
          public readonly record struct EndExperiment;
     }
     public static class Output;


     public abstract partial record State : StateLogic<State>
     {
          public record Instructions : State, IGet<Input.StartExperiment>{
               public Transition On(in Input.StartExperiment input) => To<Running>();
          }
          public record Running : State, IGet<Input.EndExperiment>{
               public Transition On(in Input.EndExperiment input) => To<End>();
          }
          
          public record End : State;
     }
}
