using UnityEngine;
using System;

namespace Clocks
{
    public class AnalogClock : Clock
    {
        [SerializeField]
        private GameObject HandHours;
        [SerializeField]
        private GameObject HandMinutes;
        [SerializeField]
        private GameObject HandSeconds;


        /// <summary>
        /// Called from ClockManager Tick method every second.
        /// </summary>
        /// <param name="time"></param>
        public override void SetTime(DateTime time)
        {
            //Debug.Log($"Setting time on '{gameObject.name}' to {time}");

            if (HandHours != null)
                HandHours.transform.localEulerAngles = new Vector3(GetHourHandDegrees(time), 0f, 0f);

            if (HandMinutes != null)
                HandMinutes.transform.localEulerAngles = new Vector3(GetMinuteHandDegrees(time), 0f, 0f);

            if (HandSeconds != null)
                HandSeconds.transform.localEulerAngles = new Vector3(GetSecondHandDegrees(time), 0f, 0f);
        }


        private float GetSecondHandDegrees(DateTime time)
        {
            return (float)time.Second * (360f / 60f);
        }

        private float GetMinuteHandDegrees(DateTime time)
        {
            return (float)time.Minute * (360f / 60f) + (float)(time.Second) * (360f / 60f / 60f);
        }

        private float GetHourHandDegrees(DateTime time)
        {
            return (float)time.Hour * (360f / 12f) +
                   (float)time.Minute * (360f / 12f / 60f) +
                   (float)time.Second * (360f / 12f / 60f / 60f);
        }
    }
}