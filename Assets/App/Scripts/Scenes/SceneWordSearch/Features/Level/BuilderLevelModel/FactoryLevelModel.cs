using System.Collections.Generic;
using App.Scripts.Libs.Factory;
using App.Scripts.Scenes.SceneWordSearch.Features.Level.Models.Level;

namespace App.Scripts.Scenes.SceneWordSearch.Features.Level.BuilderLevelModel
{
    public class FactoryLevelModel : IFactory<LevelModel, LevelInfo, int>
    {
        public LevelModel Create(LevelInfo value, int levelNumber)
        {
            var model = new LevelModel();

            model.LevelNumber = levelNumber;

            model.Words = value.words;
            model.InputChars = BuildListChars(value.words);

            return model;
        }

        private List<char> BuildListChars(List<string> words)
        {
            //напиши реализацию не меняя сигнатуру функции

            List<char> buildListChars = new List<char>();
            foreach (var word in words)
            {
                for (int i = 0; i < word.Length; i++)
                {
                    int countLetter = 0;
                    for (int j = i; j >= 0; j--)
                    {
                        if (word[i] == word[j]) countLetter++;
                    }

                    if (buildListChars.FindAll(item => item == word[i]).Count < countLetter)
                        buildListChars.Add(word[i]);
                }
            }

            return buildListChars;
        }
    }
}