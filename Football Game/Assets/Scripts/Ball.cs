using System;
using System.Collections;
using Scripts.Events;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private void OnEnable()
    {
        StartCoroutine(ReturnBall());
    }

    IEnumerator ReturnBall()
    {
        yield return new WaitForSeconds(4f);
        
        ObjectPool.Instance.ReturnObject(this.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("RKale"))
        {
            Events.OnLeftScoreChanged.Invoke(1);
            GetComponentInChildren<ParticleSystem>().Play();
        }
        if (other.CompareTag("LKale"))
        {
            Events.OnRightScoreChanged.Invoke(1);
            GetComponentInChildren<ParticleSystem>().Play();
        }
        
    }
}
