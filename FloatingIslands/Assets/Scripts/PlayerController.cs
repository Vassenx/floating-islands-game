using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    /*Basic movement*/
    [SerializeField] private CharacterController charController;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float rotSpeed = 50f;
    private Vector3 movement = Vector3.zero;

    /*Jump*/
    [SerializeField] private float jumpSpeed = 0.2f;
    [SerializeField] private float jumpDuration = 3f; //how long the jump can last
    [SerializeField] private float jumpCooldown = 2f; //how long til next jump
    [SerializeField] private float jumpDampening = 0.1f;
    private float curJumpSpeed;
    private float curJumpDuration = 0f;
    private float curJumpCooldown = 0f;

    /*Velocity = external forces here*/
    private Vector3 velocity = Vector3.zero; //for jump/gravity
    [SerializeField] private float gravity = 9f;

    /*Grounding player*/
    private float interactGroundRadius = 0.5f;
    private bool isGrounded = false;
    private float halfHeightOfPlayer;
    [SerializeField] private LayerMask groundMask;

    private void Start()
    {
        halfHeightOfPlayer = GetComponent<Collider>().bounds.extents.y;
        groundMask = LayerMask.NameToLayer("Ground");
        curJumpSpeed = jumpSpeed;
    }

    void Update()
    {
        BasicMovement();

        isGrounded = Physics.CheckSphere(transform.position - Vector3.up * halfHeightOfPlayer, interactGroundRadius, groundMask, QueryTriggerInteraction.Ignore);
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = 0f;
        }

        Jump();

        velocity.y -= gravity;
        charController.Move(velocity * Time.deltaTime);

    }

    //rotate for horizontal, move forward/backwards for vertical
    private void BasicMovement()
    {
        var yaw = Input.GetAxis("Horizontal");

        if (Mathf.Abs(yaw) > Mathf.Epsilon)
        {
            transform.Rotate(Vector3.up, yaw * Time.deltaTime * rotSpeed);
        }
        else
        {
            yaw = 0f;
        }

        movement = Input.GetAxis("Vertical") * transform.forward;
        if (movement != Vector3.zero)
        {
            charController.Move(movement * Time.deltaTime * speed);
        }
    }

    //TODO: touch up this mechanic
    //like a rocket pack jump
    private void Jump()
    {

        //continue jumping
        if (Input.GetKey(KeyCode.Space) && curJumpDuration < jumpDuration)
        {
            curJumpDuration += Time.deltaTime;

            //cancel gravity, not realistic, but gravity is annoying
            velocity.y += gravity * Time.deltaTime;

            curJumpSpeed = Mathf.Max(0.1f, curJumpSpeed - jumpDampening);
            velocity.y += curJumpSpeed;
        }
        else
        {
            if (curJumpCooldown >= jumpCooldown)
            {
                curJumpCooldown = 0;
                curJumpDuration = 0;
                curJumpSpeed = jumpSpeed;
            }
            else
            {
                curJumpCooldown += Time.deltaTime;
            }
        }
    }

}
