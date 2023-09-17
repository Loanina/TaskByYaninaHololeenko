using System;
using System.Collections.Generic;
using App.Scripts.Scenes.SceneChess.Features.ChessField.GridMatrix;
using App.Scripts.Scenes.SceneChess.Features.ChessField.Piece;
using App.Scripts.Scenes.SceneChess.Features.ChessField.Types;
using App.Scripts.Scenes.SceneChess.Features.GridNavigation.Navigator;
using UnityEngine;

namespace App.Scripts.Scenes.SceneChess.Features.GridNavigation
{
    public class ChessUnitMoveProvider
    {
        public struct ChessUnitData
        {
            public ChessPieceModel СhessPieceModel;
            public ChessGrid СhessGrid;
            public Vector2Int Position;
            //доделать сруктуру для получения данных
        }

        public static List<Vector2Int> GetPossibleChessMoves(ChessUnitData chessUnitData)
        {
            ChessUnitContext chessUnitContext;
            var pieceType = chessUnitData.СhessPieceModel.PieceType;
            chessUnitContext = pieceType switch
            {
                ChessUnitType.Pon => new ChessUnitContext(new PonState()),
                ChessUnitType.King => new ChessUnitContext(new KingState()),
                _ => null
            };

            return chessUnitContext?.GetPossibleMoves(chessUnitData); //допустимо null что делать
        }

        public interface IChessUnitState
        {
            List<Vector2Int> GetPossibleMoves(ChessUnitData chessUnitData);
        }


        public class PonState : IChessUnitState
        {
            public List<Vector2Int> GetPossibleMoves(ChessUnitData chessUnitData)
            {
                List<Vector2Int> moves = new List<Vector2Int>();
                if (chessUnitData.СhessPieceModel.Color == ChessUnitColor.White)
                {
                    var x = chessUnitData.Position.x;
                    var y = chessUnitData.Position.y + 1;

                    if (y < chessUnitData.СhessGrid.Size.y && chessUnitData.СhessGrid.Get(x, y) == null)
                    {
                        moves.Add(new Vector2Int(x, y));
                    }
                }
                else
                {
                    var x = chessUnitData.Position.x;
                    var y = chessUnitData.Position.y - 1;
                    if (y >= 0 && chessUnitData.СhessGrid.Get(x, y) == null)
                    {
                        moves.Add(new Vector2Int(x, y));
                    }
                }

                return moves;
            }
        }

        public class KingState : IChessUnitState
        {
            public List<Vector2Int> GetPossibleMoves(ChessUnitData chessUnitData)
            {
                List<Vector2Int> moves = new List<Vector2Int>();
                var grid = chessUnitData.СhessGrid;
                for (int dx = -1; dx < 2; dx++)
                {
                    for (int dy = -1; dy < 2; dy++)
                    {
                        if (dx != 0 && dy != 0)
                        {
                            var x = chessUnitData.Position.x + dx;
                            var y = chessUnitData.Position.y + dy;
                            if (x < grid.Size.x && y < grid.Size.y && x >= 0 && y >= 0 &&
                                chessUnitData.СhessGrid.Get(x, y) == null)
                            {
                                moves.Add(new Vector2Int(x, y));
                            }
                        }
                    }
                }
                return moves;
            }
        }

        public class ChessUnitContext
        {
            private IChessUnitState _state;

            public ChessUnitContext(IChessUnitState initState)
            {
                this._state = initState;
            }

            public void SetState(IChessUnitState newState)
            {
                this._state = newState;
            }

            public List<Vector2Int> GetPossibleMoves(ChessUnitData chessUnitData)
            {
                return this._state.GetPossibleMoves(chessUnitData);
            }
        }
    }
}