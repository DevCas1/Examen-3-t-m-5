namespace Sjouke.CodeStructure.Button
{
    using UnityEngine;

    public sealed class ButtonCollectionEnabler : MonoBehaviour
    {
        public RuntimeCollections.ButtonRuntimeCollection ButtonCollection;
        public void EnableButtons(bool enable)
        {
            for (int index = ButtonCollection.Items.Count-1; index >= 0; index--)
                ButtonCollection.Items[index].interactable = enable;
        }
    }
}