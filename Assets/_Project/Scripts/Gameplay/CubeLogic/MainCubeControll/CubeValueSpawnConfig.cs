using System;
using UnityEngine;

namespace Assets._Project.Scripts.Gameplay.CubeLogic.MainCubeControll
{
    [CreateAssetMenu(fileName = "CubeValueSpawnConfig", menuName = "Configs/Cube Value Spawn Config")]
    public class CubeValueSpawnConfig : ScriptableObject
    {
        private const float PERSENT_MULTIPLIER = 0.01F;

        [Serializable]
        public struct ValueProbability
        {
            public int Value;         // Power-of-2 value (2, 4, 8, ...)
            [Range(0f, 100f)]
            public float Probability; // Chance (sum of all should be 1)
        }

        [SerializeField] private ValueProbability[] _values;

        private float _totalWeight;

        private void OnValidate()
        {
            // Auto-normalize if desired
            _totalWeight = 0;
            foreach (var item in _values)
            {
                _totalWeight += item.Probability * PERSENT_MULTIPLIER;
            }
        }

        public int GetRandomValue()
        {
            float random = UnityEngine.Random.value * _totalWeight;
            float cumulative = 0;

            foreach (var item in _values)
            {
                cumulative += item.Probability * PERSENT_MULTIPLIER;
                if (random <= cumulative)
                    return item.Value;
            }

            // fallback (should not happen)
            return _values.Length > 0 ? _values[0].Value : 2;
        }
    }
}