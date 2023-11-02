
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UIElements;

public class Board : MonoBehaviour
{
    private List<Cell> grid = new List<Cell>();
    private List<Cell> freeCells = new List<Cell>();

    public Collectable applePrefab;

    private Renderer backgroundRenderer;
    private int minX = 0;
    private int maxX = 0;
    private int minY = 0;
    private int maxY = 0;



    private Collectable apple; 

    #region Unity messages

    // Start is called before the first frame update
    

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Reset() {

        InitialiseGrid();
    }

    private void OnDrawGizmos() {
       
        foreach (Cell cell in grid) {
            if (cell.IsFree) {
                Gizmos.color = Color.green;
            } else {
                Gizmos.color = Color.red;
            }
            Gizmos.DrawWireCube(new Vector3(cell.X, cell.Y), new Vector3(0.95f, 0.95f, 0.95f));
        }
    }
    #endregion

    public void Setup() {
       
        InitialiseGrid();

    }

    public void CreateApple() {
        Cell cell = GetRandomFreeCell();

        Vector3 position = new Vector3(cell.X, cell.Y, 0);
        apple = Instantiate(applePrefab,position,Quaternion.identity,transform);
        apple.Setup();
        ReserveCell(cell);
    }

    public void CheckCollision(SnakeController snake) {
        // did the snake hit an apple
        if (snake.X == apple.X && snake.Y == apple.Y) {
            // snake collided with the apple
            apple.OnCollect(snake);
        }

    }

    public bool IsOutOfBounds(ICoordinate coordinate) {
        return (coordinate.X < minX 
            || coordinate.Y < minY 
            || coordinate.X >= maxX 
            || coordinate.Y >= maxY);
        
    }

    #region Grid Functionality
    private void InitialiseGrid() {
        backgroundRenderer = GetComponent<Renderer>();
        if (backgroundRenderer == null ) {
            Debug.LogError("background renderer can not be found");
        }
        Debug.Log("initialise Grid");
        // clear old grid cells in the editor on reset
        grid.Clear();

        maxX = Mathf.CeilToInt( backgroundRenderer.bounds.max.x);
        minX = Mathf.CeilToInt( backgroundRenderer.bounds.min.x + 0.5f);
        maxY = Mathf.CeilToInt( backgroundRenderer.bounds.max.y);
        minY = Mathf.CeilToInt( backgroundRenderer.bounds.min.y + 0.5f);

        for (int y = minY; y < maxY; y++) {
            for (int x = minX; x < maxX; x++) {
                Cell currentCell = new Cell(x, y, true);
                grid.Add(currentCell);
                freeCells.Add(currentCell);
            }
        }
    }
    public Cell GetRandomFreeCell() {
        int randomIndex = Random.Range(0, freeCells.Count);
        return freeCells[randomIndex];
    }

    /// <summary>
    /// Reseerves a cell from the grid
    /// </summary>
    /// <param name="x">the x-coordinate</param>
    /// <param name="y">the y-coordinate </param>
    /// <returns>returns true if the cell is valid and avalable</returns>
    public bool ReserveCell(int x, int y) {
        Cell cell = GetCell(x,y);
        return ReserveCell(cell);
       
    }
    /// <summary>
    /// Reseerves a cell from the grid
    /// </summary>
    /// <param name="cell">the cell that should be reserved</param>
    /// <returns>returns true if the cell is valid and avalable</returns>
    public bool ReserveCell(Cell cell) {
        if (cell == null || !cell.IsFree) {
            return false;
        }
        // TODO: Reserve the node for a particular object, dont just mark it as reserved.
        cell.IsFree = false;
        freeCells.Remove(cell);
        return true;
    }

    public bool ReleaseCell(int x, int y) {
        Cell cell = GetCell(x, y);
        return ReleaseCell(cell);
    }

    public bool ReleaseCell(Cell cell) {
        if (cell == null || cell.IsFree) {
            return false;
        }
        // TODO: Reserve the node for a particular object, dont just mark it as reserved.
        cell.IsFree = true;
        freeCells.Add(cell);
        return true;
    }

    private Cell GetCell(int x, int y) {
        if (y < minY || y > maxY || x < minX || x > maxX) {
            // the coordinate is outside of the game board
            // return null indicates that.
        return null;}
        foreach (Cell cell in grid) {
            if (cell.X == x && cell.Y == y) {
                return cell;
            }
        }

        Debug.LogError($"could not find a cell for x :{x}  y:{y} this is a bug!");
        return null;
    }
    #endregion
   
    public void NotifyItemCollected(Collectable collectable) {
      
        //TODO: Releasing the collected apple cell is done by the snake passing over it
       
        CreateApple();
    }
}
