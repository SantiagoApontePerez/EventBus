using System;
using System.Collections.Generic;
using System.Linq;
using EventBus.EventBus.Interfaces;
using EventBus.EventBus.Runtime;
using EventBus.EventBus.Utility;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace EventBus.EventBus.Editor
{
    public class EventBusDebuggerWindow : EditorWindow
    {
        private void OnGUI()
        {
            _scrollPosition = EditorGUILayout.BeginScrollView(_scrollPosition);

            var events = GetScriptableObjectEvents();

            foreach (var e in events)
            {
                EditorGUILayout.BeginVertical("box");
                EditorGUILayout.LabelField(e.name, EditorStyles.boldLabel);

                DrawSection(e);

                EditorGUILayout.EndVertical();
            }

            EditorGUILayout.EndScrollView();
            DrawHelpBox();
        }

        [MenuItem("Window/EventBus/EventBus Debugger")]
        public static void ShowWindow()
        {
            GetWindow<EventBusDebuggerWindow>("EventBus Debugger");
        }

        private static void DrawHelpBox()
        {
            EditorGUILayout.HelpBox(
                "To begin, create an event/s from the menu located in: "
                + " `"
                + InspectorMenus.EventMenu
                + "` "
                + "and attach a listener to this event/s",
                MessageType.Info);
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

        private void DrawSection(ScriptableObject e)
        {
            switch (e)
            {
                case Event<int> intEvent:
                {
                    _intInput = EditorGUILayout.IntField("Raise Value", _intInput);
                    if (GUILayout.Button("Raise")) intEvent.Raise(_intInput);
                    EditorGUILayout.LabelField("Listeners", intEvent.ListenerCount.ToString());
                    DrawListenerDetails(intEvent);
                    break;
                }
                case Event<float> floatEvent:
                {
                    _floatInput = EditorGUILayout.FloatField("Raise Value", _floatInput);
                    if (GUILayout.Button("Raise")) floatEvent.Raise(_floatInput);
                    EditorGUILayout.LabelField("Listeners", floatEvent.ListenerCount.ToString());
                    DrawListenerDetails(floatEvent);
                    break;
                }
                case Event<bool> boolEvent:
                {
                    _boolInput = EditorGUILayout.Toggle("Raise Value", _boolInput);
                    if (GUILayout.Button("Raise")) boolEvent.Raise(_boolInput);
                    EditorGUILayout.LabelField("Listeners", boolEvent.ListenerCount.ToString());
                    DrawListenerDetails(boolEvent);
                    break;
                }
                case Event<string> stringEvent:
                {
                    _stringInput = EditorGUILayout.TextField("Raise Value", _stringInput);
                    if (GUILayout.Button("Raise")) stringEvent.Raise(_stringInput);
                    EditorGUILayout.LabelField("Listeners", stringEvent.ListenerCount.ToString());
                    DrawListenerDetails(stringEvent);
                    break;
                }
            }
        }

        private static void DrawListenerDetails<T>(Event<T> @event)
        {
            foreach (var listener in @event.RegistrationTimes.Keys)
            {
                var name = listener is MonoBehaviour mb ? mb.gameObject.name : listener.ToString();
                var regTime = @event.RegistrationTimes[listener];
                @event.LastInvokeTimes.TryGetValue(listener, out var lastTime);

                DrawSelectListenerObject(name, listener);

                //Registration and last invoke fields.
                EditorGUILayout.LabelField($"Registered: {regTime:HH:mm:ss}");
                EditorGUILayout.LabelField(
                    $"Last Invoked: {(lastTime == default ? "Never" : lastTime.ToString("HH:mm:ss"))}");
                EditorGUILayout.Space();
            }
        }

        private static void DrawSelectListenerObject<T>(string name, IEventListener<T> listener)
        {
            EditorGUILayout.LabelField(new GUIContent($"{name}", "Listener object name"), EditorStyles.miniLabel);

            if (listener is not Object unityObject || !GUILayout.Button(
                    new GUIContent("Select", "Select this listener in the inspector"),
                    GUILayout.Width(60))) return;

            Selection.activeObject = unityObject;
            EditorGUIUtility.PingObject(unityObject);
        }

        #region Fields

        private Vector2 _scrollPosition;
        private int _intInput;
        private float _floatInput;
        private bool _boolInput;
        private string _stringInput;

        #endregion
    }
}