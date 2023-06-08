using System;
using ICities;
using UnityEngine;
using ColossalFramework.Plugins;

namespace ODStudyF
{
    public class ODStudy : IUserMod
    {
        public string Name
        {
            get { return "Origin - Destination StudyF"; }
        }

        public string Description
        {
            get { return "This mod will perform an Origin - Destination Study collecting travel parameters from a point A to a point B"; }
        }

    }
}
