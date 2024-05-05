using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
        PlayerMovement.instance.transform.position = respawnPosition;
        PlayerMovement.instance.gameObject.SetActive(true);
    }

    //public IEnumerator RespawnWaiter()
    //{
    //    PlayerMovement.instance.gameObject.SetActive(false);

    //    yield return new WaitForSeconds(2f);

    //    PlayerMovement.instance.transform.position = respawnPosition;
    //    PlayerMovement.instance.gameObject.SetActive(true);

    //    UpdatePlayerState();
    //}

    private void UpdatePlayerState()
    {
        PlayerMovement.instance.UpdateHasKey();
    }
}
