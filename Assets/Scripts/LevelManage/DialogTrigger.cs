using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogTrigger : MonoBehaviour
{
    [SerializeField] int dialogIndex;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag=="Player")
        {
            Debug.Log("hit dialog trigger");
            Dialog.I.PlayNeedDialog(dialogIndex);
            gameObject.SetActive(false);
        }
    }
}
