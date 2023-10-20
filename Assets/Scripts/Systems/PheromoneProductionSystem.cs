using Unity.Entities;
using Unity.Mathematics;

namespace Physarium
{
    [UpdateInGroup(typeof(SimulationSystemGroup))]
    [UpdateAfter(typeof(AgentMovementSystem))]
    public partial struct PheromoneProductionSystem : ISystem
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

            foreach (var position in SystemAPI.Query<PositionData>())
            {
                var previousPosition = (int2)position.PreviousFrameValue;
                var currentPosition = (int2)position.CurrentValue;
                var deltaPosition = currentPosition - previousPosition;
                var stepsCount = math.max(math.abs(deltaPosition.x), math.abs(deltaPosition.y)) + 1;
                var stepsCountF = (float) stepsCount;
                float2 floatPosition = previousPosition;

                for (int i = 0; i < stepsCount; ++i)
                {
                    floatPosition.x += deltaPosition.x / stepsCountF;
                    floatPosition.y += deltaPosition.y / stepsCountF;

                    var mapIndex = map.GetMapIndex((int2)floatPosition);
                    var pheromoneValue = map.PheromoneValues[mapIndex];
                    pheromoneValue += settings.PheromoneGenerationPerTick;
                    map.PheromoneValues[mapIndex] = math.min(1, pheromoneValue);
                }
            }
        }
    }
}