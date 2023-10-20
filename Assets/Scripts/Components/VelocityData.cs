using Unity.Entities;
using Unity.Mathematics;

namespace Physarium
{
    public struct VelocityData : IComponentData
    {
        public float2 Value;
    }
}