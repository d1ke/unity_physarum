using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace Physarium
{
    [UpdateInGroup(typeof(SimulationSystemGroup))]
    public partial struct AgentRotateSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
        }

        public void OnDestroy(ref SystemState state)
        {
        }

        public void OnUpdate(ref SystemState state)
        {
            var settings = SystemAPI.GetSingleton<SettingsData>();
            var map = SystemAPI.GetSingleton<MapData>();

            foreach (var (direction, velocity, position) in SystemAPI.Query<RefRW<DirectionData>, RefRW<VelocityData>, PositionData>())
            {
                var agentDirection = direction.ValueRO.Value;

                var leftSensorDirection = getSensorDirection(agentDirection, -settings.SideSensorsAngle);
                var middleSensorDirection = agentDirection;
                var rightSensorDirection = getSensorDirection(agentDirection, settings.SideSensorsAngle);

                var leftSensorPosition = position.CurrentValue + leftSensorDirection * settings.SensorsDistance;
                var middleSensorPosition = position.CurrentValue + middleSensorDirection * settings.SensorsDistance;
                var rightSensorPosition = position.CurrentValue + rightSensorDirection * settings.SensorsDistance;

                var leftSensorValue = map.GetPheromone((int2)leftSensorPosition);
                var middleSensorValue = map.GetPheromone((int2)middleSensorPosition);
                var rightSensorValue = map.GetPheromone((int2)rightSensorPosition);

                if (leftSensorValue > middleSensorValue && middleSensorValue > rightSensorValue)
                {
                    direction.ValueRW.Value = leftSensorDirection;
                }
                else if (rightSensorValue > middleSensorValue && middleSensorValue > leftSensorValue)
                {
                    direction.ValueRW.Value = rightSensorDirection;
                }
                else if (leftSensorValue > middleSensorValue && rightSensorValue > middleSensorValue)
                {
                    direction.ValueRW.Value = settings.Random.NextBool() ? leftSensorDirection : rightSensorDirection;
                }

                velocity.ValueRW.Value = direction.ValueRO.Value * settings.AgentsSpeed;
            }
        }

        private static float2 getSensorDirection(float2 agentDirection, float sensorAngle)
        {
            var cos = math.cos(sensorAngle);
            var sin = math.sin(sensorAngle);
            return new float2(cos * agentDirection.x - sin * agentDirection.y, sin * agentDirection.x + cos * agentDirection.y);
        }
    }
}