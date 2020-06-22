using GTVariable;
using UnityEngine;
using UnityEngine.Events;

public class BuildTool : Tool
{
    public KeyCode actionKey = KeyCode.Mouse0;
    public KeyCode cancelKey = KeyCode.Mouse1;
    private Buildable buildingObject;
    [SerializeField]
    private FloatVariable playerResource;
    [SerializeField]
    private MovePositionCalculator moveOperation;

    public UnityEvent building;
    public UnityEvent onNotEnoughResource;
    public UnityEvent builded;
    public UnityEvent buildCancled;

    
    private IValidator validator;


    public override void Activate()
    {
        if (building == null)
        {
            return;
        }

        base.Activate();
    }

    public void Activate(Buildable buildingObject)
    {
        if (buildingObject == null) return;

        CancelOperation();

        this.buildingObject = Instantiate(buildingObject);

        Activate();

        building?.Invoke();

        if (validator == null)
        {
            validator = GetComponent<IValidator>();
        }
        validator.StartValidation(this.buildingObject);


    }

    public override void Deactivate()
    {
        base.Deactivate();

        validator?.EndValidation();
        buildingObject = null;
    }

    private void Update()
    {

        if (Input.GetKeyDown(actionKey) && validator.ChceckIfInValidePlace())
        {
            if(playerResource - buildingObject.buildingData.price < 0)
            {
                onNotEnoughResource?.Invoke();
                return;
            }

            playerResource.SetValueWithEvent(playerResource.value - buildingObject.buildingData.price);
            buildingObject.Constracted?.Invoke();
            Deactivate();
            builded?.Invoke();
        }
        else if (Input.GetKeyDown(cancelKey))
        {
            CancelOperation();
        }

        if (buildingObject != null)
        {
            MoveObject();
        }
    }


    public void MoveObject()
    {
        buildingObject.transform.position = moveOperation.Move(buildingObject.transform,buildingObject.moveData,selection.MousePosition, Vector2.zero);
    }

    private void CancelOperation()
    {
        if (buildingObject != null)
        {
            Destroy(buildingObject.gameObject);
            buildCancled?.Invoke();
            Deactivate();
        }
    }
}
