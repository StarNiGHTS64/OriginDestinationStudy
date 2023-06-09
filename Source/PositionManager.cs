﻿using System;
using ICities;
using UnityEngine;
using ColossalFramework;
using ColossalFramework.Plugins;
using ColossalFramework.UI;

namespace ODStudyF
{
    public class PositionManager : MonoBehaviour
    {

        ushort busDepotNum;
        ushort busNum;
        ushort goalNum;


        public Vector3 busDepotPosition = new Vector3(0f, 0f, 0f);
        public Vector3 busPosition = new Vector3(0f, 0f, 0f);
        public Vector3 goalPosition = new Vector3(0f, 0f, 0f);

        public Vector3 iterationBusPosition = new Vector3(0f, 0f, 0f);

        void Start()
        {
            //assignDestination();
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
            uint totalBuildings = instance2.m_buildings.m_size;


            for (ushort num = 1; num < totalBuildings; num++)
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

        public void assignDestination()
        {
            VehicleManager instance = Singleton<VehicleManager>.instance;
            uint totalVehicles = instance.m_vehicles.m_size; ;

            if (totalVehicles == 0)
            {
                return;
            }

            for (ushort num = 1; num < totalVehicles; num++)
                if (instance.m_vehicles.m_buffer[num].Info.m_Thumbnail == "Bus")
                {

                    //getPosition
                    //Compare it to the star position 
                    //ushort instanceID = instance.m_vehicles.m_buffer[num].GetInstanceID();
                    //bool result = vehicleData.Info.m_vehicleAI.SetVehicleName(num, ref vehicleData, "TargetBus");
                    Debug.Log("Hi i am a bus: " + num);
                    Debug.Log("Result: " + instance.SetVehicleName(num, "foo"));
                }

            //
        }

        public bool LookUpBus()
        {
            VehicleManager instance = Singleton<VehicleManager>.instance;
            uint totalVehicles = instance.m_vehicles.m_size;



            if (totalVehicles == 0)
            {
                return false;
            }

            for (ushort num = 1; num < totalVehicles; num++)
                if (instance.m_vehicles.m_buffer[num].Info.m_Thumbnail == "Bus")
                {
                    GetVehiclePosition(num, out iterationBusPosition);

                    if((busDepotPosition - iterationBusPosition).magnitude >= 40f && (busDepotPosition - iterationBusPosition).magnitude <= 60f)
                    {
                        Debug.Log("I FOUND ONE: " + num);
                        Vehicle.Flags flags = instance.m_vehicles.m_buffer[(int)num].m_flags;
                        //instance.m_vehicles.m_buffer[num].m_flags.SetFlags(Vehicle.Flags.CustomName, true);
                        instance.m_vehicles.m_buffer[(int)num].m_flags = (flags | Vehicle.Flags.CustomName);
                        InstanceID id = default(InstanceID);
                        id.Vehicle = num;
                        Singleton<InstanceManager>.instance.SetName(id, "TargetBus");
                        return true;
                    }
                }
            return false;
        }
        
        public bool HasDeparted()
        {
            if((busDepotPosition - busPosition).magnitude >= 65f)
            {
                return true;
            }
            return false;
        }

        public bool HasReturned()
        {
            if((busDepotPosition - busPosition).magnitude <= 55f)
            {
                return true;
            }
            return false;
        }

        public void removeCustomName()
        {
            VehicleManager instance = Singleton<VehicleManager>.instance;
            Debug.Log("ID: " + busNum);
            Vehicle.Flags flags = instance.m_vehicles.m_buffer[busNum].m_flags;
            instance.m_vehicles.m_buffer[(int)busNum].m_flags = (flags & ~Vehicle.Flags.CustomName);
            instance.m_vehicles.m_buffer[(int)busNum].m_flags = (flags & ~Vehicle.Flags.GoingBack);
            InstanceID id = default(InstanceID);
            id.Vehicle = busNum;
            Singleton<InstanceManager>.instance.SetName(id, "Bus");
            //instance.SetVehicleName(busNum, "Bus");
        }

        public bool hasArriveYet()
        {
            busNum = GetVehicleIDByCustomName("TargetBus");
            busDepotNum = GetBuildingIDByCustomName("StartPoint");
            goalNum = GetBuildingIDByCustomName("GoalPoint");

            GetBuildingPosition(busDepotNum, out busDepotPosition);
            GetBuildingPosition(goalNum, out goalPosition);
            GetVehiclePosition(busNum, out busPosition);

            /*Debug.Log("Start Position: " + busDepotPosition);
            Debug.Log("Goal Position: " + goalPosition);  
            Debug.Log("Bus Position: " + busPosition);*/

            return (goalPosition - busPosition).magnitude <= 60f;
        }

        public void BulldozeStreets()
        {
            NetManager instance = Singleton<NetManager>.instance;
            uint totalSegments = instance.m_segments.m_size;

            if (totalSegments == 0)
            {
                return;
            }


        }

        public void GetSegmentByName(string segmentName)
        {
            for (ushort num = 0; num < Singleton<NetManager>.instance.m_segments.m_size; num++)
            {
                if (Singleton<NetManager>.instance.m_segments.m_buffer[num].m_flags != NetSegment.Flags.None && Singleton<NetManager>.instance.GetSegmentName(num) == segmentName)
                {
                    //Debug.Log("Hi I" + num + " am a Segment");
                    BulldozeSegment(num);
                }
            }
        }
        public static void BulldozeSegment(ushort segmentID)
        {
            Singleton<NetManager>.instance.ReleaseSegment(segmentID, false);
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

        public void CleanTraffic()
        {
            VehicleManager instance = Singleton<VehicleManager>.instance;
            uint totalVehicles = instance.m_vehicles.m_size;

            if (totalVehicles == 0)
            {
                return;
            }

            for (ushort i = 1; i < totalVehicles; i++)
            {
                instance.ReleaseVehicle(i);
            }
        }

        public void CleanBuses()
        {
            VehicleManager instance = Singleton<VehicleManager>.instance;
            uint totalVehicles = instance.m_vehicles.m_size; ;

            if (totalVehicles == 0)
            {
                return;
            }

            for (ushort num = 1; num < totalVehicles; num++)
                if (instance.m_vehicles.m_buffer[num].Info.m_Thumbnail == "Bus")
                {
                    instance.ReleaseVehicle(num);
                }
        }

        /*public void CleanParked()
        {
            VehicleManager instance = Singleton<VehicleManager>.instance;
            uint totalParked = instance.m_parkedVehicles.m_size;

            if (totalParked == 0)
            {
                return;
            }

            for(ushort i = 1; i < totalParked; i++)
            {
              
            }
        }*/



        void Update()
        {

        }
    }



}
