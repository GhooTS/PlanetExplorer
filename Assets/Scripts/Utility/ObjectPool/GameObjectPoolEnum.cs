using System;
using System.Collections;

public class GameObjectPoolEnum<T> : IEnumerator
{
    public T[] GameObjects;

    int position = -1;
    public GameObjectPoolEnum(T[] GameObjects)
    {
        this.GameObjects = GameObjects;
    }


    object IEnumerator.Current { get { return Current; } }
    public T Current
    {
        get
        {
            try
            {
                return GameObjects[position];
            }
            catch (IndexOutOfRangeException)
            {
                throw new InvalidOperationException();
            }
        }
    }

    public bool MoveNext()
    {
        position++;
        return (position < GameObjects.Length);
    }

    public void Reset()
    {
        position = -1;
    }
}
