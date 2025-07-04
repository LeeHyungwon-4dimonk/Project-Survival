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
            Debug.LogError("Sprite-Lit-Default Shader를 찾을 수 없습니다. URP 2D가 제대로 설정됐는지 확인하세요.");
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

        Debug.Log($"씬 내 SpriteRenderer 및 TilemapRenderer {count}개에 Sprite-Lit-Default 머티리얼을 적용했습니다.");
    }
}
#endif

