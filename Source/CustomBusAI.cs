using System;
using ICities;
using UnityEngine;
using ColossalFramework;
using ColossalFramework.Plugins;
using ColossalFramework.UI;

namespace ODStudyF
{
	public class CustomBusAI : BusAI
	{
		public override void SetTarget(ushort vehicleID, ref Vehicle data, ushort targetBuilding)
		{
			if ((data.m_flags & Vehicle.Flags.DummyTraffic) != (Vehicle.Flags)0)
			{
				if (targetBuilding != data.m_targetBuilding)
				{
					this.RemoveTarget(vehicleID, ref data);
					//targetBuilding = 40240;
					data.m_targetBuilding = targetBuilding;
					if (targetBuilding != 0)
					{
						Singleton<BuildingManager>.instance.m_buildings.m_buffer[(int)targetBuilding].AddGuestVehicle(vehicleID, ref data);
					}
				}
				if (!this.StartPathFind(vehicleID, ref data))
				{
					data.Unspawn(vehicleID);
				}
			}
			else
			{
				data.m_targetBuilding = targetBuilding;
				if (targetBuilding == 0)
				{
					data.m_flags |= Vehicle.Flags.GoingBack;
				}
				if (!this.StartPathFind(vehicleID, ref data))
				{
					data.Unspawn(vehicleID);
				}
			}
		}

		private void RemoveTarget(ushort vehicleID, ref Vehicle data)
		{
			if ((data.m_flags & Vehicle.Flags.DummyTraffic) != (Vehicle.Flags)0 && data.m_targetBuilding != 0)
			{
				Singleton<BuildingManager>.instance.m_buildings.m_buffer[(int)data.m_targetBuilding].RemoveGuestVehicle(vehicleID, ref data);
				data.m_targetBuilding = 0;
			}
		}
	}
}
