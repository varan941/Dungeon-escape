using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashScreen : MonoBehaviour, IDecoration
{
    [SerializeField] float timer=0.1f;

    public void InvokeDecoration()
    {
        if (Random.Range(0.0f, 1.0f) < 0.7f)
            return;

        gameObject.SetActive(true);
        StartCoroutine(FlashCrt());        
    }

    IEnumerator FlashCrt()
    {        
        yield return new WaitForSeconds(timer);
        gameObject.SetActive(false);
    }
}
