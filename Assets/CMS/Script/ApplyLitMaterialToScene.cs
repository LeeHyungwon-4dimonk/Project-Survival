#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using UnityEngine.Tilemaps;

public class ApplyLitMaterialToScene : EditorWindow
{
    private static Material _litMaterial;

    [MenuItem("Tools/Apply Sprite-Lit-Default Material")]
    public static void ApplyLitMaterial()
    {
        Shader litShader = Shader.Find("Universal Render Pipeline/2D/Sprite-Lit-Default");
        if (litShader == null)
        {
            Debug.LogError("Sprite-Lit-Default Shader�� ã�� �� �����ϴ�. URP 2D�� ����� �����ƴ��� Ȯ���ϼ���.");
            return;
        }

        _litMaterial = new Material(litShader);

        int count = 0;

        foreach (SpriteRenderer sr in FindObjectsOfType<SpriteRenderer>())
        {
            sr.sharedMaterial = _litMaterial;
            count++;
        }

        foreach (TilemapRenderer tm in FindObjectsOfType<TilemapRenderer>())
        {
            tm.material = _litMaterial;
            count++;
        }

        Debug.Log($"�� �� SpriteRenderer �� TilemapRenderer {count}���� Sprite-Lit-Default ��Ƽ������ �����߽��ϴ�.");
    }
}
#endif

