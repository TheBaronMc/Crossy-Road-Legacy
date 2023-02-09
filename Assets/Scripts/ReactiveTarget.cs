using UnityEngine;
using System.Collections;

public class ReactiveTarget: MonoBehaviour
{
    public int NbOfLive = 1;

    private CallBack beforeDeath;
    private CallBack beforeBirth;

    public void Start()
    {
        if (beforeBirth != null)
            beforeBirth.Call();
    }

    public virtual void ReactToHit()
    {
        NbOfLive--;

        if (NbOfLive == 0)
            Die();
    }

    public void Die()
    {
        if (beforeDeath != null)
            beforeDeath.Call();

        Destroy(this.gameObject);
    }

    public int GetNbLife()
    {
        return NbOfLive;
    }

    public void SetNbLife(int lifes)
    {
        NbOfLive = lifes;
    }

    public void SetBeforeBirth(CallBack cb)
    {
        beforeBirth = cb;
    }

    public void SetBeforeDeath(CallBack cb)
    {
        beforeDeath = cb;
    }
}

