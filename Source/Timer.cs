using System;
using ICities;
using UnityEngine;
using ColossalFramework.Plugins;
using ColossalFramework.UI;

namespace ODStudy
{
    public class Timer : MonoBehaviour
    {

        //private float timerDuration = 3f * 60f;
        private float timer;

        public int buttonSignal;

        [SerializeField]
        public int firstMinute;
        [SerializeField]
        public int secondMinute;
        [SerializeField]
        public int firstSecond;
        [SerializeField]
        public int secondSecond;
        [SerializeField]
        public bool timeIsRunnig;


       void Start()
       {
            timeIsRunnig = false;
            ResetTimer();
       }

        void Update()
        {
            if (timeIsRunnig)
            {
                timer += Time.deltaTime;

            } else
            {
                ResetTimer();
            }
            UpdateTimerDisplay(timer);
            
        }

        public void ResetTimer()
        {
            timer = 0f;
        }

        public void UpdateTimerDisplay(float time)
        {
            float minutes = Mathf.FloorToInt(time / 60);
            float seconds = Mathf.FloorToInt(time % 60);

            firstMinute = Mathf.FloorToInt(minutes / 10);
            secondMinute = Mathf.FloorToInt(minutes % 10);
            firstSecond = Mathf.FloorToInt(seconds / 10);
            secondSecond = Mathf.FloorToInt(seconds % 10);

        }

    }
}
