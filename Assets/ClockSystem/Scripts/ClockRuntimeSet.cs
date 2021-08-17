using UnityEngine;
using System.Collections.Generic;

namespace Clocks
{
    [CreateAssetMenu]
    public class ClockRuntimeSet : ScriptableObject
    {
        public List<Clock> Clocks = new List<Clock>();

        public void Add(Clock clock)
        {
            if (!Clocks.Contains(clock))
                Clocks.Add(clock);
        }

        public void Remove(Clock clock)
        {
            if (Clocks.Contains(clock))
                Clocks.Remove(clock);
        }
    }
}
