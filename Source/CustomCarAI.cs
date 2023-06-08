using System;
using ICities;
using UnityEngine;
using ColossalFramework;
using ColossalFramework.Plugins;
using ColossalFramework.UI;
using ColossalFramework.Math;

namespace ODStudyF
{
    public class CustomCarAI : CarAI
    {

		public override void SimulationStep(ushort vehicleID, ref Vehicle vehicleData, ref Vehicle.Frame frameData, ushort leaderID, ref Vehicle leaderData, int lodPhysics)
		{
			uint currentFrameIndex = Singleton<SimulationManager>.instance.m_currentFrameIndex;
			frameData.m_position += frameData.m_velocity * 0.5f;
			frameData.m_swayPosition += frameData.m_swayVelocity * 0.5f;
			float num = this.m_info.m_acceleration;
			float num2 = this.m_info.m_braking;
			if ((vehicleData.m_flags & Vehicle.Flags.Emergency2) != (Vehicle.Flags)0)
			{
				num *= 2f;
				num2 *= 2f;
			}
			float magnitude = frameData.m_velocity.magnitude;
			Vector3 vector = vehicleData.m_targetPos0 - (Vector4)frameData.m_position;
			float sqrMagnitude = vector.sqrMagnitude;
			float num3 = (magnitude + num) * (0.5f + 0.5f * (magnitude + num) / num2) + this.m_info.m_generatedInfo.m_size.z * 0.5f;
			float num4 = Mathf.Max(magnitude + num, 5f);
			if (lodPhysics >= 2 && (ulong)(currentFrameIndex >> 4 & 3U) == (ulong)((long)(vehicleID & 3)))
			{
				num4 *= 2f;
			}
			float num5 = Mathf.Max((num3 - num4) / 3f, 1f);
			float num6 = num4 * num4;
			float num7 = num5 * num5;
			int i = 0;
			bool flag = false;
			if ((sqrMagnitude < num6 || vehicleData.m_targetPos3.w < 0.01f) && (leaderData.m_flags & (Vehicle.Flags.WaitingPath | Vehicle.Flags.Stopped)) == (Vehicle.Flags)0)
			{
				if (leaderData.m_path != 0U)
				{
					base.UpdatePathTargetPositions(vehicleID, ref vehicleData, frameData.m_position, ref i, 4, num6, num7);
					if ((leaderData.m_flags & Vehicle.Flags.Spawned) == (Vehicle.Flags)0)
					{
						frameData = vehicleData.m_frame0;
						return;
					}
				}
				if ((leaderData.m_flags & Vehicle.Flags.WaitingPath) == (Vehicle.Flags)0)
				{
					while (i < 4)
					{
						float minSqrDistance;
						Vector3 refPos;
						if (i == 0)
						{
							minSqrDistance = num6;
							refPos = frameData.m_position;
							flag = true;
						}
						else
						{
							minSqrDistance = num7;
							refPos = vehicleData.GetTargetPos(i - 1);
						}
						int num8 = i;
						this.UpdateBuildingTargetPositions(vehicleID, ref vehicleData, refPos, leaderID, ref leaderData, ref i, minSqrDistance);
						if (i == num8)
						{
							break;
						}
					}
					if (i != 0)
					{
						Vector4 targetPos = vehicleData.GetTargetPos(i - 1);
						while (i < 4)
						{
							vehicleData.SetTargetPos(i++, targetPos);
						}
					}
				}
				vector = vehicleData.m_targetPos0 - (Vector4)frameData.m_position;
				sqrMagnitude = vector.sqrMagnitude;
			}
			if (leaderData.m_path != 0U && (leaderData.m_flags & Vehicle.Flags.WaitingPath) == (Vehicle.Flags)0)
			{
				NetManager instance = Singleton<NetManager>.instance;
				byte b = leaderData.m_pathPositionIndex;
				byte lastPathOffset = leaderData.m_lastPathOffset;
				if (b == 255)
				{
					b = 0;
				}
				int noise;
				float num9 = 1f + leaderData.CalculateTotalLength(leaderID, out noise);
				PathManager instance2 = Singleton<PathManager>.instance;
				PathUnit.Position pathPos;
				if (instance2.m_pathUnits.m_buffer[(int)((UIntPtr)leaderData.m_path)].GetPosition(b >> 1, out pathPos))
				{
					if ((instance.m_segments.m_buffer[(int)pathPos.m_segment].m_flags & NetSegment.Flags.Flooded) != NetSegment.Flags.None && Singleton<TerrainManager>.instance.HasWater(VectorUtils.XZ(frameData.m_position)))
					{
						leaderData.m_flags2 |= Vehicle.Flags2.Floating;
					}
					instance.m_segments.m_buffer[(int)pathPos.m_segment].AddTraffic(Mathf.RoundToInt(num9 * 2.5f), noise);
					bool flag2 = false;
					if ((b & 1) == 0 || lastPathOffset == 0)
					{
						uint laneID = PathManager.GetLaneID(pathPos);
						if (laneID != 0U)
						{
							Vector3 vector2 = instance.m_lanes.m_buffer[(int)((UIntPtr)laneID)].CalculatePosition((float)pathPos.m_offset * 0.003921569f);
							float num10 = 0.5f * magnitude * magnitude / num2 + this.m_info.m_generatedInfo.m_size.z * 0.5f;
							if (Vector3.Distance(frameData.m_position, vector2) >= num10 - 1f)
							{
								instance.m_lanes.m_buffer[(int)((UIntPtr)laneID)].ReserveSpace(num9);
								flag2 = true;
							}
						}
					}
					if (!flag2 && instance2.m_pathUnits.m_buffer[(int)((UIntPtr)leaderData.m_path)].GetNextPosition(b >> 1, out pathPos))
					{
						uint laneID2 = PathManager.GetLaneID(pathPos);
						if (laneID2 != 0U)
						{
							instance.m_lanes.m_buffer[(int)((UIntPtr)laneID2)].ReserveSpace(num9);
						}
					}
				}
				if ((ulong)(currentFrameIndex >> 4 & 15U) == (ulong)((long)(leaderID & 15)))
				{
					bool flag3 = false;
					uint path = leaderData.m_path;
					int num11 = b >> 1;
					int j = 0;
					while (j < 5)
					{
						bool flag4;
						if (PathUnit.GetNextPosition(ref path, ref num11, out pathPos, out flag4))
						{
							uint laneID3 = PathManager.GetLaneID(pathPos);
							if (laneID3 != 0U && !instance.m_lanes.m_buffer[(int)((UIntPtr)laneID3)].CheckSpace(num9))
							{
								j++;
								continue;
							}
						}
						if (flag4)
						{
							this.InvalidPath(vehicleID, ref vehicleData, leaderID, ref leaderData);
						}
						flag3 = true;
						break;
					}
					if (!flag3)
					{
						leaderData.m_flags |= Vehicle.Flags.Congestion;
					}
				}
			}
			VehicleManager instance3 = Singleton<VehicleManager>.instance;
			float num12;
			if ((leaderData.m_flags & Vehicle.Flags.Stopped) != (Vehicle.Flags)0)
			{
				num12 = 0f;
			}
			else
			{
				num12 = vehicleData.m_targetPos0.w;
				if ((leaderData.m_flags & Vehicle.Flags.DummyTraffic) == (Vehicle.Flags)0)
				{
					float num13 = magnitude * 100f / Mathf.Max(1f, vehicleData.m_targetPos0.w);
					instance3.m_totalTrafficFlow += (uint)Mathf.RoundToInt(num13);
					instance3.m_maxTrafficFlow += 100U;
				}
			}
			if ((leaderData.m_flags & Vehicle.Flags.DummyTraffic) == (Vehicle.Flags)0)
			{
				if (leaderData.m_citizenUnits == 0U)
				{
					instance3.m_tempVehicleCount += 1U;
				}
				else
				{
					uint citizen = Singleton<CitizenManager>.instance.m_units.m_buffer[(int)((UIntPtr)leaderData.m_citizenUnits)].m_citizen0;
					if (citizen == 0U || (Singleton<CitizenManager>.instance.m_citizens.m_buffer[(int)((UIntPtr)citizen)].m_flags & Citizen.Flags.DummyTraffic) == Citizen.Flags.None)
					{
						instance3.m_tempVehicleCount += 1U;
					}
				}
			}
			Quaternion quaternion = Quaternion.Inverse(frameData.m_rotation);
			vector = quaternion * vector;
			Vector3 vector3 = quaternion * frameData.m_velocity;
			Vector3 vector4 = Vector3.forward;
			Vector3 vector5 = Vector3.zero;
			Vector3 zero = Vector3.zero;
			float num14 = 0f;
			float num15 = 0f;
			bool flag5 = false;
			float num16 = 0f;
			if (sqrMagnitude > 1f)
			{
				vector4 = VectorUtils.NormalizeXZ(vector, out num16);
				if (num16 > 1f)
				{
					Vector3 vector6 = vector;
					num4 = Mathf.Max(magnitude, 2f);
					num6 = num4 * num4;
					if (sqrMagnitude > num6)
					{
						vector6 *= num4 / Mathf.Sqrt(sqrMagnitude);
					}
					bool flag6 = false;
					if (vector6.z < Mathf.Abs(vector6.x))
					{
						if (vector6.z < 0f)
						{
							flag6 = true;
						}
						float num17 = Mathf.Abs(vector6.x);
						if (num17 < 1f)
						{
							vector6.x = Mathf.Sign(vector6.x);
							if (vector6.x == 0f)
							{
								vector6.x = 1f;
							}
							num17 = 1f;
						}
						vector6.z = num17;
					}
					float num18;
					vector4 = VectorUtils.NormalizeXZ(vector6, out num18);
					num16 = Mathf.Min(num16, num18);
					float num19 = 1.5707964f * (1f - vector4.z);
					if (num16 > 1f)
					{
						num19 /= num16;
					}
					float num20 = num16;
					if (vehicleData.m_targetPos0.w < 0.1f)
					{
						num12 = this.CalculateTargetSpeed(vehicleID, ref vehicleData, 1000f, num19);
						num12 = Mathf.Min(num12, CalculateMaxSpeed(num20, Mathf.Min(vehicleData.m_targetPos0.w, vehicleData.m_targetPos1.w), num2 * 0.9f));
					}
					else
					{
						num12 = Mathf.Min(num12, this.CalculateTargetSpeed(vehicleID, ref vehicleData, 1000f, num19));
						num12 = Mathf.Min(num12, CalculateMaxSpeed(num20, vehicleData.m_targetPos1.w, num2 * 0.9f));
					}
					num20 += VectorUtils.LengthXZ(vehicleData.m_targetPos1 - vehicleData.m_targetPos0);
					num12 = Mathf.Min(num12, CalculateMaxSpeed(num20, vehicleData.m_targetPos2.w, num2 * 0.9f));
					num20 += VectorUtils.LengthXZ(vehicleData.m_targetPos2 - vehicleData.m_targetPos1);
					num12 = Mathf.Min(num12, CalculateMaxSpeed(num20, vehicleData.m_targetPos3.w, num2 * 0.9f));
					num20 += VectorUtils.LengthXZ(vehicleData.m_targetPos3 - vehicleData.m_targetPos2);
					if (vehicleData.m_targetPos3.w < 0.01f)
					{
						num20 = Mathf.Max(0f, num20 - this.m_info.m_generatedInfo.m_size.z * 0.5f);
					}
					num12 = Mathf.Min(num12, CalculateMaxSpeed(num20, 0f, num2 * 0.9f));
					if (!DisableCollisionCheck(leaderID, ref leaderData))
					{
						CarAI.CheckOtherVehicles(vehicleID, ref vehicleData, ref frameData, ref num12, ref flag5, ref zero, num3, num2 * 0.9f, lodPhysics);
					}
					if (flag6)
					{
						num12 = -num12;
					}
					if (num12 < magnitude)
					{
						float num21 = Mathf.Max(num, Mathf.Min(num2, magnitude));
						num14 = Mathf.Max(num12, magnitude - num21);
					}
					else
					{
						float num22 = Mathf.Max(num, Mathf.Min(num2, -magnitude));
						num14 = Mathf.Min(num12, magnitude + num22);
					}
				}
			}
			else if (magnitude < 0.1f && flag && this.ArriveAtDestination(leaderID, ref leaderData))
			{
				leaderData.Unspawn(leaderID);
				if (leaderID == vehicleID)
				{
					frameData = leaderData.m_frame0;
				}
				return;
			}
			if ((leaderData.m_flags & Vehicle.Flags.Stopped) == (Vehicle.Flags)0 && num12 < 0.1f)
			{
				flag5 = true;
			}
			if (flag5)
			{
				vehicleData.m_blockCounter = (byte)Mathf.Min((int)(vehicleData.m_blockCounter + 1), 255);
			}
			else
			{
				vehicleData.m_blockCounter = 0;
			}
			if (num16 > 1f)
			{
				num15 = Mathf.Asin(vector4.x) * Mathf.Sign(num14);
				vector5 = vector4 * num14;
			}
			else
			{
				num14 = 0f;
				Vector3 vector7 = Vector3.ClampMagnitude(vector * 0.5f - vector3, num2);
				vector5 = vector3 + vector7;
			}
			bool flag7 = (currentFrameIndex + (uint)leaderID & 16U) != 0U;
			Vector3 vector8 = vector5 - vector3;
			Vector3 vector9 = frameData.m_rotation * vector5;
			frameData.m_velocity = vector9 + zero;
			frameData.m_position += frameData.m_velocity * 0.5f;
			frameData.m_swayVelocity = frameData.m_swayVelocity * (1f - this.m_info.m_dampers) - vector8 * (1f - this.m_info.m_springs) - frameData.m_swayPosition * this.m_info.m_springs;
			frameData.m_swayPosition += frameData.m_swayVelocity * 0.5f;
			frameData.m_steerAngle = num15;
			frameData.m_travelDistance += vector5.z;
			frameData.m_lightIntensity.x = 5f;
			frameData.m_lightIntensity.y = ((vector8.z >= -0.1f) ? 0.5f : 5f);
			frameData.m_lightIntensity.z = ((num15 >= -0.1f || !flag7) ? 0f : 5f);
			frameData.m_lightIntensity.w = ((num15 <= 0.1f || !flag7) ? 0f : 5f);
			frameData.m_underground = ((vehicleData.m_flags & Vehicle.Flags.Underground) != (Vehicle.Flags)0);
			frameData.m_transition = ((vehicleData.m_flags & Vehicle.Flags.Transition) != (Vehicle.Flags)0);
			if ((vehicleData.m_flags & Vehicle.Flags.Parking) != (Vehicle.Flags)0 && num16 <= 1f && flag)
			{
				Vector3 vector10 = vehicleData.m_targetPos1 - vehicleData.m_targetPos0;
				if (vector10.sqrMagnitude > 0.01f)
				{
					frameData.m_rotation = Quaternion.LookRotation(vector10);
				}
			}
			else if (num14 > 0.1f)
			{
				if (vector9.sqrMagnitude > 0.01f)
				{
					frameData.m_rotation = Quaternion.LookRotation(vector9);
				}
			}
			else if (num14 < -0.1f && vector9.sqrMagnitude > 0.01f)
			{
				frameData.m_rotation = Quaternion.LookRotation(-vector9);
			}
			base.SimulationStep(vehicleID, ref vehicleData, ref frameData, leaderID, ref leaderData, lodPhysics);
		}

		private static float CalculateMaxSpeed(float targetDistance, float targetSpeed, float maxBraking)
		{
			float num = 0.5f * maxBraking;
			float num2 = num + targetSpeed;
			return Mathf.Sqrt(Mathf.Max(0f, num2 * num2 + 2f * targetDistance * maxBraking)) - num;
		}

		private static bool DisableCollisionCheck(ushort vehicleID, ref Vehicle vehicleData)
		{
			if ((vehicleData.m_flags & Vehicle.Flags.Arriving) != (Vehicle.Flags)0)
			{
				float num = Mathf.Max(Mathf.Abs(vehicleData.m_targetPos3.x), Mathf.Abs(vehicleData.m_targetPos3.z));
				float num2 = 8640f;
				if (num > num2 - 100f)
				{
					return true;
				}
			}
			return false;
		}


	}
}
