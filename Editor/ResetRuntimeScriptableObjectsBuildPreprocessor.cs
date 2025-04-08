using UnityEngine;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using Scriptable_Objects_Architecture;
using Scriptable_Objects_Architecture.Runtime.Variables; // Your namespace

namespace Scriptable_Objects_Architecture.Editor {
// This class implements IPreprocessBuildWithReport to hook into the build process.
    public class ResetRuntimeScriptableObjectsBuildPreprocessor : IPreprocessBuildWithReport {
        // Determines the order of execution if multiple preprocessors exist. Lower numbers run first.
        public int callbackOrder {
            get { return 0; }
        }

        // This method is called automatically before Unity starts building the player.
        public void OnPreprocessBuild(BuildReport report) {
            Debug.Log("[Build Preprocessor] Starting reset of all RuntimeScriptableObject assets...");

            // Find all asset GUIDs for assets inheriting from RuntimeScriptableObject.
            // Using "t:TypeName" searches for assets of that type or inheriting from it.
            string[] guids = AssetDatabase.FindAssets($"t:{nameof(RuntimeScriptableObject)}");
            int resetCount = 0;
            foreach (string guid in guids) {
                // Get the path to the asset using its GUID.
                string assetPath = AssetDatabase.GUIDToAssetPath(guid);

                // Load the asset from the database.
                RuntimeScriptableObject instance = AssetDatabase.LoadAssetAtPath<RuntimeScriptableObject>(assetPath);
                if (instance != null) {
                    // Call the public OnReset method.
                    Debug.Log($"-- Resetting asset: {instance.name} at path {assetPath}", instance);
                    instance.OnReset();

                    // Mark the asset as dirty so the changes are saved into the build.
                    EditorUtility.SetDirty(instance);
                    resetCount++;
                }
                else {
                    Debug.LogWarning(
                        $"[Build Preprocessor] Failed to load RuntimeScriptableObject at path: {assetPath}");
                }
            }

            // Save all modified assets to ensure changes are included in the build.
            AssetDatabase.SaveAssets();
            Debug.Log($"[Build Preprocessor] Finished resetting {resetCount} RuntimeScriptableObject assets.");
        }
    }
}