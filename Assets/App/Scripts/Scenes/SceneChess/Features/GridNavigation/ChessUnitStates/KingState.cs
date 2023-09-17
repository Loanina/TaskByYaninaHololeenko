using System.Collections.Generic;
using UnityEngine;

namespace App.Scripts.Scenes.SceneChess.Features.GridNavigation.ChessUnitStates
{
    public class KingState : IChessUnitState
    {
        public List<Vector2Int> GetPossibleMoves(ChessUnitMoveProvider.ChessUnitData chessUnitData)
        {
            var moves = new List<Vector2Int>();
            var grid = chessUnitData.СhessGrid;
            for (var dx = -1; dx < 2; dx++)
            {
                for (var dy = -1; dy < 2; dy++)
                {
                    if (dx == 0 && dy == 0) continue;

                    var x = chessUnitData.Position.x + dx;
                    var y = chessUnitData.Position.y + dy;
                    if (x < grid.Size.x && y < grid.Size.y && x >= 0 && y >= 0 &&
                        chessUnitData.СhessGrid.Get(y, x) == null)
                    {
                        moves.Add(new Vector2Int(x, y));
                    }
                }
            }

            return moves;
        }
    }
}