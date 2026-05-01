using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerMovement : MonoBehaviour{
    [Header("Movement Settings")]
    public float speed = 8f;
    public float jumpForce = 5f;
    public float maxSpeed = 30f;
    public float friction = 2f;
    [Header("Camera Settings")]
    public float sensibility = 0.3f;
    [Header("Detection Settings")]
    public LayerMask groundLayer;
    Rigidbody body;
    CapsuleCollider col;
    bool isGrounded;
    bool onSlope;
    bool wasOnSlope; // para detectar el frame en que se sale del slope
    //Crouch
    float originalHeight;
    float crouchYscale = 0.5f;
    float crouchSpeed = 6.5f;
    bool Couching = false;
    Vector3 groundNormal = Vector3.up;
    Vector2 moveInput;
    bool isSprinting = false;
    float jumpBufferCounter = 0f;
    float jumpBufferTime = 0.15f;
    bool justJumped = false;
    float coyoteTime = 0.12f; // segundos de margen para saltar al caer de un borde
    float coyoteCounter = 0f;
    float yaw;

    void Start()
    {
        body = GetComponent<Rigidbody>();
        col = GetComponent<CapsuleCollider>();
        originalHeight = col.height;
    }
    void Update()
    {
        body.MoveRotation(Quaternion.Euler(0f, yaw, 0f));
        if (jumpBufferCounter > 0f)
            jumpBufferCounter -= Time.deltaTime;
        if (coyoteCounter > 0f)
            coyoteCounter -= Time.deltaTime;
    }
    void OnMove(InputValue value)   => moveInput  = value.Get<Vector2>();
    void OnJump(InputValue value)
    {
        if (value.isPressed)
            jumpBufferCounter = jumpBufferTime;
    }
    void OnLook(InputValue value)
    {
        yaw += value.Get<Vector2>().x * sensibility;
    }
    void OnCrouch(InputValue value){
        if (value.isPressed){
            transform.localScale = new Vector3(transform.localScale.x, crouchYscale, transform.localScale.z);
            body.AddForce(Vector3.down * 5f, ForceMode.Impulse); // para pegar al suelo al agacharse
            speed = crouchSpeed;
            Couching = true;
            if (!isGrounded){
                body.AddForce(Vector3.up * 5f, ForceMode.Impulse);
            }
        }
        if (value.isPressed == false){
            transform.localScale = new Vector3(transform.localScale.x, originalHeight-1, transform.localScale.z);
            speed = 8f;
            Couching = false;
        }
    }
    void FixedUpdate(){
        CheckGround();
        // Coyote time
        if (isGrounded)
            coyoteCounter = coyoteTime;
        bool wantsToJump = jumpBufferCounter > 0f && coyoteCounter > 0f;
        //Movimiento
        float currentTargetSpeed = (isSprinting && moveInput.y > 0f) ? speed : speed;
        Vector3 inputDir = (transform.forward * moveInput.y + transform.right * moveInput.x).normalized;
        // Si hay input, aplicar fuerza para alcanzar la velocidad objetivo
        if (inputDir != Vector3.zero){
            // En slopes, proyectar el input para que siga la inclinación sin generar fuerza vertical
            Vector3 moveDir = onSlope ? Vector3.ProjectOnPlane(inputDir, groundNormal).normalized:inputDir;
            Vector3 targetVel = moveDir * currentTargetSpeed;
            Vector3 currentFlat = new Vector3(body.linearVelocity.x, 0f, body.linearVelocity.z);
            Vector3 diff = targetVel - currentFlat;
            float forceMult = isGrounded ? 80f : 2f;
            // Solo componente horizontal para no generar velocidad vertical accidental
            body.AddForce(new Vector3(diff.x, 0f, diff.z) * forceMult, ForceMode.Force);
            // Deteccion de pared
            if (Physics.SphereCast(col.bounds.center, col.radius, inputDir, out RaycastHit wallHit, 0.05f))
            {
                if (Vector3.Angle(wallHit.normal, Vector3.up) > 60f)
                    body.linearVelocity = Vector3.ProjectOnPlane(body.linearVelocity, wallHit.normal);
            }
        }
        //Slopes y salto
        if (onSlope && isGrounded){
            if (wantsToJump){
                body.useGravity = true;
                onSlope=false; // para evitar que el salto se vea afectado por la pendiente
                body.linearVelocity = new Vector3(body.linearVelocity.x, 0f, body.linearVelocity.z);
                body.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                jumpBufferCounter = 0f;
                coyoteCounter = 0f;
                justJumped = true;
                //si se salta cuando el jugador le da a la w dar un impulso adicional para evitar que el salto se sienta pegado al suelo
                if (moveInput.y > 0f){
                    body.AddForce(transform.up * 4.8f, ForceMode.Impulse);
                }
            }
            else{
                // Pegar al slope para evitar deslizamiento
                body.useGravity = false;
                float slopeAngle = Vector3.Angle(groundNormal, Vector3.up);
                body.AddForce(-groundNormal * slopeAngle * 8f, ForceMode.Force);
            }
        }
        // En el aire o en suelo plano
        else{
            body.useGravity = true;
            // Al salir de un slope con velocidad alta se genera Y positiva no deseada:
            // cancelarla en el primer frame que se detecta la salida
            if (wasOnSlope && !onSlope && !isGrounded && !justJumped && body.linearVelocity.y > 0f)
                body.linearVelocity = new Vector3(body.linearVelocity.x, 0f, body.linearVelocity.z);
            // Salto normal
            if (wantsToJump){
                body.linearVelocity = new Vector3(body.linearVelocity.x, 0f, body.linearVelocity.z);
                body.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                jumpBufferCounter = 0f;
                coyoteCounter = 0f;
                justJumped = true;
            }
        }
        wasOnSlope = onSlope;
        // Drag y fricción
        if (justJumped){
            body.linearDamping = friction / 2f;
            justJumped = false;
        }
        else{
            body.linearDamping = isGrounded ? friction : 0.5f;
        }
        //Limite de velocidad
        Vector3 flatVel = new Vector3(body.linearVelocity.x, 0f, body.linearVelocity.z);
        if (flatVel.magnitude > maxSpeed){
            flatVel = flatVel.normalized * maxSpeed;
            body.linearVelocity = new Vector3(flatVel.x, body.linearVelocity.y, flatVel.z);
        }
    }
    void CheckGround(){
        Vector3 origin = col.bounds.center;
        float castDistance = (col.height / 3.4f) + 0.01f;
        if (Couching){
            castDistance= (col.height/20f) + 0.01f;
        }
        else{
            castDistance = (col.height / 3.4f) + 0.01f;
        }
        //Sphere cast al suelo
        if (Physics.SphereCast(origin, col.radius * 0.93f, Vector3.down, out RaycastHit hit, castDistance, groundLayer)){
            groundNormal = hit.normal;
            float angle = Vector3.Angle(groundNormal, Vector3.up);
            isGrounded = angle < 60f;
            onSlope = angle > 5f && isGrounded;
        }
        else{
            isGrounded = false;
            onSlope = false;
            groundNormal = Vector3.up;
            body.useGravity = true;
        }
    }
    void OnDrawGizmos(){
        if (col == null) return;
        Gizmos.color = Color.red;
        Vector3 origin = col.bounds.center;
        float castDistance = (col.height/3.4f) + 0.01f;
        Gizmos.DrawLine(origin, origin + Vector3.down * castDistance);
        Gizmos.DrawWireSphere(origin + Vector3.down * castDistance, col.radius * 0.93f);
    }
}