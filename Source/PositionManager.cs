using System;
using ICities;
using UnityEngine;
using ColossalFramework;
using ColossalFramework.Plugins;
using ColossalFramework.UI;

namespace ODStudy
{
    public class PositionManager : MonoBehaviour
    {

        ushort busDepotNum;
        ushort busNum;
        ushort goalNum;

        bool respowneded = false;

        public Vector3 busDepotPosition = new Vector3(0f, 0f, 0f);
        public Vector3 busPosition = new Vector3(0f, 0f, 0f);
        public Vector3 goalPosition = new Vector3(0f, 0f, 0f);

        void Start()
        {

        }

        public ushort GetVehicleIDByCustomName(string name)
        {

            for (ushort num = 1; num < 16384; num += 1)
            {
                if ((Singleton<VehicleManager>.instance.m_vehicles.m_buffer[(int)num].m_flags & Vehicle.Flags.CustomName) != (Vehicle.Flags)0)
                {
                    InstanceID id = default(InstanceID);
                    id.Vehicle = num;
                    if (Singleton<InstanceManager>.instance.GetName(id).Equals(name, StringComparison.InvariantCultureIgnoreCase))
                    {
                        return num;
                    }
                }
            }
            return 0;
        }

        public ushort GetBuildingIDByCustomName(string name)
        {
            InstanceManager instance = Singleton<InstanceManager>.instance;
            BuildingManager instance2 = Singleton<BuildingManager>.instance;
            for (ushort num = 1; num < 49152; num += 1)
            {
                if ((instance2.m_buildings.m_buffer[(int)num].m_flags & Building.Flags.CustomName) != Building.Flags.None && instance.GetName(new InstanceID
                {
                    Building = num
                }).Equals(name, StringComparison.InvariantCultureIgnoreCase))
                {
                    return num;
                }
            }
            return 0;
        }

        public bool GetVehiclePosition(ushort id, out Vector3 position)
        {
            Quaternion quaternion;
            Vector3 vector;
            bool position2 = InstanceManager.GetPosition(new InstanceID
            {
                Vehicle = id
            }, out position, out quaternion, out vector);
            //angle = quaternion.eulerAngles.y;
            return position2;
        }

        public bool GetBuildingPosition(ushort id, out Vector3 position)
        {
            Quaternion quaternion;
            Vector3 vector;
            bool position2 = InstanceManager.GetPosition(new InstanceID
            {
                Building = id
            }, out position, out quaternion, out vector);
            //angle = quaternion.eulerAngles.y;
            return position2;
        }

        public bool hasArriveYet()
        {
            busNum = GetVehicleIDByCustomName("TargetBus");
            busDepotNum = GetBuildingIDByCustomName("StartPoint");
            goalNum = GetBuildingIDByCustomName("GoalPoint");

            GetBuildingPosition(busDepotNum, out busDepotPosition);
            GetBuildingPosition(goalNum, out goalPosition);
            GetVehiclePosition(busNum, out busPosition);

            Debug.Log("Start Position: " + busDepotPosition);
            Debug.Log("Goal Position: " + goalPosition);
            Debug.Log("Bus Position: " + busPosition);

            return (goalPosition - busPosition).magnitude <= 40f;
        }

        public void reRespawn(ushort vehicleID)
        {

            /*VehicleManager instance = Singleton<VehicleManager>.instance;
            Vehicle vehicleData = instance.m_vehicles.m_buffer[vehicleID];
            VehicleInfo info = vehicleData.Info;
            //Vehicle vehicle = instance.CreateVehicle(info, new Vector3( -478.4f, 49.7f, 358.6f), default, false);
            bool vehicleCreated = vehicleManager.CreateVehicle(out newVehicle, ref simulationManager.m_randomizer,
                vehicleInfo, position,
                TransferManager.TransferReason.Single1, false, false);
            instance.ReleaseVehicle(vehicleID);*/

            //ushort newVehicle;
            VehicleManager vehicleManager = Singleton<VehicleManager>.instance;
            /*SimulationManager simulationManager = Singleton<SimulationManager>.instance;
            //VehicleInfo vehicleInfo = PrefabCollection<VehicleInfo>.GetPrefab(0);
            Vehicle vehicleData = vehicleManager.m_vehicles.m_buffer[vehicleID];
            VehicleInfo vehicleInfo = vehicleData.Info;
            Vector3 position = new Vector3(-478.4f, 49.7f, 358.6f);
            bool vehicleCreated = vehicleManager.CreateVehicle(out newVehicle, ref simulationManager.m_randomizer,
                vehicleInfo, position,
                TransferManager.TransferReason.Single1, false, false);
            */

            vehicleManager.ReleaseVehicle(vehicleID);


            /*VehicleManager instance = Singleton<VehicleManager>.instance;
            VehicleInfo info = Singleton<VehicleManager>.instance.m_prefabInfos[(int)vehicleData.Info.m_class.m_service][(int)vehicleData.Info.m_vehicleType];
            Vehicle vehicle = Singleton<VehicleManager>.instance.CreateVehicle(info, position, rotation, false);
            Singleton<VehicleManager>.instance.ReleaseVehicle(vehicleData.m_vehicle);*/
        }

        void Update()
        {
            //Debug.Log("Has Arrived?: " + hasArriveYet());
            if(hasArriveYet() && !respowneded)
            {
                busNum = GetVehicleIDByCustomName("TargetBus");
                reRespawn(busNum);
                respowneded = true;
            }
        }
    }



}
