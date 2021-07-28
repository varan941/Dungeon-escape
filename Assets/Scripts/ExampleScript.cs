using UnityEngine;

public class ExampleScript : MonoBehaviour
{
    public float hitDistance;    

    private void Update()
    {        
        RaycastHit2D raycastHit2D = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.right), hitDistance);

        if (raycastHit2D)
        {
            Debug.Log("Заметил героя: " + raycastHit2D.collider.name);
        }

        Debug.DrawRay(transform.position, transform.TransformDirection(Vector2.right * hitDistance), Color.white);
    }
}
