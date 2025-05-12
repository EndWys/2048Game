using Assets._Project.Scripts.Gameplay.GameManagment;
using Assets._Project.Scripts.ServiceLocatorSystem;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using System.Threading;
using TMPro;
using UnityEngine;

namespace Assets._Project.Scripts.UI
{
    public interface IGameUI : IService
    {
        UniTask Show();
        UniTask Hide();
    }
    public class GameUIPanel : UIPanel, IGameUI
    {
        [Header("UI References")]
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private TextMeshProUGUI _scoreLabel;
        [SerializeField] private TextMeshProUGUI _cubeCountLabel;

        [Header("Animation Settings")]
        [SerializeField] private float fadeDuration = 0.3f;

        private IGameScore _gameScore;
        private IOnFieldCubeCounter _cubeCounter;
        private GameplaySettings _settings;

        private CancellationTokenSource _tokenSource;

        public void Init()
        {
            _tokenSource = new CancellationTokenSource();

            _gameScore = ServiceLocator.Local.Get<IGameScore>();
            _cubeCounter = ServiceLocator.Local.Get<OnFieldCubeRegistry>();
            _settings = ServiceLocator.Local.Get<GameplaySettings>();
        }

        public override async UniTask Show()
        {
            if (_tokenSource.IsCancellationRequested)
                return;

            if (CachedGameObject.activeSelf)
                return;

            UpdateScore(_gameScore.Score);
            UpdateCubeCount(_cubeCounter.CubeCount);

            _gameScore.OnScoreChange += UpdateScore;
            _cubeCounter.OnCountChange += UpdateCubeCount;

            CachedGameObject.SetActive(true);
            _canvasGroup.alpha = 0f;

            await _canvasGroup.DOFade(1f, fadeDuration).SetEase(Ease.OutQuad).WithCancellation(_tokenSource.Token);
        }

        public override async UniTask Hide()
        {
            if (_tokenSource.IsCancellationRequested)
                return;

            if (!CachedGameObject.activeSelf)
                return;

            _gameScore.OnScoreChange -= UpdateScore;
            _cubeCounter.OnCountChange -= UpdateCubeCount;

            await _canvasGroup.DOFade(0f, fadeDuration).SetEase(Ease.InQuad).WithCancellation(_tokenSource.Token);

            CachedGameObject.SetActive(false);
        }

        private void UpdateScore(int score)
        {
            _scoreLabel.text = $"Score: {score}";
        }

        private void UpdateCubeCount(int count)
        {
            _cubeCountLabel.text = $"Cubes: {count} / {_settings.LoseCubesCountOnField}";
        }

        private void OnDestroy()
        {
            _tokenSource?.Cancel();
            _tokenSource?.Dispose();
        }
    }
}