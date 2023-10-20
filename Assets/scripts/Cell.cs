using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell 
{
    // coordinate represented by X and y coordinates
    private Vector2 position;
    private bool isFree;

    public bool IsFree 
    {
        get {
            return isFree;
        }  
        set {
            isFree = value;
        }
    }

    public int X {
        get { return (int)position.x; }
    }
    public int Y {
        get {
            return (int)position.y;
        }
    }

    public Cell(int x, int y, bool isFree ) {
        position = new Vector2(x,y);
        IsFree = isFree;
    }
}
