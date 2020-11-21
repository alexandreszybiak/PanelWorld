using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidablePanel : MonoBehaviour
{
    [SerializeField] public Vector2Int coordinates = new Vector2Int (0, 0);
    SlidingPuzzle slidingPuzzle;

    public void Start() {
        slidingPuzzle = transform.parent.GetComponent<SlidingPuzzle> ();
    }

    public void Hit() {
        Vector2Int emptySlotCoordinates = transform.parent.GetComponent<SlidingPuzzle> ().GetEmptySlotCoordinates ();
        if( emptySlotCoordinates.x != -1 && IsNeighbour (emptySlotCoordinates) ) {
            //transform.localPosition = new Vector3 (emptySlotCoordinates.x * 3, emptySlotCoordinates.y * -3, 0);
            StartCoroutine (Slide (new Vector3 (emptySlotCoordinates.x * slidingPuzzle.panelDimensions.x, emptySlotCoordinates.y * -slidingPuzzle.panelDimensions.y, 0)));

            slidingPuzzle.panelSlots[coordinates.x, coordinates.y] = false;
            slidingPuzzle.panelSlots[emptySlotCoordinates.x, emptySlotCoordinates.y] = true;

            coordinates = emptySlotCoordinates;

            
            
        }
    }

    private bool IsNeighbour(Vector2Int coord) {
        for ( int x = -1; x <= 1; x++ ) {
            for ( int y = -1; y <= 1; y++ ) {
                if ( x == 0 || y == 0 ) {
                    if ( coordinates.x + x == coord.x && coordinates.y + y == coord.y ) {
                        return true;
                    }
                }
            }
        }
        return false;
    }

    IEnumerator Slide(Vector3 targetPosition) {
        float percent = 0;
        float slideSpeed = 6.0f;
        while (percent <= 1 ) {
            percent += Time.deltaTime * slideSpeed;
            transform.localPosition = Vector3.Lerp (transform.localPosition, targetPosition, percent);
            yield return null;
        }
        transform.localPosition = targetPosition;
    }
}
