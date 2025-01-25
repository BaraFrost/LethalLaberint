using UnityEditor;
using UnityEngine;
using UnityEditor.SceneManagement;
using System.Linq;
using System.Reflection;

namespace Infrastructure {

    public static class LocalizationTextTranslator {

        [MenuItem("Tools/Translate All LocalizationText")]
        public static void TranslateAllLocalizationText() {
            // Перебираем все сцены в проекте
          /*  foreach (var scenePath in AssetDatabase.FindAssets("t:Scene").Select(AssetDatabase.GUIDToAssetPath)) {
                EditorSceneManager.OpenScene(scenePath);
                TranslateLocalizationTextInScene();
                EditorSceneManager.SaveScene(EditorSceneManager.GetActiveScene());
            }*/

            // Перебираем все ассеты в проекте
            foreach (var prefabPath in AssetDatabase.FindAssets("t:Prefab").Select(AssetDatabase.GUIDToAssetPath)) {
                var prefab = AssetDatabase.LoadAssetAtPath<GameObject>(prefabPath);
                if (prefab != null && TranslateLocalizationTextInObject(prefab)) {
                    EditorUtility.SetDirty(prefab);
                }
            }

            // Перебираем все ScriptableObject в проекте
            foreach (var scriptableObjectPath in AssetDatabase.FindAssets("t:ScriptableObject").Select(AssetDatabase.GUIDToAssetPath)) {
                var scriptableObject = AssetDatabase.LoadAssetAtPath<ScriptableObject>(scriptableObjectPath);
                if (scriptableObject != null && TranslateLocalizationTextInScriptableObject(scriptableObject)) {
                    EditorUtility.SetDirty(scriptableObject);
                }
            }

            // Сохраняем изменения
            AssetDatabase.SaveAssets();
            Debug.Log("Перевод всех LocalizationText завершён.");
        }

        private static void TranslateLocalizationTextInScene() {
            var rootObjects = UnityEngine.SceneManagement.SceneManager.GetActiveScene().GetRootGameObjects();
            foreach (var rootObject in rootObjects) {
                TranslateLocalizationTextInObject(rootObject);
            }
        }

        private static bool TranslateLocalizationTextInObject(GameObject obj) {
            bool hasChanges = false;
            var components = obj.GetComponentsInChildren<MonoBehaviour>(true);

            foreach (var component in components) {
                var fields = component.GetType()
                    .GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance)
                    .Where(field => field.FieldType == typeof(LocalizationText));

                foreach (var field in fields) {
                    var localizationText = field.GetValue(component) as LocalizationText;
                    if (localizationText != null) {
                        localizationText.Translate();
                        hasChanges = true;
                    }
                }
            }

            return hasChanges;
        }

        private static bool TranslateLocalizationTextInScriptableObject(ScriptableObject scriptableObject) {
            bool hasChanges = false;
            var fields = scriptableObject.GetType()
                .GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance)
                .Where(field => field.FieldType == typeof(LocalizationText));

            foreach (var field in fields) {
                var localizationText = field.GetValue(scriptableObject) as LocalizationText;
                if (localizationText != null) {
                    localizationText.Translate();
                    hasChanges = true;
                }
            }

            return hasChanges;
        }
    }

}