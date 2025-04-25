using System;
using System.Collections.Generic;

public static class P300Utils
{
    public static List<int> GenerateStimuliArray(int numberOfStimuli)
    {
        List<int> list = new List<int>();

        for (int i = 1; i <= numberOfStimuli; i++)
        {
            list.Add(i);
        }

        Random rng = new Random();

        // Fisher-Yates shuffle
        for (int i = list.Count - 1; i > 0; i--)
        {
            int j = rng.Next(i + 1); // entre 0 y i inclusive
            (list[i], list[j]) = (list[j], list[i]); // swap
        }

        return list;
    }
}

public enum P300Markers
{
    ShowNumber = 10,
    HideNumber = 11,
}