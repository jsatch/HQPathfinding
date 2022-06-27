using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.jsatch.games.pathfinding
{
    public enum AdjacencyPosition
    {
        UP, DOWN, LEFT, RIGHT
    }

    public class GridElement
    {
        public int PosX { get; private set; }
        public int PosY { get; private set; }
        public Dictionary<AdjacencyPosition, GridElement> AdjacencyList { get; set; }
        public int Cost;

        public GridElement(int x, int y)
        {
            PosX = x;
            PosY = y;
            AdjacencyList = new Dictionary<AdjacencyPosition, GridElement>();
            Cost = 0;
        }

        public GridElement(int x, int y, int cost)
        {
            PosX = x;
            PosY = y;
            AdjacencyList = new Dictionary<AdjacencyPosition, GridElement>();
            Cost = cost;
        }


        override public string ToString()
        {
            return $"{PosX}, {PosY}";
        }
    }
}
