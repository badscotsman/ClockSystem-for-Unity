using UnityEngine;
using System;
using System.Collections;
using TMPro;

namespace Clocks
{
    public class DigitalClock : Clock
    {
        [SerializeField]
        private TMP_Text _timeText;

        [SerializeField]
        private Material _lightMaterial;
        [SerializeField]
        private Color _lightOnColor;
        [SerializeField]
        private Color _lightOffColor;


        /// <summary>
        /// Called from ClockManager Tick method every second.
        /// </summary>
        /// <param name="time"></param>
        public override void SetTime(DateTime time)
        {
            //Debug.Log($"Setting time on '{gameObject.name}' to {time}");
            _timeText.text = time.ToString("h:mm tt");

            if (_lightMaterial)
            {
                BlinkLightColor();
            }
        }

        private void BlinkLightColor()
        {
            _lightMaterial.color = _lightOnColor;
            StartCoroutine(IESetLightOffTimer());
        }

        private IEnumerator IESetLightOffTimer()
        {
            yield return new WaitForSeconds(0.5f);
            _lightMaterial.color = _lightOffColor;
        }
    }
}