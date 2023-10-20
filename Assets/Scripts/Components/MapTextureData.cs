using Unity.Entities;
using UnityEngine;

namespace Physarium
{
    public class MapTextureData : IComponentData
    {
        public Color[] Colors;
        public Texture2D Texture;
    }
}