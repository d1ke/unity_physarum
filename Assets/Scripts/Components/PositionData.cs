using Unity.Entities;
using Unity.Mathematics;

namespace Physarium
{
    public struct PositionData : IComponentData
    {
        public float2 PreviousFrameValue;
        public float2 CurrentValue;
    }
}