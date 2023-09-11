using System;
using System.Collections.Generic;
using App.Scripts.Scenes.SceneChess.Features.ChessField.GridMatrix;
using App.Scripts.Scenes.SceneChess.Features.ChessField.Types;
using UnityEngine;

namespace App.Scripts.Scenes.SceneChess.Features.GridNavigation.Navigator
{
    public class ChessGridNavigator : IChessGridNavigator
    {
        private class CellMove
        {
            public Vector2Int position;
            public CellMove previousMove;

            public CellMove(Vector2Int position)
            {
                this.position = this.position;
                this.previousMove = null;
            }

            public CellMove(Vector2Int position, CellMove previousMove)
            {
                this.position = position;
                this.previousMove = previousMove;
            }
        }
        public List<Vector2Int> FindPath(ChessUnitType unit, Vector2Int from, Vector2Int to, ChessGrid grid)
        {
            //напиши реализацию не меняя сигнатуру функции
            
            var color = grid.Get(from).PieceModel.Color;
            
            Queue<CellMove> movesQueue = new Queue<CellMove>();
            movesQueue.Enqueue(new CellMove(from));

            List<CellMove> visitedCells = new List<CellMove>();
            visitedCells.Add(new CellMove(from));

            while (movesQueue.Count>0)
            {
                CellMove currentMove = movesQueue.Dequeue();

                if (currentMove.position == to)
                {
                    List<Vector2Int> path = new List<Vector2Int>();
                    while (currentMove != null)
                    {
                        path.Add(currentMove.position);
                        currentMove = currentMove.previousMove;
                    }
                    path.Reverse();
                    return path;
                }

                List<CellMove> possibleMoves = PossibleChessMoves(color, unit, from, grid.Size, currentMove);

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

        private List<CellMove> PossibleChessMoves(ChessUnitColor color,ChessUnitType type, Vector2Int startPosition, Vector2Int gridSize, CellMove previousMove)
        {
            var moves = new List<CellMove>();
            switch (type)
            {
                case ChessUnitType.Pon:
                {
                    if (color == ChessUnitColor.White)
                    {
                        if (startPosition.y + 1 <= gridSize.y) 
                            moves.Add(new CellMove(new Vector2Int(startPosition.x, startPosition.y+1), previousMove));
                    }
                    else
                    {
                        if (startPosition.y - 1 > 0)
                        {
                            moves.Add(new CellMove(new Vector2Int(startPosition.x, startPosition.y-1), previousMove));
                        }
                    }
                    break;
                }
            }

            return moves;
        }
    }
}