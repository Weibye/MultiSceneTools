// * Multi Scene Management Tools For Unity
// *
// * Copyright (C) 2022  Henrik Hustoft
// *
// * This program is free software: you can redistribute it and/or modify
// * it under the terms of the GNU General Public License as published by
// * the Free Software Foundation, either version 3 of the License, or
// * (at your option) any later version.
// *
// * This program is distributed in the hope that it will be useful,
// * but WITHOUT ANY WARRANTY; without even the implied warranty of
// * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// * GNU General Public License for more details.
// *
// * You should have received a copy of the GNU General Public License
// * along with this program.  If not, see <http://www.gnu.org/licenses/>.

using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEditor;
using HH.MultiSceneTools;

namespace HH.MultiSceneToolsEditor
{
    [CustomEditor(typeof(MultiSceneToolsConfig))]
    public class MultiSceneToolsConfig_Editor : Editor
    {
        MultiSceneToolsConfig script;

        SerializedProperty bootPath, collectionPath;

        private void OnEnable()
        {
            script = target as MultiSceneToolsConfig;

            bootPath = serializedObject.FindProperty("_BootScenePath");
            collectionPath = serializedObject.FindProperty("_SceneCollectionPath");
        }

        void setDefaultPaths()
        {
            if(script._BootScenePath == "")
            {
                script._BootScenePath = "Assets/Scenes/SampleScene.unity";
            }

            if(script._SceneCollectionPath == "")
                script._SceneCollectionPath = "Assets/_ScriptableObjects/MultiSceneTools/Collections";
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            serializedObject.Update();

            GUILayout.Space(8);
            GUILayout.Label("Info", EditorStyles.boldLabel);

            GUI.enabled = false;
            var config = EditorGUILayout.ObjectField("Current Instance", MultiSceneToolsConfig.instance, typeof(MultiSceneToolsConfig), false);


            EditorGUILayout.ObjectField(new GUIContent("Loaded Collection", "Currently loaded collection, this will be overridden if saved"), script.getCurrCollection(), typeof(SceneCollection), false);
            GUI.enabled = true;

            GUILayout.Space(8);
            GUILayout.Label("Settings", EditorStyles.boldLabel);


            // Allow Cross Scene References
            bool _CurrentAllowCrossSceneState = 
                EditorGUILayout.Toggle(
                    new GUIContent("Allow Cross Referencing", "inverted of EditorSceneManager.preventCrossSceneReferences"), script.AllowCrossSceneReferences);

            if(_CurrentAllowCrossSceneState != script.AllowCrossSceneReferences)
            {
                Undo.RegisterCompleteObjectUndo(target, "MultiSeneTools: Allow Cross Scene References = " + _CurrentAllowCrossSceneState);
                script.setAllowCrossSceneReferences(_CurrentAllowCrossSceneState);
            }
            script.updateCrossSceneReferenceState();

            // Log Scene Changes
            bool _CurrentLogScenesState = EditorGUILayout.Toggle(
                new GUIContent("Log Scene Changes", "Adds a Debug.log to OnSceneLoad. Output: Loaded Collection, Collection Load Mode"), 
                script.LogOnSceneChange);

            if(_CurrentLogScenesState != script.LogOnSceneChange)
            {
                Undo.RegisterCompleteObjectUndo(target, "MultiSeneTools: Log Scene Changes = " + _CurrentLogScenesState);
                script.setLogOnSceneChange(_CurrentLogScenesState);
            }

            serializedObject.UpdateIfRequiredOrScript();

            EditorGUILayout.PropertyField(bootPath,
                new GUIContent("Boot scene Path", "Keep this scene when loading differences. This scene will be loaded if all scenes are unloaded"));

            EditorGUILayout.PropertyField(collectionPath,
                new GUIContent("Scene Collections Path", "Path where new scene collections will be created and loaded from"));

            GUILayout.Space(10);
            if(config != MultiSceneToolsConfig.instance)
                script.setInstance(config as MultiSceneToolsConfig);

            if(GUILayout.Button("Set This As Instance"))
            {
                script.setInstance(script);
            }

            if(script.currentLoadedCollection == null)
            {
                script.findOpenSceneCollection();
            }

            if(script.currentLoadedCollection == null)
            {
                script.SetCurrentCollectionEmpty();
            }

            setDefaultPaths();
            serializedObject.ApplyModifiedProperties();
        }
    }
}