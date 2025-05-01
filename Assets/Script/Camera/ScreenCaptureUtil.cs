#if UNITY_EDITOR
using UnityEditor;
#endif
using System.IO;
using UnityEngine;

[ExecuteInEditMode]
public class ScreenCaptureUtil : MonoBehaviour
{
    [Tooltip("Base name for screenshots; an index will be appended to ensure uniqueness")]
    public string baseName = "character";
    public string fileExtension = "png";
    public int superSize = 1;

    private int captureCount = 0;

    [ContextMenu("Capture Screenshot")]
    public void CaptureScreenshot()
    {
        string relativePath = "Resources/Character/Screenshot";
        string folder = Path.Combine(Application.dataPath, relativePath);

        if (!Directory.Exists(folder))
        {
            Debug.LogError("[ScreenCaptureUtil] There is No Right Directory!! (Assets/Resources/Character/Screenshot)");
            return;
        }

        string timestamp = System.DateTime.Now.ToString("yyyyMMdd_HHmmss");
        string fileName = $"{baseName}_{timestamp}_{captureCount}.{fileExtension}";
        string fullPath = Path.Combine(folder, fileName);
        string assetPath = $"Assets/{relativePath}/{fileName}";

        ScreenCapture.CaptureScreenshot(fullPath, superSize);
        Debug.Log($"[ScreenCaptureUtil] Screenshot saved: {fullPath}");
        captureCount++;

#if UNITY_EDITOR
        // AssetDatabase and Sprite Settings
        EditorApplication.delayCall += () =>
        {
            const float timeout = 3f;
            float startTime = (float)EditorApplication.timeSinceStartup;

            EditorApplication.update += WaitForFile;

            void WaitForFile()
            {
                if (File.Exists(fullPath) || (float)EditorApplication.timeSinceStartup - startTime > timeout)
                {
                    EditorApplication.update -= WaitForFile;
                    AssetDatabase.Refresh();

                    TextureImporter importer = AssetImporter.GetAtPath(assetPath) as TextureImporter;
                    if (importer != null)
                    {
                        importer.textureType = TextureImporterType.Sprite;
                        importer.spriteImportMode = SpriteImportMode.Single; 
                        importer.SaveAndReimport();
                        Debug.Log($"[ScreenCaptureUtil] Converted to Sprite (Single): {assetPath}");
                    }
                    else
                    {
                        Debug.LogWarning($"[ScreenCaptureUtil] Could not find importer for: {assetPath}");
                    }
                }
            }
        };
#endif
    }
}
