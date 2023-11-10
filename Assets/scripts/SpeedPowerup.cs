using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPowerup : Collectable
{
    public float speedIncrease = 1f;


    public override bool ShouldCollectCreateNew {
        get {return false;}

    }

    public override void OnCollect(SnakeController snake)
    {
        // speed up the snake
        snake.SpeedUp(speedIncrease);

        // call collectable'd functionality
        base.OnCollect(snake);
    }
}
