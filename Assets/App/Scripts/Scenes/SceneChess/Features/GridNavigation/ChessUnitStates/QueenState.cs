using System.Collections.Generic;
using UnityEngine;

namespace App.Scripts.Scenes.SceneChess.Features.GridNavigation.ChessUnitStates
{
    public class QueenState : IChessUnitState
    {
        public List<Vector2Int> GetPossibleMoves(ChessUnitMoveProvider.ChessUnitData chessUnitData)
        {
            var moves = new List<Vector2Int>();
            var grid = chessUnitData.СhessGrid;
            var position = chessUnitData.Position;
            
            int[] dx = { -1, 1, 0, 0, -1, -1, 1, 1 };
            int[] dy = { 0, 0, -1, 1, -1, 1, -1, 1 };

            for (var i = 0; i < dx.Length; i++)
            {
                var x = position.x;
                var y = position.y;

                while (true)
                {
                    x += dx[i];
                    y += dy[i];

                    if (x >= grid.Size.x || y >= grid.Size.y || x < 0 || y < 0 ||
                        chessUnitData.СhessGrid.Get(y, x) != null)
                        break;
                    moves.Add(new Vector2Int(x, y));
                }
            }
            return moves;
        }
    }
}