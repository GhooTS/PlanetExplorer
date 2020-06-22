using GTAttribute;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;




public class Selection : MonoBehaviour
{
    public enum DiselectionMode
    {
        /// <summary>
        /// Diselected object by clicking <see cref="diselectionKey"/>
        /// </summary>
        Key,
        /// <summary>
        /// Diselected object by selecting nothing
        /// </summary>
        Empty,
        /// <summary>
        /// Diselected object by clicking <see cref="diselectionKey"/> or clicking nothing
        /// </summary>
        Both
    }

    public KeyCode selectionKey = KeyCode.Mouse0;
    public KeyCode diselectionKey = KeyCode.Mouse1;
    public DiselectionMode diselectionMode = DiselectionMode.Both;
    public Vector2 MousePosition { get { return cam.GetMouseWorldPosition(); } }
    [GT_ReadOnly]
    public Selecetable Selected;
    public LayerMask selectionLayer;
    public UnityEvent selected;
    public UnityEvent diselected;
    public bool LockSelection { get; set; } = false;
    private Camera cam;


    private void Start()
    {
        if (cam == null)
        {
            cam = Camera.main;
        }
    }


    private void Update()
    {
        if (LockSelection) return;


        if (Input.GetKeyDown(selectionKey))
        {
            Selecte();
        }
        if(Input.GetKeyDown(diselectionKey) && (diselectionMode == DiselectionMode.Key || diselectionMode == DiselectionMode.Both))
        {
            Diselecte();
        }
    }

    public void SetSelection(Selecetable objectToSelect)
    {
        if (objectToSelect != null)
        {
            Selected = objectToSelect;
            Selected.selected?.Invoke();
            selected?.Invoke();
        }
    }

    public bool Selecte()
    {
        RaycastHit2D hit = Physics2D.Raycast(cam.GetMouseWorldPosition(), Vector2.zero, 1, selectionLayer);

        //Check if pointer is not over UI, if is return false
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return false;
        }

        //Check if rayCast hit gameObject with Editable component
        if (hit.collider != null && hit.collider.TryGetComponent(out Selecetable output))
        {
            if (Selected == output)
            {
                return false;
            }
            if (IsObjectSelected())
            {
                Diselecte();
            }
            Selected = output;
            selected?.Invoke();
            Selected.selected?.Invoke();
            return true;
        }

        if ((diselectionMode == DiselectionMode.Empty || diselectionMode == DiselectionMode.Both) && IsObjectSelected())
        {
            Diselecte();
        }
        return false;
    }

    public void Diselecte()
    {
        if (Selected == null)
            return;

        diselected?.Invoke();
        Selected.diselected?.Invoke();
        Selected = null;
    }

    public bool IsObjectSelected()
    {
        return Selected != null;
    }
}
