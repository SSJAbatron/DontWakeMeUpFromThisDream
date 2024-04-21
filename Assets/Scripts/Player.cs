using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    float movementSpeed = 5f;
    Vector2 movement;
    Vector2 mousePosition;
    public int score = 0;
    public Text TextscoreText;
    public GameObject ScorePanel;
    // getting reference to the rigidbody of the player
    private Rigidbody2D rb;
    public Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxis("Horizontal");
        movement.y = Input.GetAxis("Vertical");
        mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
        if(WakeUpMeter.instance.getDontWakeUpMeterValue() == 0)
        {
            Debug.Log("You Lost");
            Debug.Log("Score :" + score.ToString());
            ScorePanel.SetActive(true);
            TextscoreText.text = score.ToString();
            Time.timeScale= 0;
        }
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * movementSpeed * Time.fixedDeltaTime);
        Vector2 LookDirection = mousePosition - rb.position;
        float aimAngle = Mathf.Atan2(LookDirection.y, LookDirection.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = aimAngle;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("BulletEnemy"))
        {
            // decrease don't wakeup meter
            WakeUpMeter.instance.UpdateDontWakeUpMeter(-20);
        }
    }
}
