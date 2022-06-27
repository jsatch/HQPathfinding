using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.jsatch.games.pathfinding
{
    public enum CellSelectionMode
    {
        Start, Obstacle, End
    }

    public enum PathFindingAlgorithm
    {
        BreadthFirstSearch, Dikjstra, AStar
    }
    public abstract class PathFinding
    {
        protected Dictionary<GridElement, GridElement> mCameFrom;

        public abstract List<GridElement> GetPath(GridElement startPosition, GridElement endPosition);

        public void PrintCameFrom()
        {
            foreach (var e in mCameFrom)
            {
                Debug.Log($"[{e.Key}] : {e.Value}");
            }
        }

        protected List<GridElement> CreatePathFromBeginning(GridElement endPosition)
        {
            List<GridElement> resultado = new List<GridElement>();

            GridElement elem = endPosition;
            while (true)
            {
                elem = mCameFrom[elem];
                if (elem == null)
                {
                    break;
                }
                else
                {
                    resultado.Add(elem);
                }

            }

            return resultado;

        }
    }
}
