using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.jsatch.games.pathfinding
{
    public enum AdjacencyPosition
    {
        UP, DOWN, LEFT, RIGHT, UPPER_RIGHT, UPPER_LEFT, LOWER_RIGHT, LOWER_LEFT
    }

    public class GridElement
    {
        public int PosX { get; private set; }
        public int PosY { get; private set; }
        public Dictionary<AdjacencyPosition, GridElement> AdjacencyList { get; set; }
        public int Cost = 1;
        public int BestCost = 0;
        public Vector3 centerPosition { get; private set; }
        public GridElement flowDirection;

        public GridElement(int x, int y, Vector2 cellSize)
        {
            PosX = x;
            PosY = y;
            AdjacencyList = new Dictionary<AdjacencyPosition, GridElement>();
            Cost = 1;

            centerPosition = new Vector3(
                x * cellSize.x + (cellSize.x / 2f),
                0f,
                y * cellSize.y + (cellSize.y / 2f)
            );
        }

        public GridElement(int x, int y, int cost, Vector2 cellSize)
        {
            PosX = x;
            PosY = y;
            AdjacencyList = new Dictionary<AdjacencyPosition, GridElement>();
            Cost = cost;

            centerPosition = new Vector3(
                x * cellSize.x + cellSize.x / 2f,
                0f,
                y * cellSize.y + cellSize.y / 2f
            );
        }


        override public string ToString()
        {
            return $"{PosX}, {PosY}";
        }
    }

    [System.Serializable]
    public class GridElementCost
    {
        public LayerMask layer;
        public int cost;
    }
}
