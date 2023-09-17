using System.Collections.Generic;
using App.Scripts.Scenes.SceneChess.Features.ChessField.Types;
using UnityEngine;

namespace App.Scripts.Scenes.SceneChess.Features.GridNavigation.ChessUnitStates
{
    public class PonState : IChessUnitState
    {
        public List<Vector2Int> GetPossibleMoves(ChessUnitMoveProvider.ChessUnitData chessUnitData)
        {
            List<Vector2Int> moves = new List<Vector2Int>();
            if (chessUnitData.СhessPieceModel.Color == ChessUnitColor.White)
            {
                var x = chessUnitData.Position.x;
                var y = chessUnitData.Position.y + 1;

                if (y < chessUnitData.СhessGrid.Size.y && chessUnitData.СhessGrid.Get(y, x) == null)
                {
                    moves.Add(new Vector2Int(x, y));
                }
            }
            else
            {
                var x = chessUnitData.Position.x;
                var y = chessUnitData.Position.y - 1;
                if (y >= 0 && chessUnitData.СhessGrid.Get(y, x) == null)
                {
                    moves.Add(new Vector2Int(x, y));
                }
            }

            return moves;
        }
    }
}