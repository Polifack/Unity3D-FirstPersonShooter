using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float movementSpeed = 5f;
    public float m_GroundDistance = 2f;
    public float m_JumpPower = 1f;
    public float groundCheckDistance = 1f;
    private Vector3 m_GroundNormalVector;

    // This should be moved outta here asap
    // Below vector should not be static, must be below the character
    public LayerMask whatIsGround;
    private Vector3 below = new Vector3(0, 0, 1);

    private Rigidbody m_RigidBody;
    private Transform m_MainCamera;
    private bool m_IsGrounded;
    private float m_GroundAngle;
    private Vector3 m_CamForward;             //Direccion de movimiento
    private Vector3 m_Move;

   


    private void Start()
    {
        m_RigidBody = GetComponent<Rigidbody>();
        m_MainCamera = Camera.main.transform;

        if (m_RigidBody == null)
        {
            Debug.LogWarning("[Warning]: no rigidbody found.");
        }
        if (Camera.main == null)
        {
            Debug.LogWarning("[Warning]: no main camera found.");
        }
    }

    private void Move(Vector3 movement, bool jump)
    {
        //Si el movimiento es mayor que 1 (por ejemplo, diagonales) lo normalizamos.
        if (movement.magnitude > 1f) movement.Normalize();

        CheckGround();
        CalculateGroundAngle();


        if (m_IsGrounded && jump)
        {
            Debug.Log(m_IsGrounded);
            HandleJump();
        }
        if (m_IsGrounded)
        {
            HandleGroundMovement(movement);
        }
        else
        {
            HandleAirMovement(movement);
        }
    }

    private void HandleGroundMovement(Vector3 movement)
    {
        // Check movement for wobbling
        if (movement.x != 0 || movement.y != 0)
        {
            // change this to the gamemanager or something please this is terrible 
            Camera.main.gameObject.GetComponentInParent<CameraController>().doWobble(true);
        }
        else
        {
            Camera.main.gameObject.GetComponentInParent<CameraController>().doWobble(false);
        }

        // Aplicar la velocidad en el suelo
        m_RigidBody.MovePosition(transform.position + movement * Time.deltaTime * movementSpeed);

    }

    private void HandleAirMovement(Vector3 movement)
    {
        // Disable wobbling on air
        Camera.main.gameObject.GetComponentInParent<CameraController>().doWobble(false);
        
        // Aplicar la velocidad aerea
        m_RigidBody.MovePosition(transform.position + movement * Time.deltaTime * movementSpeed);
    }

    private void HandleJump()
    {
        // Para saltar simplemente modificar la velocidad del rigidbody

        m_RigidBody.velocity = new Vector3(
            m_RigidBody.velocity.x,
            m_RigidBody.velocity.y,
            -m_JumpPower);

        m_IsGrounded = false;
    }

    private void CheckGround()
    {
        
        RaycastHit hitInfo;
        bool collision = Physics.Raycast(
            transform.position+new Vector3(0,0,-groundCheckDistance),
            below,
            out hitInfo,
            m_GroundDistance
        );
        
        // Check if collision and if the collider is inside the layer mask "what is ground"
        // Remark: layermasks and masks work as bitmasks
        if (collision && ((whatIsGround & 1 << hitInfo.collider.gameObject.layer) == 1 << hitInfo.collider.gameObject.layer))
        {
            m_IsGrounded = true;
            m_GroundNormalVector = hitInfo.normal;
        }
        else
        {
            m_IsGrounded = false;
            m_GroundNormalVector = Vector3.up;
        }
    }

    private void CalculateGroundAngle()
    {
        if (!m_IsGrounded)
        {
            m_GroundAngle = 90f;
            return;
        }
        else
        {
            m_GroundAngle = Vector3.Angle(m_GroundNormalVector, transform.forward);
        }
    }

    private void Update()
    {
        //Obtener inputs
        float xMovement = Input.GetAxis("Horizontal");
        float yMovement = Input.GetAxis("Vertical");

        //Calcular direccion movimiento respecto a la camara
        m_CamForward = Vector3.Scale(m_MainCamera.forward, new Vector3(1, 1, 0)).normalized;
        m_Move = yMovement * m_CamForward + xMovement * m_MainCamera.right;

        bool jump = Input.GetKeyDown(KeyCode.Space);

        //Aplicamos el movimiento
        Move(m_Move, jump);
    }
}
