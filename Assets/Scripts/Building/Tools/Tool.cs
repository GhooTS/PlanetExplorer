using UnityEngine;

[RequireComponent(typeof(Selection))]
public abstract class Tool : MonoBehaviour
{
    public string toolName;
    public KeyCode activationKey;
    public bool IsActive { get; private set; }
    public bool requireSelection;
    public bool activeOnSelection;
    protected Selection selection;

    private void Awake()
    {
        selection = GetComponent<Selection>();
    }

    public void SetActive(bool active)
    {
        enabled = IsActive = active;
    }

    public virtual void Activate()
    {
        SetActive(true);
        selection.LockSelection = true;
    }

    public virtual void Deactivate()
    {
        SetActive(false);
        selection.LockSelection = false;
    }
}
