using SandolkakosDigital.Utils.Editor;
using UnityEditor;
using UnityEngine;

namespace SandolkakosDigital.LayerArrangement.Editor
{
    /// <summary>
    /// That class arranges the hierarchy order of a RectTransform inside its Canvas.
    /// The behavior is similar to arraging layers on Photoshop and PowerPoint.
    /// </summary>
    
    public static class ArrangeHierarchyOrderUtility
    {
        #region Back movements
        public static void SendToBack(this RectTransform rectTransform)
        {
            Transform currentParent = rectTransform.parent;
            int currentIndex = rectTransform.GetSiblingIndex();

            if (currentIndex > 0)
            {
                Undo.SetTransformParent(rectTransform, currentParent, nameof(SendToBack));
                rectTransform.SetAsFirstSibling();
            }
        }

        public static void SendBackward(this RectTransform rectTransform)
        {
            Transform currentParent = rectTransform.parent;
            int currentIndex = rectTransform.GetSiblingIndex();

            if (currentIndex > 0)
            {
                var rootObjects = UnityEngine.SceneManagement.SceneManager.GetActiveScene().GetRootGameObjects();

                int previousIndex = currentIndex - 1;
                Transform previousSibling = currentParent != null
                    ? currentParent.GetChild(previousIndex)
                    : rootObjects[previousIndex].transform;

                bool isPreviousSibingExpanded = SceneHierarchyUtility.IsExpanded(previousSibling.gameObject);

                if (previousSibling.childCount > 0 && isPreviousSibingExpanded)
                {
                    Undo.SetTransformParent(rectTransform, previousSibling, nameof(SendBackward));
                    rectTransform.SetParent(previousSibling);
                    rectTransform.SetAsLastSibling();

                    EditorGUIUtility.PingObject(previousSibling);
                }
                else
                {
                    Undo.SetTransformParent(rectTransform, currentParent, nameof(SendBackward));
                    rectTransform.SetSiblingIndex(previousIndex);
                }
            }
            else if (currentParent != null && currentParent.parent != null)
            {
                int currentParentIndex = currentParent.GetSiblingIndex();

                Transform newParent = currentParent.parent;
                Undo.SetTransformParent(rectTransform, newParent, nameof(SendBackward));
                rectTransform.SetParent(newParent);
                rectTransform.SetSiblingIndex(currentParentIndex);

                EditorGUIUtility.PingObject(newParent);
            }
        }
        #endregion

        #region Front movements
        public static void BringToFront(this RectTransform rectTransform)
        {
            Transform currentParent = rectTransform.parent;
            int currentIndex = rectTransform.GetSiblingIndex();
            var rootObjects = UnityEngine.SceneManagement.SceneManager.GetActiveScene().GetRootGameObjects();

            int siblingsCount = currentParent?.childCount ?? rootObjects.Length;

            if (currentIndex < siblingsCount - 1)
            {
                Undo.SetTransformParent(rectTransform, currentParent, nameof(BringToFront));
                rectTransform.SetAsLastSibling();
            }
        }

        public static void BringForward(this RectTransform rectTransform)
        {
            Transform currentParent = rectTransform.parent;
            int currentIndex = rectTransform.GetSiblingIndex();
            var rootObjects = UnityEngine.SceneManagement.SceneManager.GetActiveScene().GetRootGameObjects();

            int siblingsCount = currentParent?.childCount ?? rootObjects.Length;

            if (currentIndex < siblingsCount - 1)
            {
                int nextIndex = currentIndex + 1;
                Transform nextSibling = currentParent != null
                    ? currentParent.GetChild(nextIndex)
                    : rootObjects[nextIndex].transform;

                bool isNextSibingExpanded = SceneHierarchyUtility.IsExpanded(nextSibling.gameObject);

                if (nextSibling.childCount > 0 && isNextSibingExpanded)
                {
                    Undo.SetTransformParent(rectTransform, nextSibling, nameof(BringForward));
                    rectTransform.SetParent(nextSibling);
                    rectTransform.SetAsFirstSibling();

                    EditorGUIUtility.PingObject(nextSibling);
                }
                else
                {
                    Undo.SetTransformParent(rectTransform, currentParent, nameof(BringForward));
                    rectTransform.SetSiblingIndex(nextIndex);
                }
            }
            else if (currentParent != null && currentParent.parent != null)
            {
                Transform newParent = currentParent.parent;
                int newIndex = currentParent.GetSiblingIndex() + 1;

                Undo.SetTransformParent(rectTransform, newParent, nameof(BringForward));
                rectTransform.SetParent(newParent);
                rectTransform.SetSiblingIndex(newIndex);

                EditorGUIUtility.PingObject(newParent);
            }
        }
        #endregion
    }
}