using System.Collections.Generic;
using App.Scripts.Scenes.SceneChess.Features.ChessField.GridMatrix;
using App.Scripts.Scenes.SceneChess.Features.ChessField.Piece;
using App.Scripts.Scenes.SceneChess.Features.ChessField.Types;
using App.Scripts.Scenes.SceneChess.Features.GridNavigation.ChessUnitStates;
using UnityEngine;

namespace App.Scripts.Scenes.SceneChess.Features.GridNavigation
{
    public abstract class ChessUnitMoveProvider
    {
        public struct ChessUnitData
        {
            public ChessPieceModel СhessPieceModel;
            public ChessGrid СhessGrid;
            public Vector2Int Position;
        }

        public static List<Vector2Int> GetPossibleChessMoves(ChessUnitData chessUnitData)
        {
            var pieceType = chessUnitData.СhessPieceModel.PieceType;
            var chessUnitContext = pieceType switch
            {
                ChessUnitType.Pon => new ChessUnitContext(new PonState()),
                ChessUnitType.King => new ChessUnitContext(new KingState()),
                ChessUnitType.Rook => new ChessUnitContext(new RookState()),
                ChessUnitType.Knight => new ChessUnitContext(new KnightState()),
                ChessUnitType.Bishop => new ChessUnitContext(new BishopState()),
                ChessUnitType.Queen => new ChessUnitContext(new QueenState()),
                _ => null
            };

            return chessUnitContext?.GetPossibleMoves(chessUnitData);
        }

        private class ChessUnitContext
        {
            private readonly IChessUnitState _state;

            public ChessUnitContext(IChessUnitState initState)
            {
                this._state = initState;
            }

            public List<Vector2Int> GetPossibleMoves(ChessUnitData chessUnitData)
            {
                return this._state.GetPossibleMoves(chessUnitData);
            }
        }
    }
}