using Assets._Project.Scripts.Utilities;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Assets._Project.Scripts.UI
{
    [RequireComponent(typeof(CanvasGroup))]
    public abstract class UIPanel : CachedMonoBehaviour
    {
        public abstract UniTask Hide();

        public abstract UniTask Show();
    }
}