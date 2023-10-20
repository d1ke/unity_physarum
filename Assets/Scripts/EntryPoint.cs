using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.UI;

namespace Physarium
{
    public class EntryPoint : MonoBehaviour
    {
        [SerializeField]
        private SpriteRenderer m_map;
        [SerializeField]
        private Toggle m_drawAgentsToggle;

        private void Start()
        {
            var entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;

            entityManager.CreateSingleton(new SettingsData
            {
                Random = new Unity.Mathematics.Random(999),
                AgentsSpeed = 20,
                SideSensorsAngle = math.radians(20),
                SensorsDistance = 3,
                PheromoneGenerationPerTick = 1f,
                PheromoneDryPerSecond = 0.3f,
                PheromoneSpreadingPercentPerSecond = 1f,
            });

            var mapSize = new int2(1000, 1000);
            var mapArrayLength = mapSize.x * mapSize.y;
            entityManager.CreateSingleton(new MapData
            {
                PheromoneValues = new NativeArray<float>(mapArrayLength, Allocator.Persistent),
                Size = mapSize,
            });

            var mapTexture = new Texture2D(mapSize.x, mapSize.y, DefaultFormat.LDR, TextureCreationFlags.DontUploadUponCreate);
            var mapSprite = Sprite.Create(mapTexture, new Rect(0, 0, mapTexture.width, mapTexture.height), new Vector2(0.5f, 0.5f));
            m_map.sprite = mapSprite;
            entityManager.CreateSingleton(new MapTextureData
            {
                Texture = mapTexture,
                Colors = new Color[mapArrayLength],
            });

            var agentArchetype = entityManager.CreateArchetype(
                ComponentType.ReadWrite<DirectionData>(),
                ComponentType.ReadWrite<PositionData>(),
                ComponentType.ReadWrite<VelocityData>());

            entityManager.CreateEntity(agentArchetype, 1000);

            entityManager.CreateSingleton<ResetMapTag>();
            entityManager.CreateSingleton<DrawAgentsTag>();
            entityManager.CreateSingleton<DrawPheromoneTag>();

            initUi(entityManager);
        }

        private void initUi(EntityManager entityManager)
        {
            //m_drawAgentsToggle.onValueChanged.AddListener(isOn => entityManager.re);
        }
    }
}