using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Systems.EventBus.Data;
using Systems.EventBus.Extensions;
using Systems.EventBus.Interfaces;
using Systems.EventBus.Runtime;
using UnityEditor;
using UnityEngine;

namespace Systems.EventBus.Editor
{
    public class EventBusStationWindow : EditorWindow
    {
        #region Private Fields

        private Vector2 _scroll;
        private readonly Dictionary<ScriptableObject, EventLog> _logs = new();
        private readonly Dictionary<ScriptableObject, string> _inputs = new();
        private List<ScriptableObject> _assets = new();

        #endregion

        [MenuItem("Window/EventBus Debugger")]
        public static void ShowWindow() => GetWindow<EventBusStationWindow>("EventBus Station");

        private void OnEnable()
        {
            EditorApplication.playModeStateChanged += OnPlayModeChanged;
            if (EditorApplication.isPlaying)
                SubscribeAll();
        }

        private void OnDisable()
        {
            EditorApplication.playModeStateChanged -= OnPlayModeChanged;
            UnsubscribeAll();
        }

        private void OnPlayModeChanged(PlayModeStateChange state)
        {
            switch (state)
            {
                case PlayModeStateChange.EnteredPlayMode:
                    SubscribeAll();
                    break;
                case PlayModeStateChange.ExitingPlayMode:
                    UnsubscribeAll();
                    break;
            }
        }

        private void SubscribeAll()
        {
            _logs.Clear();
            _inputs.Clear();
            _assets = FindAllEventAssets();

            foreach (var so in _assets)
            {
                _logs[so] = new EventLog { Time = default, Value = null };
                _inputs[so] = "";
                RegisterListener(so);
            }
        }

        private void UnsubscribeAll()
        {
            _logs.Clear();
            _inputs.Clear();
            _assets.Clear();
        }

        private List<ScriptableObject> FindAllEventAssets()
        {
            return AssetDatabase.FindAssets("t:ScriptableObject")
                .Select(AssetDatabase.GUIDToAssetPath)
                .Select(AssetDatabase.LoadAssetAtPath<ScriptableObject>)
                .OfType<IEvent>().Cast<ScriptableObject>()
                .ToList();
        }

        private void RegisterListener(ScriptableObject so)
        {
            var evtType = so.GetType();
            var busType = typeof(EventBus<>).MakeGenericType(evtType);
            var bindType = typeof(EventBinding<>).MakeGenericType(evtType);
            var actionType = typeof(Action<>).MakeGenericType(evtType);
            
            var method = typeof(EventBusStationWindow)
                .GetMethod(nameof(OnEventRaised), BindingFlags.NonPublic | BindingFlags.Instance)
                ?.MakeGenericMethod(evtType);

            if (method == null) return;
            
            var callback = Delegate
                .CreateDelegate(actionType, this, method);

            // new EventBinding<T>( callback )
            var binding = Activator
                .CreateInstance(bindType, callback);

            // EventBus<T>.Register(binding)
            busType
                .GetMethod("Register", BindingFlags.Public | BindingFlags.Static)
                ?.Invoke(null, new[] { binding });
        }

        // this will be called whenever any of our SO-events raise in play mode
        private void OnEventRaised<T>(T evt) where T : ScriptableObject, IEvent
        {
            ScriptableObject so   = evt;
            var log  = _logs[so];
            log.Time  = DateTime.Now;
            log.Value = ReadPayload(evt);
            _logs[so] = log;
            Repaint();
        }

        private object ReadPayload<T>(T evt)
        {
            // assumes your EventBase<TPayload> has a `GetValue` property
            var pi = evt
                .GetType()
                .GetProperty("GetValue", BindingFlags.Public | BindingFlags.Instance);
            
            return pi != null ? pi.GetValue(evt) : null;
        }

        private void OnGUI()
        {
            EditorGUILayout.LabelField("EventBus Debugger", EditorStyles.boldLabel);

            if (!EditorApplication.isPlaying)
            {
                EditorGUILayout.HelpBox("Enter Play Mode to watch & fire events.", MessageType.Info);
                return;
            }

            _scroll = EditorGUILayout.BeginScrollView(_scroll);
            foreach (var so in _assets)
            {
                var log = _logs[so];
                EditorGUILayout.BeginVertical("box");
                EditorGUILayout.ObjectField("Event Asset", so, typeof(ScriptableObject), false);
                EditorGUILayout.LabelField("Last Raised", log.Time == default
                    ? "Never"
                    : log.Time.ToString("HH:mm:ss"));
                EditorGUILayout.LabelField("Last Value", log.Value?.ToString() ?? "—");

                // input & trigger
                _inputs[so] = EditorGUILayout.TextField("New Value", _inputs[so]);
                if (GUILayout.Button("Trigger"))
                    Trigger(so, _inputs[so]);

                EditorGUILayout.EndVertical();
                GUILayout.Space(4);
            }
            EditorGUILayout.EndScrollView();
        }

        private void Trigger(ScriptableObject so, string raw)
        {
            var baseType = so.GetType().BaseType;
            if (baseType != null)
            {
                var payloadType = baseType.GetGenericArguments()[0];
                var parsed = Convert.ChangeType(raw, payloadType);

                so.GetType()
                    .GetMethod("SetValue", BindingFlags.Public | BindingFlags.Instance)
                    ?.Invoke(so, new[] { parsed });
            }

            var ext = typeof(EventExtension)
                      .GetMethod("Raise", BindingFlags.Public | BindingFlags.Static)
                      ?.MakeGenericMethod(so.GetType());
            
            if (ext != null) ext.Invoke(null, new object[] { so });
        }
    }
}