using System;

namespace Clocks
{
    internal interface ITimeProvider
    {
        event Action<DateTime> OnChangeTimeZone;
        void GetTime(Action<DateTime> callback);
    }
}