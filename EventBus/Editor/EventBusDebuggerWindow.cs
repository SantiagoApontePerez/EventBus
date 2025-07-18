using System;
using System.Collections.Generic;
using System.Linq;
using EventBus.EventBus.Runtime;
using UnityEditor;
using UnityEngine;

namespace EventBus.EventBus.Editor
{
    public class EventBusDebuggerWindow : EditorWindow
    {
        #region Fields
        
        private Vector2 _scrollPosition;
        private int _intInput;
        private float _floatInput;
        private bool _boolInput;
        private string _stringInput;

        #endregion

        [MenuItem("Window/EventBus/EventBus Debugger")]
        public static void ShowWindow()
        {
            GetWindow<EventBusDebuggerWindow>("EventBus Debugger");
        }

        private void OnGUI()
        {
            _scrollPosition = EditorGUILayout.BeginScrollView(_scrollPosition);
            
            var events = GetScriptableObjectEvents();

            foreach (var e in events)
            {
                EditorGUILayout.BeginVertical("box");
                EditorGUILayout.LabelField(e.name, EditorStyles.boldLabel);

                SwitchEventField(e);
                
                EditorGUILayout.EndVertical();
            }
            EditorGUILayout.EndScrollView();
        }

        private static IEnumerable<ScriptableObject> GetScriptableObjectEvents()
        {
            return AssetDatabase.FindAssets("t:ScriptableObject")
                .Select(guid => AssetDatabase.LoadAssetAtPath<ScriptableObject>(AssetDatabase.GUIDToAssetPath(guid)))
                .Where(PredicateEvents());
        }

        private static Func<ScriptableObject, bool> PredicateEvents()
        {
            return e => e 
                is Event<int> 
                or Event<float> 
                or Event<bool>
                or Event<string>;
        }

        private void SwitchEventField(ScriptableObject e)
        {
            switch (e)
            {
                case Event<int> intEvent:
                {
                    _intInput = EditorGUILayout.IntField("Raise Value", _intInput);
                    if(GUILayout.Button("Raise")) intEvent.Raise(_intInput);
                    EditorGUILayout.LabelField("Listeners", intEvent.ListenerCount.ToString());
                    break;
                }
                case Event<float> floatEvent:
                {
                    _floatInput = EditorGUILayout.FloatField("Raise Value", _floatInput);
                    if (GUILayout.Button("Raise")) floatEvent.Raise(_floatInput);
                    EditorGUILayout.LabelField("Listeners", floatEvent.ListenerCount.ToString());
                    break;
                }
                case Event<bool> boolEvent:
                {
                    _boolInput = EditorGUILayout.Toggle("Raise Value", _boolInput);
                    if (GUILayout.Button("Raise")) boolEvent.Raise(_boolInput);
                    EditorGUILayout.LabelField("Listeners", boolEvent.ListenerCount.ToString());
                    break;
                }
                case Event<string> stringEvent:
                {
                    _stringInput = EditorGUILayout.TextField("Raise Value", _stringInput);
                    if (GUILayout.Button("Raise")) stringEvent.Raise(_stringInput);
                    EditorGUILayout.LabelField("Listeners", stringEvent.ListenerCount.ToString());
                    break;
                }
            }
        }
    }
}