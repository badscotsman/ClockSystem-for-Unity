// ----------------------------------------------------------------------------
// Clock System for Unity
// 
// Author:  Scott H. Cameron
// Date:    08/09/21
// ----------------------------------------------------------------------------

using UnityEngine;
using System;

namespace Clocks
{
    public abstract class Clock : MonoBehaviour
    {
        [SerializeField]
        private ClockRuntimeSet _managerRuntimeSet;

        [SerializeField]
        private AudioClip _tickTock;

        private AudioSource _audio = null;


        public void OnEnable()
        {
            _managerRuntimeSet?.Add(this);
        }

        public void OnDisable()
        {
            _managerRuntimeSet?.Remove(this);
        }


        public void Tick(DateTime time)
        {
            if (_tickTock)
            {
                if (!_audio)
                {
                    if (!gameObject.TryGetComponent(out _audio))
                    {
                        _audio = gameObject.AddComponent<AudioSource>();
                    }
                }

                _audio.spatialBlend = 1.0f;
                _audio.volume = 0.5f;
                _audio.PlayOneShot(_tickTock);
            }
            
            SetTime(time);
        }

        public abstract void SetTime(DateTime time);
    }
}