using com.jsatch.games.collections;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.jsatch.games.pathfinding
{
    public class Dijkstra : PathFinding
    {
        PriorityQueue<GridElement> mFrontier;
        Dictionary<GridElement, int> mCostSoFar;

        public Dijkstra()
        {
            mFrontier = new PriorityQueue<GridElement>();
            mCameFrom = new Dictionary<GridElement, GridElement>();
            mCostSoFar = new Dictionary<GridElement, int>();
        }

        public override List<GridElement> GetPath(GridElement startPosition, GridElement endPosition)
        {
            mCameFrom.Add(startPosition, null);
            mCostSoFar.Add(startPosition, 0);
            mFrontier.Enqueue(startPosition, 0);

            while (mFrontier.Length > 0)
            {
                var currentElement = mFrontier.DequeueBottom();

                if (currentElement == endPosition) break;

                foreach (var pair in currentElement.AdjacencyList)
                {
                    if (pair.Value != null)
                    {
                        int newCost = mCostSoFar[currentElement] + pair.Value.Cost;
                        if (!mCostSoFar.ContainsKey(pair.Value) || newCost < mCostSoFar[pair.Value])
                        {
                            mCostSoFar.Add(pair.Value, newCost);
                            mFrontier.Enqueue(pair.Value, newCost);
                            mCameFrom.Add(pair.Value, currentElement);
                        }
                    }
                }
            }

            PrintCameFrom();

            return CreatePathFromBeginning(endPosition);
        }


    }
}
