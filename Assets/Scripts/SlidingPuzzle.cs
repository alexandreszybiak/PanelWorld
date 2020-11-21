using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidingPuzzle : MonoBehaviour
{
    [SerializeField] private Vector2Int dimensions = new Vector2Int(0,0);
    [SerializeField] public Vector2 panelDimensions = new Vector2(3,3);
    public bool[,] panelSlots;

    public void Start() {
        panelSlots = new bool[dimensions.x, dimensions.y];
        SetPanelSlots ();
    }

    public Vector2Int GetEmptySlotCoordinates() {
        for(int x = 0; x < panelSlots.GetLength(0); x++ ) {
            for(int y = 0; y < panelSlots.GetLength(1); y++ ) {
                if(panelSlots[x,y] == false ) {
                    return new Vector2Int (x, y);
                }
            }
        }
        return new Vector2Int(-1,0);
    }

    public void SetPanelSlots() {
        for ( int i = 0; i < transform.childCount; i++ ) {
            SlidablePanel panel = transform.GetChild (i).GetComponent<SlidablePanel> ();
            panelSlots[panel.coordinates.x, panel.coordinates.y] = true;
        }
    }
}
