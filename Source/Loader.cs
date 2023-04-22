using System;
using ICities;
using UnityEngine;
using ColossalFramework.Plugins;

namespace ODStudy
{
    public class Loader : LoadingExtensionBase
    {
        public override void OnLevelLoaded(LoadMode mode)
        {
            GameObject testGameObject = new GameObject("Test Object");
            testGameObject.AddComponent<ODStudyBehaviour>();
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
