using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.Mathematics;
using UnityEngine;

public class SnakeController : MonoBehaviour , ICoordinate
{
    public enum Direction {
        None = 0,
        Up,
        Down,
        Left,
        Right
    };
 

    [Tooltip("the inital speed of the snake"), Range(0.1f,10.0f)]
    public float speed = 1.0f;
    [Tooltip("the inital length of the snake"),Range(2,6)]
    public int length = 1;

    public GameObject headPrefab;
    public GameObject bodyPrefab;
    public GameObject tailPrefab;

    //public Board gameBoard; moved to GameManager

    private GameObject head;
    private GameObject tail;
    private List<GameObject> body = new List<GameObject>();
    private Timer moveTimer;
    private bool isInitialised = false;

    // the direction user wants to move the snake next
    private Direction inputDirection = Direction.None;
    // the direction the snake is currently moving
    private Direction movementDirection = Direction.Up;

    public int Length {
        get {
            // Length is body count + head + tail
            return body.Count + 2;
        }
    }
    public int X { get { return (int)head.transform.position.x; } }
    public int Y { get { return (int)head.transform.position.y; } }

    #region Unity messages

    private void Awake() {
        moveTimer = gameObject.AddComponent<Timer>();
    }

    // Start is called before the first frame update
   

    // Update is called once per frame
    void Update() {
        // if the object is not initalised yet we cant execute update.
        if (!isInitialised) { return; }


        Vector2 input = ReadInput();
        InterpretInput(input);
        //TestingMovingTheSnake(input);
        if (moveTimer.IsCompleted) {
            //Debug.Log("move");
            this.movementDirection = GetMovementDirection(this.inputDirection);
            MovementDirection(this.movementDirection);
            this.inputDirection = Direction.None;
            SetTimer();

            //collisions checks
            // did we hit a collectable
            // did we hit an obstacle
            GameManager.Instance.GameBoard.CheckCollision(this);

        }
    }

    private void MovementDirection(Direction movementDirection) {
        // move the snake starting from the tail one body part at a time
        int parentIndex = body.Count - 1;
        GameObject currentBodyPart = body[parentIndex];
        // release the tails cell
        GameManager.Instance.GameBoard.ReleaseCell((int)tail.transform.position.x, (int)tail.transform.position.y);
        // move the tail to the last body parts location and rotation.
        tail.transform.position = currentBodyPart.transform.position;
        tail.transform.rotation = currentBodyPart.transform.rotation;

        // move the other body parts
        for(int i = parentIndex; i >= 0; i--) {
            parentIndex--;
            if(parentIndex >= 0) {
                body[i].transform.position = body[parentIndex].transform.position;
                body[i].transform.rotation = body[parentIndex].transform.rotation;

            } else {
                body[i].transform.position = head.transform.position;
                body[i].transform.rotation = head.transform.rotation;
            }
        }
        MoveHead(movementDirection);

    }

    private void MoveHead(Direction movementDirection) {
        int dx = 0; // move head along the x axis
        int dy = 0; // move head along the y Axis
        int rotationZ = 0; // to what direction we roatate head.

        switch (movementDirection) {
            case Direction.Up: 
                dy = 1;
                rotationZ = 0;
                break;
            case Direction.Down:
                dy = -1;
                rotationZ = 180;
                break;
            case Direction.Left:
                dx = -1;
                rotationZ = 90;
                break;
            case Direction.Right:
                dx = 1;
                rotationZ = -90;
                break;
        }
        Vector3 movement = new Vector3(dx, dy, 0);
        head.transform.position += movement;
        GameManager.Instance.GameBoard.ReserveCell((int)head.transform.position.x, (int)head.transform.position.y);

        // Setting the head rotation converting Euler to Quaternion
        head.transform.rotation = Quaternion.Euler(0, 0, rotationZ);
    }

    private Direction GetMovementDirection(Direction inputDirection) {
        if (inputDirection == this.movementDirection 
            || inputDirection == Direction.None 
            || GetOpposite(inputDirection) == this.movementDirection) { 
            return this.movementDirection;  
        }
        return inputDirection;
    }

    private Direction GetOpposite(Direction direction) {
        switch (direction) {
            case Direction.Up: return Direction.Down;
            case Direction.Down: return Direction.Up;
            case Direction.Left: return Direction.Right;
            case Direction.Right: return Direction.Left;
            default: return Direction.None;           
        }
    }

    private void InterpretInput(Vector2 input) {
          if (this.inputDirection == Direction.None) {
            // user can give an input
            if (input.y > 0) {
                this.inputDirection = Direction.Up;
            } else if (input.y < 0) {
                this.inputDirection = Direction.Down;
            } else if (input.x > 0) {
                this.inputDirection= Direction.Right;
            } else if(input.x < 0) {
                this.inputDirection= Direction.Left;
            }
        }
    }

    #endregion

    private void SetTimer() {
        // speed = distance/time  => time = distance / speed.
        float time = 1 / speed; 
        moveTimer.Set(time);
        moveTimer.Run();
    }

    public void Setup() {
        Vector3 position = Vector3.zero;
        this.head = InstantiatePrefab(this.headPrefab, position, Quaternion.identity, transform);
        //gameBoard.ReserveCell((int)head.transform.position.x, (int)head.transform.position.y);

        while(Length < this.length) {
            position.y -= 1;
            GameObject bodyObject = InstantiatePrefab(this.bodyPrefab, position, Quaternion.identity, transform);
            this.body.Add(bodyObject);
        }
        position.y -= 1;
        this.tail = InstantiatePrefab(this.tailPrefab, position, Quaternion.identity, transform);
        SetTimer();
        
        isInitialised = true;   
    }

    private GameObject InstantiatePrefab (GameObject prefab, Vector3 position, Quaternion rotation, Transform parent) {
        GameObject obj = Instantiate(prefab, position, rotation, parent);
        if (!GameManager.Instance.GameBoard.ReserveCell((int)obj.transform.position.x, (int)obj.transform.position.y)) {
            Debug.LogError($"cant reserve cell for object {obj}");
        }
        return obj;

    }


    private void TestingMovingTheSnake(Vector2 input) {
        // old smooth movement code
        // snakes current position in the game world 
        Vector3 movement = new Vector3(input.x, input.y, 0) * speed * Time.deltaTime;
        transform.position += movement;
    }

    private static Vector2 ReadInput() {
        // Read input from player
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        return new Vector2 (horizontal, vertical);
    }
}
