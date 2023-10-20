using Unity.Collections.LowLevel.Unsafe;
using Unity.Entities;
using UnityEngine;

namespace Physarium
{
    [UpdateInGroup(typeof(PresentationSystemGroup))]
    [UpdateAfter(typeof(DrawStartSystem))]
    [UpdateBefore(typeof(DrawEndSystem))]
    public partial struct DrawPheromoneSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<DrawPheromoneTag>();
        }

        public void OnDestroy(ref SystemState state)
        {
        }

        public void OnUpdate(ref SystemState state)
        {
            unsafe
            {
                var map = SystemAPI.GetSingleton<MapData>();
                var mapTexture = SystemAPI.ManagedAPI.GetSingleton<MapTextureData>();
                var pheromoneColor = new Color(0.5f, 0.5f, 1, 1);

                var mapPtr = (float*) map.PheromoneValues.GetUnsafePtr();
                var mapLength = map.PheromoneValues.Length;

                for (int mapIndex = 0; mapIndex < mapLength; ++mapIndex)
                {
                    pheromoneColor.a = mapPtr[mapIndex];
                    mapTexture.Colors[mapIndex] = pheromoneColor;
                }
            }
        }
    }
}