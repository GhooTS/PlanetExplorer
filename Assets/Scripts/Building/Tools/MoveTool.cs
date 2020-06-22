using UnityEngine;
using UnityEngine.Events;

public class MoveTool : Tool
{
    public KeyCode actionKey = KeyCode.Mouse0;
    public KeyCode cancelKey = KeyCode.Mouse1;

    [SerializeField]
    private MovePositionCalculator moveOperation;
    public UnityEvent moving;
    public UnityEvent moved;

    private IValidator validator;
    private Moveable objectToMove;
    private Vector2 startPosition;
    private Vector2 positionDiffrence;


    public override void Activate()
    {
        base.Activate();

        if (selection.IsObjectSelected() && selection.Selected.TryGetComponent(out objectToMove))
        {
            startPosition = objectToMove.transform.position;
            positionDiffrence = startPosition - selection.MousePosition;

            objectToMove.moving?.Invoke();
            moving?.Invoke();

            if (validator == null)
            {
                validator = GetComponent<IValidator>();
            }
            validator.StartValidation(objectToMove);
        }
        else
        {
            Deactivate();
        }
    }

    public override void Deactivate()
    {
        base.Deactivate();

        validator?.EndValidation();
        objectToMove = null;
    }

    private void Update()
    {

        if (Input.GetKeyDown(actionKey) && validator.ChceckIfInValidePlace())
        {
            moved?.Invoke();
            Deactivate();
        }
        else if (Input.GetKeyDown(cancelKey))
        {
            CancelOperation();
        }

        if (objectToMove != null)
        {
            MoveObject();
        }
    }


    public void MoveObject()
    {
        objectToMove.transform.position = moveOperation.Move(objectToMove.transform,objectToMove.moveData, (Vector2)selection.MousePosition, Vector2.zero);
    }

    private void CancelOperation()
    {
        moved?.Invoke();
        Deactivate();
    }
}
