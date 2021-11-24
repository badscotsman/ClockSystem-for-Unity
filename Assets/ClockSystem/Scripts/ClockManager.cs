// ----------------------------------------------------------------------------
// Clock System for Unity
// 
// Author:  Scott H. Cameron
// Date:    08/09/21
// ----------------------------------------------------------------------------

using UnityEngine;
using System;
using System.Collections;

namespace Clocks
{
    // TODO: implement a day/night cycle driven from ClockManager time?
    // TODO: implement precise sunrise/sunset times? Ref. https://sunrise-sunset.org/api
    
    public class ClockManager : MonoBehaviour
    {
        [SerializeField]
        private ClockRuntimeSet _runtimeSet;

        public enum TimeRefernces { SystemTime, SpecifiedTime, TimeProvider }
        [SerializeField]
        private TimeRefernces _timeReference = TimeRefernces.SystemTime;
        
        [SerializeField]
        private string _timeSpecifiedStart = "12/21/2020 5:02:00 AM";
        private DateTime _timeProviderStart;
        private DateTime _timeStart;

        private ITimeProvider _timeProvider;
        private readonly WaitForSeconds _tickCycle = new WaitForSeconds(1f);


        private void Start()
        {
            _timeStart = DateTime.Now;
            TickTock();
        }


        private void OnDisable()
        {
            if (_timeProvider != null)
            {
                _timeProvider.OnChangeTimeZone -= SetProviderTimeStart;
            }            
        }


        private void TickTock()
        {
            StartCoroutine(IETick());
        }

        private IEnumerator IETick()
        {
            while (true)
            {
                foreach (var clock in _runtimeSet.Clocks)
                {
                    switch (_timeReference)
                    {
                        case TimeRefernces.SystemTime:
                            clock.Tick(DateTime.Now);
                            break;

                        case TimeRefernces.SpecifiedTime:
                            clock.Tick(DateTime.Parse(_timeSpecifiedStart).AddSeconds(DateTime.Now.Subtract(_timeStart).TotalSeconds));
                            break;

                        case TimeRefernces.TimeProvider:
                            if (_timeProvider == null)
                            {
                                SetTimeProvider();
                                break;
                            }
                            
                            clock.Tick(_timeProviderStart.AddSeconds(DateTime.Now.Subtract(_timeStart).TotalSeconds));
                            break;
                    }
                }

                yield return _tickCycle;
            }
        }


        private void SetTimeProvider()
        {
            if (gameObject.TryGetComponent<ITimeProvider>(out _timeProvider))
            {
                _timeProvider.GetTime(SetProviderTimeStart);
                _timeProvider.OnChangeTimeZone += SetProviderTimeStart;
            }
            else
            {
                Debug.LogWarning("No Time Provider found!");
            }
        }

        private void SetProviderTimeStart(DateTime dateTime)
        {
            _timeStart = DateTime.Now;
            _timeProviderStart = dateTime;
        }
    }
}