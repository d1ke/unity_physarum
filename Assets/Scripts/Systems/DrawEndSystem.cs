using Unity.Entities;

namespace Physarium
{
    [UpdateInGroup(typeof(PresentationSystemGroup))]
    public partial struct DrawEndSystem : ISystem
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
            mapTexture.Texture.SetPixels(mapTexture.Colors);
            mapTexture.Texture.Apply();
        }
    }
}