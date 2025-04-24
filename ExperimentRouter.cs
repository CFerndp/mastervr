using System.Collections.Generic;

namespace ExperimentConstants
{
    public static class ExperimentRouter
    {
        public static readonly Dictionary<string, string> Routes = new Dictionary<string, string>()
        {
            {"P300", "res://experiments/p300/Settings/P300Settings.tscn"},
        };
    }
}
