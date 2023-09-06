using System;
using System.Collections.Generic;
using App.Scripts.Scenes.SceneFillwords.Features.FillwordModels;
using UnityEngine;
using UnityEngine.Windows;
using System.IO;
using System.Linq;
using File = System.IO.File;

namespace App.Scripts.Scenes.SceneFillwords.Features.ProviderLevel
{
    public class ProviderFillwordLevel : IProviderFillwordLevel
    {
        public GridFillWords LoadModel(int index)
        {
            //напиши реализацию не меняя сигнатуру функции

            try
            {
                var levelNum = index - 1;

                string[] levelsList = File.ReadAllLines("Assets/App/Resources/Fillwords/pack_0.txt");
                string[] wordsList = File.ReadAllLines("Assets/App/Resources/Fillwords/words_list.txt");

                var levelInfo = levelsList[levelNum].Split(' ');

                //словарь номер слова - номера букв слова
                Dictionary<int, int[]> levelWords = new Dictionary<int, int[]>();

                for (int i = 0; i < levelInfo.Length / 2; i++)
                {
                    int wordNum = int.Parse(levelInfo[i * 2]);
                    var wordLetters = levelInfo[i * 2 + 1].Split(';').Select(Int32.Parse).ToArray();

                    levelWords.Add(wordNum, wordLetters);
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