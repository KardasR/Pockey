using System;
using UnityEngine;
using UnityEngine.UIElements;

public class Puck : MonoBehaviour
{
    #region Fields

    /// <summary>
    /// How long to wait until allowing the puck to be picked up.
    /// </summary>
    [SerializeField]
    private float pickupCooldownDuration;

    /// <summary>
    /// Time left before cooldown time is over.
    /// </summary>
    private float pickupCooldownTimeleft;

    /// <summary>
    /// Rigidbody2D component of the puck.
    /// </summary>
    private Rigidbody2D body;

    /// <summary>
    /// Is the puck currently held?
    /// </summary>
    private bool isHeld = false;
    
    /// <summary>
    /// Can the puck be picked up?
    /// <para/>
    /// Can't be held or have time left on the cooldown timer.
    /// </summary>
    public bool IsPickupable => !isHeld && pickupCooldownTimeleft <= 0f;

    #endregion Fields

    #region Public Methods

    /// <summary>
    /// Turn off physics so the puck can be carried easier. Mark the puck is held.
    /// </summary>
    public void Hold(Transform holdPoint)
    {
        isHeld = true;

        // stop physics
        body.simulated = false;

        // Attach to the puck hold point
        transform.SetParent(holdPoint);
        transform.localPosition = Vector3.zero;
    }

    /// <summary>
    /// Shoot the puck with the given force. Mark the puck is not held.
    /// </summary>
    /// <param name="force">Force to impulse push the puck with.</param>
    public void Release(Vector2 force)
    {
        pickupCooldownTimeleft = pickupCooldownDuration;

        isHeld = false;
        transform.SetParent(null);

        // turn physics back on
        body.simulated = true;
        body.linearVelocity = Vector2.zero; // avoid stacking forces
        body.AddForce(force, ForceMode2D.Impulse);
    }

    #endregion Public Methods

    #region Core Unity Methods

    //
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
