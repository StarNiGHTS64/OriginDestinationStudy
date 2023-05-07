using System;
using ICities;
using UnityEngine;
using ColossalFramework.Plugins;
using ColossalFramework.UI;

namespace ODStudy
{
    public class Loader : LoadingExtensionBase
    {
        public override void OnLevelLoaded(LoadMode mode)
        {
            GameObject testGameObject = new GameObject("Z Test Object");
            testGameObject.AddComponent<ODStudyBehaviour>();

            GameObject timerBus = new GameObject("Z Timer Object");
            timerBus.AddComponent<Timer>();

            GameObject positionManager = new GameObject("Z Position Manager");
            positionManager.AddComponent<PositionManager>();

            GameObject endGoal = new GameObject("Z End Goal");
            endGoal.AddComponent<EndGoal>();

            GameObject startPoint = new GameObject("Z Start Point");
            startPoint.AddComponent<StartPoint>();

            GameObject busDataHandler = new GameObject("Z Bus Data Handler");

            GameObject generalDataHandler = new GameObject("Z General Data Handler");
            generalDataHandler.AddComponent<GeneralDataHandler>();

            //This get the default UIView
            UIView view = UIView.GetAView();
            
            //This adds an UIComponent to the view

            //GUI Components from Bus State
            UIComponent stateUIC = view.AddUIComponent(typeof(StatePanel));
            UIDragHandle statePanelDrag = (UIDragHandle)stateUIC.AddUIComponent(typeof(UIDragHandle));

            UILabel stateLabel = (UILabel)stateUIC.AddUIComponent(typeof(StateLabel));

            UILabel stateText = (UILabel)stateUIC.AddUIComponent(typeof(StateText));
            UIButton stateLight = (UIButton)stateUIC.AddUIComponent(typeof(StateLight));
            UISprite stateSprite = (UISprite)stateUIC.AddUIComponent(typeof(StateSprite));
            UIButton stateLightHightlight = (UIButton)stateUIC.AddUIComponent(typeof(StateHightLight));

            //GUI Components from Control Panel
            UIComponent stageUIC = view.AddUIComponent(typeof(StagePanel));
            UIDragHandle stagePanelDrag = (UIDragHandle)stageUIC.AddUIComponent(typeof(UIDragHandle));

            UILabel stageLabel = (UILabel)stageUIC.AddUIComponent(typeof(StageLabel));

            UIButton spawnbutton = (UIButton)stageUIC.AddUIComponent(typeof(SpawnButton));
            UIButton clearbutton = (UIButton)stageUIC.AddUIComponent(typeof(ClearButton));
            UIButton stopbutton = (UIButton)stageUIC.AddUIComponent(typeof(StopButton));

            //GUI Components from Metrics
            UIComponent attributesUIC = view.AddUIComponent(typeof(AttributesPanel));
            UIDragHandle attributesPanelDrag = (UIDragHandle)attributesUIC.AddUIComponent(typeof(UIDragHandle));

            UILabel attributesLabel = (UILabel)attributesUIC.AddUIComponent(typeof(AttributesLabel));

            UILabel timerText = (UILabel)attributesUIC.AddUIComponent(typeof(TimerLabel));
            UILabel timerTime = (UILabel)attributesUIC.AddUIComponent(typeof(TimerTime));

        }
    }

    public class ODStudyBehaviour : MonoBehaviour
    {
        void Start()
        {
            //DebugOutputPanel.AddMessage(PluginManager.MessageType.Message, "IT WORKS!!! OH MY STARS!!!!");
            Debug.Log("IT WORKS!!! OH MY STARS!!!!");
        }
        void Update()
        {

        }
    }
}
