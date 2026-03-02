using System;
using System.Collections.Generic;
using Fsi.General.Timers.Element;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UIElements;

namespace Fsi.General.Timers.Manager
{
    public class TimerManagerEditor : EditorWindow
    {
        [FormerlySerializedAs("m_VisualTreeAsset")]
        [SerializeField]
        private VisualTreeAsset visualTreeAsset = default;

        private List<TimerElement> timers;
        
        [MenuItem("FSI/Timers Manager")]
        public static void OpenWindow()
        {
            TimerManagerEditor wnd = GetWindow<TimerManagerEditor>();
            wnd.titleContent = new GUIContent("Timer Manager");
        }

        private void OnEnable()
        {
            TimerManager.TimerAdded += AddTimer;
            TimerManager.TimerRemoved += RemoveTimer;
        }

        private void OnDisable()
        {
            TimerManager.TimerAdded -= AddTimer;
            TimerManager.TimerRemoved -= RemoveTimer;
        }

        public void CreateGUI()
        {
            VisualElement root = rootVisualElement;
            visualTreeAsset.CloneTree(root);
        }
        
        #region UI Control

        private void AddTimer(Timer timer)
        {
            
        }

        private void RemoveTimer(Timer timer)
        {
            
        }
        
        #endregion
    }
}
