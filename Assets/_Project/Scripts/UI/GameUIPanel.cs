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
        [SerializeField] private float _panelFadeDuration = 0.3f;
        [SerializeField] private float _textPunchScaleDuration = 0.1f;
        [SerializeField] private float _textPunchSize = 0.1f;

        private IGameScore _gameScore;
        private IOnFieldCubeCounter _cubeCounter;
        private GameplaySettings _settings;

        private CancellationToken _cancellationToken;

        public void Init()
        {
            _cancellationToken = CachedGameObject.GetCancellationTokenOnDestroy();

            _gameScore = ServiceLocator.Local.Get<IGameScore>();
            _cubeCounter = ServiceLocator.Local.Get<OnFieldCubeRegistry>();
            _settings = ServiceLocator.Local.Get<GameplaySettings>();
        }

        public override async UniTask Show()
        {
            if (_cancellationToken.IsCancellationRequested)
                return;

            if (CachedGameObject.activeSelf)
                return;

            UpdateScore(_gameScore.Score);
            UpdateCubeCount(_cubeCounter.CubeCount);

            _gameScore.OnScoreChange += UpdateScore;
            _cubeCounter.OnCountChange += UpdateCubeCount;

            CachedGameObject.SetActive(true);
            _canvasGroup.alpha = 0f;

            await _canvasGroup.DOFade(1f, _textPunchScaleDuration).SetEase(Ease.OutQuad).WithCancellation(_cancellationToken);
        }

        public override async UniTask Hide()
        {
            if (_cancellationToken.IsCancellationRequested)
                return;

            if (!CachedGameObject.activeSelf)
                return;

            _gameScore.OnScoreChange -= UpdateScore;
            _cubeCounter.OnCountChange -= UpdateCubeCount;

            await _canvasGroup.DOFade(0f, _textPunchScaleDuration).SetEase(Ease.InQuad).WithCancellation(_cancellationToken);

            CachedGameObject.SetActive(false);
        }

        private void UpdateScore(int score)
        {
            _scoreLabel.text = $"Score: {score}";
            AnimatePunch(_scoreLabel.rectTransform);
        }

        private void UpdateCubeCount(int count)
        {
            _cubeCountLabel.text = $"Cubes: {count} / {_settings.LoseCubesCountOnField}";
            AnimatePunch(_cubeCountLabel.rectTransform);
        }

        private void AnimatePunch(RectTransform target)
        {
            // Kill existing animation if any
            target.DOKill();

            // Reset to normal before animating
            target.localScale = Vector3.one;

            // Apply punch scale
            target.DOPunchScale(Vector3.one * _textPunchSize, _textPunchScaleDuration)
                .SetEase(Ease.OutBack);
        }
    }
}