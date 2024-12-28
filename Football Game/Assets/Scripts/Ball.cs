using Scripts.Data;
using Scripts.Events;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("RKale"))
        {
            Events.OnLeftScoreChanged.Invoke(1);
        }
        if (other.CompareTag("LKale"))
        {
            Events.OnRightScoreChanged.Invoke(1);
        }
        
    }
}
