using GTVariable;
using System.Collections.Generic;
using UnityEngine;

public class Power : MonoBehaviour
{
    public float range;
    public IntVariable capacity;
    public List<Powerable> powerables = new List<Powerable>();

    public PowerConnection connectionPrefab;
    public int maxConnection;
    private List<PowerConnection> powerConnection = new List<PowerConnection>();

    private void OnDisable()
    {
        if (powerConnection == null)
            return;

        for (int i = 0; i < powerConnection.Count; i++)
        {
            powerConnection[i].Clear();
        }
    }

    private void Update()
    {
        for (int i = 0; i < powerConnection.Count; i++)
        {
            powerConnection[i].Clear();
        }

        for (int i = 0; i < powerables.Count; i++)
        {
            powerConnection[i].Connect(powerables[i].transform);
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Powerable powerable))
        {
            powerables.Add(powerable);
            powerable.TurnPowerOn();

            if (powerables.Count > powerConnection.Count)
            {
                AddPowerConnection();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Powerable powerable))
        {
            powerables.Remove(powerable);
            powerable.TurnPowerOff();
        }
    }


    private void AddPowerConnection()
    {
        powerConnection.Add(Instantiate(connectionPrefab, transform));
    }
}
