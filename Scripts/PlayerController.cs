using UnityEngine;

public class PlayerController : MonoBehaviour
{
	public float speed = 7f;

	Vector3 movement;
	public Animator anim;
	Rigidbody playerRigidbody;
    int floorMask;
    float camRayLenght = 100f;

    Transform cam;
    Vector3 camForward;
    Vector3 move;
    Vector3 moveInput;

    float forwardAmount;
    float turnAmount;

    void Awake ()
	{
        floorMask = LayerMask.GetMask("Floor");

		playerRigidbody = GetComponent <Rigidbody> ();

        cam = Camera.main.transform;

	}

	void FixedUpdate ()
	{
		float h = Input.GetAxisRaw ("Horizontal");
		float v = Input.GetAxisRaw ("Vertical");

        if (cam != null)
        {
            camForward = Vector3.Scale(cam.up, new Vector3(1, 0, 1));
            move = v * camForward + h * cam.right;
        }
        else
        {
            move = v * Vector3.forward + h * Vector3.right;
        }
        if (move.magnitude > 1) {
            move.Normalize();
        }

		Animating(move);

		Turning ();
        Move(h,v);

	}

	void Move (float h, float v)
	{
		movement.Set (h, 0, v);
		movement = movement.normalized * speed * Time.deltaTime;
        Vector3 transform = new Vector3(base.transform.position.x,
            base.transform.position.y, base.transform.position.z);
		playerRigidbody.MovePosition (transform + movement);
	}

	void Turning ()
	{
        Ray CamRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit floorHit;

        if(Physics.Raycast(CamRay, out floorHit, camRayLenght, floorMask))
        {
            Vector3 playerToMouse = floorHit.point - transform.position;

            playerToMouse.y = 0f;

            Quaternion newRotation = Quaternion.LookRotation(playerToMouse);

            playerRigidbody.MoveRotation(newRotation);
        }

	}

	void Animating (Vector3 move)
	{
		
		if(move.magnitude > 1)
        {
            move.Normalize();
        }

        this.moveInput = move;

        ConvertMoveInput();
        UpdateAnimator();
        
	}

    void ConvertMoveInput() {
        Vector3 localMove = transform.InverseTransformDirection(moveInput);
        turnAmount = localMove.x;

        forwardAmount = localMove.z;
    }

    void UpdateAnimator()
    {
        anim.SetFloat("Forward", forwardAmount, 0.1f, Time.deltaTime);
        anim.SetFloat("Turn", turnAmount, 0.1f, Time.deltaTime);
    }

}