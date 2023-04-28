using System.Collections;
using UnityEngine;

public class SidewayMovement : MonoBehaviour
{
    [Header("Speed")]
    [Space]
    [SerializeField] private float _speed;
    [Header("Side Limit")]
    [SerializeField] private float _minLimitAmount;
    [SerializeField] private float _maxLimitAmount;
    [Space]
    [SerializeField] private MouseInput _mouseInput;

    private void Update()
    {
        if (Player.isMoving)
        {
            if (transform.childCount > 40)
            {
                _minLimitAmount = -0.7f;
                _maxLimitAmount = 0.7f;
            }

            float swerveAmount = Time.deltaTime * _speed * _mouseInput.MoveFactorX;
            swerveAmount = Mathf.Clamp(swerveAmount, _minLimitAmount, _maxLimitAmount);
            transform.Translate(swerveAmount, 0f, 0f);
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, _minLimitAmount, _maxLimitAmount), transform.position.y, transform.position.z);
        }
    }

    public void MoveToCenter()
    {
        StartCoroutine(MoveToTheCenter());
    }

    private IEnumerator MoveToTheCenter()
    {
        while (transform.position.x != 0)
        {
            transform.localPosition = Vector3.Lerp(transform.position,
                new Vector3(0f, transform.position.y, transform.position.z), Time.deltaTime * 2);

            yield return null;
        }
    }
}
