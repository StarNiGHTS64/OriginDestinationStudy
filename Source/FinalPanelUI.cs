using System;
using System.Collections.Generic;
using ICities;
using UnityEngine;
using ColossalFramework;
using ColossalFramework.Plugins;
using ColossalFramework.UI;

namespace ODStudyF
{
    class FinalPanel : UIPanel
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

    }

    class StageLabelName : UILabel
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

    }

    class ProgressBar : UIPanel
    {
        public override void Start()
        {
            this.isInteractive = true;
            this.backgroundSprite = "ButtonMenu";
            this.color = new Color(155, 155, 155, 255);
            this.width = 150;
            this.height = 15;
            this.relativePosition = new Vector3(20, 40);
        }
    }

    class ProgressBarFull : UIPanel
    {
        public GameObject generalData;
        int currentStage = 0;
        public override void Start()
        {
            generalData = GameObject.Find("Z General Data Handler");
            this.isInteractive = true;
            this.backgroundSprite = "ButtonMenu";
            this.color = new Color(0, 240, 0, 255);
            this.width = 10;
            this.height = 15;
            this.relativePosition = new Vector3(20, 40);
        }

        public override void Update()
        {
            currentStage = generalData.GetComponent<GeneralDataHandler>().stage;
            this.width = 15 + (currentStage * (135 / 11));
        }
    }

    class NumberLabel : UILabel
    {
        public GameObject generalData;
        public ushort currentStage;


        public override void Start()
        {
            generalData = GameObject.Find("Z General Data Handler");
            currentStage = generalData.GetComponent<GeneralDataHandler>().stage;

            this.isInteractive = true;
            this.backgroundSprite = "ButtonMenu";
            this.text = (currentStage + "/11").ToString();
            this.width = 190;
            this.height = 30;
            this.relativePosition = new Vector3(75f, 60);
        }
        public override void Update()
        {
            currentStage = generalData.GetComponent<GeneralDataHandler>().stage;
            this.text = (currentStage + "/11").ToString();

        }

    }

    class ChangeStageButton : UIButton
    {
        public GameObject generalData;
        public ushort currentStage;

        public override void Start()
        {
            generalData = GameObject.Find("Z General Data Handler");
            currentStage = generalData.GetComponent<GeneralDataHandler>().stage;

            this.isInteractive = true;
            this.color = new Color32(0, 255, 0, 255);

            //Set the text to show on the button
            this.text = "Next";

            // Set the button dimensions
            this.width = 50;
            this.height = 50;

            //Style the button
            this.normalBgSprite = "IconPolicyBaseCircle";
            this.disabledBgSprite = "IconPolicyBaseCircle";
            this.hoveredBgSprite = "IconPolicyBaseCircleFocused";
            this.focusedBgSprite = "IconPolicyBaseCircle";
            this.pressedBgSprite = "IconPolicyBaseCirclePressed";

            this.tooltip = "Next Stage";

            this.textColor = new Color32(255, 255, 255, 255);
            this.disabledTextColor = new Color32(7, 7, 7, 255);
            this.hoveredTextColor = new Color32(7, 132, 255, 255);
            this.focusedTextColor = new Color32(255, 255, 255, 255);
            this.pressedTextColor = new Color32(30, 30, 44, 255);

            this.eventClick += NextClick;

            //Enable button sounds
            this.playAudioEvents = true;

            //Place the button
            this.relativePosition = new Vector3(25, 80);
        }



        public void NextClick(UIComponent component, UIMouseEventParameter eventParam)
        {
            Debug.Log("I AM: " + currentStage);
            currentStage = generalData.GetComponent<GeneralDataHandler>().stage;
            switch (currentStage)
            {
                case 0:
                case 2:
                case 3:
                case 4:
                case 6:
                case 7:
                case 9:
                    generalData.GetComponent<GeneralDataHandler>().stage++;
                    currentStage = generalData.GetComponent<GeneralDataHandler>().stage;
                    generalData.GetComponent<GeneralDataHandler>().StageHandler(currentStage);
                    Debug.Log("Stage: " + (currentStage - 1) + " to " + (currentStage));
                    break;

                default:
                    Debug.Log("Cannot Change Stage Right Now");
                    break;
            }
            Debug.Log("NOW I AM: " + currentStage);
        }
    }

    class TestButton : UIButton
    {

        public override void Start()
        {

            this.isInteractive = true;
            this.color = new Color32(255, 255, 0, 255);

            //Set the text to show on the button
            this.text = "Test";

            // Set the button dimensions
            this.width = 50;
            this.height = 50;

            //Style the button
            this.normalBgSprite = "IconPolicyBaseCircle";
            this.disabledBgSprite = "IconPolicyBaseCircle";
            this.hoveredBgSprite = "IconPolicyBaseCircleFocused";
            this.focusedBgSprite = "IconPolicyBaseCircle";
            this.pressedBgSprite = "IconPolicyBaseCirclePressed";

            this.tooltip = "Testing";

            this.textColor = new Color32(255, 255, 255, 255);
            this.disabledTextColor = new Color32(7, 7, 7, 255);
            this.hoveredTextColor = new Color32(7, 132, 255, 255);
            this.focusedTextColor = new Color32(255, 255, 255, 255);
            this.pressedTextColor = new Color32(30, 30, 44, 255);

            this.eventClick += TestClick;

            //Enable button sounds
            this.playAudioEvents = true;

            //Place the button
            this.relativePosition = new Vector3(110, 80);
        }

        public void TestClick(UIComponent component, UIMouseEventParameter eventParam)
        {
            Debug.Log("Hey Look at Me Im a Test");
        }

    }
}
