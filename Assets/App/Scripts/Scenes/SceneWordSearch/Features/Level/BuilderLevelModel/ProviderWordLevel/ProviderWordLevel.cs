using System;
using App.Scripts.Scenes.SceneWordSearch.Features.Level.Models.Level;
using UnityEngine;

namespace App.Scripts.Scenes.SceneWordSearch.Features.Level.BuilderLevelModel.ProviderWordLevel
{
    public class ProviderWordLevel : IProviderWordLevel
    {
        public LevelInfo LoadLevelData(int levelIndex)
        {
            //напиши реализацию не меняя сигнатуру функции
            try
            {
                LevelInfo levelInfo =
                    JsonUtility.FromJson<LevelInfo>(Resources.Load<TextAsset>("WordSearch/Levels/" + levelIndex).text);
                return levelInfo;
            }
            catch (Exception e)
            {
                Debug.Log(e);
                return null;
            }
        }
    }
}