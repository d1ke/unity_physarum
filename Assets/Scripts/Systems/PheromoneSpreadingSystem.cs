using System;
using Unity.Assertions;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace Physarium
{
    [UpdateInGroup(typeof(SimulationSystemGroup))]
    [UpdateAfter(typeof(PheromoneProductionSystem))]
    public partial struct PheromoneSpreadingSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
        }

        public void OnDestroy(ref SystemState state)
        {
        }

        public void OnUpdate(ref SystemState state)
        {
            unsafe
            {
                // var settings = SystemAPI.GetSingleton<SettingsData>();
                // var neighbourCoefficient = math.min(1, settings.PheromoneSpreadingPercentPerSecond * SystemAPI.Time.DeltaTime);
                // var centerCoefficient = 1 - neighbourCoefficient * 2;
                // Assert.AreEqual(centerCoefficient + neighbourCoefficient + neighbourCoefficient, 1);
                //
                // var map = SystemAPI.GetSingleton<MapData>();
                // var pheromoneValues = (float*) map.PheromoneValues.GetUnsafePtr();
                // var pheromoneValuesLength = map.PheromoneValues.Length;
                //
                // Span<float> temp = stackalloc float[pheromoneValuesLength];
                //
                // for (var position = int2.zero; position.y < map.Size.y; ++position.y)
                // {
                //     for (position.x = 0; position.x < map.Size.x; ++position.x)
                //     {
                //         var centerMapIndex = map.GetMapIndex(position);
                //         var pheromoneValue = pheromoneValues[centerMapIndex] * centerCoefficient;
                //
                //         pheromoneValue += map.GetPheromone(new int2(position.x - 1, position.y)) * neighbourCoefficient;
                //         pheromoneValue += map.GetPheromone(new int2(position.x + 1, position.y)) * neighbourCoefficient;
                //
                //         temp[centerMapIndex] = pheromoneValue;
                //     }
                // }
                //
                // for (var position = int2.zero; position.x < map.Size.x; ++position.x)
                // {
                //     for (position.y = 0; position.y < map.Size.y; ++position.y)
                //     {
                //         var centerMapIndex = map.GetMapIndex(position);
                //         var pheromoneValue = temp[centerMapIndex] * centerCoefficient;
                //
                //         pheromoneValue += map.GetPheromone(new int2(position.x, position.y - 1)) * neighbourCoefficient;
                //         pheromoneValue += map.GetPheromone(new int2(position.x, position.y + 1)) * neighbourCoefficient;
                //
                //         pheromoneValues[centerMapIndex] = pheromoneValue;
                //     }
                // }
            }
        }
    }
}