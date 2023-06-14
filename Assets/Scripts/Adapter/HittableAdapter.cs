using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HittableAdapter : MonoBehaviour
{
    public UnityEvent<int> OnHit;


    public void TakeHit(int damage)
    {
        OnHit?.Invoke(damage);
    }
}
