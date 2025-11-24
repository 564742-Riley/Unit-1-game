using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    Rigidbody2D rb;
    float xv, yv;
    bool isGrounded;
    public Animator anim;
    //private float Move;
    SpriteRenderer sr;
    public float lives;
    HelperScript helper;
    public GameObject weapon;
    public LayerMask groundLayer;
    bool faceLeft; // true = facing left

    public CoinManager cm;
    void Start()
    {
        // set the mask to be ground
        groundLayer = LayerMask.GetMask("Ground");

        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        helper = gameObject.AddComponent<HelperScript>();
    }



    void Shoot()
    {

        if (Input.GetButtonDown("Fire1"))
        {
            GameObject clone;

            clone = Instantiate(weapon, transform.position, transform.rotation);

            Rigidbody2D rb = clone.GetComponent<Rigidbody2D>();

            rb.linearVelocity = new Vector2(15, 0);

            rb.transform.position = new Vector3(transform.position.x, transform.position.y + 2, transform.position.z + 1);

        }
    }

    // Update is called once per frame
    void Update()
    {
        float xvel, yvel;
        bool faceLeft = false;


        lives = 5;
        xvel = rb.linearVelocity.x;
        yvel = rb.linearVelocity.y;


        if (Input.GetKeyDown(KeyCode.LeftAlt) || (Input.GetKeyDown(KeyCode.Space) && isGrounded))
        {
            yvel = 7;
            print("do jump");
            anim.SetBool("isJumping", true);
        }




        if (Input.GetKey("d"))
        {
            xvel = 7;
        }
        else
        {
            xvel = 0;
        }

        if (Input.GetKey("a"))
        {
            xvel = -7;
        }



        if (xvel >= 0.1f || xvel <= -0.1f)
        {
            anim.SetBool("isRunning", true);
        }
        else
        {
            anim.SetBool("isRunning", false);
        }





        //flip sprite
        if (xvel > 0)
        {
            helper.DoFlipObject(false);
        }
        if (xvel < 0)
        {
            helper.DoFlipObject(true);
        }




        //check for landing back on ground

        if( yvel < 0 && isGrounded )
        {
            anim.SetBool("isJumping", false);

        }




        //do the groundcheck
        if (ExtendedRayCollisionCheck(0, 0) == true)
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }


        rb.linearVelocity = new Vector3(xvel, yvel, 0);

    }

    public bool ExtendedRayCollisionCheck(float xoffs, float yoffs)
    {

        float rayLength = 0.5f; // length of raycast
        bool hitSomething = false;

        Vector3 offset = new Vector3(xoffs, yoffs, 0);


        //cast a ray downward
        RaycastHit2D hit;

        hit = Physics2D.Raycast(transform.position, Vector2.down, rayLength, groundLayer);

        Color hitColor = Color.white;


        if (hit.collider != null)
        {
            print("Player has collided with ground layer ");
            hitColor = Color.green;
            hitSomething = true;
        }
        Debug.DrawRay(transform.position + offset, -Vector3.up * rayLength, hitColor);
        return hitSomething;
    }



    private void OnCollisionEnter2D(Collision2D col)
    {
        print("tag=" + col.gameObject.tag);

        if( col.gameObject.tag == "rock")
        {
            print("I've been hit by a rock!");
        }
    }

     
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("coin"))
        {
            Destroy(other.gameObject);
            cm.coinCount++;
        }
                
    }
}



