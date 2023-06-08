using System;
using ICities;
using UnityEngine;
using ColossalFramework;
using ColossalFramework.Plugins;
using ColossalFramework.UI;
using System.Reflection;

namespace ODStudyF
{
    public class GeneralDataHandler : MonoBehaviour
    {
        /*-------------------------- Timer --------------------------------------*/
        public GameObject positionManager;

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
        public bool simulationIsPaused;
        [SerializeField]
        public int simulationSpeed;

        public bool hasArrived = false;
        public bool hasArrived2 = false;
        public bool busFound = false;

        public int busState = 0;

        public bool hasDeparted1 = false;
        public bool hasDeparted2 = false;

        public bool hasReturned = false;

        public float firstTimeTimer = 0.0f;
        public float secondTimeTimer = 0.0f;
        public float repopulateTimer = 60.0f;

        public void StageHandler(ushort stage)
        {

            positionManager = GameObject.Find("Z Position Manager");
            switch (stage)
            {
                case 0:
                    //Debug.Log("Current Stage 0");
                    simulationIsPaused = true;
                    busState = 0;
                    break;

                case 1:
                    //Debug.Log("Current Stage 1");

                    simulationIsPaused = false;
                    busState = 1;
                    break;

                case 2:
                    //Debug.Log("Current Stage 2");
                    simulationIsPaused = true;
                    busState = 2;
                    break;

                case 3:
                    string segmentName;
                    positionManager.GetComponent<PositionManager>().GetSegmentByName("DestroyMe");
                    for(int i = 0; i < 5; i++)
                    {
                        segmentName = "DestroyMe" + i;
                        Debug.Log(segmentName);
                        positionManager.GetComponent<PositionManager>().GetSegmentByName(segmentName);
                    }
                    busState = 3;
                    break;

                case 4:
                    positionManager.GetComponent<PositionManager>().CleanTraffic();
                    busState = 3;
                    break;

                case 5:
                    simulationIsPaused = false;
                    simulationSpeed = 16;
                    break;

                case 6:
                    simulationIsPaused = true;
                    simulationSpeed = 1;
                    ChangeSimulationSpeed(simulationSpeed);
                    break;

                case 7:
                    positionManager.GetComponent<PositionManager>().CleanBuses();
                    break;

                case 8:
                    simulationIsPaused = false;
                    busState = 4;
                    break;

                case 9:
                    simulationIsPaused = true;
                    busState = 0;
                    break;

                case 10:
                    simulationIsPaused = false;
                    busState = 1;
                    break;

                case 11:
                    simulationIsPaused = true;
                    busState = 2;
                    break;

                default:
                    break;
            }
        }

        public void ChangeSimulationSpeed(int simulationSpeed)
        {
            if (Singleton<SimulationManager>.exists)

            {
                //Singleton<SimulationManager>.instance.AddAction(delegate ()
                //{
                    Singleton<SimulationManager>.instance.SelectedSimulationSpeed = simulationSpeed + 1;
                //});
            }
        }

        public int GetSimulationSpeed()
        {
            if (Singleton<SimulationManager>.exists)
            {
                return Singleton<SimulationManager>.instance.SelectedSimulationSpeed - 1;
            }
            return 0;
        }

        public void Start()
        {
            positionManager = GameObject.Find("Z Position Manager");
            StageHandler(stage);
        }

        public void Update()
        {
            SimulationManager.instance.SimulationPaused = simulationIsPaused;

            if (stage == 1)
            {
                if (!hasDeparted1)
                {
                    hasDeparted1 = positionManager.GetComponent<PositionManager>().HasDeparted();
                }
                
                
                hasArrived = positionManager.GetComponent<PositionManager>().hasArriveYet();
                if (hasArrived)
                {
                    Debug.Log("The bus has arrived");
                    positionManager.GetComponent<PositionManager>().removeCustomName();
                    stage++;
                    StageHandler(stage);
                } else if (hasDeparted1)
                {
                    firstTimeTimer += ((Time.deltaTime * 6) * Mathf.Pow(2,GetSimulationSpeed()));
                }
            }
            if (stage == 5)
            {
                repopulateTimer -= Time.deltaTime;
                ChangeSimulationSpeed(simulationSpeed);
                Debug.Log("Simulation Speed: " + GetSimulationSpeed());
                if (repopulateTimer < 0)
                {
                    stage++;
                    StageHandler(stage);
                }
            }
            if (stage == 8)
            {
                busFound = positionManager.GetComponent<PositionManager>().LookUpBus();
                if (busFound)
                {
                    stage++;
                    StageHandler(stage);
                }
            }
            if (stage == 10)
            {

                if (!hasDeparted2)
                {
                    hasDeparted2 = positionManager.GetComponent<PositionManager>().HasDeparted();
                }

                hasArrived2 = positionManager.GetComponent<PositionManager>().hasArriveYet();
                if(hasArrived2)
                {
                    Debug.Log("The bus has arrived");
                    positionManager.GetComponent<PositionManager>().removeCustomName();
                    stage++;
                    StageHandler(stage);
                } else if (hasReturned && hasDeparted2)
                {
                    hasReturned = false;
                    hasDeparted2 = false;
                    secondTimeTimer = 0.0f;
                }
                else if (hasDeparted2)
                {
                    hasReturned = positionManager.GetComponent<PositionManager>().HasReturned();
                    secondTimeTimer += ((Time.deltaTime * 6.1f) * Mathf.Pow(2, GetSimulationSpeed()));
                }
            }

            

        }
    }
}
