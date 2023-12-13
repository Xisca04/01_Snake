using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Snake : MonoBehaviour
{
    private enum Direction
    {
        Left,
        Right,
        Down,
        Up
    }

    private enum State
    {
        Alive,
        Dead
    }

    private class SnakeBodyPart
    {
        private SnakeMovePosition snakeMovePosition; // SnakeBodyPart's 2D position
        private Transform transform;

        public SnakeBodyPart(int bodyIndex)
        {
            GameObject snakeBodyPartGameObject = new GameObject("Snake Body",
                typeof(SpriteRenderer));
            SpriteRenderer snakeBodyPartSpriteRenderer = snakeBodyPartGameObject.GetComponent<SpriteRenderer>();
            snakeBodyPartSpriteRenderer.sprite =
                GameAssets.Instance.snakeBodySprite;
            snakeBodyPartSpriteRenderer.sortingOrder = -bodyIndex;
            transform = snakeBodyPartGameObject.transform;
        }

        public void SetMovePosition(SnakeMovePosition snakeMovePosition)
        {
            // Position (gridPosition)
            this.snakeMovePosition = snakeMovePosition; // 2D position + SnakeBodyPart's direction
            Vector2Int gridPosition = snakeMovePosition.GetGridPosition();
            transform.position = new Vector3(gridPosition.x,
                gridPosition.y, 0); // 3D POSITION OF THE G.O

            // Direction
            float angle;
            switch (snakeMovePosition.GetDirection())
            {
                default:
                case Direction.Left: // Currently Going Left
                    switch (snakeMovePosition.GetPreviousDirection())
                    {
                        default: // Previously Going Left
                            angle = 90;
                            break;
                        case Direction.Down: // Previously Going Down
                            angle = -45;
                            break;
                        case Direction.Up: // Previously Going Up
                            angle = 45;
                            break;
                    }
                    break;
                case Direction.Right: // Currently Going Right
                    switch (snakeMovePosition.GetPreviousDirection())
                    {
                        default: // Previously Going Right
                            angle = -90;
                            break;
                        case Direction.Down: // Previously Going Down
                            angle = 45;
                            break;
                        case Direction.Up: // Previously Going Up
                            angle = -45;
                            break;
                    }
                    break;
                case Direction.Up: // Currently Going Up
                    switch (snakeMovePosition.GetPreviousDirection())
                    {
                        default: // Previously Going Up
                            angle = 0;
                            break;
                        case Direction.Left: // Previously Going Left
                            angle = 45;
                            break;
                        case Direction.Right: // Previously Going Right
                            angle = -45;
                            break;
                    }
                    break;
                case Direction.Down: // Currently Going Down
                    switch (snakeMovePosition.GetPreviousDirection())
                    {
                        default: // Previously Going Down
                            angle = 180;
                            break;
                        case Direction.Left: // Previously Going Left
                            angle = -45;
                            break;
                        case Direction.Right: // Previously Going Right
                            angle = 45;
                            break;
                    }
                    break;
            }

            transform.eulerAngles = new Vector3(0, 0, angle);
        }
    }

    private class SnakeMovePosition
    {
        private SnakeMovePosition previousSnakeMovePosition;
        private Vector2Int gridPosition;
        private Direction direction;

        public SnakeMovePosition(SnakeMovePosition previousSnakeMovePosition, Vector2Int gridPosition, Direction direction)
        {
            this.previousSnakeMovePosition = previousSnakeMovePosition;
            this.gridPosition = gridPosition;
            this.direction = direction;
        }

        public Vector2Int GetGridPosition()
        {
            return gridPosition;
        }

        public Direction GetDirection()
        {
            return direction;
        }

        public Direction GetPreviousDirection()
        {
            if (previousSnakeMovePosition == null)
            {
                return Direction.Right;
            }
            return previousSnakeMovePosition.GetDirection();
        }

    }

    #region Variables
    private Vector2Int gridPosition; // Posición 2D de la cabeza
    private Vector2Int startGridPosition;
    private Direction gridMoveDirection; // Dirección de la cabeza

    private float horizontalInput, verticalInput;

    private float gridMoveTimer;
    private float gridMoveTimerMax = 0.5f; // La serpiente se moverá a cada segundo

    private LevelGrid levelGrid;

    private int snakeBodySize; // Cantidad de partes del cuerpo (sin cabeza)
    private List<SnakeMovePosition> snakeMovePositionsList; // Posiciones y direcciones de cada parte (por orden)
    private List<SnakeBodyPart> snakeBodyPartsList;

    private State state;

    private Scene actualScene;
    #endregion

    private void Awake()
    {
        startGridPosition = new Vector2Int(0, 0);
        gridPosition = startGridPosition;

        gridMoveDirection = Direction.Up; // Default direction: up
        transform.eulerAngles = Vector3.zero; // Default rotation: up

        snakeBodySize = 0;
        snakeMovePositionsList = new List<SnakeMovePosition>();
        snakeBodyPartsList = new List<SnakeBodyPart>();

        state = State.Alive;

        // Get the actual scene
        actualScene = SceneManager.GetActiveScene();
    }

    private void Update()
    {
        switch (state)
        {
            case State.Alive:

                HandleMoveDirection();
                HandleGridMovement();
                break;

            case State.Dead:
                break;
        }
    }

    public void Setup(LevelGrid levelGrid)
    {
        // Snake's levelGrid = levelGrid that cames by parameter
        this.levelGrid = levelGrid;
    }

    private void HandleGridMovement() // Relative to the 2D movement
    {
        gridMoveTimer += Time.deltaTime;
        if (gridMoveTimer >= gridMoveTimerMax)
        {
            gridMoveTimer -= gridMoveTimerMax; // Timer reset
            SoundManager.PlaySound(SoundManager.Sound.SnakeMove);

            SnakeMovePosition previousSnakeMovePosition = null;
            if (snakeMovePositionsList.Count > 0)
            {
                previousSnakeMovePosition = snakeMovePositionsList[0];
            }

            SnakeMovePosition snakeMovePosition = new SnakeMovePosition(previousSnakeMovePosition, gridPosition, gridMoveDirection);
            snakeMovePositionsList.Insert(0, snakeMovePosition);
           
            //Relation between enum Direction and vectores: left, right, up and down.
            Vector2Int gridMoveDirectionVector;
            switch (gridMoveDirection)
            {
                default:
                case Direction.Left:
                    gridMoveDirectionVector = new Vector2Int(-1, 0);
                    break;
                case Direction.Right:
                    gridMoveDirectionVector = new Vector2Int(1, 0);
                    break;
                case Direction.Down:
                    gridMoveDirectionVector = new Vector2Int(0, -1);
                    break;
                case Direction.Up:
                    gridMoveDirectionVector = new Vector2Int(0, 1);
                    break;
            }
            gridPosition += gridMoveDirectionVector; // Moves the position of the snake's head
            gridPosition = levelGrid.ValidateGridPosition(gridPosition);

            // ¿Have I eaten food?
            bool snakeAteFood = levelGrid.TrySnakeEatFood(gridPosition);
            if (snakeAteFood)
            {
                // The body grows
                snakeBodySize++;
                CreateBodyPart();
                SoundManager.PlaySound(SoundManager.Sound.SnakeEat);
                
                if (actualScene.name == "TimerLevel")
                {
                    Timer.Instance.timeLeft += Timer.Instance.timerFood; // When snake eats: +5 seconds to the timer
                    StartCoroutine(Timer.Instance.AddedTimeFood());
                }
            }

            if (snakeMovePositionsList.Count > snakeBodySize)
            {
                snakeMovePositionsList.
                    RemoveAt(snakeMovePositionsList.Count - 1);
            }
           
            foreach (SnakeMovePosition movePosition in snakeMovePositionsList)
            {
                if(gridPosition == movePosition.GetGridPosition()) // If position coincides with a body part -> GAME OVER
                {
                    // GAME OVER
                    state = State.Dead;
                    GameManager.Instance.SnakeDied();
                }
            }
           
            if (actualScene.name == "TimerLevel") // Check if the player is in the TIMER LEVEL
            {
                if (Timer.Instance.timeLeft == 0.0f) // GAME OVER if timer = 0
                {
                    state = State.Dead;
                    GameManager.Instance.SnakeDied();
                }
            }

            transform.position = new Vector3(gridPosition.x, gridPosition.y, 0);
            transform.eulerAngles = new Vector3(0, 0, GetAngleFromVector(gridMoveDirectionVector));
            UpdateBodyParts();
        }
    }

    private void HandleMoveDirection()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        // Change direction to UP
        if (verticalInput > 0) // If I've pressed W or Up arrow
        {
            if (gridMoveDirection != Direction.Down) // If I'm going horizontal
            {
                // Change direction to up (0,1)
                gridMoveDirection = Direction.Up;
            }
        }

        // Change direction to DOWN
        if (verticalInput < 0) // If I've pressed S or Down arrow
        {
            if (gridMoveDirection != Direction.Up)
            {
                gridMoveDirection = Direction.Down;
            }
        }

        // Change direction to RIGHT
        if (horizontalInput > 0)
        {
            if (gridMoveDirection != Direction.Left)
            {
                gridMoveDirection = Direction.Right;
            }
        }

        // Change direction to LEFT
        if (horizontalInput < 0)
        {
            if (gridMoveDirection != Direction.Right)
            {
                gridMoveDirection = Direction.Left;
            }
        }
    }

    private float GetAngleFromVector(Vector2Int direction)
    {
        float degrees = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        if (degrees < 0)
        {
            degrees += 360;
        }

        return degrees - 90;
    }

    public Vector2Int GetGridPosition()
    {
        return gridPosition;
    }

    public List<Vector2Int> GetFullSnakeBodyGridPosition()
    {
        List<Vector2Int> gridPositionList = new List<Vector2Int>() { gridPosition };
        foreach (SnakeMovePosition snakeMovePosition in snakeMovePositionsList)
        {
            gridPositionList.Add(snakeMovePosition.GetGridPosition());
        }
        return gridPositionList;
    }

    private void CreateBodyPart()
    {
        snakeBodyPartsList.Add(new SnakeBodyPart(snakeBodySize));
    }

    private void UpdateBodyParts()
    {
        for (int i = 0; i < snakeBodyPartsList.Count; i++)
        {
            snakeBodyPartsList[i].SetMovePosition(snakeMovePositionsList[i]);
        }
    }
}

