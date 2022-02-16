using System.IO;
using UnityEditor;
using UnityEngine;

namespace Snorlax.LocaleEditor
{
    [InitializeOnLoad]
    public class PluginManager : AssetPostprocessor
    {
        static PluginManager() { EditorApplication.update += Initialize; }

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
            if (Path.GetFileName(assetPath).Equals(Path.GetFileName(ImportPackage.PATH_INSTALL)) || File.Exists(ImportPackage.PATH_INSTALL))
                EditorPrefs.DeleteKey(Application.identifier + ".locale");

            return AssetDeleteResult.DidDelete;
        }
    }
}