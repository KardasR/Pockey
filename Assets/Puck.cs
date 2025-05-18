using System;
using UnityEngine;

public class Puck : MonoBehaviour
{
    #region Fields

    [SerializeField]
    private float pickupCooldownDuration;

    private float pickupCooldownTimeleft;

    private Rigidbody2D body;

    private bool isHeld = false;

    #endregion Fields

    #region Public Properties

    public bool IsPickupable
    {
        get
        {
            return !isHeld && pickupCooldownTimeleft <= 0f;
        }
    }

    #endregion Public Properties

    #region Public Methods

    public void Hold()
    {
        isHeld = true;
        body.simulated = false;
    }

    public void Release(Vector2 force)
    {
        pickupCooldownTimeleft = pickupCooldownDuration;

        isHeld = false;
        body.simulated = true;
        body.AddForce(force, ForceMode2D.Impulse);
    }

    #endregion Public Methods

    #region Core Unity Methods

    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (pickupCooldownTimeleft > 0f)
            pickupCooldownTimeleft = MathF.Max(0, pickupCooldownTimeleft - Time.deltaTime);
    }
    
    #endregion Core Unity Methods
}
