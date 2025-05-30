using UnityEngine;
using UnityEditor;
using System.IO;
using System.Linq;

public class ExportSprites
{
    [MenuItem("Assets/Export Sliced Sprites")]
    static void Export()
    {
        // ���� ���õ� �ؽ�ó ����� ���
        var tex = Selection.activeObject as Texture2D;
        if (tex == null) { Debug.LogError("Texture2D�� �������ּ���."); return; }

        string path = AssetDatabase.GetAssetPath(tex);
        // ��������Ʈ ��������
        var sprites = AssetDatabase.LoadAllAssetsAtPath(path).OfType<Sprite>().ToArray();
        // ��� ����
        string exportDir = "Assets/ExportedSprites";
        if (!Directory.Exists(exportDir)) Directory.CreateDirectory(exportDir);

        // �б� ���� �ѱ�
        var ti = AssetImporter.GetAtPath(path) as TextureImporter;
        if (ti != null && !ti.isReadable)
        {
            ti.isReadable = true;
            ti.SaveAndReimport();
        }

        foreach (var sp in sprites)
        {
            Rect r = sp.rect;
            var pixels = tex.GetPixels(
                (int)r.x, (int)r.y,
                (int)r.width, (int)r.height
            );
            var newTex = new Texture2D((int)r.width, (int)r.height);
            newTex.SetPixels(pixels);
            newTex.Apply();

            byte[] png = newTex.EncodeToPNG();
            File.WriteAllBytes($"{exportDir}/{sp.name}.png", png);
            Object.DestroyImmediate(newTex);
        }

        AssetDatabase.Refresh();
        Debug.Log($"Exported {sprites.Length} sprites to {exportDir}");
    }
}
