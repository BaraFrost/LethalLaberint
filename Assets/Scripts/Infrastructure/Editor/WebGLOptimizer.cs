using UnityEditor;
using UnityEngine;

public class WebGLOptimizer : EditorWindow
{
    [MenuItem("Tools/WebGL Optimizer")]
    public static void OptimizeForWebGL() {
        // Optimize Quality Settings
        for (int i = 0; i < QualitySettings.names.Length; i++) {
            QualitySettings.SetQualityLevel(i, false);
            QualitySettings.shadowDistance = 20f; // Уменьшаем дальность теней
            QualitySettings.anisotropicFiltering = AnisotropicFiltering.Disable;
            QualitySettings.realtimeReflectionProbes = false;
            QualitySettings.lodBias = 1.5f; // Увеличиваем LOD-скейл
        }
        Debug.Log("Quality settings optimized.");

        // Optimize Graphics Settings
        PlayerSettings.colorSpace = ColorSpace.Gamma;
        PlayerSettings.graphicsJobs = true; // Включаем Graphics Jobs
        PlayerSettings.MTRendering = true; // Мультипоточный рендеринг

        Debug.Log("Graphics settings optimized.");

        // Optimize Build Settings for WebGL
        PlayerSettings.WebGL.compressionFormat = WebGLCompressionFormat.Gzip; // Включаем GZIP-сжатие
        PlayerSettings.WebGL.memorySize = 256; // Оптимальное значение памяти в MB
        PlayerSettings.WebGL.linkerTarget = WebGLLinkerTarget.Wasm; // Используем WebAssembly
        PlayerSettings.WebGL.exceptionSupport = WebGLExceptionSupport.None; // Отключаем исключения

        Debug.Log("Build settings optimized for WebGL.");

        // Optimize textures and materials
        OptimizeTextures();

        Debug.Log("All optimizations applied successfully.");
    }

    private static void OptimizeTextures() {
        string[] allAssets = AssetDatabase.GetAllAssetPaths();
        foreach (string assetPath in allAssets) {
            TextureImporter textureImporter = AssetImporter.GetAtPath(assetPath) as TextureImporter;
            if (textureImporter != null) {
                textureImporter.textureCompression = TextureImporterCompression.Compressed;
                textureImporter.crunchedCompression = true;
                textureImporter.compressionQuality = 50; // Баланс между качеством и размером
                textureImporter.mipmapEnabled = false; // Отключаем MipMap
                AssetDatabase.ImportAsset(assetPath);
            }
        }
        Debug.Log("Textures optimized.");
    }

    [MenuItem("Tools/Optimize Audio Clips for Web")]
    public static void OptimizeAudioClips() {
        string[] guids = AssetDatabase.FindAssets("t:AudioClip");
        int totalClips = guids.Length;

        if (totalClips == 0) {
            Debug.Log("No AudioClips found in the project.");
            return;
        }

        for (int i = 0; i < totalClips; i++) {
            string path = AssetDatabase.GUIDToAssetPath(guids[i]);
            AudioImporter importer = AssetImporter.GetAtPath(path) as AudioImporter;

            if (importer == null)
                continue;

            // Настройки импорта
            importer.forceToMono = true; // Преобразование в моно для экономии памяти
            importer.loadInBackground = true; // Загружаем аудио в фоне

            AudioImporterSampleSettings settings = importer.defaultSampleSettings;
            settings.loadType = AudioClipLoadType.CompressedInMemory; // Сжатие в памяти
            settings.compressionFormat = AudioCompressionFormat.Vorbis; // Формат Vorbis
            settings.quality = 0.1f; // Качество (оптимальное значение)

            importer.defaultSampleSettings = settings;

            AssetDatabase.ImportAsset(path, ImportAssetOptions.ForceUpdate);
            Debug.Log($"Optimized: {path} ({i + 1}/{totalClips})");
        }

        Debug.Log("Audio optimization complete.");
    }
}
