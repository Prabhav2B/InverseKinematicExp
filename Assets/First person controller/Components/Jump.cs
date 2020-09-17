using UnityEngine;

public class Jump : MonoBehaviour
{
    [SerializeField]
    GroundCheck groundCheck;
    Rigidbody rigidbody;
    public float jumpStrength = 2;
    public event System.Action Jumped;


    void Reset()
    {
        groundCheck = GetComponentInChildren<GroundCheck>();
        if (!groundCheck)
            groundCheck = GroundCheck.Create(transform);
    }

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    void AlterGravity()
    {
        //rigidbody.AddForce(-Vector3.up * 3 * jumpStrength, ForceMode.Impulse);
    }

    void LateUpdate()
    {
        if (Input.GetButtonDown("Jump") && groundCheck.isGrounded)
        {
            rigidbody.AddForce(Vector3.up * 3 * jumpStrength, ForceMode.Impulse);
            Jumped?.Invoke();
            Invoke("AlterGravity", 0.5f);
        }
    }
}
