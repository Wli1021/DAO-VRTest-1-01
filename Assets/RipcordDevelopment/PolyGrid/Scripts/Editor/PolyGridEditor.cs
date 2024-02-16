using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

// /-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\
//
// 						    PolyGrid 1.0, Copyright © 2018, Ripcord Development
//											PolyGridEditor.cs
//										   info@ripcorddev.com
//
// \-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/

//ABOUT - This script controls the appearance of the PolyGrid component in the inspector

namespace PolyGrid
{
    [CustomEditor(typeof(PolyGrid))]
    public class PolyGridEditor : Editor
    {
        SerializedProperty m_Rows;
        SerializedProperty m_Columns;
        SerializedProperty m_Spacing;

        SerializedProperty m_ShowVertices;
        SerializedProperty m_ShowEdgeRows;
        SerializedProperty m_ShowEdgeColumns;
        SerializedProperty m_VertexSize;
        SerializedProperty m_EdgeThickness;

        SerializedProperty m_VertexRange;
        SerializedProperty m_MinTime;
        SerializedProperty m_MaxTime;
        SerializedProperty m_SpeedMultiplier;
        SerializedProperty m_AutoStart;


        protected virtual void OnEnable ()
        {
            m_Rows = this.serializedObject.FindProperty("rows");
            m_Columns = this.serializedObject.FindProperty("columns");
            m_Spacing = this.serializedObject.FindProperty("spacing");

            m_ShowVertices = this.serializedObject.FindProperty("showVertices");
            m_ShowEdgeRows = this.serializedObject.FindProperty("showEdgeRows");
            m_ShowEdgeColumns = this.serializedObject.FindProperty("showEdgeColumns");
            m_VertexSize = this.serializedObject.FindProperty("vertexSize");
            m_EdgeThickness = this.serializedObject.FindProperty("edgeThickness");

            m_VertexRange = this.serializedObject.FindProperty("vertexRange");
            m_MinTime = this.serializedObject.FindProperty("minTime");
            m_MaxTime = this.serializedObject.FindProperty("maxTime");
            m_SpeedMultiplier = this.serializedObject.FindProperty("speedMultiplier");
            m_AutoStart = this.serializedObject.FindProperty("autoStart");
        }

        public override void OnInspectorGUI ()
        {
            DrawDefaultInspector ();

            PolyGrid polyGrid = (PolyGrid)target;

            this.serializedObject.Update();

            // GRID CONSTRUCTION ----------------------------------------
            GUILayout.BeginVertical("Box");
            GUILayout.Label("Grid Construction");

            GUILayout.BeginHorizontal("Box");
            polyGrid.vertex = (GameObject)EditorGUILayout.ObjectField("Vertex Prefab", polyGrid.vertex, typeof(GameObject), true);
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal("Box");
            polyGrid.vertexMaterial = (Material)EditorGUILayout.ObjectField("Vertex Material", polyGrid.vertexMaterial, typeof(Material), true);
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal("Box");
            polyGrid.edgeMaterial = (Material)EditorGUILayout.ObjectField("Edge Material", polyGrid.edgeMaterial, typeof(Material), true);
            GUILayout.EndHorizontal();

            GUILayout.EndVertical();
            EditorGUILayout.Space();


            // GRID DIMENSIONS ----------------------------------------
            GUILayout.BeginVertical("Box");
            GUILayout.Label("Grid Dimensions");

            GUILayout.BeginHorizontal("Box");
            EditorGUILayout.IntSlider(m_Rows, 2, 32);
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal("Box");
            EditorGUILayout.IntSlider(m_Columns, 2, 32);
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal("Box");
            EditorGUILayout.Slider(m_Spacing, 0.01f, 10.0f);
            GUILayout.EndHorizontal();

            GUILayout.EndVertical();
            EditorGUILayout.Space();

            // GRID APPEARANCE ----------------------------------------
            GUILayout.BeginVertical("Box");
            GUILayout.Label("Grid Appearance");

            GUILayout.BeginVertical("Box");
            polyGrid.showVertices = EditorGUILayout.Toggle("Show Vertices", m_ShowVertices.boolValue);
            polyGrid.showEdgeRows = EditorGUILayout.Toggle("Show Edge Rows", m_ShowEdgeRows.boolValue);
            polyGrid.showEdgeColumns = EditorGUILayout.Toggle("Show Edge Columns", m_ShowEdgeColumns.boolValue);
            GUILayout.EndVertical();

            GUILayout.BeginHorizontal("Box");
            EditorGUILayout.Slider(m_VertexSize, 0.01f, 32.0f);
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal("Box");
            EditorGUILayout.Slider(m_EdgeThickness, 0.01f, 32.0f);
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal("Box");
            polyGrid.vertexColour = EditorGUILayout.ColorField("Vertex Colour", polyGrid.vertexColour);
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal("Box");
            polyGrid.edgeColour = EditorGUILayout.ColorField("Edge Colour", polyGrid.edgeColour);
            GUILayout.EndHorizontal();

            GUILayout.EndVertical();
            EditorGUILayout.Space();


            // GRID MOVEMENT ----------------------------------------
            GUILayout.BeginVertical("Box");
            GUILayout.Label("Grid Movement");

            GUILayout.BeginHorizontal("Box");
            EditorGUILayout.Slider(m_VertexRange, 0.01f, 32.0f);
            GUILayout.EndHorizontal();

            GUILayout.BeginVertical("Box");
            EditorGUILayout.MinMaxSlider("Move Time", ref polyGrid.minTime, ref polyGrid.maxTime, 0.01f, 10.0f);
            m_MinTime.floatValue = Mathf.Clamp(EditorGUILayout.FloatField("Min.", m_MinTime.floatValue), 0.0f, polyGrid.maxTime);
            m_MaxTime.floatValue = Mathf.Clamp(EditorGUILayout.FloatField("Max.", m_MaxTime.floatValue), polyGrid.minTime, 10.0f);
            GUILayout.EndVertical();

            GUILayout.BeginHorizontal("Box");
            EditorGUILayout.Slider(m_SpeedMultiplier, 0.1f, 10.0f);
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal("Box");
            polyGrid.autoStart= EditorGUILayout.Toggle("Auto Start", m_AutoStart.boolValue);
            GUILayout.EndHorizontal();

            GUILayout.EndVertical();
            EditorGUILayout.Space();

            if (GUILayout.Button ("BuildGrid", GUILayout.Height(32) )) {
                polyGrid.BuildGrid();
            }
            EditorGUILayout.Space();

            this.serializedObject.ApplyModifiedProperties();

            SceneView.RepaintAll();
        }
    }
}
