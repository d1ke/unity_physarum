using Unity.Entities;
using Unity.Mathematics;

namespace Physarium
{
    [UpdateInGroup(typeof(InitializationSystemGroup))]
    public partial struct ResetMapSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<ResetMapTag>();
        }

        public void OnDestroy(ref SystemState state)
        {
        }

        public void OnUpdate(ref SystemState state)
        {
            var settings = SystemAPI.GetSingleton<SettingsData>();
            var map = SystemAPI.GetSingleton<MapData>();

            for (int i = 0; i < map.PheromoneValues.Length; ++i)
                map.PheromoneValues[i] = 0;

            foreach (var (position, velocity, direction) in SystemAPI.Query<RefRW<PositionData>, RefRW<VelocityData>, RefRW<DirectionData>>())
            {
                position.ValueRW.CurrentValue = settings.Random.NextInt2(0, map.Size);
                direction.ValueRW.Value = math.normalize(settings.Random.NextInt2(-map.Size, map.Size));
                velocity.ValueRW.Value = direction.ValueRO.Value * settings.AgentsSpeed;
            }

            state.EntityManager.DestroyEntity(SystemAPI.GetSingletonEntity<ResetMapTag>());
        }
    }
}