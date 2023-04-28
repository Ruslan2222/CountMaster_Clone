using UnityEngine;
using DG.Tweening;
using System;
using System.Collections;

public class Character : MonoBehaviour
{
    [SerializeField] private Animator _characterAnimator;
    [SerializeField] private ParticleSystem _blood;
    [SerializeField] private Rigidbody _rigidbody;

    private Player _player;

    private void Start()
    {
        _characterAnimator.SetBool("run", true);
        _player = transform.GetComponentInParent<Player>();
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "Enemy":
                if (other.transform.parent.childCount > 0)
                {
                    Destroy(other.gameObject);
                    Kill();
                }
                break;

            case "Ramp":
                transform.DOJump(new Vector3(transform.position.x, transform.position.y, transform.position.z + 2), 1f, 1, 1f)
                         .SetEase(Ease.Flash);
                break;
            case "Obstacles":
                Kill();
                break;
            case "Step":
                Steps(other);
                break;

        }


    }

    private void Kill()
    {
        Instantiate(_blood, transform.position, Quaternion.identity);
        _player.LoseGame();
        Destroy(gameObject);
    }

    private void Steps(Collider collider)
    {
       
        _player.moveCamera = true;
        transform.parent.parent = null;
        transform.parent = null;
        _rigidbody.isKinematic = GetComponent<Collider>().isTrigger = false;
        _characterAnimator.SetBool("run", false);

        if (_player.transform.childCount == 2)
        {
            StartCoroutine(StopGame(collider));
        }

    }

    private IEnumerator StopGame(Collider collider)
    {
        #region AddCoins
        int levelIncome = PlayerPrefs.GetInt("levelIncome");
        float multiplier = float.Parse(collider.name);
        float income = 35 * levelIncome / 10;
        int levelCoins = (35 + (int)income) * (int)multiplier;
        PlayerPrefs.SetFloat("levelCoins", levelCoins);
        #endregion
        yield return new WaitForSeconds(0.2f);
        collider.GetComponent<Renderer>().material.DOColor(new Color(0.4f, 0.98f, 0.65f), 0.5f).SetLoops(1000, LoopType.Yoyo)
                .SetEase(Ease.Flash);
        ForwardMovement.speed = 0;
        yield return new WaitForSeconds(2f);
        _player.winPanel.SetActive(true);
    }
}
