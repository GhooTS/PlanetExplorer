using UnityEngine;
using UnityEngine.Events;

public class ToolManage : MonoBehaviour
{
    [SerializeField]
    private Tool[] tools;
    private int activeToolIndex = -1;
    private Selection selection;
    public UnityEvent onToolChange;


    private void Awake()
    {
        tools = GetComponents<Tool>();
        selection = GetComponent<Selection>();
    }

    private void Start()
    {
        tools = GetComponents<Tool>();
        selection = GetComponent<Selection>();
        DeactivateAllTools();
    }


    private void OnEnable()
    {
        selection.selected.AddListener(ActiveAutoTools);
        selection.diselected.AddListener(DeactivateAutoTools);
    }

    private void OnDisable()
    {
        selection.selected.RemoveListener(ActiveAutoTools);
        selection.diselected.RemoveListener(DeactivateAutoTools);
    }


    private void ActiveAutoTools()
    {
        foreach (var tool in tools)
        {
            if (tool.activeOnSelection)
            {
                tool.Activate();
            }
        }
    }

    private void DeactivateAutoTools()
    {
        foreach (var tool in tools)
        {
            if (tool.activeOnSelection)
            {
                tool.Deactivate();
            }
        }
    }

    private void DeactivateAllTools()
    {
        foreach (var tool in tools)
        {
            tool.Deactivate();
        }
    }

    private void Update()
    {
        if(activeToolIndex >= 0 && activeToolIndex < tools.Length && tools[activeToolIndex].IsActive == false)
        {
            activeToolIndex = -1;
        }
        ChangeToolByKey();
        UnlockSelectionIfNonToolActive();
    }

    private void UnlockSelectionIfNonToolActive()
    {
        if (selection.LockSelection)
        {
            foreach (var tool in tools)
            {
                if (tool.IsActive) return;
            }

            selection.LockSelection = false;
        }
    }

    private void ChangeToolByKey()
    {
        for (int i = 0; i < tools.Length; i++)
        {
            if (Input.GetKeyDown(tools[i].activationKey))
            {
                ChangeTool(i);
            }
        }
    }

    public void ChangeTool(string name)
    {
        for (int i = 0; i < tools.Length; i++)
        {
            if (tools[i].toolName == name)
            {
                ChangeTool(i);
                return;
            }
        }

        Debug.LogWarning($"you trying activated tool {name}, but it doesn't existe");
    }

    public void ChangeTool(int index)
    {
        if (index >= tools.Length || index == activeToolIndex)
        {
            Debug.Log("tool is allready active or doesn't exist");
            return;
        }
        if (tools[index].requireSelection == false || selection.IsObjectSelected())
        {
            if (activeToolIndex != -1)
            {
                tools[activeToolIndex].Deactivate();
            }
            tools[index].Activate();
            activeToolIndex = index;
            onToolChange?.Invoke();
        }
        else
        {
            Debug.LogWarning($"Tool {tools[index].name} require selection to be activated");
        }

    }

    public T GetToolOfType<T>()
        where T : Tool
    {
        foreach (var tool in tools)
        {
            if(tool.GetType() == typeof(T))
            {
                return tool as T;
            }
        }

        return null;
    }

}
