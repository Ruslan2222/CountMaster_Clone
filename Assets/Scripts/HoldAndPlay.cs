using UnityEngine;

public class HoldAndPlay : MonoBehaviour
{
    [SerializeField] private GameObject _gamePanel;

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Moved)
            {
                _gamePanel.SetActive(false);
                Player.isMoving = true;
                enabled = false;
            }

        }
    }
}
