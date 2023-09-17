using System.Collections.Generic;
using System.Linq;
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
            public readonly float Cost;
            public readonly CellMove PreviousMove;

            public CellMove(Vector2Int position, float cost)
            {
                Position = position;
                Cost = cost;
                PreviousMove = null;
            }

            public CellMove(Vector2Int position, CellMove previousMove, float cost)
            {
                Position = position;
                Cost = cost;
                PreviousMove = previousMove;
            }
        }

        private CellMove FindMinCostCellMove(List<CellMove> cellMoves)
        {
            var minCostCellMove = cellMoves[0];
            foreach (var cellMove in cellMoves)
            {
                if (cellMove.Cost < minCostCellMove.Cost)
                    minCostCellMove = cellMove;
            }

            return minCostCellMove;
        }

        public List<Vector2Int> FindPath(ChessUnitType unit, Vector2Int from, Vector2Int to, ChessGrid grid)
        {
            if (from == to) return null;

            ChessUnitMoveProvider.ChessUnitData chessUnitData;
            chessUnitData.СhessPieceModel = new ChessPieceModel(unit, grid.Get(from).PieceModel.Color);
            chessUnitData.СhessGrid = grid;
            chessUnitData.Position = from;

            var startCellMove = new CellMove(from, GetCost(from, to));

            List<CellMove> waitingCellMoves = new List<CellMove>();
            waitingCellMoves.AddRange(PossibleChessMoves(startCellMove, chessUnitData, to));

            List<CellMove> checkedCellMoves = new List<CellMove> { startCellMove };

            while (waitingCellMoves.Count > 0)
            {
                var checkCellMove = FindMinCostCellMove(waitingCellMoves);

                if (checkCellMove.Position == to)
                {
                    return CalculateCellMovePath(checkCellMove);
                }

                waitingCellMoves.Remove(checkCellMove);

                if (checkedCellMoves.All(x => x.Position != checkCellMove.Position))
                {
                    checkedCellMoves.Add(checkCellMove);

                    chessUnitData.Position = checkCellMove.Position;
                    waitingCellMoves.AddRange(PossibleChessMoves(checkCellMove, chessUnitData, to));
                }
            }

            return null;
        }

        private List<Vector2Int> CalculateCellMovePath(CellMove cellMove)
        {
            var path = new List<Vector2Int>();
            var currentCellMove = cellMove;

            while (currentCellMove != null)
            {
                path.Add(currentCellMove.Position);
                currentCellMove = currentCellMove.PreviousMove;
            }

            path.Reverse();
            path.RemoveAt(0);

            foreach (var vec in path)
            {
                Debug.Log("(" + vec.x + " , " + vec.y + ")");
            }

            return path;
        }

        private float GetCost(Vector2Int cellPosition, Vector2Int targetPosition)
        {
            return Vector2Int.Distance(cellPosition, targetPosition);
        }

        private List<CellMove> PossibleChessMoves(CellMove previousMove,
            ChessUnitMoveProvider.ChessUnitData chessUnitData, Vector2Int targetPosition)

        {
            List<CellMove> possibleChessMoves = new List<CellMove>();
            var vectorMoves = ChessUnitMoveProvider.GetPossibleChessMoves(chessUnitData);

            if (vectorMoves is null) return possibleChessMoves;

            foreach (var move in vectorMoves)
            {
                possibleChessMoves.Add(new CellMove(move, previousMove,
                    GetCost(move, targetPosition)));
            }

            return possibleChessMoves;
        }
    }
}