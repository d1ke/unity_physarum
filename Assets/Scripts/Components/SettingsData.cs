using Unity.Entities;
using Unity.Mathematics;

namespace Physarium
{
    public struct SettingsData : IComponentData
    {
        public Random Random;
        public float AgentsSpeed;
        public float SideSensorsAngle;
        public float SensorsDistance;
        public float PheromoneGenerationPerTick;
        public float PheromoneDryPerSecond;
        public float PheromoneSpreadingPercentPerSecond;
    }
}