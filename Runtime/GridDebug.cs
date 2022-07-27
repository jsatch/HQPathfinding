using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace com.jsatch.games.pathfinding
{
    public enum CostTypes
    {
        Initial, Integrated
    }

    public class GridDebug : MonoBehaviour
    {
        public static GridDebug Instance { get; private set; }

        [SerializeField]
        private CostTypes costToShown;

        private com.jsatch.games.pathfinding.Grid mGrid;

        private void Awake()
        {
            Instance = this;
        }

        private void OnDrawGizmos()
        {
            if (mGrid != null)
            {
                DrawGrid(Color.blue);
            }
        }

        public void SetGrid(com.jsatch.games.pathfinding.Grid grid)
        {
            mGrid = grid;
        }

        private void DrawGrid(Color color)
        {
            Gizmos.color = color;
            Vector3 size = new Vector3(mGrid.CellSize.x, 0f, mGrid.CellSize.y);
            //Vector3 size = Vector3.one * mGrid.cellSize.x ;

            for (int i = 0; i < mGrid.Height; i++)
            {
                for(int j = 0; j < mGrid.Width; j++)
                {
                    GridElement elem = mGrid.GetElement(j, i);
                    Vector3 centerPos = elem.centerPosition;
                    centerPos.y = 0.2f;
                    Gizmos.DrawWireCube(centerPos, size);
                    if (costToShown == CostTypes.Initial)
                    {
                        Handles.Label(
                            centerPos - Vector3.right * mGrid.CellSize.x / 2f, 
                            elem.Cost.ToString());
                    }else
                    {
                        Handles.Label(
                            centerPos - Vector3.right * mGrid.CellSize.x / 2f,
                            elem.BestCost.ToString());
                    }
                }
            }
        }

        private void DrawGridImp(Color color)
        {
            Gizmos.color = color;
            Vector3 size = new Vector3(mGrid.CellSize.x, 0f, mGrid.CellSize.y);
            //Vector3 size = Vector3.one * mGrid.cellSize.x ;

            for (int i = 0; i < mGrid.Height; i++)
            {
                for (int j = 0; j < mGrid.Width; j++)
                {
                    Vector3 centerPos = new Vector3(
                        j * size.x + (size.x / 2f),
                        0.2f,
                        i * size.z + (size.z / 2f)
                    );
                    //Debug.Log(centerPos);
                    Gizmos.DrawWireCube(centerPos, size);
                }
            }
        }
    }

}
