using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public interface ITurretAmmunitionSystem
{
    void StartReloading();
    void Reload();
    bool IsReadyToShoot();
    bool OutOfAmmunition();
    bool ReloadEnd();
    bool HasFullAmmunition();
    void DecreasteAmmuntionCount(int amount);
}

public class TurretAmunition : MonoBehaviour, ITurretAmmunitionSystem
{
    public float reloadDuration;
    public int maxAmmunition;

    [Header("Events")]
    public UnityEvent reloading;
    public UnityEvent reloaded;

    private int currentAmunition;
    private float reloadEndTime = 0;
    private bool IsReloading = false;

    private void Start()
    {
        currentAmunition = maxAmmunition;
    }

    private IEnumerator WiatForReloadTimeEnd()
    {
        var reloadWaiter = new WaitForSeconds(reloadDuration);
        yield return reloadWaiter;
        Reload();
    }
    public void Reload()
    {
        currentAmunition = maxAmmunition;
        IsReloading = false;
        reloaded?.Invoke();
    }

    public void DecreasteAmmuntionCount(int amount)
    {
        currentAmunition -= amount;
    }

    public bool ReloadEnd()
    {
        return reloadEndTime < Time.time && IsReloading == false;
    }

    public void StartReloading()
    {
        reloading?.Invoke();
        reloadEndTime = Time.time + reloadDuration;
        IsReloading = true;
        StartCoroutine(WiatForReloadTimeEnd());
    }


    public bool IsReadyToShoot()
    {
        return OutOfAmmunition() == false && ReloadEnd();
    }

    public bool OutOfAmmunition()
    {
        return currentAmunition <= 0;
    }

    public bool HasFullAmmunition()
    {
        return currentAmunition == maxAmmunition;
    }
}
