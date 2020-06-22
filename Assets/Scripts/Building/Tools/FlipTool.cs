using UnityEngine;

public class FlipTool : Tool
{
    public KeyCode flipKey = KeyCode.Q;
    private Flipable flipable;

    public override void Activate()
    {
        base.Activate();
        if (selection.Selected.TryGetComponent(out flipable) == false)
        {
            Deactivate();
        }
    }

    public override void Deactivate()
    {
        base.Deactivate();
        flipable = null;
    }

    private void Update()
    {
        if (Input.GetKeyDown(flipKey))
        {
            flipable.Flip();
        }
    }
}
