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
        public GameObject positionManager;
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
        public bool simulationState;
        [SerializeField]
        public float simulationSpeed;

        public bool hasArrived = false;

        /*-------------------------- Bus ----------------------------------------*/

        /*-------------------------- Spawn Point --------------------------------*/

        /*-------------------------- Start Point --------------------------------*/

        /*-------------------------- End Goal -----------------------------------*/

        public void StageHandler(ushort stage)
        {
            positionManager = GameObject.Find("Z Position Manager");
            switch (stage)
            {
                case 0:
                    //Debug.Log("Current Stage 0");
                    simulationState = true;
                    break;

                case 1:
                    //Debug.Log("Current Stage 1");

                    simulationState = false;
                    break;

                case 2:
                    //Debug.Log("Current Stage 2");
                    simulationState = true;
                    break;

                case 3:
                    positionManager.GetComponent<PositionManager>().GetSegmentByName("DestroyMe");
                    break;

                case 4:
                    positionManager.GetComponent<PositionManager>().CleanTraffic();
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
            positionManager = GameObject.Find("Z Position Manager");
            StageHandler(stage);
        }

        public void Update()
        {
            SimulationManager.instance.SimulationPaused = simulationState;

            if (stage == 1)
            {
                hasArrived = positionManager.GetComponent<PositionManager>().hasArriveYet();
                if (hasArrived)
                {
                    Debug.Log("The bus has arrived");
                    positionManager.GetComponent<PositionManager>().removeCustomName();
                    stage++;
                    StageHandler(stage);
                }
            }

            firstMinute = timerBus.GetComponent<Timer>().firstMinute;
            secondMinute = timerBus.GetComponent<Timer>().secondMinute;
            firstSecond = timerBus.GetComponent<Timer>().firstSecond;
            secondSecond = timerBus.GetComponent<Timer>().secondSecond;
            
            timeIsRunnig = timerBus.GetComponent<Timer>().timeIsRunnig;

        }
    }
}
