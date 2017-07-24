using UnityEngine;
using System.Collections;

public class EnemyBirdController : MonoBehaviour
{

    Rigidbody2D playerRB;
	private float speed = 1f;
    Animator anim;
    bool facingRight;
    public LayerMask groundLayer;
    private float jumpForce = .5f;

	private OstrichPlayerController thePlayer;
	public float playerRange;

	public LayerMask playerLayer;
	public bool playerInRange;
	public bool vulnearble = true;

	//Only allow enemy to jump twice before touching the ground again
	//true - can jump, false - cannot
	//ex. jump1 = false, jump2 = true --> in air (one jump) can jump again.
	bool jump1, jump2 = true;

    // Use this for initialization
    void Start()
    {
        facingRight = true;
        playerRB = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        anim.SetBool("Flying", false); //set to not fly at first
        anim.SetBool("Walking", false); //not walking at first

		thePlayer = FindObjectOfType<OstrichPlayerController> ();
    }


    // Update is called once per frame
    void Update()
    {
		//checks if player is in enemy range
		playerInRange = Physics2D.OverlapCircle (transform.position, playerRange, playerLayer);

		if (playerInRange) {
			//makes the enemy move towards the player if it is in range
			transform.position = Vector3.MoveTowards (transform.position, thePlayer.transform.position, speed * Time.deltaTime);
		}
	}

    void FixedUpdate()
    {
		
		playerRB.velocity = new Vector2(speed, 0);

		//If the player is in range, run code from Update method
		if (playerInRange)
			return;

		int random = Random.Range (0, 10);
		if (random > 6) {
			//Don't jump if above specific Y pos
			if (this.transform.position.y < 228.8f) {
				Flap ();
			}
		}
			
		//If enemy is grounded, reset jumps and set animator booleans
		if (IsGrounded ()) {
			//Reset jumps
			jump1 = true;
			jump2 = true;

			anim.SetBool ("Walking", true);
			anim.SetBool ("Flying", false);

		} else {
			anim.SetBool("Walking", false);
			anim.SetBool("Flying", true);
		}
		
    }

	//Flip if enemy collides with ground or other objects (other than LeftSide and RightSide wrapping compnents)
	void OnTriggerEnter2D(Collider2D other)
	{
		//Debug.Log ("collide with " + other.tag);
		if (other.tag == "VulnerableSpot") {
			vulnearble = false;
		}
		if (! (other.tag == "Side") ) {
			speed *= -1;
			playerRB.velocity = new Vector2 (speed, 0);
			Flip ();
		}
	}
	void OnTriggerExit2D(Collider2D other)
	{
		if (other.tag == "VulnerableSpot") {
			vulnearble = true;
		}
	}
		
    void Flip()
    {
        //flips player in the direction its facing
        facingRight = !facingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    public void Flap()
    {
       // Debug.Log("flapping");
        playerRB.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        anim.SetBool("Walking", false);
        anim.SetBool("Flying", true);
    }

    bool IsGrounded()
    {

        if (Physics2D.Raycast(this.transform.position, Vector2.down, 0.2f, groundLayer.value))
        {
            return true;
        }
        else
        {
            return false;
        }

    }

	void OnDrawGizmosSelected(){
		
		//draws circle that will follow player if it is in that range
		Gizmos.DrawSphere (transform.position, playerRange);

	}
	

}
