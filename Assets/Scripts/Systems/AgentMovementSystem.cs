using Unity.Burst;
using Unity.Entities;

namespace Physarium
{
    [BurstCompile]
    [UpdateInGroup(typeof(SimulationSystemGroup))]
    [UpdateAfter(typeof(AgentRotateSystem))]
    public partial struct AgentMovementSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
        }

        public void OnDestroy(ref SystemState state)
        {
        }

        public void OnUpdate(ref SystemState state)
        {
            foreach (var (position, velocity) in SystemAPI.Query<RefRW<PositionData>, VelocityData>())
            {
                position.ValueRW.PreviousFrameValue = position.ValueRO.CurrentValue;
                position.ValueRW.CurrentValue += velocity.Value * SystemAPI.Time.DeltaTime;
            }
        }
    }
}