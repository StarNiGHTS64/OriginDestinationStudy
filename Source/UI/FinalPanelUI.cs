using System;
using System.Collections.Generic;
using ICities;
using UnityEngine;
using ColossalFramework;
using ColossalFramework.Plugins;
using ColossalFramework.UI;
namespace ODStudy
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
            this.text = currentStage.ToString();
            this.width = 190;
            this.height = 30;
            this.relativePosition = new Vector3(27.5f, 15);
        }
        public override void Update()
        {
            currentStage = generalData.GetComponent<GeneralDataHandler>().stage;
            this.text = currentStage.ToString();

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
            this.disabledBgSprite = "IconPolicyBaseCircleDisabled";
            this.hoveredBgSprite = "IconPolicyBaseCircleFocused";
            this.focusedBgSprite = "IconPolicyBaseCircleHovered";
            this.pressedBgSprite = "IconPolicyBaseCirclePressed";

            this.tooltip = "Next Stage";

            this.textColor = new Color32(255, 255, 255, 255);
            this.disabledTextColor = new Color32(7, 7, 7, 255);
            this.hoveredTextColor = new Color32(7, 132, 255, 255);
            this.focusedTextColor = new Color32(255, 255, 255, 255);
            this.pressedTextColor = new Color32(30, 30, 44, 255);

            this.eventClick += ButtonClick;

            //Enable button sounds
            this.playAudioEvents = true;

            //Place the button
            this.relativePosition = new Vector3(10, 60);
        }



        public void ButtonClick(UIComponent component, UIMouseEventParameter eventParam)
        {
            Debug.Log("I AM: " + currentStage);
            switch (currentStage)
            {
                case 0:
                    generalData.GetComponent<GeneralDataHandler>().stage++;
                    currentStage = generalData.GetComponent<GeneralDataHandler>().stage;
                    generalData.GetComponent<GeneralDataHandler>().StageHandler(currentStage);
                    Debug.Log("Stage: " + (currentStage) + " to " + (currentStage + 1));
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
            this.disabledBgSprite = "IconPolicyBaseCircleDisabled";
            this.hoveredBgSprite = "IconPolicyBaseCircleFocused";
            this.focusedBgSprite = "IconPolicyBaseCircleHovered";
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
            this.relativePosition = new Vector3(70, 60);
        }

        public void TestClick(UIComponent component, UIMouseEventParameter eventParam)
        {

            Debug.Log("Hey Look at Me Im a Test");
        }
    }
}
