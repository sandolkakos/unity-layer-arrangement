using SandolkakosDigital.CommonUtilities.Editor;
using UnityEditor;
using UnityEngine;

namespace SandolkakosDigital.LayerArrangement.Editor
{   
    [CustomEditor(typeof(RectTransform), true)]
    [CanEditMultipleObjects]
    public class RectTransformCustomInspector : UnityEditor.Editor
    {
        // TODO: Check if it is possible to move these Shortcuts to Unity Preferences window.
        private EventModifiers stepModifier = EventModifiers.Control;
        private EventModifiers sendToModifier = EventModifiers.Control | EventModifiers.Shift;

        private KeyCode forwardKeyCode = KeyCode.RightBracket;
        private KeyCode backwardKeyCode = KeyCode.LeftBracket;

        // Unity's built-in editor
        private UnityEditor.Editor defaultEditor;
        private RectTransform rectTransform;

        private void OnEnable()
        {
            EditorApplication.hierarchyWindowItemOnGUI += OnHierarchyWindowItemGUI;

            defaultEditor = DefaultEditorUtility.CreateDefaultEditor(targets, "UnityEditor.RectTransformEditor");
            rectTransform = target as RectTransform;
        }

        private void OnDisable()
        {
            EditorApplication.hierarchyWindowItemOnGUI -= OnHierarchyWindowItemGUI;
            DefaultEditorUtility.DestroyDefaultEditor(defaultEditor);
        }

        public override void OnInspectorGUI()
        {
            defaultEditor.OnInspectorGUI();
        }

        private void OnHierarchyWindowItemGUI(int instanceID, Rect selectionRect)
        {
            CheckArrangeLayerShortcut();
        }

        private void CheckArrangeLayerShortcut()
        {
            Event e = Event.current;

            if (e.type == EventType.KeyDown)
            {
                if (e.keyCode == backwardKeyCode)
                {
                    if (e.modifiers == stepModifier)
                    {
                        rectTransform.SendBackward();
                        e.Use();
                    }
                    else if (e.modifiers == sendToModifier)
                    {
                        rectTransform.SendToBack();
                        e.Use();
                    }
                }
                else if (e.keyCode == forwardKeyCode)
                {
                    if (e.modifiers == stepModifier)
                    {
                        rectTransform.BringForward();
                        e.Use();
                    }
                    else if (e.modifiers == sendToModifier)
                    {
                        rectTransform.BringToFront();
                        e.Use();
                    }
                }
            }
        }
    }
}