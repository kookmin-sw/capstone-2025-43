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
    public int superSize = 1; // 1 = native resolution, 2 = 2¡¿ resolution, etc.

    private int captureCount = 0;

    // Called from the context menu in the Inspector
    [ContextMenu("Capture Screenshot")]
    public void CaptureScreenshot()
    {
        // Folder to save screenshots (Assets/Character/Screenshot)
        string folder = Path.Combine(Application.dataPath, "Characters", "Screenshots");
        if (!Directory.Exists(folder))
            Directory.CreateDirectory(folder);

        // Generate file name
        string timestamp = System.DateTime.Now.ToString("yyyyMMdd_HHmmss");
        string fileName = $"{baseName}_{timestamp}_{captureCount}.{fileExtension}";
        string fullPath = Path.Combine(folder, fileName);

        // Capture screenshot
        ScreenCapture.CaptureScreenshot(fullPath, superSize);
        Debug.Log($"[ScreenCaptureUtil] Screenshot saved: {fullPath}");
        captureCount++;

#if UNITY_EDITOR
        // Refresh the AssetDatabase so the new file appears in the Project window
        AssetDatabase.Refresh();
#endif
    }
}
