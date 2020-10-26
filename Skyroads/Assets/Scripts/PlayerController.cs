using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    private const float SPEED_TO_NORMAL_ROTATION = 4f;
    private const float SPEED_CAMERA_EFFECT = 3f;

    [SerializeField] private float speedLeftAndRight;
    [SerializeField] private float speedForward;
    [SerializeField] private float speedRot;

    [SerializeField] private Vector2 clampPosition;
    [SerializeField] private Vector2 clampRotation;

    [SerializeField] private int boostMultiply;
    [SerializeField] private float boostTime;
    /// <summary>
    /// x - distance on SmoothFollow, y - height on SmoothFollow , z - field of view
    /// </summary>
    [SerializeField] private Vector3 smoothBoost;


    private bool isBoost;
    /// <summary>
    /// x - distance on SmoothFollow, y - height on SmoothFollow , z - field of view
    /// </summary>
    private Vector3 smoothStart;
    private CharacterController characterController;
    private Quaternion startRot;
    private SmoothFollow smooth;

    private void OnEnable()
    {
        EventManager.OnStartGame += StartPlayer;
    }
    private void OnDisable()
    {
        EventManager.OnStartGame -= StartPlayer;
    }
    private void Start()
    {
        startRot = transform.rotation;
        smooth = FindObjectOfType<SmoothFollow>();
        smoothStart = new Vector3(smooth.distance, smooth.height, Camera.main.fieldOfView);
        characterController = GetComponent<CharacterController>();
    }
    private void Update()
    {
        if (GameController.instance.gameState == GameState.Play)
        {
            Move();
            CameraEffectByBoost();
        }
    }

    private void CameraEffectByBoost()
    {
        if (isBoost)
        {
            SmoothCameraEffect(smoothBoost.x, smoothBoost.y, smoothBoost.z);
        }
        else
        {
            SmoothCameraEffect(smoothStart.x, smoothStart.y, smoothStart.z);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Obstacals")
        {
            Dead();
            Destroy(other.gameObject);
        }
    }

    private void StartPlayer()
    {
        GameController.instance.gameState = GameState.Play;
    }
    private void Dead()
    {
        EventManager.EndGame();
        gameObject.SetActive(false);
    }

    private void ClampPlayerPositionAndRotation()
    {
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, clampPosition.x, clampPosition.y), transform.position.y, transform.position.z);
        var clampZ = ClampAngle(transform.eulerAngles.z, clampRotation.x, clampRotation.y);
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, clampZ);
    }

    private void Move()
    {
        characterController.Move(-Vector3.forward * speedForward * Time.deltaTime);

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            LeftMove();
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            RightMove();
        }
        else
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, startRot, SPEED_TO_NORMAL_ROTATION * Time.deltaTime);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Boost();
        }


        ClampPlayerPositionAndRotation();
    }

    private void Boost()
    {
        if (!isBoost)
        {
            StartCoroutine(BoostWait());
        }
    }
    IEnumerator BoostWait()
    {
        GameController.instance.multiplyScore = 2;
        isBoost = true;
        speedForward *= boostMultiply;
        yield return new WaitForSeconds(boostTime);
        speedForward /= boostMultiply;
        isBoost = false;
        GameController.instance.multiplyScore = 1;
    }

    private void SmoothCameraEffect(float dist, float height, float fieldOfView)
    {
        smooth.distance = Mathf.Lerp(smooth.distance, dist, Time.deltaTime * SPEED_CAMERA_EFFECT);
        smooth.height = Mathf.Lerp(smooth.height, height, Time.deltaTime * SPEED_CAMERA_EFFECT);
        Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, fieldOfView, Time.deltaTime * SPEED_CAMERA_EFFECT);
    }
    private void LeftMove()
    {
        characterController.Move(Vector3.right * speedLeftAndRight * Time.deltaTime);
        transform.Rotate(Vector3.forward * Time.deltaTime * speedRot);
    }
    private void RightMove()
    {
        characterController.Move(Vector3.left * speedLeftAndRight * Time.deltaTime);
        transform.Rotate(-Vector3.forward * Time.deltaTime * speedRot);
    }
    private float ClampAngle(float angle, float min, float max)
    {
        if (angle < 90f || angle > 270f)
        {
            if (angle > 180)
            {
                angle -= 360f;
            }
            if (max > 180)
            {
                max -= 360f;
            }
            if (min > 180)
            {
                min -= 360f;
            }
        }
        angle = Mathf.Clamp(angle, min, max);
        if (angle < 0)
        {
            angle += 360f;
        }
        return angle;
    }


}
