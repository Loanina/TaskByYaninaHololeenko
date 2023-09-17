using System;
using App.Scripts.Scenes.SceneFillwords.Features.FillwordModels;
using UnityEngine;
using System.Linq;
using App.Scripts.Libs.FilesLoader;

namespace App.Scripts.Scenes.SceneFillwords.Features.ProviderLevel
{
    public class ProviderFillwordLevel : IProviderFillwordLevel
    {
        private readonly Loader _loader = new Loader();

        public GridFillWords LoadModel(int index)
        {
            if (index < 1) return null;

            var levelsTextAsset = _loader.LoadTextAsset("Fillwords/pack_0");
            if (levelsTextAsset is null)
                return null;

            var wordsTextAsset = _loader.LoadTextAsset("Fillwords/words_list");
            if (wordsTextAsset is null)
                return null;

            var levelInfo = levelsTextAsset.text.Split('\n')[index - 1].Split(' ');

            var levelWordsCount = levelInfo.Length / 2;

            var gridLetters = "";
            var lettersCount = 0;
            for (var i = 0; i < levelWordsCount; i++)
            {
                var word = wordsTextAsset.text.Split('\n')[int.Parse(levelInfo[i * 2])];

                var letterNums = levelInfo[i * 2 + 1].Split(';').Select(int.Parse).ToList();

                for (var j = 0; j < word.Length; j++)
                {
                    if (!letterNums.Contains(j))
                        letterNums.Add(j);
                    gridLetters += word[letterNums.IndexOf(j)];
                }

                lettersCount += word.Length;
            }

            var gridSize = (int)Math.Sqrt(lettersCount);
            var gridFillWords = new GridFillWords(new Vector2Int(gridSize, gridSize));

            var letterIndex = 0;
            for (var i = 0; i < gridSize; i++)
            {
                for (var j = 0; j < gridSize; j++)
                {
                    gridFillWords.Set(i, j, new CharGridModel(gridLetters[letterIndex]));
                    letterIndex++;
                }
            }

            return gridFillWords;
        }
    }
}