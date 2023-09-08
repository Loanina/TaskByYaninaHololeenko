using System;
using System.Collections.Generic;
using App.Scripts.Scenes.SceneFillwords.Features.FillwordModels;
using UnityEngine;
using UnityEngine.Windows;
using System.IO;
using System.Linq;
using File = System.IO.File;
using System.Collections.Generic;

namespace App.Scripts.Scenes.SceneFillwords.Features.ProviderLevel
{
    public class ProviderFillwordLevel : IProviderFillwordLevel
    {
        public GridFillWords LoadModel(int index)
        {
            //напиши реализацию не меняя сигнатуру функции
            try
            {
                string[] levelsList = File.ReadAllLines("Assets/App/Resources/Fillwords/pack_0.txt");
                string[] wordsList = File.ReadAllLines("Assets/App/Resources/Fillwords/words_list.txt");

                var levelInfo = levelsList[index-1].Split(' ');

                //словарь номер слова - номера букв слова
                var levelWords = new Dictionary<int, int[]>();

                for (int i = 0; i < levelInfo.Length / 2; i++)
                {
                    var wordNum = int.Parse(levelInfo[i * 2]);
                    var wordLettersNums = levelInfo[i * 2 + 1].Split(';').Select(Int32.Parse).ToArray();
                    levelWords.Add(wordNum, wordLettersNums);
                }

                //устанавливаем размерность грида
                Vector2Int gridSize = new Vector2Int(levelWords.Keys.Count, levelWords.Keys.Count);
                GridFillWords gridFillWords = new GridFillWords(gridSize);
                
                //написть как заполняем грид букваи
                foreach (var key in levelWords.Keys)
                {
                    var word = wordsList[key]; 
                    var letterNums = levelWords[key];
                    
                    
                    //меняем порядок букв в слове
                    for (int i = 0; i <= letterNums.Length; i++)
                    {
                        CharGridModel letter = new CharGridModel(word[letterNums[i]]);
                        //занести букву в грид
                    }
                    
                }

            }
            catch (Exception e)
            {
                Debug.Log(e);
            }

            throw new NotImplementedException();
        }
    }
}