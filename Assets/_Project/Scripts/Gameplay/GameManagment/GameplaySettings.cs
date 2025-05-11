using Assets._Project.Scripts.Gameplay.CubeLogic.MainCubeControll;
using Assets._Project.Scripts.ServiceLocatorSystem;
using UnityEngine;

namespace Assets._Project.Scripts.Gameplay.GameManagment
{
    [CreateAssetMenu(fileName = "GameplaySettings", menuName = "Game/Gameplay Settings")]
    public class GameplaySettings : ScriptableObject, IService
    {
        [field: Header("Cubes")]
        [field: SerializeField] public CubeValueSpawnConfig CubeValueSpawnConfig { get; private set; }

        [field: Header("Game Over")]
        [field: Min(1)]
        [field: SerializeField] public int LoseCubesCountOnField { get; private set; } = 5;
    }
}