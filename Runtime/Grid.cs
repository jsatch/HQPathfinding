using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.jsatch.games.pathfinding
{
    public class Grid
    {
        private int mWidth;
        private int mHeight;
        private List<GridElement> mElements;


        public Grid(int width, int height)
        {
            mWidth = width;
            mHeight = height;
            mElements = new List<GridElement>();

            for (int i = 0; i < mHeight; i++)
            {
                for (int j = 0; j < mWidth; j++)
                {
                    var element = new GridElement(j, i);
                    mElements.Add(element);
                }
            }

            //Adding adjacency. In limits, adding null
            for (int i = 0; i < mHeight; i++)
            {
                for (int j = 0; j < mWidth; j++)
                {
                    GridElement element = GetElement(j, i);
                    element.AdjacencyList.Add(AdjacencyPosition.UP, i < mHeight - 1 ? GetElement(j, i + 1) : null);
                    element.AdjacencyList.Add(AdjacencyPosition.RIGHT, j < mWidth - 1 ? GetElement(j + 1, i) : null);
                    element.AdjacencyList.Add(AdjacencyPosition.DOWN, i > 0 ? GetElement(j, i - 1) : null);
                    element.AdjacencyList.Add(AdjacencyPosition.LEFT, j > 0 ? GetElement(j - 1, i) : null);
                }
            }
        }
        public GridElement GetElement(int x, int y)
        {
            if (mElements != null)
            {
                return mElements[y * mWidth + x];
            }
            return null;
        }

        public GridElement GetElement(Vector3 pos)
        {
            return mElements[(5 + Mathf.FloorToInt(pos.z)) * mWidth + (5 + Mathf.FloorToInt(pos.x))];
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
