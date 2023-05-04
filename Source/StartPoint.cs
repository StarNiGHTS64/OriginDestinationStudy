using System;
using ICities;
using UnityEngine;
using ColossalFramework.Plugins;
using ColossalFramework.UI;

namespace ODStudy
{
    public class StartPoint : MonoBehaviour
    {
        

        void Start()
        {
            GameObject startPoint = new GameObject("Z StartObject");
            startPoint.AddComponent<MeshRenderer>();

            MeshFilter startPointMesh = startPoint.AddComponent<MeshFilter>();
            GameObject newMesh = Resources.Load("StartPoint") as GameObject;
            startPointMesh.mesh = newMesh.GetComponent<MeshFilter>().sharedMesh;
        }

        void Update()
        {
            
        }
    }
}
