using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

    public class PlayerController : MonoBehaviour
    {
    public float jumpForce = 3f;
    private bool is_grounded = false;
    private Rigidbody rb; 
    private int count;
    private int jumpCount;

    private float movementX;
    private float movementY;

    public float speed = 0;

    public TextMeshProUGUI countText;

    public GameObject winTextObject;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        count = 0;
        SetCountText();
        winTextObject.SetActive(false);
    }
     // This function is called when a move input is detected.
    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x; 
        movementY = movementVector.y; 
    }
        private void FixedUpdate() 
    {
        Vector3 movement = new Vector3 (movementX, 0.0f, movementY);
        rb.AddForce(movement * speed); 
    }
    void Update()
    {
        // if space is presses, if ball hasnt jumped twice
        if (Input.GetKeyDown(KeyCode.Space) && jumpCount < 2)
        {
            Jump();
        }
    }
    void Jump()
    {
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z); // Reset Y velocity
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        jumpCount++;
    
    }

    private void OnCollisionEnter(Collision collision)
    {
       if (collision.gameObject.CompareTag("Ground"))
        {
            is_grounded = true;
            jumpCount = 0; // Reset jump count when landing
        }
      if (collision.gameObject.CompareTag("Enemy"))
    {
 // Destroy the current object
        Destroy(gameObject); 
        winTextObject.gameObject.SetActive(true);
        winTextObject.GetComponent<TextMeshProUGUI>().text = "You Lose!";
 
    }

}
 void OnTriggerEnter(Collider other) 
    {
    if (other.gameObject.CompareTag("PickUp")) 
        {
            other.gameObject.SetActive(false);
            count = count + 1;
            SetCountText();
        }
    }

 void SetCountText() 
    {
        countText.text = "Count: " + count.ToString();

 // Check if the count has reached or exceeded the win condition.
 if (count >= 7)
        {
            winTextObject.SetActive(true);
            Destroy(GameObject.FindGameObjectWithTag("Enemy"));
        }
    }

}