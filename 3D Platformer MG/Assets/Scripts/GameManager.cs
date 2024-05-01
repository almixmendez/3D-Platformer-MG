using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private Vector3 respawnPosition;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        respawnPosition = PlayerMovement.instance.transform.position;
    }

    void Update()
    {
        // Deslockear el cursor del mouse.
        if (Input.GetKeyDown("escape"))
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }

    public void Respawn()
    {
        StartCoroutine("RespawnWaiter");
    }

    public IEnumerator RespawnWaiter()
    {
        PlayerMovement.instance.gameObject.SetActive(false);

        yield return new WaitForSeconds(2f);

        PlayerMovement.instance.transform.position = respawnPosition;
        PlayerMovement.instance.gameObject.SetActive(true);
    }
}
