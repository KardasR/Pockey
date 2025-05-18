using UnityEngine;

public class Player : MonoBehaviour
{
    #region Fields

    [SerializeField]
    private float skatingSpeed;

    [SerializeField]
    private float shootingSpeed;

    [SerializeField]
    private float sprintModifier;

    [SerializeField]
    private Transform puckHoldPoint;

    private Rigidbody2D body;

    private Vector2 skatingDirection;

    private bool isSprinting;

    private Puck heldPuck;

    #endregion Fields

    #region Private Methods

    private void Skate()
    {
        body.linearVelocity = skatingDirection * (isSprinting ? skatingSpeed * sprintModifier : skatingSpeed);
    }

    private void ShootPuck()
    {
        heldPuck.Release(skatingDirection * shootingSpeed);
        heldPuck = null;
    }

    private void MovePuck()
    {
        if (heldPuck != null)
        {
            heldPuck.transform.position = puckHoldPoint.position;
        }
    }

    #endregion Private Methods

    #region Public Methods

    public void TryPickupPuck(Puck puck)
    {
        if (heldPuck == null && puck.IsPickupable)
        {
            heldPuck = puck;
            puck.Hold();
        }
    }

    #endregion

    #region Core Unity Methods

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        skatingDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        if (Input.GetKeyDown(KeyCode.LeftShift))
            isSprinting = true;

        if (Input.GetKeyUp(KeyCode.LeftShift))
            isSprinting = false;

        if (Input.GetKeyDown(KeyCode.Space) && heldPuck != null)
            ShootPuck();
    }

    void FixedUpdate()
    {
        Skate();
        MovePuck();
    }

    #endregion Core Unity Methods
}
