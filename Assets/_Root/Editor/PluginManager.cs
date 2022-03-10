using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Snorlax.LocaleEditor
{
    [InitializeOnLoad]
    public class PluginManager : AssetPostprocessor
    {
        static PluginManager()
        {
            EditorApplication.update += Initialize;
            
            AssetDatabase.importPackageCompleted += _ =>
            {
                if (_.ToLower().Equals("locale-package"))
                {
                    ScriptingDefinition1.AddDefineSymbolOnAllPlatforms("PANCAKE_LOCALE");
                }
            };
        }

        private static void Initialize()
        {
            EditorApplication.update -= Initialize;

            if (!ImportPackage.IsLocaleImported()) ImportPackage.ImportLocale(false);
        }
    }

    public class PostDelete : UnityEditor.AssetModificationProcessor
    {
        private static AssetDeleteResult OnWillDeleteAsset(string assetPath, RemoveAssetOptions options)
        {
            if (Path.GetFileName(assetPath).Equals(Path.GetFileName(ImportPackage.PATH_INSTALL)) || assetPath.Equals("Assets/Plugins/Locale") ||
                assetPath.Equals("Assets/Plugins") || CheckFolderContainLocale(assetPath))
            {
                EditorPrefs.DeleteKey(Application.identifier + ".locale");
                ScriptingDefinition1.RemoveDefineSymbolOnAllPlatforms("PANCAKE_LOCALE");
            }

            return AssetDeleteResult.DidNotDelete;
        }

        private static bool CheckFolderContainLocale(string path)
        {
            var flag = false;
            var result = path.Split('/').ToList();

            foreach (string s in result)
            {
                if (s.Equals("Locale")) flag = true;
            }

            return flag;
        }
    }
}