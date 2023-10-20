using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Profiling;

namespace Physarium
{
    public struct MapData : IComponentData
    {
        public int2 Size;
        public NativeArray<float> PheromoneValues;

        private static ProfilerMarker m_wrapPositionProfilerMarker = new ProfilerMarker("WrapPosition");
        public int2 WrapPosition(int2 position)
        {
            // using var profileScope = m_wrapPositionProfilerMarker.Auto();

            position = (position % Size + Size) % Size;

            // if (position.x < 0)
            //     position.x += Size.x;
            //
            // if (position.y < 0)
            //     position.y += Size.y;

            return position;
        }

        public int GetMapIndex(int2 position)
        {
            position = WrapPosition(position);
            return position.y * Size.x + position.x;
        }

        public float GetPheromone(int2 position)
        {
            return PheromoneValues[GetMapIndex(position)];
        }
    }
}