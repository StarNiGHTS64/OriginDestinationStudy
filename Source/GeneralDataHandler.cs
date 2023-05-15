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

        [SerializeField]
        public ushort stage;
        [SerializeField]
        public float simulationSpeed;

        /*-------------------------- Bus ----------------------------------------*/

        /*-------------------------- Spawn Point --------------------------------*/

        /*-------------------------- Start Point --------------------------------*/

        /*-------------------------- End Goal -----------------------------------*/

        public void StageHandler(ushort stage)
        {
            switch (stage)
            {
                case 0:
                    Debug.Log("Current Stage 0");
                    SimulationManager.instance.SimulationPaused = true;
                    break;

                case 1:
                    Debug.Log("Current Stage 1");
                    SimulationManager.instance.SimulationPaused = false;
                    break;

                case 2:
                    break;

                case 3:
                    break;

                case 4:
                    break;

                case 5:
                    break;

                case 6:
                    break;

                case 7:
                    break;

                case 8:
                    break;

                case 9:
                    break;
                    
                case 10:
                    break;

                default:
                    break;
            }
        }

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
