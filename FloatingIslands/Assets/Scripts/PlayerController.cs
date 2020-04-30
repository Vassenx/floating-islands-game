using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    /*Interactions*/
    private Interact focus;
    [SerializeField] private float interactDist = 15f;

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
    private float ySpeed = 0f;
    [SerializeField] private float gravity = 9f;

    /*Grounding player
    private float interactGroundRadius = 0.5f;
    private bool isGrounded = false;
    private float halfHeightOfPlayer;
    [SerializeField] private LayerMask groundMask;*/

    private void Start()
    {
        //halfHeightOfPlayer = GetComponent<Collider>().bounds.extents.y;
        //groundMask = LayerMask.NameToLayer("Ground");
        curJumpSpeed = jumpSpeed;
    }

    void Update()
    {
        RotateHorizontal();

        //isGrounded = Physics.CheckSphere(transform.position - Vector3.up * halfHeightOfPlayer, interactGroundRadius, groundMask, QueryTriggerInteraction.Ignore);
        if (charController.isGrounded && velocity.y < 0)
        {
            velocity.y = 0f;
        }

        velocity = Input.GetAxis("Vertical") * transform.forward * speed;

        Jump();

        velocity.y -= gravity * Time.deltaTime;
        charController.Move(velocity * Time.deltaTime);

        /*Interactions*/
        if (Input.GetKeyDown(KeyCode.R) && IsInteracting())
        {
            focus.TryInteract(gameObject);
        }

    }

    private bool IsInteracting()
    {
        RaycastHit raycastHit;
        Vector3 infrontPos = transform.forward * interactDist;

        if (Physics.Raycast(transform.position, transform.position - infrontPos, out raycastHit, 50f))
        {

            focus = raycastHit.transform.GetComponent<Interact>();
            if (focus != null)
            {
                return true;
            }
        }
        else if (Physics.Raycast(transform.position, transform.position + infrontPos, out raycastHit, 50f))
        {
            //for the tall objs
            focus = raycastHit.transform.GetComponent<Interact>();
            if (focus != null)
            {
                return true;
            }
        }

        return false;
    }

    //rotate for horizontal
    private void RotateHorizontal()
    {
        var yaw = Input.GetAxis("Horizontal");


        if (Mathf.Abs(yaw) > Mathf.Epsilon)
        {
            transform.Rotate(Vector3.up, yaw * Time.deltaTime * rotSpeed);
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
