using com.jsatch.games.collections;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.jsatch.games.pathfinding
{
    public class Grid
    {
        public Vector2 CellSize { get; private set; }

        public int Width { get; private set; }
        public int Height { get; private set; }
        private List<GridElement> mElements;


        public Grid(int width, int height, Vector2 cellSize)
        {
            this.Width = width;
            this.Height = height;
            mElements = new List<GridElement>();
            this.CellSize = cellSize;

            for (int i = 0; i < this.Height; i++)
            {
                for (int j = 0; j < this.Width; j++)
                {
                    var element = new GridElement(j, i, cellSize);
                    mElements.Add(element);
                }
            }

            //Adding adjacency. In limits, adding null
            for (int i = 0; i < this.Height; i++)
            {
                for (int j = 0; j < this.Width; j++)
                {
                    GridElement element = GetElement(j, i);
                    element.AdjacencyList.Add(AdjacencyPosition.UP, i < this.Height - 1 ? GetElement(j, i + 1) : null);
                    element.AdjacencyList.Add(AdjacencyPosition.RIGHT, j < this.Width - 1 ? GetElement(j + 1, i) : null);
                    element.AdjacencyList.Add(AdjacencyPosition.DOWN, i > 0 ? GetElement(j, i - 1) : null);
                    element.AdjacencyList.Add(AdjacencyPosition.LEFT, j > 0 ? GetElement(j - 1, i) : null);

                    element.AdjacencyList.Add(AdjacencyPosition.UPPER_LEFT, i < this.Height - 1  && j > 0 ? GetElement(j - 1, i + 1) : null);
                    element.AdjacencyList.Add(AdjacencyPosition.UPPER_RIGHT, i < this.Height - 1 && j < this.Width - 1 ? GetElement(j + 1, i + 1) : null);
                    element.AdjacencyList.Add(AdjacencyPosition.LOWER_LEFT, i > 0 && j > 0 ? GetElement(j - 1, i - 1) : null);
                    element.AdjacencyList.Add(AdjacencyPosition.LOWER_RIGHT, i > 0 && j < this.Width - 1 ? GetElement(j + 1, i - 1) : null);
                }
            }
        }
        public GridElement GetElement(int x, int y)
        {
            if (mElements != null)
            {
                return mElements[y * Width + x];
            }
            return null;
        }

        public GridElement GetElement(Vector3 pos)
        {
            return mElements[
                (Mathf.FloorToInt(pos.z) / (int)CellSize.y) * Width + (Mathf.FloorToInt(pos.x) / (int)CellSize.y)
            ];
        }

        public void CalculateCost(List<GridElementCost> layers)
        {
            Vector3 colliderExtends = new Vector3(CellSize.x / 2f,  CellSize.x / 2f, CellSize.y / 2f);
            foreach (GridElement element in mElements)
            {
                bool isThereSomething = false;
                foreach (var layer in layers)
                {
                    Collider[] colliders = Physics.OverlapBox(element.centerPosition,
                        colliderExtends, Quaternion.identity, layer.layer);
                    if (colliders.Length > 0)
                    {
                        element.Cost += layer.cost;
                        isThereSomething = true;
                    }
                }
                if (!isThereSomething) element.Cost = 1;
                    
            }
        }

        public void CalculateIntegratedCost(GridElement destination)
        {
            PriorityQueue<GridElement> mFrontier = new PriorityQueue<GridElement>();
            Dictionary<GridElement, int> mCostSoFar = new Dictionary<GridElement, int>();

            destination.Cost = 0;
            destination.BestCost = 0;
            mCostSoFar.Add(destination, 0);
            mFrontier.Enqueue(destination, 0);

            while (mFrontier.Length > 0)
            {
                var currentElement = mFrontier.DequeueBottom();

                foreach (var pair in currentElement.AdjacencyList)
                {
                    if (pair.Value != null)
                    {
                        //if (pair.Value.Cost > 1000) continue; // unaccesible zones

                        int newCost = mCostSoFar[currentElement] + pair.Value.Cost;
                        
                        if (!mCostSoFar.ContainsKey(pair.Value) || newCost < mCostSoFar[pair.Value])
                        {
                            pair.Value.BestCost = newCost;
                            mCostSoFar.Add(pair.Value, newCost);
                            mFrontier.Enqueue(pair.Value, newCost);
                        }
                    }
                }
            }

        }

        public void CalculateFlowDirections()
        {
            foreach(var elem in mElements)
            {
                GridElement flowElement = null;
                int bestCost = int.MaxValue;
                foreach (var neighbor in elem.AdjacencyList)
                {
                    if (neighbor.Value != null && neighbor.Value.BestCost < bestCost)
                    {
                        flowElement = neighbor.Value;
                        bestCost = neighbor.Value.BestCost;
                    }
                }
                elem.flowDirection = flowElement;
            }
        }

        public void Print()
        {
            foreach (var element in mElements)
            {
                Debug.Log($"{ element } : [({element.AdjacencyList[AdjacencyPosition.UP]}), ({element.AdjacencyList[AdjacencyPosition.DOWN]}), ({element.AdjacencyList[AdjacencyPosition.LEFT]}), ({element.AdjacencyList[AdjacencyPosition.RIGHT]}) ]");
            }
        }
    }
}
