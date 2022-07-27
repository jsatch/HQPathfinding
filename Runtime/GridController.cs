using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.jsatch.games.pathfinding
{
    public class GridController : MonoBehaviour
    {
        public Vector2Int gridSize;
        public Vector2 cellSize;
        public List<GridElementCost> layers = new List<GridElementCost>();

        public Transform subject;
        public float speed = 1f;

        public static GridController Instance { get; private set; }

        private Grid mGrid;

        private bool mMove = false;
        private GridElement mNextDestination;
        private GridElement mDestinationElem;

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            mGrid = new Grid(gridSize.x, gridSize.y, cellSize);
            mGrid.CalculateCost(layers);
            mGrid.CalculateIntegratedCost(mGrid.GetElement(0,0));
            mGrid.CalculateFlowDirections();

            GridDebug.Instance.SetGrid(mGrid);

        }

        public void SelectDestination(Vector3 destination)
        {
            mDestinationElem = mGrid.GetElement(destination);

            if (mDestinationElem != null)
            {
                mGrid.CalculateCost(layers);
                mGrid.CalculateIntegratedCost(mDestinationElem);
                mGrid.CalculateFlowDirections();

                mNextDestination = mGrid.GetElement(subject.position).flowDirection;
                mMove = true;
            }
        }

        private void Update()
        {
            if (mMove)
            {
                if (Vector3.Distance(subject.position,mNextDestination.centerPosition) < 0.1)
                {
                    mNextDestination = mNextDestination.flowDirection;
                }
                Vector3 direction = mNextDestination.centerPosition - subject.position;
                subject.Translate(speed * Time.deltaTime * direction.normalized);

                if (Vector3.Distance(subject.position, mDestinationElem.centerPosition) < 0.1)
                {
                    mMove = false;
                }
            }

            
        }
    }

}
