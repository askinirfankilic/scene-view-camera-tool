using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SceneViewCameraTransform)), InitializeOnLoad]
public class SceneViewCameraTransform : Editor
{
    private static readonly string IS_ENABLED = "isEnabled";

    static SceneViewCameraTransform()
    {
        if (EditorPrefs.GetBool(IS_ENABLED))
        {
            Enable();
        }
        else
        {
            Disable();
        }
    }

#pragma warning disable CS0414
    private static bool _isEnabled;
#pragma warning restore CS0414


    [MenuItem("Tools/Custom Tools/Enable Scene Camera Transform")]
    public static void Enable()
    {
        if (_isEnabled) return;

        EditorPrefs.SetBool(IS_ENABLED, true);
        _isEnabled = true;
        SceneView.duringSceneGui += OnSceneGUI;
    }

    [MenuItem("Tools/Custom Tools/Disable Scene Camera Transform")]
    public static void Disable()
    {
        if (!_isEnabled) return;

        EditorPrefs.SetBool(IS_ENABLED, false);
        _isEnabled = false;
        SceneView.duringSceneGui -= OnSceneGUI;
    }


    private static void OnSceneGUI(SceneView view)
    {
        Handles.BeginGUI();
        {
            var camTransform = SceneView.currentDrawingSceneView.camera.transform;
            var pos = camTransform.position;
            var rot = camTransform.eulerAngles;
            GUIStyle _boxStyle = new GUIStyle("box");
            var rect = SceneView.currentDrawingSceneView.camera.pixelRect;
            GUILayout.BeginArea(new Rect(10, rect.height - 120, 200, 110), _boxStyle);
            {
                EditorGUILayout.Vector3Field("Scene Camera Position", pos);
                EditorGUILayout.Vector3Field("Scene Camera Rotation", rot);
                EditorGUILayout.FloatField("Magnitude To World Origin", pos.magnitude);
            }
            GUILayout.EndArea();
        }
        Handles.EndGUI();
    }
}
