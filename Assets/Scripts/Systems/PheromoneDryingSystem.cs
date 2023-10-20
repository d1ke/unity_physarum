using Unity.Burst;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Entities;
using Unity.Mathematics;

namespace Physarium
{
    [BurstCompile]
    [UpdateInGroup(typeof(SimulationSystemGroup))]
    [UpdateAfter(typeof(PheromoneSpreadingSystem))]
    public partial struct PheromoneDryingSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
        }

        public void OnDestroy(ref SystemState state)
        {
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var settings = SystemAPI.GetSingleton<SettingsData>();
            var map = SystemAPI.GetSingleton<MapData>();

            var xSize = map.Size.x;
            var ySize = map.Size.y;
            var dryValue = settings.PheromoneDryPerSecond * SystemAPI.Time.DeltaTime;
            var len = map.PheromoneValues.Length;

            for (int i = 0; i < len; ++i)
            {
                var value = map.PheromoneValues[i];
                value -= dryValue;
                if (value < 0)
                    value = 0;
                map.PheromoneValues[i] = value; //math.max(0, value);
            }

            // var mapPheromoneValues = (float*) map.PheromoneValues.GetUnsafePtr();
            // for (var i = 0; i < len; ++i)
            // {
            //     mapPheromoneValues[i] -= dryValue;
            //     if (mapPheromoneValues[i] < 0)
            //         mapPheromoneValues[i] = 0;
            // }
        }
    }
}