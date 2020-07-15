using UnityEngine;
using UnityEngine.UI;

namespace Tools
{
    public static class ExtensionMethods
    {
        public static void WarnIfReferenceIsNull(this GameObject component, GameObject objectReference)
        {
            if (component == null)
                Debug.LogWarning("Missing GameObject reference!", objectReference);
        }

        public static void WarnIfReferenceIsNull(this Component component, GameObject objectReference)
        {
            if (component == null)
                Debug.LogWarning($"Missing Component reference!", objectReference);
        }

        public static void WarnIfReferenceIsNull(this Object component, GameObject objectReference)
        {
            if (component == null)
                Debug.LogWarning("Missing Object reference!", objectReference);
        }

        public static void SetText(this Text textComponent, string text)
        {
            if (textComponent != null)
                textComponent.text = text;
        }

        public static void SetText(this Text textComponent, int value)
        {
            if (textComponent != null)
                textComponent.text = value.ToString();
        }
    }
}
