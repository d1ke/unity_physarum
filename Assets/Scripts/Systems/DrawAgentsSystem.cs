using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace Physarium
{
    [UpdateInGroup(typeof(PresentationSystemGroup))]
    [UpdateAfter(typeof(DrawPheromoneSystem))]
    [UpdateBefore(typeof(DrawEndSystem))]
    public partial struct DrawAgentsSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<DrawAgentsTag>();
        }

        public void OnDestroy(ref SystemState state)
        {
        }

        public void OnUpdate(ref SystemState state)
        {
            var map = SystemAPI.GetSingleton<MapData>();
            var mapTexture = SystemAPI.ManagedAPI.GetSingleton<MapTextureData>();
            var agentColor = new Color(1, 0, 0);

            foreach (var position in SystemAPI.Query<PositionData>())
            {
                var mapIndex = map.GetMapIndex((int2) position.CurrentValue);
                mapTexture.Colors[mapIndex] = agentColor;
            }
        }
    }
}