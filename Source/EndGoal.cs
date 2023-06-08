using System;
using ICities;
using UnityEngine;
using ColossalFramework.Plugins;
using ColossalFramework.UI;

namespace ODStudyF
{
    public class EndGoal : MonoBehaviour
    {
        GameObject endGoalObject = new GameObject("Z EndGoalObject");
        GameObject endGoal;
        Vector3 goalPosition;
        void Start()
        {
            endGoal = GameObject.Find("Z Position Manager");

            SpriteRenderer spriteRenderer = endGoalObject.AddComponent<SpriteRenderer>();
            Sprite newSprite = Resources.Load<Sprite>("GoalPointClean");
            spriteRenderer.sprite = newSprite;
        }

        void Update()
        {
            goalPosition = endGoal.GetComponent<PositionManager>().goalPosition;
            endGoalObject.transform.position = new Vector3(goalPosition.x, goalPosition.y, 50f);
        }
    }
}
