using System.Collections.Generic;
using App.Scripts.Scenes.SceneChess.Features.ChessField.GridMatrix;
using App.Scripts.Scenes.SceneChess.Features.ChessField.Piece;
using App.Scripts.Scenes.SceneChess.Features.ChessField.Types;
using UnityEngine;

namespace App.Scripts.Scenes.SceneChess.Features.GridNavigation.Navigator
{
    public class ChessGridNavigator : IChessGridNavigator
    {
        public class CellMove
        {
            public Vector2Int Position;
            public CellMove PreviousMove;

            public CellMove(Vector2Int position)
            {
                Position = position;
                PreviousMove = null;
            }

            public CellMove(Vector2Int position, CellMove previousMove)
            {
                Position = position;
                PreviousMove = previousMove;
            }
        }

        public List<Vector2Int> FindPath(ChessUnitType unit, Vector2Int from, Vector2Int to, ChessGrid grid)
        {
            //напиши реализацию не меняя сигнатуру функции

            ChessUnitMoveProvider.ChessUnitData chessUnitData;
            chessUnitData.СhessPieceModel = new ChessPieceModel(unit, grid.Get(from).PieceModel.Color);
            chessUnitData.СhessGrid = grid; //

            Queue<CellMove> movesQueue = new Queue<CellMove>();
            movesQueue.Enqueue(new CellMove(from));

            List<CellMove> visitedCells = new List<CellMove>();
            visitedCells.Add(new CellMove(from));

            while (movesQueue.Count > 0)
            {
                CellMove currentMove = movesQueue.Dequeue();

                if (currentMove.Position == to)
                {
                    List<Vector2Int> path = new List<Vector2Int>();
                    while (currentMove != null)
                    {
                        path.Add(currentMove.Position);
                        currentMove = currentMove.PreviousMove;
                    }

                    path.Reverse();
                    path.RemoveAt(0); // изменить?
                    foreach (var vec in path)
                    {
                        Debug.Log(vec.x + "," + vec.y);
                    }

                    return path;
                }

                // List<CellMove> possibleMoves = PossibleChessMoves(color, unit, from, grid, currentMove);
                chessUnitData.Position = currentMove.Position;
                List<CellMove> possibleMoves = PossibleChessMoves(currentMove, chessUnitData);

                foreach (var move in possibleMoves)
                {
                    if (!visitedCells.Contains(move))
                    {
                        movesQueue.Enqueue(move);
                        visitedCells.Add(move);
                    }
                }
            }

            return new List<Vector2Int>();
        }

        private List<CellMove> PossibleChessMoves(CellMove previousMove,
            ChessUnitMoveProvider.ChessUnitData chessUnitData)
        {
            List<CellMove> possibleChessMoves = new List<CellMove>();
            var vectorMoves = new ChessUnitMoveProvider().GetPossibleChessMoves(chessUnitData);
            
            if (vectorMoves is null) return null;
            
            foreach (var move in vectorMoves)
            {
                possibleChessMoves.Add(new CellMove(move, previousMove));
            }

            return possibleChessMoves;
        }
    }
}