using UnityEngine;

public class Snake : MonoBehaviour
{
    public GameField GameField;

    public SnakePart HeadPrefab;
    public SnakePart BodyPrefab;

    public Vector2Int StartCellId = new Vector2Int(5, 5);

    public float MoveDelay = 1.3f;

    private SnakePart[] _parts;
    private Vector2Int _moveDirection = Vector2Int.up;
    private float _moveTimer;

    public void CreateSnake()
    {
        _parts = new SnakePart[0];
        AddPart(HeadPrefab, StartCellId);
        AddPart(BodyPrefab, StartCellId + Vector2Int.down);
    }

    private void AddPart(SnakePart partPrefab, Vector2Int cellId)
    {
        IncreasePartsArrayLenght();

        SnakePart newSnakePart = Instantiate(partPrefab);
        _parts[_parts.Length - 1] = newSnakePart;

        SetPartCell(newSnakePart, cellId);
    }

    private void IncreasePartsArrayLenght()
    {
        SnakePart[] tempParts = _parts;
        _parts = new SnakePart[tempParts.Length + 1];

        for (int i = 0; i < tempParts.Length; i++)
        {
            _parts[i] = tempParts[i];
        } 
    }

    private void SetPartCell(SnakePart part, Vector2Int cellId)
    {
        Vector2 cellPosition = GameField.GetCellPosition(cellId.x, cellId.y);
        part.SetCellPosition(cellId, cellPosition);
    }

    private void Update()
    {
        GetMoveDirection();
        MoveTimerTick();
    }

    private void GetMoveDirection()
    {
        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) && _moveDirection != Vector2Int.down)
        {
            SetMoveDirection(Vector2Int.up);
        }
        else if ((Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) && _moveDirection != Vector2Int.right)
        {
            SetMoveDirection(Vector2Int.left);
        }
        else if ((Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) && _moveDirection != Vector2Int.up)
        {
            SetMoveDirection(Vector2Int.down);
        }
        else if ((Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) && _moveDirection != Vector2Int.left)
        {
            SetMoveDirection(Vector2Int.right);
        }
    }

    private void SetMoveDirection(Vector2Int moveDirection)
    {
        _moveDirection = moveDirection;
        SetHeadRotation(moveDirection);
        Move();
    }

    private void MoveTimerTick()
    {
        _moveTimer += Time.deltaTime;
        if(_moveTimer >= MoveDelay)
        {
            Move();
        }
    }

    private void Move()
    {
        _moveTimer = 0;
        for (int i = _parts.Length - 1; i >= 0; i--)
        {
            Vector2Int partCellId = _parts[i].GetCellId();
            if (i == 0)
            {
                partCellId = MoveCellId(partCellId, _moveDirection);
            }
            else
            {
                partCellId = _parts[i - 1].GetCellId();
            }
            SetPartCell(_parts[i], partCellId);
        }
    }

    private Vector2Int MoveCellId(Vector2Int cellId, Vector2Int direction)
    {
        cellId += direction;

        if(cellId.x >= GameField.CellsInRow)
        {
            cellId.x = 0;
        }
        else if (cellId.x < 0)
        {
            cellId.x = GameField.CellsInRow - 1;
        }

        if (cellId.y >= GameField.CellsInRow)
        {
            cellId.y = 0;
        }
        else if (cellId.y < 0)
        {
            cellId.y = GameField.CellsInRow - 1;
        }

        return cellId;
    }

    private void SetHeadRotation(Vector2Int direction)
    {
        Vector3 headEuler = Vector3.zero;

        if (direction.x == 0 && direction.y == 1)
        {
            headEuler = new Vector3(0f, 0f, 0f);
        }
        else if (direction.x == 1 && direction.y == 0)
        {
            headEuler = new Vector3(0f, 0f, -90f);
        }
        else if (direction.x == 0 && direction.y == -11)
        {
            headEuler = new Vector3(0f, 0f, 180f);
        }
        else if (direction.x == -1 && direction.y == 0)
        {
            headEuler = new Vector3(0f, 0f, 90f);
        }

        _parts[0].transform.eulerAngles = headEuler;
    }

}
