using System.Collections.Generic;
using UnityEngine;

public class UndoSystem : MonoBehaviour
{

    public static UndoSystem undoSystem;

    [Range(1, 128)]
    public int maxCommand;
    public int count;

    public KeyCode undoKey;
    public KeyCode redoKey;

    private LinkedList<Command> commands = new LinkedList<Command>();
    private LinkedListNode<Command> currentCommand;
    readonly private LinkedListNode<Command> gouradFirst = new LinkedListNode<Command>(null);
    readonly private LinkedListNode<Command> gouradLast = new LinkedListNode<Command>(null);


    private void Start()
    {
        UndoSystem.undoSystem = this;
        gouradFirst.Value = new CommandEmpty();
        gouradLast.Value = new CommandEmpty();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.LeftControl))
        {
            if (Input.GetKeyDown(undoKey))
            {
                Undo();
            }
            else if (Input.GetKeyDown(redoKey))
            {
                Redo();
            }
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            Add();
        }

        count = commands.Count;
    }

    [ContextMenu("Add Test Command")]
    private void Add()
    {
        Command command = new CommandEmpty();
        AddCommand(command);
    }

    public void AddCommand(Command command)
    {

        if (commands.Count == maxCommand)
        {
            commands.Last.Value.OnDestroy();
            commands.RemoveLast();
        }

        ClearRedo();

        commands.AddFirst(command);
        currentCommand = gouradFirst;
    }

    [ContextMenu("Undo")]
    public void Undo()
    {
        if (currentCommand == gouradFirst && commands.First != null)
        {
            currentCommand = commands.First;
        }

        if (currentCommand != null)
        {
            currentCommand.Value.UnExecute();
            //first command is the most recent one, that why we going next not prev command
            if (currentCommand.Next != null)
            {
                currentCommand = currentCommand.Next;
            }
            else
            {
                currentCommand = gouradLast;
            }
        }
    }

    [ContextMenu("Redo")]
    public void Redo()
    {
        if (currentCommand == gouradLast && commands.Last != null)
        {
            currentCommand = commands.Last;
        }

        if (currentCommand != null)
        {
            currentCommand.Value.Execute();
            if (currentCommand.Previous != null)
            {
                currentCommand = currentCommand.Previous;
            }
            else //set first if we get to the begining of the list
            {
                currentCommand = gouradFirst;
            }
        }
    }



    public void ClearAll()
    {
        commands.Clear();
    }

    public void ClearRedo()
    {

        if (currentCommand == gouradFirst && commands.First != null)
        {
            currentCommand = commands.First;
        }

        if (currentCommand != null)
        {
            while (commands.First != currentCommand && commands.Count != 0)
            {
                commands.First.Value.OnDestroy();
                commands.RemoveFirst();
            }
        }
    }

    private void GetToCommand(Command toCommand)
    {
        if (toCommand != null)
        {
            LinkedListNode<Command> foundCommand = commands.Find(toCommand);

            if (foundCommand != null)
            {
                currentCommand = foundCommand;
            }
        }
    }

    public void ClearUndo(Command toCommand = null)
    {
        GetToCommand(toCommand);

        if (currentCommand == gouradLast && commands.Last != null)
        {
            currentCommand = commands.Last;
        }

        if (currentCommand != null)
        {
            while (commands.Last != currentCommand && commands.Count != 0)
            {
                commands.Last.Value.OnDestroy();
                commands.RemoveLast();
            }
        }
    }

    public void CancelCommand(Command command)
    {
        command.UnExecute();
        commands.Remove(command);
    }

}
