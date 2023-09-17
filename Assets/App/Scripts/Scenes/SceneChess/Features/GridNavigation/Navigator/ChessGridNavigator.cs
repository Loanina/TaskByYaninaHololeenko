using System;
using System.Collections.Generic;
using System.Linq;
using App.Scripts.Scenes.SceneChess.Features.ChessField.GridMatrix;
using App.Scripts.Scenes.SceneChess.Features.ChessField.Piece;
using App.Scripts.Scenes.SceneChess.Features.ChessField.Types;
using Unity.Mathematics;
using UnityEngine;

namespace App.Scripts.Scenes.SceneChess.Features.GridNavigation.Navigator
{
    public class ChessGridNavigator : IChessGridNavigator
    {
        public class CellMove
        {
            public Vector2Int Position;
            public int Cost;
            public CellMove PreviousMove;

            public CellMove(Vector2Int position, int cost)
            {
                Position = position;
                Cost = cost;
                PreviousMove = null;
            }

            public CellMove(Vector2Int position, CellMove previousMove, int cost)
            {
                Position = position;
                Cost = cost;
                PreviousMove = previousMove;
            }
        }

        public List<Vector2Int> FindPath(ChessUnitType unit, Vector2Int from, Vector2Int to, ChessGrid grid)
        {
            //напиши реализацию не меняя сигнатуру функции
            
            if (from == to) return null;

            ChessUnitMoveProvider.ChessUnitData chessUnitData;
            chessUnitData.СhessPieceModel = new ChessPieceModel(unit, grid.Get(from).PieceModel.Color);
            chessUnitData.СhessGrid = grid;
            chessUnitData.Position = from;

            var startCellMove = new CellMove(from, GetCost(from, to, from));

            List<CellMove> waitingCellMoves = new List<CellMove>();
            waitingCellMoves.AddRange(PossibleChessMoves(startCellMove, chessUnitData, to));

            List<CellMove> checkedCellMoves = new List<CellMove>();
            checkedCellMoves.Add(startCellMove);

            while (waitingCellMoves.Count > 0)
            {
                var checkCellMove = waitingCellMoves.Where(x => x.Cost == waitingCellMoves.Min(y => y.Cost)).FirstOrDefault();

                if (checkCellMove.Position == to)
                {
                    return CalculateCellMovePath(checkCellMove);
                }

                waitingCellMoves.Remove(checkCellMove);
                
                if (!checkedCellMoves.Where(x => x.Position == checkCellMove.Position).Any())
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
                Debug.Log("("+vec.x+" , "+vec.y+")");
            }
            
            return path;
        }

        private int GetCost(Vector2Int cellPosition, Vector2Int targetPosition, Vector2Int previousPosition)
        {
            var previousDistance = previousPosition - cellPosition;
            var targetDistance = targetPosition - cellPosition;
            return previousDistance.x + previousDistance.y + targetDistance.x + targetDistance.y;
        }

        private List<CellMove> PossibleChessMoves(CellMove previousMove,
            ChessUnitMoveProvider.ChessUnitData chessUnitData, Vector2Int targetPosition)

        {
            List<CellMove> possibleChessMoves = new List<CellMove>();
            var vectorMoves = ChessUnitMoveProvider.GetPossibleChessMoves(chessUnitData);

            if (vectorMoves is null) return null;

            foreach (var move in vectorMoves)
            {
                possibleChessMoves.Add(new CellMove(move, previousMove,
                    GetCost(move, targetPosition, previousMove.Position)));
            }

            return possibleChessMoves;
        }
    }
}