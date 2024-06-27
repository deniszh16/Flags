using UnityEditor;
using UnityEngine;

namespace Flags.Editor
{
    public static class Tools
    {
        [MenuItem("Tools/Clear PlayerPrefs")]
        public static void ClearPlayerPrefs()
        {
            PlayerPrefs.DeleteAll();
            PlayerPrefs.Save();
        }
    }
}