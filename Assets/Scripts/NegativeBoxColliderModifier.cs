#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

// Negative Box Collider Fixing Tool
// Reference: https://bonnate.tistory.com/538
public class NegativeBoxColliderModifier : MonoBehaviour
{
    [MenuItem("Tools/Bonnate/Negative Box Collider Modifier")]
    static void OpenNegativeBoxColliderModifierWindow()
    {
        // 에디터 창 열기
        NegativeBoxColliderWindow window = EditorWindow.GetWindow<NegativeBoxColliderWindow>("Negative Box Collider Modifier");
        window.Show();
    }
}

public class NegativeBoxColliderWindow : EditorWindow
{
    private Vector2 mScrollPosition; // 스크롤

    void OnGUI()
    {
        mScrollPosition = GUILayout.BeginScrollView(mScrollPosition);

        int cnt = 0;

        foreach (BoxCollider collider in FindObjectsOfType<BoxCollider>(true))
        {
            if (collider.transform.lossyScale.x * collider.size.x > 0 && collider.transform.lossyScale.y * collider.size.y > 0 && collider.transform.lossyScale.z * collider.size.z > 0)
                continue;

            ++cnt;

            GUILayout.BeginHorizontal();
            GUILayout.Label($"{collider.gameObject.name}", EditorStyles.boldLabel);
            if (GUILayout.Button("View", GUILayout.Width(60)))
            {
                SceneView.lastActiveSceneView.LookAt(collider.transform.position);
                Selection.activeGameObject = collider.gameObject;
            }

            if (GUILayout.Button("Fix", GUILayout.Width(60)))
            {
                Undo.RecordObject(collider, "Fix Collider");

                SerializedObject colliderObj = new SerializedObject(collider);

                if (collider.transform.lossyScale.x * collider.size.x < 0)
                    colliderObj.FindProperty("m_Size.x").floatValue *= -1;
                if (collider.transform.lossyScale.y * collider.size.y < 0)
                    colliderObj.FindProperty("m_Size.y").floatValue *= -1;
                if (collider.transform.lossyScale.z * collider.size.z < 0)
                    colliderObj.FindProperty("m_Size.z").floatValue *= -1;

                colliderObj.ApplyModifiedProperties();
            }
            GUILayout.EndHorizontal();
        }

        GUILayout.EndScrollView();

        if (cnt == 0)
            GUILayout.Label($"There's no negative Box colliders in scene!", EditorStyles.boldLabel);

        GUILayout.Space(20);

        GUILayout.BeginHorizontal();

        EditorGUILayout.LabelField("Powered by: Bonnate");

        if (GUILayout.Button("Github", GetHyperlinkLabelStyle()))
        {
            OpenURL("https://github.com/bonnate");
        }

        if (GUILayout.Button("Blog", GetHyperlinkLabelStyle()))
        {
            OpenURL("https://bonnate.tistory.com/");
        }

        GUILayout.EndHorizontal();
    }

    #region _HYPERLINK
    private GUIStyle GetHyperlinkLabelStyle()
    {
        GUIStyle style = new GUIStyle(GUI.skin.label);
        style.normal.textColor = new Color(0f, 0.5f, 1f);
        style.stretchWidth = false;
        style.wordWrap = false;
        return style;
    }

    private void OpenURL(string url)
    {
        EditorUtility.OpenWithDefaultApp(url);
    }
    #endregion

    #region 
    private void Log(string content)
    {
        Debug.Log($"<color=cyan>[WAV Easy Volume Editor]</color> {content}");
    }
    #endregion
}
#endif
