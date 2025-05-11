using Assets._Project.Scripts.ServiceLocatorSystem;
using UnityEngine;

namespace Assets._Project.Scripts.Gameplay.GameplayInput
{
    public class InputUpdater
    {
        private InputHandler _handler;

        public InputUpdater Init()
        {
            var iInput = ServiceLocator.Local.Get<IInputHandler>();

            if (iInput is InputHandler input)
            {
                _handler = input;
            }
            else
            {
                Debug.Log("InputHandler has a wrong type");
            }

            return this;
        }

        public void Update()
        {
            _handler?.UpdateInput();
        }
    }
}