using System;
using System.Collections.Generic;
using App.Scripts.Scenes.SceneChess.Features.ChessField.GridMatrix;
using App.Scripts.Scenes.SceneChess.Features.ChessField.Types;
using UnityEngine;

namespace App.Scripts.Scenes.SceneChess.Features.GridNavigation.Navigator
{
    public class ChessGridNavigator : IChessGridNavigator
    {
        public List<Vector2Int> FindPath(ChessUnitType unit, Vector2Int from, Vector2Int to, ChessGrid grid)
        {
            //напиши реализацию не меняя сигнатуру функции

            var path = new List<Vector2Int>();
            var color = grid.Get(from).PieceModel.Color;
            var pathFound = false;

            do
            {
                var moves = PossibleChessMoves(color, unit, from, grid.Size);
                if (moves.Contains(to))
                {
                    path.Add(to);
                    pathFound = true;
                    return path;
                }
                else if (moves.Count == 0)
                {
                    return null;
                }
                else return null; /// повторять и запоминать все пути

            } while (!pathFound);
            
            throw new NotImplementedException();
        }

        private List<Vector2Int> PossibleChessMoves(ChessUnitColor color,ChessUnitType type, Vector2Int startPosition, Vector2Int gridSize)
        {
            var moves = new List<Vector2Int>();
            switch (type)
            {
                case ChessUnitType.Pon:
                {
                    if (color == ChessUnitColor.White)
                    {
                        if (startPosition.y + 1 <= gridSize.y) 
                            moves.Add(new Vector2Int(startPosition.x, startPosition.y+1));
                    }
                    else
                    {
                        if (startPosition.y - 1 > 0)
                        {
                            moves.Add(new Vector2Int(startPosition.x, startPosition.y-1));
                        }
                    }
                    break;
                }
            }

            return moves;
        }
     
    }
}