using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireWall : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float followSpeed;
    [SerializeField] Animator animator;
    [SerializeField] float fireTimer = 2f;

    public bool canMove = true;

    private void Start()
    {
        target = Player.I.transform;
    }

    void Update()
    {
        if (canMove)
        {
            float x = Mathf.Lerp(transform.parent.position.x, target.position.x, followSpeed * Time.deltaTime);
            transform.parent.position = new Vector3(x, transform.parent.position.y, transform.parent.position.z);
        }
    }

    public void EnableMove()
    {
        Debug.Log("MOVE");
        canMove = true;
    }

    public void DisableMove()
    {
        Debug.Log("STOP");
        canMove = false;
    }

    public void DoFireAfterTimer()
    {
        Debug.Log("FIRE");
        StartCoroutine(DoFireCrt());
    }

    private IEnumerator DoFireCrt()
    {
        yield return new WaitForSeconds(fireTimer);
        animator.SetTrigger("DoFireTrig");
    }
}
