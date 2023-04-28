using UnityEngine;

public class ForwardMovement : MonoBehaviour
{
    public static float speed;

    private void Start()
    {
        speed = 3f;
    }

    private void FixedUpdate()
    {
        if (Player.isMoving)
        {
            transform.Translate(Vector3.forward * speed * Time.fixedDeltaTime);
        }
    }
}
