using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float playerSpeed = 10f;
    [SerializeField] float jumpForce = 2f;

    [SerializeField] Rigidbody rb;

    [SerializeField] Animator animator;
    [SerializeField] Transform player;

    [SerializeField] GameObject winPanel;
    [SerializeField] GameObject wheresKeyPanel;
    [SerializeField] GameObject loosePanel;
    [SerializeField] GameObject chest;
    [SerializeField] public TextMeshProUGUI keyText;
    
    public GameManager gameManager;

    public static PlayerMovement instance;

    private bool isGrounded;
    private bool hasKey;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        winPanel.SetActive(false);
        wheresKeyPanel.SetActive(false);
        hasKey = PlayerPrefs.GetInt("hasKey", 0) == 1;
        UpdateKeyText();
    }

    private void Update()
    {
        // Movimiento del player.
        float forwardMovement = Input.GetAxis("Vertical") * playerSpeed;
        float horizontalMovement = Input.GetAxis("Horizontal") * playerSpeed;

        forwardMovement *= Time.deltaTime;
        horizontalMovement *= Time.deltaTime;

        transform.Translate(horizontalMovement, 0, forwardMovement);

        // Animaciones.
        animator.SetFloat("VelZ", Mathf.Abs(horizontalMovement) + Mathf.Abs(forwardMovement));
        animator.SetBool("Grounded", isGrounded);

        // Salto.
        isGrounded = Physics.Raycast(transform.position, Vector3.down, 0.1f);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        // Condiciones de victoria.
        if (other.CompareTag("Key"))
        {
            Debug.Log("Player picked up the key!");
            keyText.text = "Key: found!";
            hasKey = true;
            UpdateKeyText();
            Destroy(other.gameObject);
            PlayerPrefs.SetInt("hasKey", hasKey ? 1 : 0);
        }
        else if (other.CompareTag("Win") && hasKey)
        {
            Debug.Log("Player won!");
            winPanel.SetActive(true);
            chest.SetActive(true);
            hasKey = false;
            UpdateKeyText();
            PlayerPrefs.SetInt("hasKey", hasKey ? 1 : 0);
        }
        else
        {
            wheresKeyPanel.SetActive(true);
        }

        // Condicion de derrota.
        if (other.gameObject.CompareTag("DeathFloor"))
        {
            loosePanel.SetActive(true);
            hasKey = false;
            UpdateKeyText();
            PlayerPrefs.SetInt("hasKey", hasKey ? 1 : 0);
        }
    }

    public void OkButton()
    {
        wheresKeyPanel.SetActive(false);
        winPanel.SetActive(false);
    }

    private void UpdateKeyText()
    {
        keyText.text = hasKey ? "Key: found!" : "Key: not found :c";
    }

    public void UpdateHasKey()
    {
        hasKey = PlayerPrefs.GetInt("hasKey", 0) == 1;
        keyText.text = hasKey ? "Key: found!" : "Key: not found :c";
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
        Debug.Log("Adió");
        Application.Quit();
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
