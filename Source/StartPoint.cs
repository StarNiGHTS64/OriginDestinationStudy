using System;
using ICities;
using UnityEngine;
using ColossalFramework.Plugins;
using ColossalFramework.UI;

namespace ODStudyF
{
    public class StartPoint : MonoBehaviour
    {
        GameObject endGoalObject = new GameObject("Z StartPointObject");
        GameObject endGoal;
        Vector3 startPosition;
        void Start()
        {
            endGoal = GameObject.Find("Z Position Manager");

            SpriteRenderer spriteRenderer = endGoalObject.AddComponent<SpriteRenderer>();
            Sprite newSprite = Resources.Load<Sprite>("StartPointClean");
            spriteRenderer.sprite = newSprite;
        }

        void Update()
        {
            startPosition = endGoal.GetComponent<PositionManager>().busDepotPosition;
            endGoalObject.transform.position = new Vector3(startPosition.x, startPosition.y, 50f);
        }
    }
}
