using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Scripts")]
    [SerializeField] private GameManager gameManagerScript;

    [Header("Move Horizontal")]
    [SerializeField] private float horizontalSpeed; //horizontal movement speed
    [SerializeField] private float horizontalLimit; //horizontal movement limits (left and right)

    [Header("Move Forward")]
    [SerializeField] private float forwardSpeed; //forward movement speed

    [Header("Rotation Values")]
    [SerializeField] private float lerpValue;
    [SerializeField] private float rotationValueY; //max rotation value

    private float _horizontalValue;
    private float _newPositionX;

    private Rigidbody _rb;
    private Animator _anim;

    private bool isTouch = false;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _anim = GetComponent<Animator>();
        gameManagerScript.OnStartGame += () => SetKinematic(false);
        gameManagerScript.OnRestartGame += () => SetKinematic(false);
        _anim.speed = 2f;
    }
    private void Update()
    {
        if (gameManagerScript.gameIsStarted && !gameManagerScript.isLosed && !gameManagerScript.isFinished)
        {
            AnimationControl("IsRun", true);
            AnimationControl("IsDead", false);
            AnimationControl("IsFinish", false);
           
        }
        else if (gameManagerScript.isLosed)
        {
            AnimationControl("IsDead", true);
        }
        else if (gameManagerScript.isFinished)
        {
            AnimationControl("IsFinish", true);
        }
    }

    private void FixedUpdate()
    {
        if (gameManagerScript.gameIsStarted && !gameManagerScript.isLosed && !gameManagerScript.isFinished)
        {
            CheckPlayerInput();
            CheckHorizontalValue();
            SetPlayerRotation();
            SetPlayerMovementForward();
            SetPlayerMovementHorizontal();
        }
    }
    private void CheckPlayerInput()
    {
        if (Input.GetMouseButton(0))
        {
            isTouch = true;
        }
        else
        {
            isTouch = false;
        }
    }
    private void CheckHorizontalValue()
    {
        if (isTouch)
        {
            _horizontalValue = Input.GetAxisRaw("Mouse X");
        }
        else
        {
            _horizontalValue = 0;
        }
    }
    private void SetPlayerMovementHorizontal()
    {
        _newPositionX = transform.position.x + _horizontalValue * horizontalSpeed * Time.fixedDeltaTime;
        _newPositionX = Mathf.Clamp(_newPositionX, -horizontalLimit, horizontalLimit); //horizontal movement limitation

        transform.position = new Vector3(_newPositionX, transform.position.y, transform.position.z);
    }

    private void SetPlayerMovementForward()
    {
        _rb.velocity = forwardSpeed * Time.fixedDeltaTime * Vector3.forward; //move player forward
    }

    //rotation to mouse position
    private void SetPlayerRotation()
    {
        if (isTouch)
        {
            if (_horizontalValue > 0)
            {
                transform.rotation = 
                    Quaternion.Slerp(transform.rotation, Quaternion.Euler(0f, rotationValueY, 0f), lerpValue * Time.deltaTime);
            }
            else if (_horizontalValue < 0)
            {
                transform.rotation = 
                    Quaternion.Slerp(transform.rotation, Quaternion.Euler(0f, -rotationValueY, 0f), lerpValue * Time.deltaTime);
            }
        }
        else
        {
            transform.rotation =
                Quaternion.Slerp(transform.rotation, Quaternion.Euler(0f, 0f, 0f), lerpValue * Time.deltaTime);
        }
    }

    public void SetKinematic(bool _boolean)
    {
        _rb.isKinematic = _boolean;
    }

    private void AnimationControl(string _nameAnimation, bool _boolean)
    {
        _anim.SetBool(_nameAnimation, _boolean);
    }
}
