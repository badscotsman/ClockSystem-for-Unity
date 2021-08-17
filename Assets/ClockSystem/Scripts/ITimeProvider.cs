using System;

namespace Clocks
{
    internal interface ITimeProvider
    {
        event Action<DateTime> ChangeTimeZone;
        void GetTime(Action<DateTime> callback);
    }
}