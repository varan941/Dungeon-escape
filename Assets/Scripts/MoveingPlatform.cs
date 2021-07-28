using UnityEngine;

public class MoveingPlatform : MonoBehaviour // только для движ. по у
{    
    [SerializeField] private float speed;
    [SerializeField] private Transform firstPoint, secPoint;

    public float Speed
    {
        get => speed; 
        set
        {
            if (value != speed)
                speed = value;
        }
    }

    private void FixedUpdate()
    {
        if (transform.position.y > secPoint.position.y)
            Speed *= -1;
        if (transform.position.y < firstPoint.position.y)
            Speed *= -1;

        transform.Translate(Vector3.up * Time.deltaTime*speed);
    }



}
