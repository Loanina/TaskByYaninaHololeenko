using System;
using App.Scripts.Scenes.SceneFillwords.Features.FillwordModels;
using UnityEngine;
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
                //string[] levelsList = File.ReadAllLines("Assets/App/Resources/Fillwords/pack_0.txt");
                //string[] wordsList = File.ReadAllLines("Assets/App/Resources/Fillwords/words_list.txt");
                var levelsTextAsset = Resources.Load<TextAsset>("Fillwords/pack_0"); //2
                var wordsTextAsset = Resources.Load<TextAsset>("Fillwords/words_list"); //2

                // var levelInfo = levelsList.[index-1].Split(' ');
                string[] levelInfo = levelsTextAsset.text.Split('\n')[index-1].Split(' '); //2

                var levelWordsCount = levelInfo.Length / 2;

                string gridLetters = "";
                int lettersCount = 0;
                for (int i = 0; i < levelWordsCount; i++)
                {
                    //var word = wordsList[int.Parse(levelInfo[i * 2])];
                    var word = wordsTextAsset.text.Split('\n')[int.Parse(levelInfo[i * 2])]; //2
                    
                    var letterNums = levelInfo[i * 2 + 1].Split(';').Select(Int32.Parse).ToList();
                    
                    //добавляем буквы в нужном порядке
                    for (int j = 0; j < word.Length; j++)
                    {
                        if (letterNums.IndexOf(j)==-1) 
                            letterNums.Add(j);
                        gridLetters += word[letterNums.IndexOf(j)];
                    }
                    
                    lettersCount += word.Length;
                }
                
                //устанавливаем размерность грида
                var gridSize = (int)Math.Sqrt(lettersCount);
                GridFillWords gridFillWords = new GridFillWords(new Vector2Int(gridSize,gridSize));
                
                int letterIndex = 0;
                for (int i=0; i < gridSize; i++){
                    for (int j = 0; j <gridSize; j++)
                    {
                        gridFillWords.Set(i,j, new CharGridModel(gridLetters[letterIndex]));
                        letterIndex++;
                    }
                }
                
                return gridFillWords;
            }
            catch (Exception e)
            {
                Debug.Log(e);
                return null;
            }
        }
    }
}