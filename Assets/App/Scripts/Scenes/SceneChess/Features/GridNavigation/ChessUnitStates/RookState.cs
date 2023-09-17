using System.Collections.Generic;
using UnityEngine;

namespace App.Scripts.Scenes.SceneChess.Features.GridNavigation.ChessUnitStates
{
    public class RookState : IChessUnitState
    {
        public List<Vector2Int> GetPossibleMoves(ChessUnitMoveProvider.ChessUnitData chessUnitData)
        {
            var moves = new List<Vector2Int>();
            var grid = chessUnitData.СhessGrid;
            var chessPosition = chessUnitData.Position;

            for (var i = chessPosition.x + 1; i < grid.Size.x; i++)
            {
                if (chessUnitData.СhessGrid.Get(chessPosition.y, i) != null) break;
                moves.Add(new Vector2Int(i, chessPosition.y));
            }

            for (var i = chessPosition.x - 1; i >= 0; i--)
            {
                if (chessUnitData.СhessGrid.Get(chessPosition.y, i) != null) break;
                moves.Add(new Vector2Int(i, chessPosition.y));
            }

            for (var i = chessPosition.y + 1; i < grid.Size.y; i++)
            {
                if (chessUnitData.СhessGrid.Get(i, chessPosition.x) != null) break;
                moves.Add(new Vector2Int(chessPosition.x, i));
            }

            for (var i = chessPosition.y - 1; i >= 0; i--)
            {
                if (chessUnitData.СhessGrid.Get(i, chessPosition.x) != null) break;
                moves.Add(new Vector2Int(chessPosition.x, i));
            }

            return moves;
        }
    }
}