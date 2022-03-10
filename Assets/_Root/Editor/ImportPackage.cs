using System;

namespace Snorlax.LocaleEditor
{
#if UNITY_EDITOR
    using System.IO;
    using System.Reflection;
    using UnityEditor;
    using UnityEngine;


    public static class ImportPackage
    {
        private const string PACKAGE_PATH = "Assets/_Root/Packages/locale-package.unitypackage";
        internal const string PATH_INSTALL = "Assets/Plugins/Locale/PreciseLocale.cs";
        private const string PACKAGE_UPM_PATH = "Packages/com.snorlax.locale/Packages/locale-package.unitypackage";
        private const string LOCALE_NAMESPACE = "Snorlax.Locale";
        private const string LOCALE_CLASS = "PreciseLocale";

        [MenuItem("Package/Import Locale")]
        public static void Import()
        {
            ImportLocale(true);
        }

        public static bool IsLocaleImported()
        {
            var imported = EditorPrefs.GetBool(Application.identifier + ".locale", false);
            var locale = FindClass(LOCALE_CLASS, LOCALE_NAMESPACE);

            return locale != null || imported;
        }

        public static void ImportLocale(bool interactive)
        {
            EditorPrefs.SetBool(Application.identifier + ".locale", true);
            string path = PACKAGE_PATH;
            if (!File.Exists(path)) path = !File.Exists(Path.GetFullPath(PACKAGE_UPM_PATH)) ? PACKAGE_PATH : PACKAGE_UPM_PATH;
            AssetDatabase.ImportPackage(path, interactive);
        }
        /// <summary>
        /// Returns the first class found with the specified class name and (optional) namespace and assembly name.
        /// Returns null if no class found.
        /// </summary>
        /// <returns>The class.</returns>
        /// <param name="className">Class name.</param>
        /// <param name="nameSpace">Optional namespace of the class to find.</param>
        /// <param name="assemblyName">Optional simple name of the assembly.</param>
        public static Type FindClass(string className, string nameSpace = null, string assemblyName = null)
        {
            string typeName = string.IsNullOrEmpty(nameSpace) ? className : nameSpace + "." + className;
            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();

            foreach (Assembly asm in assemblies)
            {
                // The assembly must match the given one if any.
                if (!string.IsNullOrEmpty(assemblyName) && !asm.GetName().Name.Equals(assemblyName))
                {
                    continue;
                }

                try
                {
                    Type t = asm.GetType(typeName);

                    if (t != null && t.IsClass)
                        return t;
                }
                catch (ReflectionTypeLoadException e)
                {
                    foreach (var le in e.LoaderExceptions)
                        Debug.LogException(le);
                }
            }

            return null;
        }
    }

#endif
}