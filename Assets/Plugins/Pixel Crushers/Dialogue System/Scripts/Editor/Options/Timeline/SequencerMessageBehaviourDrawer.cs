// Recompile at 27/02/2025 2:50:49 PM
#if USE_TIMELINE
#if UNITY_2017_1_OR_NEWER
using UnityEditor;
using UnityEngine;

namespace PixelCrushers.DialogueSystem
{

    [CustomPropertyDrawer(typeof(SequencerMessageBehaviour))]
    public class SequencerMessageBehaviourDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.PropertyField(position, property.FindPropertyRelative("message"));
        }
    }
}
#endif
#endif
