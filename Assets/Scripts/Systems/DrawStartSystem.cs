using Unity.Entities;
using UnityEngine;

namespace Physarium
{
    [UpdateInGroup(typeof(PresentationSystemGroup))]
    public partial struct DrawStartSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
        }

        public void OnDestroy(ref SystemState state)
        {
        }

        public void OnUpdate(ref SystemState state)
        {
            var mapTexture = SystemAPI.ManagedAPI.GetSingleton<MapTextureData>();
            var mapLength = mapTexture.Colors.Length;
            for (int i = 0; i < mapLength; ++i)
                mapTexture.Colors[i] = Color.clear;
        }
    }
}