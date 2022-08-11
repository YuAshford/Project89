using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{

    public CharacterController controller;

    public float speed = 12f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;
    public float jumpHeightSecond = 2f;
    public float playerDashSpeed = 36f;
    public float dashActiveTime = 0.2f;
    public float timeUpdatePlayerPosition = 1.5f;

    public int currentRoomsCount = 0;

    public Transform PlayerOldPosition;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    Vector3 velocity;
    public static bool isGrounded;
    bool isJumpOnce = false;
    public bool isDashing = false;

    private float fromDeathToMenu = 2f;
    public GameObject deathObject;

    private void Start()
    {
        PlayerOldPosition = gameObject.transform;
        deathObject = GameObject.Find("Canvas").transform.GetChild(0).gameObject;
        deathObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!PlayerManager.PlayerIsDead)
        {

            isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

            if (isGrounded && velocity.y < 0)
            {
                velocity.y = -2f;
                isJumpOnce = false;
            }

            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

            Vector3 move = transform.right * x + transform.forward * z;

            if (!isDashing)
                controller.Move(move * speed * Time.deltaTime);
            else
            {
                StartCoroutine(Dashing(move));
                //newDashing(move);
                //controller.Move(move * playerDashSpeed * Time.deltaTime);
                //dashActiveTime -= Time.deltaTime;
            }

            if (dashActiveTime <= 0)
            {
                isDashing = false;
                dashActiveTime = 0.2f;
            }

            if (Input.GetButtonDown("Jump") && isGrounded)
            {
                PlayerJump(jumpHeight, true);
                /*velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
                isJumpOnce = true;*/
            }
            else if (Input.GetButtonDown("Jump") && !isGrounded && isJumpOnce)
            {
                PlayerJump(jumpHeightSecond, false);
                /*velocity.y = Mathf.Sqrt(jumpHeightSecond * -2f * gravity);
                isJumpOnce = false;*/
            }

            velocity.y += gravity * Time.deltaTime;

            controller.Move(velocity * Time.deltaTime);

            if (timeUpdatePlayerPosition <= 3f)
            {
                timeUpdatePlayerPosition += Time.deltaTime;
            }
            else
            {
                PlayerOldPosition = gameObject.transform;
                timeUpdatePlayerPosition = 0;
            }
        }
        else
        {
            deathObject.SetActive(true);
            gameObject.transform.GetChild(1).GetChild(0).gameObject.SetActive(false);
            gameObject.transform.GetChild(1).GetComponent<MouseLook>().enabled = false;
            if (fromDeathToMenu < 0)
            {
                SceneManager.LoadScene(0);
            }
            else
                fromDeathToMenu -= Time.deltaTime;
        }
    }

    void PlayerJump(float jHeight, bool jOnce)
    {
        velocity.y = Mathf.Sqrt(jHeight * -2f * gravity);
        isJumpOnce = jOnce;
    }

    public IEnumerator Dashing(Vector3 move)
    {
        //controller.AddForce(move * playerDashSpeed, ForceMode.VelocityChange);
        controller.Move(move * playerDashSpeed * Time.deltaTime);
       // Debug.Log("11111");
        yield return new WaitForSeconds(dashActiveTime);
        

        isDashing = false;
        //Debug.Log(isDashing);
    }

    public void newDashing(Vector3 move)
    {
        while (dashActiveTime > 0)
        {
            controller.Move(move * playerDashSpeed * Time.deltaTime);
            dashActiveTime -= Time.deltaTime;
        }

        isDashing = false;
        dashActiveTime = 0.2f;

    }
}
