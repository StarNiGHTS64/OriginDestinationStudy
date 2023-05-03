using System;
using ICities;
using UnityEngine;
using ColossalFramework.Plugins;
using ColossalFramework.UI;

namespace ODStudy
{
    public class GeneralDataHandler : MonoBehaviour
    {
        /*-------------------------- Timer --------------------------------------*/
        public GameObject timerBus;
        public Component timerBusValues;

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

        /*-------------------------- Bus ----------------------------------------*/

        /*-------------------------- Spawn Point --------------------------------*/

        /*-------------------------- Start Point --------------------------------*/

        /*-------------------------- End Goal -----------------------------------*/


        public void Start()
        {
            timerBus = GameObject.Find("Z Timer Object"); 

        }

        public void Update()
        {
            
            firstMinute = timerBus.GetComponent<Timer>().firstMinute;
            secondMinute = timerBus.GetComponent<Timer>().secondMinute;
            firstSecond = timerBus.GetComponent<Timer>().firstSecond;
            secondSecond = timerBus.GetComponent<Timer>().secondSecond;
            
            timeIsRunnig = timerBus.GetComponent<Timer>().timeIsRunnig;

        }
    }
}
