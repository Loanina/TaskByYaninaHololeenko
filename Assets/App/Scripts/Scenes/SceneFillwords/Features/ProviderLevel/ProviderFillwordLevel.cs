using System;
using App.Scripts.Scenes.SceneFillwords.Features.FillwordModels;
using UnityEngine;
using System.Linq;
using App.Scripts.Libs.FilesLoader;

namespace App.Scripts.Scenes.SceneFillwords.Features.ProviderLevel
{
    public class ProviderFillwordLevel : IProviderFillwordLevel
    {
        private Loader _loader = new Loader(); //проверить
        
        public GridFillWords LoadModel(int index)
        {
            //напиши реализацию не меняя сигнатуру функции
            if (index < 1) return null; //
            
            var levelsTextAsset = _loader.LoadTextAsset("Fillwords/pack_0");
            var wordsTextAsset = _loader.LoadTextAsset("Fillwords/words_list");

            if (levelsTextAsset is null) //проверить
            {
                Debug.Log("Fillwords levels are null");
                return null;
            }

            if (wordsTextAsset is null) //проверить
            {
                Debug.Log("Fillwords words list is null");
                return null;
            }
            
            string[] levelInfo = levelsTextAsset.text.Split('\n')[index - 1].Split(' ');

            var levelWordsCount = levelInfo.Length / 2;

            string gridLetters = "";
            int lettersCount = 0;
            for (int i = 0; i < levelWordsCount; i++)
            {
                var word = wordsTextAsset.text.Split('\n')[int.Parse(levelInfo[i * 2])];

                var letterNums = levelInfo[i * 2 + 1].Split(';').Select(Int32.Parse).ToList();

                for (int j = 0; j < word.Length; j++)
                {
                    if (!letterNums.Contains(j))
                        letterNums.Add(j);
                    gridLetters += word[letterNums.IndexOf(j)]; //
                }

                lettersCount += word.Length;
            }

            var gridSize = (int)Math.Sqrt(lettersCount);
            GridFillWords gridFillWords = new GridFillWords(new Vector2Int(gridSize, gridSize));

            int letterIndex = 0;
            for (int i = 0; i < gridSize; i++)
            {
                for (int j = 0; j < gridSize; j++)
                {
                    gridFillWords.Set(i, j, new CharGridModel(gridLetters[letterIndex]));
                    letterIndex++;
                }
            }

            return gridFillWords;
        }
    }
}