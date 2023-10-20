using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour, ICoordinate
{
    public int score = 1;

    public int X { get { return (int)transform.position.x; } }
    public int Y { get { return (int)transform.position.y; } }
  

    public void Setup() {

    }
    
    public void OnCollect(SnakeController snake) {
        // 1. give player score 
        GameManager.Instance.AddScore(score);

        // 2. notifiy gameboard the apple has been collected.
        GameManager.Instance.GameBoard.NotifyItemCollected(this);

        // 3. destroy the collectable
        Destroy(gameObject);
    }
}
