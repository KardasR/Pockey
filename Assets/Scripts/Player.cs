using UnityEngine;

public class Player : MonoBehaviour
{
    #region Serialized Fields

    [Header("Movement Settings")]
    
    /// <summary>
    /// Base movement speed.
    /// </summary>
    [SerializeField]
    private float skatingSpeed;

    /// <summary>
    /// Multiplier applied to the skating speed when sprinting.
    /// </summary>
    [SerializeField]
    private float sprintModifier;

    [Header("Shooting Settings")]

    /// <summary>
    /// Speed the puck is shoot.
    /// </summary>
    [SerializeField]
    private float shootingSpeed;
    
    [Header("References")]

    /// <summary>
    /// Where the puck is held when picked up.
    /// </summary>
    [SerializeField]
    private Transform puckHoldPoint;

    #endregion Serialized Fields

    #region Private Fields

    /// <summary>
    /// Rigidbody2D component of the player.
    /// </summary>
    private Rigidbody2D body;

    /// <summary>
    /// The direction the player is moving.
    /// </summary>
    private Vector2 skatingDirection;

    /// <summary>
    /// Is the player currently sprinting?
    /// </summary>
    private bool isSprinting;

    /// <summary>
    /// Reference to the held puck if there is one.
    /// </summary>
    private Puck heldPuck;

    /// <summary>
    /// Animator to control animations of the player.
    /// </summary>
    private Animator animator;

    /// <summary>
    /// Keep track of this here for when the user is not pressing a key.
    /// </summary>
    private bool facingLeft;

    #endregion Private Fields

    #region Private Methods

    /// <summary>
    /// Movement method for the player.
    /// </summary>
    private void Skate()
    {
        body.linearVelocity = skatingDirection * (isSprinting ? skatingSpeed * sprintModifier : skatingSpeed);
    }

    /// <summary>
    /// Method to shoot the currently held puck in the same direction as the player.
    /// </summary>
    private void ShootPuck()
    {
        heldPuck.Release(shootingSpeed * (skatingDirection == Vector2.zero ? Vector2.left : skatingDirection));
        heldPuck = null;
    }

    /// <summary>
    /// Update the animations so they're the correct direction.
    /// </summary>
    private void UpdateAnimator()
    {
        bool isMoving = skatingDirection.sqrMagnitude > 0.01f;
        facingLeft = isMoving ? skatingDirection.x < 0: facingLeft;

        animator.SetBool("isMoving", isMoving);
        animator.SetBool("facingLeft", facingLeft);
    }

    #endregion Private Methods

    #region Public Methods

    /// <summary>
    /// Try to pickup a puck when one is encountered.
    /// </summary>
    /// <param name="puck"></param>
    public void TryPickupPuck(Puck puck)
    {
        if (heldPuck == null && puck.IsPickupable)
        {
            heldPuck = puck;
            puck.Hold(puckHoldPoint);
        }
    }

    #endregion

    #region Core Unity Methods

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        skatingDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        isSprinting = Input.GetKey(KeyCode.LeftShift);

        if (Input.GetKeyDown(KeyCode.Space) && heldPuck != null)
            ShootPuck();

        UpdateAnimator();
    }

    // Called at fixed intervals for physics updates
    void FixedUpdate()
    {
        Skate();
    }

    #endregion Core Unity Methods
}
