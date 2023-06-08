using System;
using ICities;
using UnityEngine;
using ColossalFramework.Plugins;
using ColossalFramework.UI;
using ColossalFramework;

namespace ODStudyF
{
    class DisableUnusedFeatures : MonoBehaviour
    {

        public void DisableEvents()
        {
            BuildingManager instance = Singleton<BuildingManager>.instance;
            uint totalBuildings = instance.m_buildings.m_size;

            if (totalBuildings == 0)
            {
                return;
            }

            for (ushort i = 1; i < totalBuildings; i++)
            {
                if (instance.m_buildings.m_buffer[i].m_flags == Building.Flags.None)
                {
                    continue;
                }

                instance.m_buildings.m_buffer[i].m_crimeBuffer = 0;

                instance.m_buildings.m_buffer[i].m_healthProblemTimer = 0;
                instance.m_buildings.m_buffer[i].m_health = 0;
                instance.m_buildings.m_buffer[i].m_childHealth = 0;
                instance.m_buildings.m_buffer[i].m_seniorHealth = 0;

                instance.m_buildings.m_buffer[i].m_fireIntensity = 0;
                instance.m_buildings.m_buffer[i].m_fireHazard = 0;

                instance.m_buildings.m_buffer[i].m_garbageBuffer = 0;
                instance.m_buildings.m_buffer[i].m_garbageTrafficRate = 0;

                instance.m_buildings.m_buffer[i].m_deathProblemTimer = 0;

                instance.m_buildings.m_buffer[i].m_workerProblemTimer = 0;
            }
        }

        public void Start()
        {

        }
        public void Update()
        {
            DisableEvents();
        }

    }

    public class UnlimitedMoney : EconomyExtensionBase
    {

        public override long OnUpdateMoneyAmount(long budget)
        {
            return long.MaxValue;
        }

        public override bool OverrideDefaultPeekResource
        {
            get { return true; }
        }

        public override int OnPeekResource(EconomyResource resource, int amount)
        {
            return amount;
        }
    }
}
