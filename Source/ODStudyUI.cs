﻿using System;
using System.Collections.Generic;
using ICities;
using UnityEngine;
using ColossalFramework;
using ColossalFramework.Plugins;
using ColossalFramework.UI;

namespace ODStudyF
{
    /*----------------------- State -----------------------*/
    class StatePanel : UIPanel
    {
        public override void Start()
        {
            this.isInteractive = true;
            this.backgroundSprite = "ButtonMenuHovered";
            this.color = new Color32(220, 220, 220, 220);
            this.width = 150;
            this.height = 120;
            this.position = new Vector3(-930, 85);
        }

        public override void Update()
        {

        }
    }

    class StateLabel : UILabel
    {
        public override void Start()
        {
            this.isInteractive = true;
            this.backgroundSprite = "ButtonMenuHovered";
            this.text = "Estado del Bus";
            this.width = 190;
            this.height = 30;
            this.relativePosition = new Vector3(16, 15);

        }

        public override void Update()
        {

        }
    }

    class StateText : UILabel
    {
        public GameObject carState;

        String stateText = " ";
        int busState;

        public override void Start()
        {
            carState = GameObject.Find("Z General Data Handler");

            this.text = stateText;
            this.height = 30;
            this.width = 160;
            this.relativePosition = new Vector3(25, 95);
        }

        public override void Update()
        {

            busState = carState.GetComponent<GeneralDataHandler>().busState;

            switch (busState)
            {
                case 0:
                    stateText = "En Espera...";
                    break;

                case 1:
                    stateText = "En Camino...";
                    break;

                case 2:
                    stateText = "Llego a Meta";
                    break;

                case 3:
                    stateText = "No Disponible";
                    break;

                case 4:
                    stateText = "Buscando...";
                    break;

                default:
                    stateText = "En Espera...";
                    break;
            }

            this.text = stateText;
        }
    }

    class StateHightLight : UIButton
    {
        public GameObject carState;

        int busState;
        public override void Start()
        {
            carState = GameObject.Find("Z General Data Handler");

            this.isInteractive = true;
            this.color = new Color32(255, 255, 255, 255);
            this.height = 60;
            this.width = 50;
            this.normalBgSprite = "ToolbarIconGroup1Hovered";
            this.disabledBgSprite = "ToolbarIconGroup1Hovered";
            this.hoveredBgSprite = "ToolbarIconGroup1Hovered";
            this.focusedBgSprite = "ToolbarIconGroup1Hovered";
            this.pressedBgSprite = "ToolbarIconGroup1Hovered";
            this.relativePosition = new Vector3(50, 34);
        }

        public override void Update()
        {
            busState = carState.GetComponent<GeneralDataHandler>().busState;

            switch (busState)
            {
                case 0:
                    this.color = new Color32(255, 255, 0, 255);
                    break;

                case 1:
                    this.color = new Color32(0, 0, 255, 255);
                    break;

                case 2:
                    this.color = new Color32(0, 255, 0, 255);
                    break;

                case 3:
                    this.color = new Color32(255, 0, 0, 255);
                    break;

                case 4:
                    this.color = new Color32(255, 255, 255, 255);
                    break;

                default:
                    this.color = new Color32(255, 255, 255, 255);
                    break;
            }
        }
    }

    class StateLight : UIButton
    {
        public GameObject carState;

        int busState;
        public override void Start()
        {
            carState = GameObject.Find("Z General Data Handler");

            this.isInteractive = true;
            this.color = new Color32(255, 255, 255, 255);
            this.height = 50;
            this.width = 50;
            this.normalBgSprite = "IconPolicyBaseCircle";
            this.disabledBgSprite = "IconPolicyBaseCircle";
            this.hoveredBgSprite = "IconPolicyBaseCircle";
            this.focusedBgSprite = "IconPolicyBaseCircle";
            this.pressedBgSprite = "IconPolicyBaseCircle";
            this.relativePosition = new Vector3(50, 38);
        }

        public override void Update()
        {
            busState = carState.GetComponent<GeneralDataHandler>().busState;

            switch (busState)
            {
                case 0:
                    this.color = new Color32(255, 255, 0, 255);
                    break;

                case 1:
                    this.color = new Color32(0, 0, 255, 255);
                    break;

                case 2:
                    this.color = new Color32(0, 255, 0, 255);
                    break;

                case 3:
                    this.color = new Color32(255, 0, 0, 255);
                    break;

                case 4:
                    this.color = new Color32(255, 255, 255, 255);
                    break;

                default:
                    this.color = new Color32(255, 255, 255, 255);
                    break;
            }
        }
    }

    class StateSprite : UISprite
    {
        public override void Start()
        {

            this.spriteName = "InfoIconTrafficCongestion";
            this.height = 30;
            this.width = 30;
            this.relativePosition = new Vector3(60, 48);
        }

        public override void Update()
        {

        }

    }

    /*----------------------- Stage -----------------------*/
    class StagePanel : UIPanel
    {
        public override void Start()
        {
            this.isInteractive = true;
            this.backgroundSprite = "ButtonMenu";
            this.color = new Color32(220, 220, 220, 220);
            this.width = 190;
            this.height = 150;
            this.position = new Vector3(-950, -50);
        }

        public override void Update()
        {

        }
    }

    class StageLabel : UILabel
    {
        public override void Start()
        {
            this.isInteractive = true;
            this.backgroundSprite = "ButtonMenu";
            this.text = "Panel de Control";
            this.width = 190;
            this.height = 30;
            this.relativePosition = new Vector3(27.5f, 15);

        }

        public override void Update()
        {

        }
    }

    public class StopButton : UIButton
    {
        public GameObject timer;

        public override void Start()
        {
            timer = GameObject.Find("Z Timer Object");

            this.isInteractive = true;
            this.color = new Color32(255, 0, 0, 255);

            //Set the text to show on the button
            this.text = "Stop";

            // Set the button dimensions
            this.width = 50;
            this.height = 50;

            //Style the button
            this.normalBgSprite = "IconPolicyBaseCircle";
            this.disabledBgSprite = "IconPolicyBaseCircleDisabled";
            this.hoveredBgSprite = "IconPolicyBaseCircleFocused";
            this.focusedBgSprite = "IconPolicyBaseCircleHovered";
            this.pressedBgSprite = "IconPolicyBaseCirclePressed";

            this.tooltip = "Stop";

            this.textColor = new Color32(255, 255, 255, 255);
            this.disabledTextColor = new Color32(7, 7, 7, 255);
            this.hoveredTextColor = new Color32(7, 132, 255, 255);
            this.focusedTextColor = new Color32(255, 255, 255, 255);
            this.pressedTextColor = new Color32(30, 30, 44, 255);

            this.eventClick += ButtonClick;


            //Enable button sounds
            this.playAudioEvents = true;

            //Place the button
            //this.transformPosition = new Vector3(-1.65f, 0.97f);
            this.relativePosition = new Vector3(10, 60);
        }

        public void ButtonClick(UIComponent component, UIMouseEventParameter eventParam)
        {
            timer.GetComponent<Timer>().timeIsRunnig = false;
            Debug.Log("Hey Look at Me Im a Stop");
        }

        public override void Update()
        {

        }
    }

    class ClearButton : UIButton
    {

        public override void Start()
        {
            this.isInteractive = true;
            this.color = new Color32(255, 255, 0, 255);

            //Set the text to show on the button
            this.text = "Clear";

            // Set the button dimensions
            this.width = 50;
            this.height = 50;

            //Style the button
            this.normalBgSprite = "IconPolicyBaseCircle";
            this.disabledBgSprite = "IconPolicyBaseCircleDisabled";
            this.hoveredBgSprite = "IconPolicyBaseCircleFocused";
            this.focusedBgSprite = "IconPolicyBaseCircleHovered";
            this.pressedBgSprite = "IconPolicyBaseCirclePressed";

            this.tooltip = "Clear";

            this.textColor = new Color32(255, 255, 255, 255);
            this.disabledTextColor = new Color32(7, 7, 7, 255);
            this.hoveredTextColor = new Color32(7, 132, 255, 255);
            this.focusedTextColor = new Color32(255, 255, 255, 255);
            this.pressedTextColor = new Color32(30, 30, 44, 255);

            this.eventClick += ButtonClick;

            //Enable button sounds
            this.playAudioEvents = true;

            //Place the button
            //this.transformPosition = new Vector3(-1.65f, 0.97f);
            this.relativePosition = new Vector3(70, 60);
        }



        public void ButtonClick(UIComponent component, UIMouseEventParameter eventParam)
        {
            //random = GetRandomCitizen();
            //Debug.Log("Hey Look at Me Im a Clear");
            //cleanTraffic();
        }

        public override void Update()
        {

        }
    }

    class SpawnButton : UIButton
    {
        public GameObject timer;

        public override void Start()
        {
            timer = GameObject.Find("Z Timer Object");

            this.isInteractive = true;
            this.color = new Color32(0, 255, 0, 255);

            //Set the text to show on the button
            this.text = "Start";

            // Set the button dimensions
            this.width = 50;
            this.height = 50;

            //Style the button
            this.normalBgSprite = "IconPolicyBaseCircle";
            this.disabledBgSprite = "IconPolicyBaseCircleDisabled";
            this.hoveredBgSprite = "IconPolicyBaseCircleFocused";
            this.focusedBgSprite = "IconPolicyBaseCircleHovered";
            this.pressedBgSprite = "IconPolicyBaseCirclePressed";

            this.tooltip = "Start";

            this.textColor = new Color32(255, 255, 255, 255);
            this.disabledTextColor = new Color32(7, 7, 7, 255);
            this.hoveredTextColor = new Color32(7, 132, 255, 255);
            this.focusedTextColor = new Color32(255, 255, 255, 255);
            this.pressedTextColor = new Color32(30, 30, 44, 255);

            this.eventClick += ButtonClick;


            //Enable button sounds
            this.playAudioEvents = true;

            //Place the button
            //this.transformPosition = new Vector3(-1.65f, 0.97f);
            this.relativePosition = new Vector3(130, 60);
        }

        public void ButtonClick(UIComponent component, UIMouseEventParameter eventParam)
        {
            AssignName();
            //timer.GetComponent<Timer>().timeIsRunnig = true;
            //Debug.Log("Hey Look at Me Im a Spawn");
        }

        public void AssignName()
        {
            VehicleManager instance = Singleton<VehicleManager>.instance;
            uint totalVehicles = instance.m_vehicles.m_size;

            if (totalVehicles == 0)
            {
                return;
            }

            for (ushort num = 1; num < totalVehicles; num++)
                if (instance.m_vehicles.m_buffer[num].Info.m_Thumbnail == "Bus")
                {
                    Vehicle.Flags flags = instance.m_vehicles.m_buffer[(int)num].m_flags;
                    //instance.m_vehicles.m_buffer[num].m_flags.SetFlags(Vehicle.Flags.CustomName, true);
                    instance.m_vehicles.m_buffer[(int)num].m_flags = (flags | Vehicle.Flags.CustomName);
                    InstanceID id = default(InstanceID);
                    id.Vehicle = num;
                    Singleton<InstanceManager>.instance.SetName(id, "Foo");
                    Debug.Log("Hi i am a bus: " + num);
                }
        }

        public override void Update()
        {

        }
    }

    /*----------------------- Attributes -----------------------*/
    class AttributesPanel : UIPanel
    {
        public override void Start()
        {
            this.isInteractive = true;
            this.backgroundSprite = "ButtonMenuDisabled";
            this.color = new Color32(220, 220, 220, 220);
            this.width = 380;
            this.height = 150;
            this.position = new Vector3(580, 480);
        }

        public override void Update()
        {

        }

    }

    class AttributesLabel : UILabel
    {
        public override void Start()
        {
            this.isInteractive = true;
            this.backgroundSprite = "ButtonMenuDisabled";
            this.text = "Metricas";
            this.width = 190;
            this.height = 30;
            this.relativePosition = new Vector3(153, 15);

        }

        public override void Update()
        {

        }
    }

    class TimerTime : UILabel
    {
        public GameObject timerBus;

        int secondsUnit, secondsDec, minutesUnit, minutesDec;
        float time = 0;

        public override void Start()
        {
            timerBus = GameObject.Find("Z General Data Handler");

            secondsUnit = 0;
            secondsDec = 0;
            minutesUnit = 0;
            minutesDec = 0;

            this.text = minutesDec.ToString() + minutesUnit.ToString() + ":" + secondsDec.ToString() + secondsUnit.ToString();
            this.textColor = new Color32(255, 0, 0, 255);
            this.width = 73;
            this.height = 18;
            this.relativePosition = new Vector3(200, 60);

        }

        public void UpdateTimerDisplay(float time)
        {
            float minutes = Mathf.FloorToInt(time / 60);
            float seconds = Mathf.FloorToInt(time % 60);

            minutesDec = Mathf.FloorToInt(minutes / 10);
            minutesUnit = Mathf.FloorToInt(minutes % 10);
            secondsDec = Mathf.FloorToInt(seconds / 10);
            secondsUnit = Mathf.FloorToInt(seconds % 10);

        }

        public override void Update()
        {

            time = timerBus.GetComponent<GeneralDataHandler>().firstTimeTimer;
            UpdateTimerDisplay(time);


            this.text = minutesDec.ToString() + minutesUnit.ToString() + ":" + secondsDec.ToString() + secondsUnit.ToString();
        }
    }

    class TimerLabel : UILabel
    {

        public override void Start()
        {

            this.text = "Tiempo 1:";
            this.width = 73;
            this.height = 18;
            this.relativePosition = new Vector3(50, 60);

        }

        public override void Update()
        {

        }
    }

    class TimerTime2 : UILabel
    {
        public GameObject timerBus;

        int secondsUnit, secondsDec, minutesUnit, minutesDec;
        float time = 0;

        public override void Start()
        {
            timerBus = GameObject.Find("Z General Data Handler");

            secondsUnit = 0;
            secondsDec = 0;
            minutesUnit = 0;
            minutesDec = 0;

            this.text = minutesDec.ToString() + minutesUnit.ToString() + ":" + secondsDec.ToString() + secondsUnit.ToString();
            this.textColor = new Color32(255, 0, 0, 255);
            this.width = 73;
            this.height = 18;
            this.relativePosition = new Vector3(200, 90);

        }

        public void UpdateTimerDisplay(float time)
        {
            float minutes = Mathf.FloorToInt(time / 60);
            float seconds = Mathf.FloorToInt(time % 60);

            minutesDec = Mathf.FloorToInt(minutes / 10);
            minutesUnit = Mathf.FloorToInt(minutes % 10);
            secondsDec = Mathf.FloorToInt(seconds / 10);
            secondsUnit = Mathf.FloorToInt(seconds % 10);

        }

        public override void Update()
        {
            time = timerBus.GetComponent<GeneralDataHandler>().secondTimeTimer;
            UpdateTimerDisplay(time);


            this.text = minutesDec.ToString() + minutesUnit.ToString() + ":" + secondsDec.ToString() + secondsUnit.ToString();
        }
    }

    class TimerLabel2 : UILabel
    {

        public override void Start()
        {

            this.text = "Tiempo 2:";
            this.width = 73;
            this.height = 18;
            this.relativePosition = new Vector3(50, 90);

        }

        public override void Update()
        {

        }
    }

}


