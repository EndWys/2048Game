using TMPro;
using UnityEngine;

namespace Assets._Project.Scripts.Gameplay.CubeLogic.CubeObject.ÑubeComponents
{
    public class CubeView : MonoBehaviour
    {
        [SerializeField] private Renderer _cubeRenderer;
        [SerializeField] private TextMeshPro[] _texts;

        public void UpdateView(int value)
        {
            foreach (var text in _texts)
            {
                text.text = value.ToString();
                text.color = GetTextColor(value);
            }
            _cubeRenderer.material.color = GetCubeColor(value);
        }

        private Color GetCubeColor(int value)
        {
            float hue = Mathf.Repeat(Mathf.Log(value, 2) * 0.1f, 1f);
            return Color.HSVToRGB(hue, 0.6f, 0.9f);
        }

        private Color GetTextColor(int value)
        {
            return value <= 4 ? Color.black : Color.white;
        }
    }
}