namespace Sjouke.CodeStructure.Button
{
    using UnityEngine;

    public sealed class MenuButton : MonoBehaviour
    {
        public RuntimeCollections.ButtonRuntimeCollection Collection;
        public GameObjectReference ActiveButton;
        private UnityEngine.UI.Button _button;

        private void Awake()
        {
            _button = GetComponent<UnityEngine.UI.Button>();
        }

        private void OnEnable()
        {
            Collection.Add(_button);
        }

        private void OnDisable()
        {
            Collection.Remove(_button);
        }

        public void OnMouseClick()
        {
            if (ActiveButton != gameObject) return;
            Debug.Log($"Yay, {gameObject.name} has been pressed, and knows it!");
        }
    }
}