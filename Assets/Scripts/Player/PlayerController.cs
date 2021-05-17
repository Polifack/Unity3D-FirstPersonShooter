using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float movementSpeed = 5f;
    public int maxHP = 100;
    private int currentHP;
    public float invulnTime = 0.5f;
    private float invulnTimeCtr = 0;

    // Movement management
    private Rigidbody rigidBody;
    private Transform mainCamera;
    private Vector3 cameraDirection;             
    private Vector3 movementVector;

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        mainCamera = Camera.main.transform;
        currentHP = maxHP;
    }

    public void doTakeDamage(int ammount)
    {
        // If is not vulnerable return 
        if (invulnTimeCtr < invulnTime) return;

        AudioManager.instance.playPlayerHurtSFX();
        currentHP -= ammount;
        currentHP = Mathf.Clamp(currentHP, 0, maxHP);
        if (currentHP == 0)
        {
            GameStateManager.instance.toState(new DefeatState());
        }
        UIManager.instance.setHealth(currentHP);
    }

    public void doTakeHealing(int ammount)
    {
        currentHP += ammount;
        currentHP = Mathf.Clamp(currentHP, 0, maxHP);
        UIManager.instance.setHealth(currentHP);
    }

    private void Move(Vector3 movement)
    {
        Vector3.ClampMagnitude(movement, movementSpeed);

        if (movement.x != 0 || movement.y != 0)
        {
            Camera.main.gameObject.GetComponentInParent<CameraController>().doWobble(true);
        }
        else
        {
            Camera.main.gameObject.GetComponentInParent<CameraController>().doWobble(false);
        }

        // Aplicar la velocidad relativa a timedeltatime
        rigidBody.MovePosition(transform.position + movement * Time.deltaTime * movementSpeed);

    }
    private void Update()
    {
        //Obtener inputs
        float xMovement = Input.GetAxis("Horizontal");
        float yMovement = Input.GetAxis("Vertical");

        //Calcular direccion movimiento respecto a la camara
        cameraDirection = Vector3.Scale(mainCamera.forward, new Vector3(1, 1, 0)).normalized;
        movementVector = yMovement * cameraDirection + xMovement * mainCamera.right;

        // Update invulnerability 
        if (invulnTimeCtr<invulnTime)
        invulnTimeCtr += Time.deltaTime;

        //Aplicamos el movimiento
        Move(movementVector);
    }
}
