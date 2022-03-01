using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public float runForce = 50f;
    public float jumpForce = 20f;
    public float jumpBonus = 60f;
    public float maxRunSpeed = 6f;

    public bool touchingGround;
    
    private Rigidbody body;
    private Collider collider;
    private Animator animatorComponent;
    
    void Start()
    {
        body = GetComponent<Rigidbody>();
        collider = GetComponent<Collider>();
        animatorComponent = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // Logic for touching ground
        float castDistance = collider.bounds.extents.y + 0.1f;
        touchingGround = Physics.Raycast(transform.position, Vector3.down, castDistance);

        // Logic for left/right movement
        float axis = Input.GetAxis("Horizontal");
        body.AddForce(Vector3.right * axis * runForce, ForceMode.Force);

        // Jumping
        if (touchingGround && Input.GetKeyDown(KeyCode.Space))
        {
            body.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            animatorComponent.SetBool("Jumping", true);
        }
        else if (body.velocity.y > 0f && Input.GetKey(KeyCode.Space))
        {
            body.AddForce(Vector3.up * jumpBonus, ForceMode.Force);
            animatorComponent.SetBool("Jumping", true);
        }
        
        // Speed clamp & Slowdown
        if (Mathf.Abs(body.velocity.x) >= maxRunSpeed)
        {
            float tempX = maxRunSpeed * Mathf.Sign(body.velocity.x);
            body.velocity = new Vector3(tempX, body.velocity.y, body.velocity.z);
        }

        if (Mathf.Abs(axis) < 0.1f)
        {
            float tempX = body.velocity.x * (1f - Time.deltaTime * 5f);
            body.velocity = new Vector3(tempX, body.velocity.y, body.velocity.z);
        }

        // Animation logic
        if (touchingGround)
        {
            animatorComponent.SetBool("Jumping", false);
        }

        animatorComponent.SetFloat("Speed", body.velocity.magnitude);
    }
}
