using App.Scripts.Libs.FilesLoader;
using App.Scripts.Scenes.SceneWordSearch.Features.Level.Models.Level;
using UnityEngine;

namespace App.Scripts.Scenes.SceneWordSearch.Features.Level.BuilderLevelModel.ProviderWordLevel
{
    public class ProviderWordLevel : IProviderWordLevel
    {
        private readonly Loader _loader = new Loader();

        public LevelInfo LoadLevelData(int levelIndex)
        {
            var levelInfo =
                JsonUtility.FromJson<LevelInfo>(_loader.LoadTextAsset("WordSearch/Levels/" + levelIndex).text);
            return levelInfo;
        }
    }
}