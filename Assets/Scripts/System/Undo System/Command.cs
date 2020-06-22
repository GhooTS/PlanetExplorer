﻿public abstract class Command
{

    public abstract void Execute();
    public abstract void UnExecute();

    public virtual void OnDestroy()
    {

    }
}
