using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.jsatch.games.pathfinding
{
    class BreadthFirstSearch : PathFinding
    {
        private Queue<GridElement> mQueue;

        public BreadthFirstSearch()
        {
            mQueue = new Queue<GridElement>();
            mCameFrom = new Dictionary<GridElement, GridElement>();
        }

        public override List<GridElement> GetPath(GridElement startPosition, GridElement endPosition)
        {
            mQueue.Enqueue(startPosition);
            mCameFrom.Add(startPosition, null);

            while (mQueue.Count > 0)
            {
                var currentElement = mQueue.Dequeue();
                foreach (var pair in currentElement.AdjacencyList)
                {
                    if ((pair.Value != null) && !mCameFrom.ContainsKey(pair.Value))
                    {
                        mQueue.Enqueue(pair.Value);
                        mCameFrom[pair.Value] = currentElement;
                    }
                }
            }

            PrintCameFrom();

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
