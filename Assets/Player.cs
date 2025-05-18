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

    private Rigidbody2D body;

    private Vector2 skatingDirection;

    private bool isSprinting;

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

        //body.AddForce(movementDirection, ForceMode2D.Force);
    }

    void FixedUpdate()
    {
        body.linearVelocity = skatingDirection * (isSprinting ? skatingSpeed * sprintModifier : skatingSpeed);
    }

    #endregion Core Unity Methods
}
