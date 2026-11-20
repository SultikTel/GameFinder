using UnityEngine;

namespace Finder.Configs
{
    [CreateAssetMenu(fileName = "New Difficulties", menuName = "Difficulties")]
    public class Difficulty : ScriptableObject
    {
        [SerializeField, Min(1)] private int _width, _height;

        public int Height => _height;
        public int Width => _width;
    }
}