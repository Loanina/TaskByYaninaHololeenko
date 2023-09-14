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

            var color = grid.Get(from).PieceModel.Color;

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

                List<CellMove> possibleMoves = PossibleChessMoves(color, unit, from, grid, currentMove);

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

        private List<CellMove> PossibleChessMoves(ChessUnitColor color, ChessUnitType type, Vector2Int startPosition,
            ChessGrid grid, CellMove previousMove)
        {
            var gridSize = grid.Size;
            var moves = new List<CellMove>();
            int x;
            int y;
            switch (type)
            {
                case ChessUnitType.Pon:
                {
                    if (color == ChessUnitColor.White)
                    {
                        x = startPosition.x;
                        y = startPosition.y + 1;

                        if (y <= gridSize.y && grid.Get(x, y) == null)
                        {
                            moves.Add(new CellMove(new Vector2Int(x, y), previousMove));
                        }
                    }
                    else
                    {
                        x = startPosition.x;
                        y = startPosition.y - 1;
                        if (y > 0 && grid.Get(x, y) == null)
                        {
                            moves.Add(new CellMove(new Vector2Int(x, y), previousMove));
                        }
                    }

                    break;
                }
                case ChessUnitType.King:
                {
                    for (int dx = -1; dx < 2; dx++)
                    {
                        for (int dy = -1; dy < 2; dy++)
                        {
                            x = startPosition.x + dx;
                            y = startPosition.y + dy;
                            if (x <= gridSize.x && y <= gridSize.y &&
                                grid.Get(x, y) == null && dx + dy != 0)
                            {
                                moves.Add(new CellMove(new Vector2Int(x, y), previousMove));
                            }
                        }
                    }

                    break;
                }
            }

            /*
            foreach (var move in moves)
            {
                Debug.Log("("+move.position.x+","+move.position.y+")");
            }
            */
            return moves;
        }
    }
}