using UnityEngine;

namespace App.Scripts.Libs.FilesLoader
{
    public interface ILoader <T>
    {
        public T LoadTextAsset(string path);
    }
}
