using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;

namespace Editor.ObjectProviders
{
    public class VillainDataProvider : IEnumerable<VillainData>
    {
        protected string filter = "t:VillainData";
        protected string[] searchFolders = new[] {"Assets/ScriptableObjects"};

        public IEnumerator<VillainData> GetEnumerator()
        {
            return AssetDatabase.FindAssets(filter, searchFolders)
                .Select(AssetDatabase.GUIDToAssetPath)
                .Select(path => AssetDatabase.LoadMainAssetAtPath(path) as VillainData)
                .GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable<VillainData>) this).GetEnumerator();
    }
}
