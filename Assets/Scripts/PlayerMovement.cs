using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour {

    bool alive = true;
    public Animator anim;
    [SerializeField] LayerMask groundMask;
    [SerializeField] float jumpSpeed = 1000f;
    public float speed = 5;
    [SerializeField] Rigidbody rb;
    CapsuleCollider cc;
    float horizontalInput;
    [SerializeField] float horizontalMultiplier = 2;
    [SerializeField] GameObject panel;
    public float speedIncreasePerPoint = 0.1f;


    private void Start()
    {
        cc = GetComponent<CapsuleCollider>();

    }

    private void FixedUpdate()
    {
        if (!alive) return;
       

        Vector3 forwardMove = transform.forward * speed * Time.fixedDeltaTime;
        Vector3 horizontalMove = transform.right * horizontalInput * speed * Time.fixedDeltaTime * horizontalMultiplier;
        rb.MovePosition(rb.position + forwardMove + horizontalMove);

       
    }
    private void Update () {
        horizontalInput = Input.GetAxis("Horizontal");

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            anim.SetBool("left", true);
        }else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            anim.SetBool("right", true);
        }
        else
        {
            anim.SetBool("left", false);
            anim.SetBool("right", false);
        }


        if (Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetBool("jump", true);
            Jump();

        }
        else
        {
            
                anim.SetBool("jump", false);
            
        }

        if (Input.GetKeyDown(KeyCode.RightControl))
        {
            anim.SetBool("slide", true);
            
          //  Debug.Log("ctrl pressed");
        }
        else
        {
            anim.SetBool("slide", false);
            
           // Debug.Log("ctrl not pressed");
        }

        if (transform.position.y < -5) {
            anim.SetBool("fallDead", true);
            Die();
        }
	}

    public void Die ()
    {
        if (GameManager.inst.score > GameManager.inst.highScore)
        {
            GameManager.inst.SaveScore();
        }
        alive = false;
        anim.SetBool("dead", true);
        // Restart the game
        panel.SetActive(true);
        //Invoke("Restart", 1);
    }

    

    void Jump()
    {
        float height = GetComponent<Collider>().bounds.size.y;
        bool isGrounded = Physics.Raycast(transform.position,Vector3.down,(height/2)+0.1f,groundMask);
        if (isGrounded && anim.GetBool("jump"))
        {
            rb.AddForce(Vector3.up * jumpSpeed);
            
            Debug.Log("heelo jump");
        }
        
        
    }

    
}