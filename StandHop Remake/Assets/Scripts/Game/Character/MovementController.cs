using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    [SerializeField] private float m_StepInterval;
    [SerializeField] private float walkingSpeed = 7.5f;
    [SerializeField] private float gravity = 20.0f;
    [SerializeField] private float default_height = 1.7f;
    [SerializeField] private float crouch_height = 0.7f;
    [SerializeField] private float defaultspeed = 4f;
    [SerializeField] private float crouchspeed = 2f;
    [SerializeField] private float _crouchspeed = 0.5f;
    [SerializeField] private float _crouchdele = 1.5f;
    [SerializeField] private bool _crouch;
    [SerializeField] private bool _jump;
    [SerializeField] private LayerMask LayerMask;
    
    [SerializeField] private GameObject playerCamera, CheckGround;
    [SerializeField] private float lookSpeed = 2.0f;
    [SerializeField] private float lookXLimit = 45.0f;
    [SerializeField] private float jumpHeight = 3f; 

    public float crouch_del;

    private bool canJump = true;
    private float m_NextStep;
    private float m_StepCycle;
    
    [SerializeField] private Animator Arms;

    [SerializeField] private AudioClip[] FootStepsStone, FootStepsGrass, FootStepsWood, FootStepsMetal;
    [SerializeField] private AudioSource source;

    [SerializeField] private GameObject UnCrouchButton, CrouchButton;

    [HideInInspector]
    public Vector2 RunAxis;

    public Vector2 LookAxis;

    CharacterController characterController;
    Vector3 moveDirection = Vector3.zero;
    float rotationX = 0;

    [HideInInspector]
    public bool canMove = true;


    void Start()
    {
        m_StepCycle = 0f;
        m_NextStep = m_StepCycle/2f;
        characterController = GetComponent<CharacterController>();
    }

    private void FixedUpdate() 
    {
        if(_crouch == true)
        {
            characterController.height = Mathf.Lerp(characterController.height, crouch_height, Time.deltaTime * _crouchspeed);
            crouch_del = _crouchdele;
        }     
        if(_crouch == false)
        {
            characterController.height = Mathf.Lerp(characterController.height, default_height, Time.deltaTime * _crouchspeed);
            crouch_del = 1;
        }   
        WalkAudio(5f);
    }

    void Update()
    {
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);
        float curSpeedX = canMove ? (walkingSpeed) * SimpleInput.GetAxis("Vertical") : 0;
        float curSpeedY = canMove ? (walkingSpeed) * SimpleInput.GetAxis("Horizontal") : 0;
        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);
        moveDirection.y = movementDirectionY;

        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }

        if(characterController.isGrounded)
        {
            if (moveDirection.x !=0 || moveDirection.z !=0)
            {
                Arms.GetComponent<Animator>().SetBool("Walk", true);
            }
            else Arms.GetComponent<Animator>().SetBool("Walk", false);
        }
        else Arms.GetComponent<Animator>().SetBool("Walk", false);

        characterController.Move(moveDirection * Time.deltaTime);

        if (canMove)
        {
            rotationX += - SimpleInput.GetAxis("LookY") * lookSpeed;
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            transform.rotation *= Quaternion.Euler(0, SimpleInput.GetAxis("LookX") * lookSpeed, 0);     
        }
        if(_jump == true)
        {
            Jump();
        }
    }

    public void JumpButton(bool value)
    {
        _jump = value;
    }

    public void Jump()
    {
        if(canJump == true)
        {
            if(characterController.isGrounded)
            {
                moveDirection.y = jumpHeight;
            }
        }
    }

    public void Crouching()
    {
        if(_crouch == false)
        {
            walkingSpeed = crouchspeed;
            canJump = false;
            UnCrouchButton.SetActive(true);
            CrouchButton.SetActive(false);
            _crouch = true;
            return;
        }
        if(_crouch == true)
        {
            walkingSpeed = defaultspeed;
            canJump = true;
            UnCrouchButton.SetActive(false);
            CrouchButton.SetActive(true);
            _crouch = false;
        }
    }

    private void PlayFootStepAudio()
    {
        if (!characterController.isGrounded) { return; }

        RaycastHit hit;
        if(Physics.Raycast(CheckGround.transform.position, CheckGround.transform.forward, out hit, 1, LayerMask))
        {
            Debug.Log("Finded");
            if(CheckGround.transform.gameObject.tag == "Concrete")
            {
                int n = Random.Range(1, FootStepsStone.Length);
                source.clip = FootStepsStone[n];
                source.PlayOneShot(source.clip);
                FootStepsStone[n] = FootStepsStone[0];
                FootStepsStone[0] = source.clip;
            }
            if(CheckGround.transform.gameObject.tag == "Wood")
            {
                int n = Random.Range(1, FootStepsWood.Length);
                source.clip = FootStepsWood[n];
                source.PlayOneShot(source.clip);
                FootStepsWood[n] = FootStepsWood[0];
                FootStepsWood[0] = source.clip;
            }
            if(CheckGround.transform.gameObject.tag == "Grass")
            {
                int n = Random.Range(1, FootStepsGrass.Length);
                source.clip = FootStepsGrass[n];
                source.PlayOneShot(source.clip);
                FootStepsGrass[n] = FootStepsGrass[0];
                FootStepsGrass[0] = source.clip;
            }
            if(CheckGround.transform.gameObject.tag == "Metal")
            {
                int n = Random.Range(1, FootStepsMetal.Length);
                source.clip = FootStepsGrass[n];
                source.PlayOneShot(source.clip);
                FootStepsMetal[n] = FootStepsMetal[0];
                FootStepsMetal[0] = source.clip;
            }
        }
    }

    private void WalkAudio(float speed)
    {
        if(_crouch == false)
        {
            if (characterController.velocity.sqrMagnitude > 0 && moveDirection.x != 0 || moveDirection.z != 0)
            {
                m_StepCycle += characterController.velocity.magnitude + speed * Time.fixedDeltaTime;
            }

            if (!(m_StepCycle > m_NextStep))
            {
                return;
            }

            m_NextStep = m_StepCycle + m_StepInterval;

            PlayFootStepAudio();
        }
    }    
}
