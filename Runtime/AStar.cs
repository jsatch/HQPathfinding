using com.jsatch.games.collections;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.jsatch.games.pathfinding
{
    public class AStar : PathFinding
    {
        PriorityQueue<GridElement> mFrontier;
        Dictionary<GridElement, int> mCostSoFar;

        public AStar()
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
                        //Debug.Log($"GE:({pair.Value.PosX}, {pair.Value.PosY}) value: {pair.Value.Cost}");
                        //Debug.Log($"{mCostSoFar[currentElement]}");
                        int newCost = mCostSoFar[currentElement] + pair.Value.Cost;
                        if (!mCostSoFar.ContainsKey(pair.Value) || newCost < mCostSoFar[pair.Value])
                        {
                            mCostSoFar.Add(pair.Value, newCost);
                            mFrontier.Enqueue(pair.Value, newCost + Heuristic(endPosition, pair.Value));
                            mCameFrom.Add(pair.Value, currentElement);
                        }
                    }
                }
            }

            PrintCameFrom();

            return CreatePathFromBeginning(endPosition);
        }

        private int Heuristic(GridElement goal, GridElement current)
        {
            return Mathf.Abs(goal.PosX - current.PosX) + Mathf.Abs(goal.PosY - current.PosY);
        }
    }
}
