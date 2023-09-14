using UnityEngine;

namespace App.Scripts.Libs.FilesLoader
{
    public class Loader : ILoader<TextAsset>
    {
        public TextAsset LoadTextAsset(string path)
        {
            return Resources.Load<TextAsset>(path);
        }
    }
}