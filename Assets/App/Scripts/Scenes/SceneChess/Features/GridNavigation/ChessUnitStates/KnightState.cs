using System.Collections.Generic;
using UnityEngine;

namespace App.Scripts.Scenes.SceneChess.Features.GridNavigation.ChessUnitStates
{
    public class KnightState : IChessUnitState
    {
        public List<Vector2Int> GetPossibleMoves(ChessUnitMoveProvider.ChessUnitData chessUnitData)
        {
            var moves = new List<Vector2Int>();
            var grid = chessUnitData.СhessGrid;
            var position = chessUnitData.Position;

            int[] dx = { -2, -1, 1, 2, 2, 1, -1, -2 };
            int[] dy = { 1, 2, 2, 1, -1, -2, -2, -1 };

            for (var i = 0; i < dx.Length; i++)
            {
                var x = position.x + dx[i];
                var y = position.y + dy[i];

                if (x < grid.Size.x && y < grid.Size.y && x >= 0 && y >= 0 &&
                    chessUnitData.СhessGrid.Get(y, x) == null)
                {
                    moves.Add(new Vector2Int(x, y));
                }
            }

            return moves;
        }
    }
}